using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TipTracker
{
    internal class TipEntryViewModel
    {
        private object _selectedServer;
        public string ServerId { get; set; }

        public string TipAmount { get; set; }

        public string SelectedServer => _selectedServer.ToString() ?? string.Empty;

        public void AddTip()
        {

        }

        public void ClearForm()
        {

        }
    }
}
