using Core.Data;
using Core.Models;
using System.Data;
using System.IO.Abstractions;
using System.Text;

namespace Core.FileIo;

public class LegacyFileReader
{
    private readonly IFileSystem _fileSystem;
    private readonly string _filePath;

    public LegacyFileReader(string filePath) 
        : this(new FileSystem(), filePath)
    { }

    public LegacyFileReader(IFileSystem fileSystem, string filePath)
    {
        _fileSystem = fileSystem;
        _filePath = filePath;
    }

    public DataStore LoadData()
    {
        string fileContents;

        try
        {
            fileContents = _fileSystem.File.ReadAllText(_filePath);
        }
        catch (Exception ex)
        {
            throw new Exception("Failed to read file.", ex);
        }

        var fileDataSet = new FileDataSet();
            
        try
        {
            fileContents = DecodeBase64String(fileContents);
            var stringReader = new StringReader(fileContents);

            fileDataSet.ReadXml(stringReader);
        }
        catch (Exception ex)
        {
            throw new Exception("Failed to read file contents.", ex);
        }

        return ReadDataSet(fileDataSet);
    }

    private DataStore ReadDataSet(FileDataSet data)
    {
        var payPeriodInfo = data.Settings.ToPayPeriod();
        var servers = data.Servers
            .AsEnumerable()
            .Select(r => r.ToServer())
            .ToDictionary(x => x.ServerId.PosId, x => x);
        var functions = data.SpecialFunctions
            .AsEnumerable()
            .Select(r => r.ToEvent())
            .ToDictionary(x => x.Name, x => x);
        var creditCard = new TipType("Credit Card", TrackingReason.Owed, DateDetermination.BusinessDate);
        var roomCharge = new TipType("Room Charge", TrackingReason.Owed, DateDetermination.BusinessDate);
        var specialFunction = new TipType("Special Function", TrackingReason.Owed, DateDetermination.Event);
        var cash = new TipType("Cash", TrackingReason.Claimed, DateDetermination.PayPeriodEnd);
        var tips = new List<Tip>(data.Tips.Rows.Count);

        foreach(FileDataSet.TipsRow row in data.Tips.Rows)
        {
            var amount = decimal.Parse(row.Amount);
            var server = servers[row.ServerNumber];
            var date = DateOnly.FromDateTime(row.WorkingDate);

            if(!row.IsSpecialFunctionNull())
            {
                var function = functions[row.SpecialFunction];
                tips.Add(new Tip(amount, server, function, specialFunction));
                continue;
            }

            switch(row.Description)
            {
                case "Cash":
                    tips.Add(new Tip(amount, server, payPeriodInfo, cash));
                    break;
                case "Room Charge":
                    tips.Add(new Tip(amount, server, date, roomCharge));
                    break;
                case "Credit Card":
                    tips.Add(new Tip(amount, server, date, creditCard));
                    break;
                default:
                    throw new InvalidOperationException("Unrecognized tip type.");
            }
        }

        return DataBuilder
            .Create()
            .ForPayPeriod(payPeriodInfo)
            .WithServers(servers.Select(s => s.Value))
            .WithFunctions(functions.Select(f => f.Value))
            .WithTipTypes(new[] { creditCard, roomCharge, specialFunction, cash })
            .AddExistingTips(tips)
            .BuildData();
    }


    private string DecodeBase64String(string value)
    {
        var bytes = Convert.FromBase64String(value);
        return Encoding.ASCII.GetString(bytes);
    }
}