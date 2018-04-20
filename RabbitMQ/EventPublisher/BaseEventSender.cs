using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EventPublisher
{
    /// <summary>
    /// 事件发布者
    /// </summary>
    public class BaseEventSender
    {
        private int time { get; set; }
        public BaseEventSender(int time)
        {
            this.time = time;
        }

        /// <summary>
        /// 声明委托
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public delegate void MqHandler(object sender, MqEventArgs e);

        /// <summary>
        /// 声明状态变化的事件
        /// </summary>
        public event MqHandler OnDataChange;

        /// <summary>
        /// 模拟事件发生器
        /// </summary>
        public void Run()
        {
            new Thread(
                new ThreadStart(this.Data))
                .Start();
        }

        /// <summary>
        /// 方法主体
        /// </summary>
        public void Data()
        {
            if (time > 0)
            {
                while (time > 0)
                {
                    OnDataChange(this, new MqEventArgs(time));
                    time--;
                }
            }
            else
            {
                throw new Exception("输入大于0的数");
            }
        }
    }
}
