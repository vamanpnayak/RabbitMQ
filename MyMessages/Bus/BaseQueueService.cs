using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyNetQ;
using EasyNetQ.Topology;
using ExchangeType = RabbitMQ.Client.ExchangeType;

namespace MyMessages.Bus
{
    public class BaseQueueService
    {
        private string _exchangeName = "MerchantCreateV2";
        private string _queueOne = "merchant.create.request.v2";

        protected IExchange Exchange;
        protected IQueue Queue;
        public BaseQueueService(IAdvancedBus bus)
        {
            //if (bus == null) return;
            Exchange = bus.ExchangeDeclare(_exchangeName, ExchangeType.Topic);
            Queue = bus.QueueDeclare(_queueOne);
            bus.Bind(Exchange, Queue, "merchant.created");
        }
    }
}
