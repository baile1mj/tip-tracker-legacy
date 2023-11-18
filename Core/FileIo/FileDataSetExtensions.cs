using Core.Models;

namespace Core.FileIo;

public static class FileDataSetExtensions
{
    public static Server ToServer(this FileDataSet.ServersRow serverDataRow)
    {
        return new Server(new ServerId(serverDataRow.ServerNumber), serverDataRow.FirstName, 
            serverDataRow.LastName, !serverDataRow.SuppressChit);
    }

    public static PayPeriod ToPayPeriod(this FileDataSet.SettingsDataTable settings)
    {
        var settingsLookup = settings
            .AsEnumerable()
            .ToDictionary(x => x.Setting, x => x.Value);

        return new PayPeriod(DateOnly.FromDateTime(DateTime.Parse(settingsLookup["PeriodStart"])), 
            DateOnly.FromDateTime(DateTime.Parse(settingsLookup["PeriodEnd"])),
            DateOnly.FromDateTime(DateTime.Parse(settingsLookup["WorkingDate"])));
    }

    public static Function ToEvent(this FileDataSet.SpecialFunctionsRow eventDataRow)
    {
        return new Function(eventDataRow.SpecialFunction, DateOnly.FromDateTime(eventDataRow._Date));
    }
}