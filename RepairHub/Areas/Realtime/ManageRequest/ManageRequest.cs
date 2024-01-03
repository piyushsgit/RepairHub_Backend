using Microsoft.AspNetCore.SignalR;
using Model.UsersModels;
using Services.User;

namespace RepairHub.Areas.Realtime.ManageRequest
{
    public class ManageRequest : Hub
    {
        private readonly IUserService _userService;
        public ManageRequest(IUserService userService) 
        {
            userService = _userService;
        
        }
        public override async Task OnConnectedAsync()
        {
            // Code to execute when a client connects to this hub
            // For example, you might want to log the connection or perform certain operations          

            await base.OnConnectedAsync();
        }


        public async Task UserFirstRequest(InsertRequestmodel req)
        {
           // var result =await _userService.InsertRequest(req);
            
        }
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            // Code to execute when a client disconnects from this hub
            // This could involve cleaning up resources, updating status, etc.
            await base.OnDisconnectedAsync(exception);
        }

    }
}
