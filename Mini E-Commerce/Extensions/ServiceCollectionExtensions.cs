using MiniECommerce.Application.Abstracts;
using MiniECommerce.Application.Implementations;
using MiniECommerce.Infrastructure.Abstracts;
using MiniECommerce.Infrastructure.Repositories;
using MiniECommerce.Application.Abstracts;
using MiniECommerce.Application.Implementations;

namespace MiniECommerce.Extensions
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
