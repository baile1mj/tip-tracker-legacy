using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TipTracker
{
    public class Server
    {
        public static readonly ServerComparer Comparer = new();

        public string Id { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public bool SuppressChit { get; }
        public bool IsActive { get; set; }


    }
}
