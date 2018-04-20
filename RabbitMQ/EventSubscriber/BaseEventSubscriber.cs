using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventPublisher;
using RabbitMQ.Client;

namespace EventSubscriber
{

    /// <summary>
    /// 订阅者
    /// </summary>
    public class BaseEventSubscriber
    {
        private ConnectionFactory factory;
        private string QueueName = "Event_Data_Change";
        /// <summary>
        /// 在订阅者的构造中注册事件
        /// </summary>
        /// <param name="sender"></param>
        public BaseEventSubscriber(BaseEventSender sender)
        {
            this.factory = new ConnectionFactory();
            factory.HostName = "localhost";
            factory.UserName = "guest";
            factory.Password = "guest";
            sender.OnDataChange += new BaseEventSender.MqHandler(this.BaseSubscriberProxyMethod);
        }

        /// <summary>
        /// 反注册事件
        /// </summary>
        /// <param name="sender"></param>
        public void UnRegisterBaseEventSender(BaseEventSender sender)
        {
            sender.OnDataChange -= new BaseEventSender.MqHandler(this.BaseSubscriberProxyMethod);
        }

        /// <summary>
        /// 订阅者的代理方法
        /// </summary>
        /// <param name="o"></param>
        /// <param name="e"></param>
        public void BaseSubscriberProxyMethod(object o, MqEventArgs e)
        {
            using (var conn = factory.CreateConnection())
            {
                using (var channel = conn.CreateModel())
                {
                    channel.QueueDeclare(QueueName, true, false, false, null);
                    IBasicProperties properites = channel.CreateBasicProperties();
                    properites.DeliveryMode = 2;
                    string message = "current Num is :" + e.time;
                    var body = Encoding.UTF8.GetBytes(message);
                    channel.BasicPublish("", QueueName, properites, body);

                }
            }
        }

    }
}
