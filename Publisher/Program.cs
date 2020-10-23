﻿using Common;
using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.Text;

namespace Publisher
{
     class Program
     {
          static void Main(string[] args)
          {
               Console.WriteLine("Hello World!");

               var publisherSocket = new PublisherSocket();
               publisherSocket.Connect(Settings.BROKER_IP, Settings.BROKER_PORT);

               if (publisherSocket.IsConnected)
               {
                    while (true)
                    {
                         var payload = new PayLoad();

                         Console.Write("Enter the topic: ");
                         payload.Topic = Console.ReadLine().ToLower();

                         Console.Write("Enter the message: ");
                         payload.Message = Console.ReadLine();

                         var payloadString = JsonConvert.SerializeObject(payload);
                         byte[] data = Encoding.UTF8.GetBytes(payloadString);

                         publisherSocket.Send(data);
                    }
               }
               
          }
     }
}
