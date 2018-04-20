using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventPublisher
{
    /// <summary>
    /// 事件参数类
    /// </summary>
  public  class MqEventArgs:EventArgs
    {
        public int time { get; set; }
     

        public MqEventArgs(int time)
        {
            this.time = time;
        }

    }
}
