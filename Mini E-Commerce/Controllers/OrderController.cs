using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mini_E_Commerce.Application.Abstracts;
using Mini_E_Commerce.Application.Dto.Order.Response;
using Mini_E_Commerce.Bases;
using Mini_E_Commerce.Core.Enums;
using Mini_E_Commerce.Extensions;
using MiniECommerce.Application.Dto.Category.Response;

namespace Mini_E_Commerce.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        public async Task<ApiResult<ResponseGetOrderDto>> AddOrder(Guid userId)
        {
            if (!ModelState.IsValid)
            {
                var errors = string.Join(", ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
                return ApiResult<ResponseGetOrderDto>.BadRequest(errors);
            }
            var res = await _orderService.AddOrderAsync(userId);
            if (!res.IsSuccess)
            {
                return this.ToApiResult(res);
            }
            return ApiResult<ResponseGetOrderDto>.Created(res.Value);

        }

        [HttpGet]
        public async Task<ApiResult<List<ResponseGetOrderDto>>> GetAllOrders(Guid userId)
        {
            if (!ModelState.IsValid)
            {
                var errors = string.Join(", ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
                return ApiResult<List<ResponseGetOrderDto>>.BadRequest(errors);
            }
            var res = await _orderService.GetAllOrdersAsync(userId);
            if (!res.IsSuccess) {
                return this.ToApiResult(res);
            }
            return ApiResult<List<ResponseGetOrderDto>>.Ok(res.Value);
        }

        [HttpGet]
        public async Task<ApiResult<List<ResponseGetOrderDetailsDto>>> GetOrderDetails(Guid userId, Guid orderId)
        {
            if (!ModelState.IsValid)
            {
                var errors = string.Join(", ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
                return ApiResult<List<ResponseGetOrderDetailsDto>>.BadRequest(errors);
            }
            var res = await _orderService.GetOrderDetailsAsync(userId, orderId);
            if (!res.IsSuccess)
            {
                return this.ToApiResult(res);
            }
            return ApiResult<List<ResponseGetOrderDetailsDto>>.Ok(res.Value);
        }
        [HttpGet]
        public async Task<ApiResult<List<ResponseGetOrderDto>>> GetSearchOrders(Guid adminId, Guid? customerId, OrderStatus? status, DateTime? fromDate, DateTime? endDate) {
            if (!ModelState.IsValid)
            {
                var errors = string.Join(", ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
                return ApiResult<List<ResponseGetOrderDto>>.BadRequest(errors);
            }
            var res = await _orderService.GetSearchOrdersAsync(adminId, customerId, status, fromDate, endDate);
            if (!res.IsSuccess)
            {
                return this.ToApiResult(res);
            }
            return ApiResult<List<ResponseGetOrderDto>>.Ok(res.Value);


        }
        [HttpGet]
        public async Task<ApiResult<ResponseGetOrderDto>> UpdateStatusAsync(Guid adminId, Guid orderId, OrderStatus orderStatus)
        {
            if (!ModelState.IsValid)
            {
                var errors = string.Join(", ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
                return ApiResult<ResponseGetOrderDto>.BadRequest(errors);
            }
            var res = await _orderService.UpdateStatusAsync(adminId, orderId, orderStatus);
            if (!res.IsSuccess)
            {
                return this.ToApiResult(res);
            }
            return ApiResult<ResponseGetOrderDto>.Ok(res.Value);
        }


    }
}
