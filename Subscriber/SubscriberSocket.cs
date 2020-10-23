using Common;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Subscriber
{
     class SubscriberSocket
     {
          private Socket _socket;
          private string _topic;

          public SubscriberSocket(string topic)
          {
               _topic = topic;
               _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
          }

          public void Connect(string ipAddress, int port)
          {
               _socket.BeginConnect(new IPEndPoint(IPAddress.Parse(ipAddress), port), ConnectedCallBack, null);
               Console.WriteLine("Waiting for a connection");
          }

          private void ConnectedCallBack(IAsyncResult asyncResult)
          {
               if (_socket.Connected)
               {
                    Console.WriteLine("Subscriber connected to broker.");
                    Subscribe();
                    StartReceive();
               }
               else
               {
                    Console.WriteLine("Error: Subscriber could not connected to broker.");
               }
          }

          private void Subscribe()
          {
               var data = Encoding.UTF8.GetBytes("subscribe#" + _topic);
               Send(data);
          }

          private void StartReceive()
          {
               ConnectionInfo connection = new ConnectionInfo();
               connection.Socket = _socket;

               _socket.BeginReceive(connection.Data, 0, connection.Data.Length, SocketFlags.None, ReceiveCallBack, connection);
          }

          private void ReceiveCallBack(IAsyncResult asyncResult)
          {
               ConnectionInfo connection = asyncResult.AsyncState as ConnectionInfo;

               try
               {
                    SocketError response;
                    int buffSize = _socket.EndReceive(asyncResult, out response);

                    if(response == SocketError.Success)
                    {
                         byte[] payloadBytes = new byte[buffSize];
                         Array.Copy(connection.Data, payloadBytes, payloadBytes.Length);

                         PayloadHandler.Handle(payloadBytes);
                    }
               
               }
               catch (Exception e)
               {
                    Console.WriteLine($"Can't receive data from broker. {e.Message}");
               }
               finally
               {
                    try
                    {
                         connection.Socket.BeginReceive(connection.Data, 0, connection.Data.Length, SocketFlags.None, ReceiveCallBack, connection);
                    }
                    catch(Exception e)
                    {
                         Console.WriteLine($"{e.Message}");
                         connection.Socket.Close();
                    }
               }
          }

          private void Send(byte[] data)
          {
               try
               {
                    _socket.Send(data);
               }
               catch(Exception e) {
                    Console.WriteLine($"Could not send data: {e.Message}");
               }
          }
     }
}
