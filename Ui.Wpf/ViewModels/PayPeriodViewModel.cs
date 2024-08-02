using Core;
using Core.Models;

namespace TipTracker.ViewModels
{
    internal class PayPeriodViewModel : ViewModelBase
    {
        private readonly PayPeriod _payPeriod;

        public string StartDate => _payPeriod.StartDate.ToShortDateString();

        public string EndDate => _payPeriod.EndDate.ToShortDateString();

        public string CurrentDate => _payPeriod.CurrentDate.ToShortDateString();

        public PayPeriodViewModel(PayPeriod payPeriod)
        {
            _payPeriod = payPeriod;
        }
    }
}
