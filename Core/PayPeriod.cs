using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TipTracker
{
    public class PayPeriod
    {
        private readonly List<Server> _servers = new();
        private readonly List<Tip> _tips = new();
        private readonly List<Event> _events = new();

        public DateOnly StartDate { get; }
        public DateOnly EndDate { get; }
        public DateOnly CurrentDate { get; }
        public IReadOnlyList<Server> Servers => _servers;
        public IReadOnlyList<Tip> Tips => _tips;
        public IReadOnlyList<Event> Events => _events;

        public void AddServer(Server newServer)
        {
            
            _servers.Contains(newServer, Server.Comparer);
        }
    }
}
