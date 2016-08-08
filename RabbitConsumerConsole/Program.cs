using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyMessage;
using RabbitPublisher.Bus;

namespace RabbitConsumerConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var messagingService = new AmqpMessagingService();
            var advancedBus = messagingService.GetRabbitMqBus();

            var queueService = new QueueService(advancedBus);
           
            var queue = new EasyNetQ.Topology.Queue("merchant.create.request.v2", false);
            queueService.ReceiveMessageFromQueueWithEnQ<Merchant>(advancedBus, queue,"");

            //advancedBus.Consume(queue, x => x
            //                    .Add<Merchant>((message, info) =>
            //                 {
            //                     Console.WriteLine("Got MyMessage {0}", message.Body.FirstName);
            //                 })

            //);


        }
    }
}
