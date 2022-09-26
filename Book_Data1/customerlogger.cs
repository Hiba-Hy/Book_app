using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Data1
{
    public class customerlogger
    {
       public void log(string mess)
        {
            StreamWriter sw = new StreamWriter("C:\\Users\\HIBA-HY\\source\\repos\\Book_Domin\\Book_mainApp\\log.txt", true);
            sw.WriteLine(mess);
            sw.Write("..............");
            sw.Flush();
            sw.Close();

        }
    }
}
