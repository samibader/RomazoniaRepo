using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EShop.WEB.Models.Paypal
{
    public class PaypalLogger
    {
        public static string LogDirectoryPath = HttpContext.Current.Server.MapPath("~/App_Data/");

        public static void Log(String lines)
        {
            // Write the string to a file.append mode is enabled so that the log
            // lines get appended to  test.txt than wiping content and writing the log

            try
            {
                System.IO.StreamWriter file = new System.IO.StreamWriter(LogDirectoryPath + "\\Error.log", true);
                file.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + " --> " + lines);
                file.Close();
            }
            catch(Exception e)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }
        }
    }
}