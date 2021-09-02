using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LogInWindowsForm
{
    static class Program
    {
        public static int userID; // declaring this for global variable as Unique ID for Every User 
        // change your paramater string in the server explorer and properties of the .accdb file
        public static string connParam = @"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = C:\Users\Acer\Desktop\LogInWindowsForm\LogInData.accdb";

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new LogInForm());
        }
    }
}
