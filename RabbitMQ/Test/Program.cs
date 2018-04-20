using EventPublisher;
using EventSubscriber;
using RabbitMQ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    class Program
    {

        /// <summary>
        /// 测试方法
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            BaseEventSender sender = new BaseEventSender(1000);
            BaseEventSubscriber subscriber = new BaseEventSubscriber(sender);
            sender.Run();
            RabbitMqMessageReceiver receiver = new RabbitMqMessageReceiver();
            receiver.ReceiveMessage();
            Console.ReadKey();
        }
    }
}
