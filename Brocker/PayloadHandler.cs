using Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brocker
{
     class PayloadHandler
     {
          public static void Handle(byte[] payloadBytes, ConnectionInfo connectionInfo)
          {
               var payloadString = Encoding.UTF8.GetString(payloadBytes);

               if (payloadString.StartsWith("subscribe#"))
               {
                    connectionInfo.Topic = payloadString.Split("subscribe#").LastOrDefault();

                    ConnectionStorage.Add(connectionInfo); 
               }
               else
               {
                    PayLoad payload = JsonConvert.DeserializeObject<PayLoad>(payloadString);
                    PayloadStorage.Add(payload);
               }
               
          }
     }
}
