using Mini_E_Commerce.Application.Dto.Order.Request;
using Mini_E_Commerce.Application.Dto.Order.Response;
using Mini_E_Commerce.Core.Enums;
using MiniECommerce.Core.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mini_E_Commerce.Application.Abstracts
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
