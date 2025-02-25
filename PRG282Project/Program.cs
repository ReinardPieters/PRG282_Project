﻿using PRG282Project.BusinessProcess;
using PRG282Project.DataHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PRG282Project
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmLogIn());

            UserManager userManager = new UserManager();

            List<User> users = userManager.LoadUsers();
        }
    }
}
