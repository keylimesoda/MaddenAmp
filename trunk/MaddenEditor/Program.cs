using System;
using System.Collections.Generic;
using System.Windows.Forms;

using MaddenEditor.Forms;
using MaddenEditor.Db;

namespace MaddenEditor
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            int dbIndex = -1;

            try
            {
                dbIndex = TDB.TDBOpen("test.ros");

                int count = TDB.TDBDatabaseGetTableCount(dbIndex);

                Console.WriteLine("test.ros has {0} tables in it", count);
            }
            catch (DllNotFoundException e)
            {
                Console.WriteLine(e.ToString());
            }

            Application.Run(new MainForm());

            Console.WriteLine("Exiting...");

            try
            {
                TDB.TDBClose(dbIndex);
            }
            catch (DllNotFoundException e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}