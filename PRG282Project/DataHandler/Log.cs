using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRG282Project.DataHandler
{
    internal class Log
    {
        //Logs what changes the current signed in user makes to Log.txt
        public Log(string user, string alteration) 
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Log.txt");

            FileStream fs = new FileStream(path, FileMode.Append);
            
            StreamWriter sw = new StreamWriter(fs);

            sw.WriteLine($"{user} : {alteration} {DateTime.Now}");

            sw.Close();
            fs.Close();
        }
    }
}
