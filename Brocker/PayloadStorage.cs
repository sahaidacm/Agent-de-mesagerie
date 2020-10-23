using Common;
using System.Collections.Concurrent;

namespace Brocker
{
     static class PayloadStorage
     {
          private static ConcurrentQueue<PayLoad> _payloadQueue;


          static PayloadStorage()
          {
               _payloadQueue = new ConcurrentQueue<PayLoad>();
          }

          public static void Add(PayLoad payload)
          {
               _payloadQueue.Enqueue(payload);
          }

          public static PayLoad GetNext()
          {
               PayLoad payload = null;
               _payloadQueue.TryDequeue(out payload);
               return payload;
          }

          public static bool IsEmpty()
          {
               return _payloadQueue.IsEmpty;
          }
     }
}
