using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ
{
    public class RabbitMqMessageReceiver
    {
        private ConnectionFactory factory;
        private string QueueName;
        public RabbitMqMessageReceiver()
        {
            factory = new ConnectionFactory();
            factory.HostName = "localhost";
            factory.UserName = "guest";
            factory.Password = "guest";
            QueueName = "Event_Data_Change";
        }

        /// <summary>
        /// 接收RabbitMq中的内容
        /// </summary>
        public void ReceiveMessage()
        {
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(QueueName, true, false, false, null);
                    var consumer = new QueueingBasicConsumer(channel);
                    channel.BasicConsume(QueueName, false, consumer);
                    channel.BasicQos(0, 1, false);

                    while (true)
                    {
                        BasicDeliverEventArgs ea = consumer.Queue.Dequeue();
                        byte[] bytes = ea.Body;
                        string str = Encoding.UTF8.GetString(bytes);
                        Console.WriteLine(str);
                        channel.BasicAck(ea.DeliveryTag, false);
                    }
                }
            }
        }
    }
}
