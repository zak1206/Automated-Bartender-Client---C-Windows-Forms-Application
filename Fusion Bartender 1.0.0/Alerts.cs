using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fusion_Bartender_1._0._0
{
    public class Alerts
    {
        private Alert alert = null;
        public List<Alert> AlertsList { get; set; } = new List<Alert>();

        public void NewAlert(int priority, string title, string msg, bool reocurring, int reocurTimer)
        {
            alert = new Alert(priority, title, msg, reocurring, reocurTimer);
            AlertsList.Add(alert);
        }

        public bool HasUnreadAlert()
        {
            foreach (Alert alerts in AlertsList)
            {
                if (!alerts.MarkedAsRead)
                    return true;
            }
            return false;
        }

        public void ClearLowPriorityAlerts()
        {
            List<Alert> NewAlertsList = new List<Alert>();

            foreach (Alert alerts in AlertsList)
            {
                if (alerts.Priority < 0)
                {
                    NewAlertsList.Add(alerts);
                }
            }

            AlertsList = NewAlertsList;
        }

        public void MarkAllAsRead(bool markAsRead)
        {
            foreach (Alert alerts in AlertsList)
            {
                alerts.MarkedAsRead = markAsRead;
            }
        }
    }

    public class Alert
    {
        public int Priority { get; set; } = 0;//    [0=NONE]    [1=LOW]     [2=HIGH]    [3=SEVERE]
        public string AlertTitle { get; set; } = "";
        public DateTime AlertTime { get; set; } = DateTime.Now;
        public string AlertMessage { get; set; } = "";
        public bool ReOcurring { get; set; } = false;
        public int ReoccuranceTime { get; set; } = 0;//    [60000 = 1min]     [300000 = 5min] 
        public bool MarkedAsRead { get; set; } = false;
        public Alert(int priority, string title, string msg, bool reocurring, int reocurTimer)
        {
            Priority = priority;
            AlertTitle = title;
            AlertMessage = msg;
            ReOcurring = reocurring;
            ReoccuranceTime = reocurTimer;
        }
    }
}
