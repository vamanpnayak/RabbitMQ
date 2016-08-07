using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Web;
using RabbitMQ.Client;

namespace RabbitPublisher.Bus
{
    public class QueueService
    {
        private bool _durable = true;

        private string _publishSubscribeExchangeName = "MerchantCreate";
        private string _publishSubscribeQueueOne = "merchant.create.request.v1";
        private IModel _queueChannel;
        public QueueService(IModel model)
        {
            model.ExchangeDeclare(_publishSubscribeExchangeName, ExchangeType.Fanout, true);
            model.QueueDeclare(_publishSubscribeQueueOne, _durable, false, false, null);
            model.QueueBind(_publishSubscribeQueueOne, _publishSubscribeExchangeName, "");
        }
       
        public void PublishMessageToQueue(object data,IModel model)
        {
            IBasicProperties basicProperties = model.CreateBasicProperties();
            basicProperties.SetPersistent(_durable);
            byte[] messageBytes;
            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream())
            {
                bf.Serialize(ms, data);
                messageBytes = ms.ToArray();
            }
            model.BasicPublish(_publishSubscribeExchangeName, "", basicProperties, messageBytes);
        }
    }
}