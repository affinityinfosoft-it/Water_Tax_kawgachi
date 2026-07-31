using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BObject
{
    public class DashboardModel
    {
        public decimal WaterTaxCollection { get; set; }
        public decimal BeneficiaryCollection { get; set; }
        public decimal ReconnectionCollection { get; set; }
        public decimal FormSalesCollection { get; set; }
        public decimal MaterialCollection { get; set; }
        public decimal FerruleCollection { get; set; }
        public decimal CautionMoneyCollection { get; set; }
        public decimal VanBookingCollection { get; set; }
        public decimal TankBookingCollection { get; set; }
        public decimal TotalCollection { get; set; }
    }

    public class DashboardChart
    {
        public string Label { get; set; }
        public decimal Amount { get; set; }
    }
}
