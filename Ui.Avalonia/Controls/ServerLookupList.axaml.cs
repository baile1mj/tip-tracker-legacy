using Avalonia;
using Avalonia.Controls;
using System.Collections.ObjectModel;
using TipTracker.Models;

namespace TipTracker.Controls;

public partial class ServerLookupList : UserControl
{
    public static readonly StyledProperty<ObservableCollection<Server>> ServersProperty =
        AvaloniaProperty.Register<ServerLookupList, ObservableCollection<Server>>(
            nameof(Servers),
            defaultValue: new ObservableCollection<Server>(),
            defaultBindingMode: Avalonia.Data.BindingMode.TwoWay);


    public ObservableCollection<Server> Servers
    {
        get => GetValue(ServersProperty);
        set => SetValue(ServersProperty, value);
    }

    public ServerLookupList()
    {
        InitializeComponent();
    }
}