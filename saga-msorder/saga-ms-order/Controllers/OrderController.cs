﻿using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Saga_BlockSeat.Infra;
using Saga_BlockSeat.ViewModel;
using rabbitmq_message;
using rabbitmq_message.BusConfiguration;
using System;
using System.Threading.Tasks;

namespace Saga_BlockSeat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly ISendEndpointProvider _sendEndpointProvider;
        private readonly IOrderDataAccess _orderDataAccess;

        public OrderController(
          ISendEndpointProvider sendEndpointProvider, IOrderDataAccess orderDataAccess)
        {
            _sendEndpointProvider = sendEndpointProvider;
            _orderDataAccess = orderDataAccess;
        }
      
        [HttpPost]
        [Route("createorder")]
        public async Task<IActionResult> CreateOrderUsingStateMachineInDb([FromBody] OrderModel orderModel)
        {
            orderModel.OrderId = Guid.NewGuid();
            var endpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri("queue:" + BusConstants.StartOrderTranastionQueue));

            _orderDataAccess.SaveOrder(orderModel);

            await endpoint.Send<IStartOrder>(new
            {
                OrderId = orderModel.OrderId,
                PaymentCardNumber = orderModel.CardNumber,
                ProductName = orderModel.ProductName
            });

            return Ok("Success");
        }
    }
}