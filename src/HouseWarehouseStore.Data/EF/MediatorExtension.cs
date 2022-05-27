using MediatR;

namespace HouseWarehouseStore.Data.EF
{
    public static class MediatorExtension
    {
        public static async Task DispatchDomainEventsAsync(this IMediator mediator, HouseWarehouseStoreDbContext ctx)
        {
        }
    }
}