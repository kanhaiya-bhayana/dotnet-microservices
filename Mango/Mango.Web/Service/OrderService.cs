using Mango.Web.Models;
using Mango.Web.Service.IService;
using static Mango.Web.Utility.StaticDetails;

namespace Mango.Web.Service
{
    public class OrderService : IOrderService
    {
        private readonly IBaseService _baseService;
        public OrderService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        

        public async Task<ResponseDto?> CreateOrder(CartDto cartDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = ApiType.POST,
                Data = cartDto,
                Url = OrderAPIBase + "/api/order/CreateOrder"
            });
        }

        
    }
}
