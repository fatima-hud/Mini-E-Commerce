using Mini_E_Commerce.Application.Abstracts;
using Mini_E_Commerce.Application.Implementations;
using Mini_E_Commerce.Infrastructure.Abstracts;
using Mini_E_Commerce.Infrastructure.Repositories;
using MiniECommerce.Application.Abstracts;
using MiniECommerce.Application.Implementations;

namespace Mini_E_Commerce.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICategoryService,CategoryService>(); 
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ICartService, CartService>();
            services.AddScoped<ICartRepository, CartRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<ICartItemRepository,CartItemRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IOrderItemRepository, OrderItemRepository>();
        }
          
    }
}
