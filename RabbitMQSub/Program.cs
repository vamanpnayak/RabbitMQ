using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;

namespace RabbitMQSub
{
    public class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory
            {
                UserName = "guest",
                Password = "guest",
                VirtualHost = "/",
                HostName = "172.22.32.31",
                Port = AmqpTcpEndpoint.UseDefaultPort
            };

            var conn = factory.CreateConnection();

            var channel = conn.CreateModel();

            var result = channel.BasicGet("netsQueue", false);

            if (result == null)
            {
            }
            else
            {
                var props = result.BasicProperties;
                var body = result.Body;
                channel.BasicAck(result.DeliveryTag, false);
            }

            //channel.ExchangeDeclare("netsExchg", ExchangeType.Direct);
            //channel.QueueDeclare("netsQueue", false, false, false, null);
            //channel.QueueBind("netsQueue", "netsExchg", "netsRoutingKey");
        }
    }
}
