using HouseWarehouseStore.Common;
using Master.Service;
using Microsoft.AspNetCore.SignalR;

namespace Master.Api.SignalRHubs
{
    public class ConnectRealTimeHub : Hub
    {
        private IAdminService _adminService;

        public ConnectRealTimeHub(IAdminService adminService)
        {
            _adminService = adminService;
        }

        public async Task MasterCreate(string id)
        {
            var res = new ResultMessageResponse()
            {
                data = id
            };
            await Clients.Others.SendAsync("MasterCreateToCLient", res);
        }

        public async Task MasterEdit(string id)
        {
            var res = new ResultMessageResponse()
            {
                data = id,
            };
            await Clients.Others.SendAsync("MasterEditToCLient", res, id);
        }

        public async Task MasterDelete(string id)
        {
            var res = new ResultMessageResponse()
            {
                data = id,
            };
            await Clients.Others.SendAsync("MasterDeleteToCLient", res, id);
        }
    }
}