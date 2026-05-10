using Mini_E_Commerce.Application.Abstracts;
using Mini_E_Commerce.Application.Dto.Cart.Request;
using Mini_E_Commerce.Application.Dto.Cart.Response;
using Mini_E_Commerce.Core.Models;
using Mini_E_Commerce.Infrastructure.Abstracts;
using MiniECommerce.Core.Results;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Mini_E_Commerce.Application.Implementations
{
    public class CartService : ICartService
    {
        private readonly IUserRepository _userRepository;
        private readonly ICartRepository _cartRepository;
        private readonly IProductRepository _productRepository;
        private readonly ICartItemRepository _cartItemRepository;

        public CartService(IUserRepository userRepository, ICartRepository cartRepository, IProductRepository productRepository,ICartItemRepository cartItemRepository)
        {
            _userRepository = userRepository;
            _cartRepository = cartRepository;
            _productRepository = productRepository;
            _cartItemRepository = cartItemRepository;
        }

        public async Task<Result> AddAsync(Guid userId, RequestAddToCartDto request)
        {
            var trans=await _cartRepository.BeginTransactionAsync();
            try
            {
                var user = await _userRepository.GetByIdAsync(userId);
                if (user == null)
                {
                    await trans.RollbackAsync();
                    return Result.NotFound("User not found.");
                }
                var cart = await _cartRepository.GetCartByUserIdAsync(userId);
                if (cart == null)
                {
                    cart = new CartModel
                    {
                        CustomerId = userId,

                    };
                    await _cartRepository.AddAsync(cart);

                }
                var product = await _productRepository.GetByIdAsync(request.ProductId);
                if (product == null)
                {
                    await trans.RollbackAsync();
                    return Result.NotFound("Product not found.");
                }
                if (product.StockQuantity < request.Quantity)
                {
                    await trans.RollbackAsync();
                    return Result.BadRequest($"Stock Quantity {product.StockQuantity} is less than request ");
                }
               
                var existingCartItem = await _cartItemRepository.CheckItemExistAsync(cart.Id, request.ProductId);
                if (existingCartItem != null)
                {
                    if (existingCartItem.Quantity + request.Quantity > product.StockQuantity)
                    {
                        await trans.RollbackAsync();
                        return Result.BadRequest($"Total quantity in cart exceeds available stock. Current in cart: {existingCartItem.Quantity}, requested: {request.Quantity}, available: {product.StockQuantity}");
                    }
                    existingCartItem.Quantity += request.Quantity;
                    await _cartItemRepository.UpdateAsync(existingCartItem);
                }
                else
                {
                    var cartItem = new CartItemModel
                    {
                        CartId = cart.Id,
                        ProductId = request.ProductId,
                        Quantity = request.Quantity
                        
                        
                    };

                    await _cartItemRepository.AddAsync(cartItem);
                }
                await trans.CommitAsync();
                return Result.Success("Product added to cart successfully.");
            }
            catch (Exception ex)
            {
                await trans.RollbackAsync();
                return Result.Failure("An error occurred while adding to cart.", new List<string> { ex.Message });
            }




        }

        public async Task<Result> DeleteAsync(Guid userId, Guid cartItemId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
            {

                return Result.NotFound("User not found.");
            }
            var cart = await _cartRepository.GetCartByUserIdAsync(userId);
            if (cart == null ) {
                return Result.NotFound("Cart not found.");
                }   
            
            var cartItem = await _cartItemRepository.GetByIdAsync(cartItemId);
            if(cartItem == null)
            {
                return Result.NotFound("Cart item not found.");
            }
            if (cart.Id != cartItem.CartId)
            {
                return Result.BadRequest("Cart item does not belong to the user's cart.");
            }
            await _cartItemRepository.DeleteAsync(cartItem);
            return Result.Success("Cart item removed successfully.");


        }

        public async Task<Result<List<ResponseGetCartDto>>> GetAsync(Guid userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
            {
                
                return Result<List<ResponseGetCartDto>>.NotFound("User not found.");
            }
            var cart = await _cartRepository.GetCartByUserIdAsync(userId);
            if (cart == null)
            {
                return Result<List<ResponseGetCartDto>>.Success(new List<ResponseGetCartDto>(), "Cart is empty.");
            }
            var cartItems = await _cartItemRepository.GetAllAsync(cart.Id);

            var response = cartItems.Select(item => new ResponseGetCartDto
            { 
                Id = item.Id,
                ProductId = item.ProductId,
                ProductName = item.Product.Name,
                Quantity = item.Quantity,
                Price = item.Product.Price
            }).ToList();
          
            return Result<List<ResponseGetCartDto>>.Success(response, "Cart retrieved successfully.");
        }

        public  async Task<Result<ResponseGetCartItemDto>> UpdateAsync(Guid userId, RequestUpdateCartDto request)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
            {
                return Result<ResponseGetCartItemDto>.NotFound("User not found.");
            }
            var cartItem= await _cartItemRepository.GetByIdAsync(request.CartItemId);
            if (cartItem == null)
            {
                return Result<ResponseGetCartItemDto>.NotFound("Cart item not found.");
            }
            var cart = await _cartRepository.GetCartByUserIdAsync(userId);

            if (cart.Id!=cartItem.CartId)
            {
                return Result<ResponseGetCartItemDto>.BadRequest("Cart item does not belong to the user's cart.");
            }
            var product = await _productRepository.GetByIdAsync(cartItem.ProductId);
            if (product == null)
            {
                return Result<ResponseGetCartItemDto>.NotFound("Product not found.");
            }
            if(product.StockQuantity < request.Quantity)
            {
                return Result<ResponseGetCartItemDto>.BadRequest($"Stock Quantity {product.StockQuantity} is less than request ");
            }

            cartItem.Quantity = request.Quantity;
          
            await _cartItemRepository.UpdateAsync(cartItem);

            var response = new ResponseGetCartItemDto
            {
                CartItemId = cartItem.Id,
                ProductId = cartItem.ProductId,
                Quantity = cartItem.Quantity,
                Price =  product.Price 
            };

            return Result<ResponseGetCartItemDto>.Success(response, "Cart item updated successfully.");
        }
    }
}
