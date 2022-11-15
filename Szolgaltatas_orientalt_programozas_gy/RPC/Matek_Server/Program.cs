using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Text;
using System.Threading.Tasks;

namespace Matek_Server
{
    internal class Program
    {
        static void Main(string[] args)
        {
            TcpChannel chan = new TcpChannel(8888);
            ChannelServices.RegisterChannel(chan, false);
            RemotingConfiguration.RegisterWellKnownServiceType(typeof(Matek_Imp.Matek), "Matek", WellKnownObjectMode.SingleCall);
            Console.WriteLine("A szerver elindult!");
            Console.ReadLine();
        }
    }
}
