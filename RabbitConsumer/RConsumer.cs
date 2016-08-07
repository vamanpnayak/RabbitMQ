using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Timers;
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
        public RConsumer()
        {
            _scannerTimer = new Timer(TimeSpan.FromMinutes(1).TotalMilliseconds);
            _scannerTimer.Elapsed += (sender, agrs) => CheckIfProcessorShouldExecute();
        }

        protected override void OnStart(string[] args)
        {
            if (_scannerTimer.Interval > 0)
                _scannerTimer.Start();
        }

        private void CheckIfProcessorShouldExecute()
        {
            //if (!DeleteExpriedConsumerTempPaymentMethodsEngine.EnableDeleteExpiredTempPaymentMethods)
            //    return;

            //var now = DateTime.Now;
            //if (now.Minute != 30)
            //    return;

            //Execute();
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
                //AmqpMessagingService messagingService = new AmqpMessagingService();
                //var connection = messagingService.GetRabbitMqConnection();
                //var model = connection.CreateModel();

                //var queueService = new QueueService(model);
                //var result =queueService.ReceivePublishSubscribeMessageReceiverOne<Merchant>(model);
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
            
        }
    }
}
