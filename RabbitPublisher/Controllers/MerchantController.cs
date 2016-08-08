using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Web.Http.Results;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using MyMessage;
using RabbitMQ.Client;
using RabbitPublisher.Bus;
using RabbitPublisher.Models;
using RabbitPublisher.Providers;
using RabbitPublisher.Results;
using EasyNetQ;

namespace RabbitPublisher.Controllers
{
    [System.Web.Http.RoutePrefix("api/Merchant")]
    public class MerchantController : ApiController
    {
        public IHttpActionResult Post([FromBody]Merchant merchant)
        {
            // publish message into RabbitMq
            //var messagingService = new AmqpMessagingService();
            //var connection = messagingService.GetRabbitMqConnection();
            //var model = connection.CreateModel();

            //var queueService = new QueueService(model);
            //queueService.PublishMessageToQueue(merchant, model);

            //return Ok();


            var messagingService = new AmqpMessagingService();
            var advancedBus = messagingService.GetRabbitMqBus();

            var queueService = new QueueService(advancedBus);
            queueService.PublishMessageToQueueWithEnQ(merchant, advancedBus);
            advancedBus.Dispose();
            return Ok();

        }

        public async Task<IHttpActionResult> Get()
        {
            var messagingService = new AmqpMessagingService();
            var advancedBus = messagingService.GetRabbitMqBus();

            var queueService = new QueueService(advancedBus);
            var queue = new EasyNetQ.Topology.Queue("merchant.create.request.v2", false);
            queueService.ReceiveMessageFromQueueWithEnQ<Merchant>(advancedBus, queue,"");
            return Ok();
        }

    }
}
