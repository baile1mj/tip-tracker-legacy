using TipTracker.Ui.Controls;
using TipTracker.Ui.DataObjects;
using TipTracker.Ui.ViewData;

namespace TipTracker.Ui;

public partial class MainForm : Form
{
    private List<Server> _servers = new()
    {
        new Server("1019", "Watkiss", "Lauren"),
        new Server("1022", "Blanning", "Brade"),
        new Server("1005", "Sambells", "Lawton"),
        new Server("1021", "Ruperti", "Claiborne"),
        new Server("1016", "Coupar", "Darcy"),
        new Server("1018", "Klein", "Blisse"),
        new Server("1024", "Jachimczak", "Nedi"),
        new Server("1012", "Landall", "Lena"),
        new Server("1011", "Melior", "Renate"),
        new Server("1017", "Garshore", "Wilmar"),
    };
    
    public MainForm()
    {
        InitializeComponent();
        tipEditor2.ErrorOccurred += HandleError;
        tipEditor2.LookupServer = LookupServer;

        var serverViews = _servers
            .Select(x => new ServerView(x))
            .Order(ServerViewSortComparer.DefaultComparer)
            .ToList();

        var serversBindingList = new SortableBindingList<ServerView>(serverViews, ServerViewSortComparer.Create);
        serverListBindingSource.DataSource = serversBindingList;
    }

    private void HandleError(object sender, Error error)
    {
        error = Error.Get(error);
        MessageBox.Show(error.Message, error.Summary, MessageBoxButtons.OK);
    }

    private Server LookupServer(string posId)
    {
        return _servers.FirstOrDefault(s => s.PosId == posId);
    }
}