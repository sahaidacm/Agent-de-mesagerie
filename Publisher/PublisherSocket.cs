using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Publisher
{
     
     class PublisherSocket
     {
          private Socket _socket;
          public bool IsConnected;
         
          public PublisherSocket()
          {
               _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
          }

          internal void Connect(object bROKER_IP, object bROKER_PORT)
          {
               throw new NotImplementedException();
          }

          public void  Connect(string  ipAddress, int port)
          {
               _socket.BeginConnect(new IPEndPoint(IPAddress.Parse(ipAddress), port), ConnectedCallBack, null);
               Thread.Sleep(2000); 
          }

          public void Send(byte[] data)
          {
               try
               {
                    _socket.Send(data);
               }catch(Exception e)
               {
                    Console.WriteLine($"Could not send data: {e.Message}");
               }
          }

          private void ConnectedCallBack(IAsyncResult asyncResult)
          {
               if (_socket.Connected)
               {
                    Console.WriteLine("Sender connected to broker.");
               }
               else
               {
                    Console.WriteLine("Error: Sender not connected to broker.");
               }

               IsConnected = _socket.Connected;
          }
     }
}
