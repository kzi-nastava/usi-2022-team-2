using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Threading;
using HealthCare_System.Core.EquipmentTransfers;
using HealthCare_System.Core.EquipmentTransfers.Model;
using HealthCare_System.Core.Rooms;
using HealthCare_System.Database;
using HealthCare_System.GUI.Main;

namespace HealthCare_System
{
    public partial class App : Application
    {
        HealthCareDatabase database;

        RoomService roomService;
        EquipmentTransferService equipmentTransferService;

        void App_Startup(object sender, StartupEventArgs e)
        {
            database = new();

            roomService = new RoomService(null, null, null, null, null, database.RoomRepo);
            equipmentTransferService = new EquipmentTransferService(database.EquipmentTransferRepo, roomService);
            roomService.EquipmentTransferService = equipmentTransferService;

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;
            timer.Start();

            Window mainWindow = new MainWindow(database);
            mainWindow.Show();
            
        }

        void timer_Tick(object sender, EventArgs e)
        {
            DoTransfers();
        }

        public void DoTransfers()
        {
            if (equipmentTransferService.Transfers().Count > 0)
            {
                List<Transfer> copyTransfers = new List<Transfer>();
                foreach (Transfer copyTransfer in equipmentTransferService.Transfers())
                {
                    copyTransfers.Add(copyTransfer);
                }

                foreach (Transfer transfer in copyTransfers)
                {
                    if (transfer.MomentOfTransfer < DateTime.Now)
                        equipmentTransferService.ExecuteTransfer(transfer);
                }
            }


        }
    }
}
