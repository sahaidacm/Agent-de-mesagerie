using Common;
using System;
using System.Threading.Tasks;

namespace Brocker
{
     class Program
     {
          static void Main(string[] args)
          {
               Console.WriteLine("Brocker!");

               BrockerSocket socket = new BrockerSocket();
               socket.Start(Settings.BROKER_IP, Settings.BROKER_PORT);

               var worker = new Worker();
               Task.Factory.StartNew(worker.DoSendMessageWork, TaskCreationOptions.LongRunning);
               Console.ReadLine();
          }
     }
}
