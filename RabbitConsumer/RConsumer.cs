﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Timers;
using EasyNetQ;
using RabbitMQ.Client;
using MyMessage;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.MessagePatterns;
using RabbitPublisher.Bus;

namespace RabbitConsumer
{
    public partial class RConsumer : ServiceBase
    {
        private readonly Timer _scannerTimer;
        private bool _isRunning;
        private AmqpMessagingService _messagingService;
        private IAdvancedBus _advancedBus;
        public RConsumer()
        {
            _scannerTimer = new Timer(TimeSpan.FromMinutes(1).TotalMilliseconds);
            _scannerTimer.Elapsed += (sender, agrs) => CheckIfProcessorShouldExecute();
            _messagingService = new AmqpMessagingService();
            _advancedBus = _messagingService.GetRabbitMqBus();
            
        }

        protected override void OnStart(string[] args)
        {
            if (_scannerTimer.Interval > 0)
                _scannerTimer.Start();
            Execute();
        }

        private void CheckIfProcessorShouldExecute()
        {
            //if (!DeleteExpriedConsumerTempPaymentMethodsEngine.EnableDeleteExpiredTempPaymentMethods)
            //    return;

            //var now = DateTime.Now;
            //if (now.Minute != 30)
            //    return;

            Execute();
        }

        public void Execute()
        {

            if (_isRunning)
            {
                //_systemLogRepository.Trace("DeleteExpriedConsumerTempPaymentMethodsEngine is already in a running state");
                return;
            }

            _isRunning = true;

            try
            {
                var queueService = new QueueService(_advancedBus);
                var queue = new EasyNetQ.Topology.Queue("merchant.create.request.v2", false);

                queueService.ReceiveMessageFromQueueWithEnQ<Merchant>(_advancedBus, queue, "testQ.txt");
            }
            catch (Exception ex)
            {
                
            }
            finally
            {
                _isRunning = false;
            }
        }
       
        protected override void OnStop()
        { 
            if (_scannerTimer.Enabled)
                _scannerTimer.Stop();
            _advancedBus.Dispose();
        }


    }
}
