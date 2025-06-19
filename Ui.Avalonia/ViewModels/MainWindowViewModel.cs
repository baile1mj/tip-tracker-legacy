using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.Json.Serialization;
using TipTracker.Controls;
using TipTracker.Models;

namespace TipTracker.ViewModels
{
    public interface IServerSource
    {
        ObservableCollection<Server> Servers { get; set; }
    }

    public partial class MainWindowViewModel : ViewModelBase, IServerSource
    {
        public ObservableCollection<Server> Servers { get; set; } = new();

        public MainWindowViewModel()
        {
            //var servers = new List<Server>
            //{
            //    new Server { Number = "1000", LastName = "Smith", FirstName = "George" },
            //    new Server { Number = "1001", LastName = "James", FirstName = "Etta" }
            //};

            //Servers = new ObservableCollection<Server>(servers);

            //AddServers();
        }

        public void AddServers()
        {
            Servers.Add(new Server { Number = "1000", LastName = "John", FirstName = "Elton" });
            Servers.Add(new Server { Number = "1001", LastName = "James", FirstName = "Etta" });
        }
    }
}
