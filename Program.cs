using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alpha_Test_42_Bot
{
    class Program
    {
        static void Main(string[] args)
        {
            Properties.Settings.Default.Reload();
            //Properties.Settings.Default.Reset();
            MyBot bot = new MyBot();
        }
    }
}
