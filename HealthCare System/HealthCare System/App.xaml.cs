using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Threading;
using HealthCare_System.Model;
using HealthCare_System.factory;

namespace HealthCare_System
{
    public partial class App : Application
    {
        HealthCareFactory factory;
        void App_Startup(object sender, StartupEventArgs e)
        {
            factory = new HealthCareFactory();
            Window mainWindow = new MainWindow(factory);
            mainWindow.Show();
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMinutes(1);
            timer.Tick += timer_Tick;
            timer.Start();
        }

        void timer_Tick(object sender, EventArgs e)
        {
            DoTransfers();
        }

        public void DoTransfers()
        {
            if (factory.TransferController.Transfers.Count > 0)
            {
                List<Transfer> copyTransfers = new List<Transfer>();
                foreach (Transfer copyTransfer in factory.TransferController.Transfers)
                {
                    copyTransfers.Add(copyTransfer);
                }

                foreach (Transfer transfer in copyTransfers)
                {
                    if (transfer.MomentOfTransfer < DateTime.Now)
                        factory.ExecuteTransfer(transfer);
                }
            }


        }
    }
}
