using System;
using System.Collections.Generic;

namespace ClothCycles
{
    internal class Admin : Account
    {
        public string AdminID { get; set; }

        public void ManageUsers(List<User> users)
        {
            foreach (var user in users)
            {
                Console.WriteLine($"Admin {Username} is managing user {user.UserID}");
            }
        }

        public void MonitorActivities()
        {
            Console.WriteLine($"Admin {Username} is monitoring activities.");
        }

        public void SystemMaintenance()
        {
            Console.WriteLine($"Admin {Username} is performing system maintenance.");
        }
    }
}
