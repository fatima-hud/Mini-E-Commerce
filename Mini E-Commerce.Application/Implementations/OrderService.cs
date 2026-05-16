using Mini_E_Commerce.Application.Abstracts;
using Mini_E_Commerce.Application.Dto.Order.Response;
using Mini_E_Commerce.Core.Enums;
using Mini_E_Commerce.Core.Models;
using Mini_E_Commerce.Infrastructure.Abstracts;
using MiniECommerce.Core.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mini_E_Commerce.Application.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IUserRepository _userRepository;
        private readonly ICartRepository _cartRepository;
        private readonly ICartItemRepository _cartItemRepository;
        private readonly IProductRepository _productRepository;
        private readonly IOrderItemRepository _orderItemRepository;

        public OrderService(IOrderRepository orderRepository, IUserRepository userRepository, ICartRepository cartRepository,ICartItemRepository cartItemRepository,IProductRepository productRepository,IOrderItemRepository orderItemRepository)
        {
            _orderRepository = orderRepository;
            _userRepository = userRepository;
            _cartRepository = cartRepository;
            _cartItemRepository = cartItemRepository;
            _productRepository = productRepository;
            _orderItemRepository = orderItemRepository;
        }

        public async Task<Result<ResponseGetOrderDto>> AddOrderAsync(Guid userId)
        {
            var trans = await _orderRepository.BeginTransactionAsync();
            try
            {
                var user = await _userRepository.GetByIdAsync(userId);
                if (user == null)
                {
                    await trans.RollbackAsync();
                    return Result<ResponseGetOrderDto>.NotFound("User not found");
                }
                var cart = await _cartRepository.GetCartByUserIdAsync(userId);
                if (cart is null)
                {
                    await trans.RollbackAsync();
                    return Result<ResponseGetOrderDto>.NotFound("Cart not found");
                }
                var cartItems = await _cartItemRepository.GetAllAsync(cart.Id);
                if (!cartItems.Any())
                {
                    await trans.RollbackAsync();
                    return Result<ResponseGetOrderDto>.BadRequest("Cart is empty");
                }
                foreach (var cartItem in cartItems)
                {
                    var product = await _productRepository.GetByIdAsync(cartItem.ProductId);
                    if (cartItem.Quantity > product.StockQuantity)
                    {
                        await trans.RollbackAsync();
                        return Result<ResponseGetOrderDto>.BadRequest("Stock Quantity is less than request");
                    }
                }

                var totalPrice = cartItems.Sum(e => e.Product.Price * e.Quantity);
                foreach (var cartItem in cartItems)
                {
                    var product = await _productRepository.GetByIdAsync(cartItem.ProductId);
                    product.StockQuantity -= cartItem.Quantity;
                }
                await _productRepository.SaveChangesAsync();

                var order = new OrderModel
                {
                    CustomerId = userId,
                    TotalAmount = totalPrice,
                    Status = OrderStatus.Processing

                };
                await _orderRepository.AddAsync(order);
                var orderItems = cartItems.Select(x => new OrderItemModel
                {
                    OrderId = order.Id,
                    ProductId = x.ProductId,
                    Quantity = x.Quantity,
                    UnitPrice = x.Product.Price

                }).ToList();
                await _orderItemRepository.AddRangeAsync(orderItems);
                await _cartItemRepository.DeleteRangeAsync(cartItems);
                await _productRepository.SaveChangesAsync();
                await trans.CommitAsync();
                var ret = new ResponseGetOrderDto
                {
                    OrderId = order.Id,
                    Status = order.Status,
                    CreatedAt = order.CreatedAt,
                    TotalAmount = order.TotalAmount,
                    UpdatedAt = order.UpdatedAt

                };
                return Result<ResponseGetOrderDto>.Success(ret);
            }
            catch (Exception ex)
            {
                await trans.RollbackAsync();
                return Result<ResponseGetOrderDto>.BadRequest(ex.Message);


            }

            
        }

        public async Task<Result<List<ResponseGetOrderDto>>> GetAllOrdersAsync(Guid userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
            {
                return Result<List<ResponseGetOrderDto>>.NotFound("User not found");
            }
            var orders = await _orderRepository.GetAllByUserAsync(userId);
            var response = new List<ResponseGetOrderDto>();
            if (orders == null)
            {
                return Result<List<ResponseGetOrderDto>>.Success(response);
            }
             response = orders.Select(o => new ResponseGetOrderDto
            {
                OrderId = o.Id,
                Status = o.Status,
                CreatedAt = o.CreatedAt,
                TotalAmount = o.TotalAmount,
                UpdatedAt = o.UpdatedAt
            }).ToList();
            return Result<List<ResponseGetOrderDto>>.Success(response);
        }

        public async Task<Result<List<ResponseGetOrderDetailsDto>>>GetOrderDetailsAsync(Guid userId, Guid orderId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
            {
                return Result<List<ResponseGetOrderDetailsDto>>.NotFound("User not found");
            }
            var order= await _orderRepository.GetByIdAsync(orderId);
            if(order == null || order.CustomerId != userId)
            {
                return Result<List<ResponseGetOrderDetailsDto>>.NotFound("Order not found");
            }
            var orderItems = await _orderItemRepository.GetOrderItemsAsync(orderId);
            var response = orderItems.Select(oi => new ResponseGetOrderDetailsDto
            {
                OrderItemId = oi.Id,
                ProductId = oi.ProductId,
                Quantity = oi.Quantity,
                UnitPrice = oi.UnitPrice
            }).ToList();
            return Result<List<ResponseGetOrderDetailsDto>>.Success(response);
        }

        public async Task<Result<List<ResponseGetOrderDto>>> GetSearchOrdersAsync(Guid adminId, Guid? customerId, OrderStatus? status, DateTime? fromDate, DateTime? endDate)
        {
            var admin = await _userRepository.GetByIdAsync(adminId);
            if (admin == null)
            {

                return Result<List<ResponseGetOrderDto>>.NotFound("User not found");
            }
            if (admin.Role != "Admin")
            {
                return Result<List<ResponseGetOrderDto>>.BadRequest("You don't allow to search ");
            }
            var orders = await _orderRepository.GetSearchOrdersAsync(customerId, status, fromDate, endDate);
            var ret = new List<ResponseGetOrderDto>();
            if (!orders.Any())
            {
                return Result<List<ResponseGetOrderDto>>.Success(ret);
            }
             ret=orders.Select(e=>new ResponseGetOrderDto
             {
                 OrderId=e.Id,
                 Status=e.Status,
                 CreatedAt=e.CreatedAt,
                 TotalAmount=e.TotalAmount,
                 UpdatedAt=e.UpdatedAt
             }).ToList();

            return Result<List<ResponseGetOrderDto>>.Success(ret);

        }

        public async Task<Result<ResponseGetOrderDto>> UpdateStatusAsync(Guid adminId, Guid orderId, OrderStatus orderStatus)
        {
            var admin = await _userRepository.GetByIdAsync(adminId);
            if (admin== null)
            {
               
                return Result<ResponseGetOrderDto>.NotFound("User not found");
            }
            if(admin.Role != "Admin")
            {
                return Result<ResponseGetOrderDto>.BadRequest("You don't allow to update order status");
            }
            var order = await _orderRepository.GetByIdAsync(orderId);
            if (order == null) {
                return Result<ResponseGetOrderDto>.NotFound("Order not found");
            }
            order.Status = orderStatus;
            await _orderRepository.SaveChangesAsync();
            var ret= new ResponseGetOrderDto
            {
                OrderId = order.Id,
                Status = order.Status,
                CreatedAt = order.CreatedAt,
                TotalAmount = order.TotalAmount,
                UpdatedAt = order.UpdatedAt
            };
            return Result<ResponseGetOrderDto>.Success(ret);

        }
    }
}
