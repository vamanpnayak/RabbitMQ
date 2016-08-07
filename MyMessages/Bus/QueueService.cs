using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using EasyNetQ;
using EasyNetQ.Topology;
using MyMessage;
using MyMessages.Bus;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.MessagePatterns;

namespace RabbitPublisher.Bus
{
    public class QueueService : BaseQueueService
    {
        private bool _durable = true;

        private string _publishSubscribeExchangeName = "MerchantCreate";
        private string _publishSubscribeQueueOne = "merchant.create.request.v1";
        private IModel _queueChannel;
        //public QueueService(IModel model)
        //{
        //    model.ExchangeDeclare(_publishSubscribeExchangeName, ExchangeType.Fanout, true);
        //    model.QueueDeclare(_publishSubscribeQueueOne, _durable, false, false, null);
        //    model.QueueBind(_publishSubscribeQueueOne, _publishSubscribeExchangeName, "");
        //}
       
        public QueueService(IAdvancedBus bus): base(bus)
        {
            
        }

        //public void PublishMessageToQueue(object data,IModel model)
        //{
        //    IBasicProperties basicProperties = model.CreateBasicProperties();
        //    basicProperties.Persistent =_durable;
        //    var messageBytes = SerializeData(data);
        //    model.BasicPublish(_publishSubscribeExchangeName, "", basicProperties, messageBytes);
        //}
        public void PublishMessageToQueueWithEnQ<T>(T data, IAdvancedBus bus) where T: class
        {
            var message = new Message<T>(data);
            bus.Publish(Exchange, "merchant.created",  false , message);
        }
        //private static byte[] SerializeData(object data)
        //{
        //    byte[] messageBytes;
        //    BinaryFormatter bf = new BinaryFormatter();
        //    using (MemoryStream ms = new MemoryStream())
        //    {
        //        bf.Serialize(ms, data);
        //        messageBytes = ms.ToArray();
        //    }
        //    return messageBytes;
        //}

        //private static object DeSerializeData(byte[] data)
        //{
        //    BinaryFormatter bf = new BinaryFormatter();
        //    using (MemoryStream ms = new MemoryStream(data))
        //    {
        //        return bf.Deserialize(ms);
        //    }

        //}

        //public TReturn ReceivePublishSubscribeMessageReceiverOne<TReturn>(IModel model)
        //{
        //    model.BasicQos(0, 1, false);
        //    Subscription subscription = new Subscription(model, _publishSubscribeQueueOne, false);
        //    BasicDeliverEventArgs deliveryArguments = subscription.Next();
        //    var data = DeSerializeData(deliveryArguments.Body);

        //    subscription.Ack(deliveryArguments);
        //    return (TReturn)data;
        //}

        //public T ReceiveMessageFromQueueWithEnQ<T>(IAdvancedBus bus) where T : class
        //{
        //   T result = null;
        //   bus.Consume<T>(Queue, (message, info) => Task.Factory.StartNew(() =>
        //   {
        //       result = message.Body;
        //   }));

        //   return result;
        //}


        public Merchant ReceiveMessageFromQueueWithEnQ(IAdvancedBus bus)
        {
            Merchant  result = null;
            bus.Consume<Merchant>(Queue, (msg, info) =>
               {
                   // Implement your handling logic here
                   result = msg.Body;
               });

            return result;
        }
    }
}