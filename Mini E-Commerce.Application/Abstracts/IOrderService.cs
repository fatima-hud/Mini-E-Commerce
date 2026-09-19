using MiniECommerce.Application.Dto.Order.Request;
using MiniECommerce.Application.Dto.Order.Response;
using MiniECommerce.Core.Enums;
using MiniECommerce.Core.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniECommerce .Application.Abstracts
{
    public interface IOrderService
    {
        Task<Result<ResponseGetOrderDto>> AddOrderAsync(Guid userId);
        Task<Result<List<ResponseGetOrderDto>>> GetAllOrdersAsync(Guid userId);
        Task<Result<List<ResponseGetOrderDetailsDto>>> GetOrderDetailsAsync(Guid userId,Guid orderId);
        Task<Result<ResponseGetOrderDto>>UpdateStatusAsync(Guid userId, Guid orderId, OrderStatus orderStatus);
        Task<Result<List<ResponseGetOrderDto>>>GetSearchOrdersAsync(Guid adminId,Guid?customerId, OrderStatus? status, DateTime? fromDate, DateTime? endDate);
    }
}
