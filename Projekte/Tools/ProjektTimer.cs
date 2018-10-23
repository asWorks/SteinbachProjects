using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Threading;

namespace ProjektDB.Tools
{
    public static class ProjektTimer
    {
        public static DispatcherTimer dt = new DispatcherTimer();
        private static bool bIsInit = false;
        public static void init()
        {
            if (!bIsInit)
            {
                ProjektTimer.dt.Tick += dt_Tick;
                var s = new ProjektDB.Properties.Settings();
                ProjektTimer.dt.Interval =  TimeSpan.FromSeconds( DAL.Session.MailTimerIntervall);
                ProjektTimer.dt.Start();
                bIsInit = true;
                CallMailReminder();

            }
            
        }

        static void dt_Tick(object sender, EventArgs e)
        {

            //var mail = new libSendOutlookMail.OutlookMail();
            //mail.sendmail("me@asWorks.de", "Test Timer", "Test Timer " + DateTime.Now.ToString());
            ProjektTimer.CallMailReminder();


        }

        static void CallMailReminder()
        {
            //var mail = new CheckMailReminder();
            //mail.checkMail();

        }

    }
}
