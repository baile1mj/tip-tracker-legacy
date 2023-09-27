using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TipTracker
{
    internal class ServerCollection
    {
        private readonly List<Server> _servers = new();
        private readonly List<Server> _activeServers = new();

        public Result AddServer(Server newServer)
        {
            GuardAgainst.Null(newServer, nameof(newServer));

            _servers.Add(newServer);

            if (newServer.IsActive)
            {
                _activeServers.Add(newServer);
            }

            return null;
        }

        public Result RemoveServer(Server server)
        {
            GuardAgainst.Null(server, nameof(server));

            _servers.Remove(server);
            _activeServers.Remove(server);

            return null;
        }
    }
}
