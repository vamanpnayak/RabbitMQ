using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;
using MyMessages.Service;

namespace RabbitMQPub
{
    public class Program
    {
        static void Main(string[] args)
        {


            var rabbitMqService = new RabbitMqService();
            IConnection connection = rabbitMqService.GetRabbitMqConnection();
            IModel model = connection.CreateModel();

            var messageBodyBytes = Encoding.UTF8.GetBytes("Hello, world!");

            var props = model.CreateBasicProperties();
            props.Persistent = false;
           // props.ContentType = "text/plain";
            //props.DeliveryMode = 2;
            model.BasicPublish("netsExchg", "netsRoutingKey",
                                props,
                               messageBodyBytes);

        }

        private static void SetupInitialTopicQueue(IModel model)
        {
            model.ExchangeDeclare("netsExchg", ExchangeType.Direct);
            model.QueueDeclare("netsQueue", false, false, false, null);
            model.QueueBind("netsQueue", "netsExchg", "netsRoutingKey");
        }
    }
}
