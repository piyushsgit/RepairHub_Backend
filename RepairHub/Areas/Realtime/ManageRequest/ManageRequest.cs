using Common.CommonMethods;
using Microsoft.AspNetCore.SignalR;
using Model.CaseRequestResponse;
using Model.ConnectionInfo;
using Services.ManageRealtime;
using Services.User;

namespace RepairHub.Areas.Realtime.ManageRequest
{
    public class ManageRequest : Hub
    {
        
        private readonly IManageRequestService _requestservice;
        public ManageRequest(IManageRequestService manageRequestService) 
        {
        
            _requestservice = manageRequestService;
        
        }
        public override async Task OnConnectedAsync()
        {
            try
            {
                ConnectionModel model = new();

                model.UserId = Convert.ToInt32(StaticMethods.GetDecrypt(Context.GetHttpContext().Request.Query["userId"]));
                model.RequestConnectionId = Context.ConnectionId;

                await _requestservice.UpsertConnectionIds(model);
            }
            catch (Exception ex) { }



            await base.OnConnectedAsync();
        }


        public async Task UserFirstRequest(InsertRequestmodel req)
        {
            try
            {
                var response = await _requestservice.InsertRequest(req);

                if (response.Success)
                {
                    response.Data.UserId = StaticMethods.GetEncrypt(req.UserId);
                    var decryptedShopId = Convert.ToInt32(StaticMethods.GetDecrypt(req.ShopId));

                    var shopConnectionId = (await _requestservice.GetConnectionInfo())
                        .FirstOrDefault(item => item.ShopId == decryptedShopId)?
                        .RequestConnectionId;

                    if (!string.IsNullOrEmpty(shopConnectionId))
                    {
                        await Clients.Clients(shopConnectionId).SendAsync("RecieveFirstRequest", response);
                    }
                    else { throw new Exception(); }
                }
                else { throw new Exception(); }
            }
            catch { throw new Exception(); }

        }


        public async Task ShopAcceptance(CaseDetailOnShopAcceptance details)
        {
            try
            {
                var response = await _requestservice.InsertCaseDetailOnShopAcceptance(details);

                if (response.Success)
                {

                    if (!string.IsNullOrEmpty(response.Data.RequestConnectionId))
                    {
                        await Clients.Clients(response.Data.RequestConnectionId).SendAsync("ShopAcceptance", response);
                    }
                    else { throw new Exception(); }
                }
                else { throw new Exception(); }
            }
            catch { throw new Exception(); }

        }

        public async Task CommonRequestResponse(RequestUpdate details)
        {
            try
            {
                var response = await _requestservice.SimpleRequestUpdate(details);

                if (response.Success)
                {

                    if (!string.IsNullOrEmpty(response.Data.RequestConnectionId))
                    {
                        await Clients.Clients(response.Data.RequestConnectionId).SendAsync("CommonManageRequest", response);
                    }
                    else { throw new Exception(); }
                }
                else { throw new Exception(); }
            }
            catch { throw new Exception(); }

        }
        public async Task BillGeneration(BillRequest details)
        {
            try
            {
                var response = await _requestservice.BillGenetaion(details);

                if (response.Success)
                {

                    if (!string.IsNullOrEmpty(response.Data.RequestConnectionId))
                    {
                        await Clients.Clients(response.Data.RequestConnectionId).SendAsync("BillGeneration", response);
                    }
                    else { throw new Exception(); }
                }
                else { throw new Exception(); }
            }
            catch { throw new Exception(); }

        }




        public override async Task OnDisconnectedAsync(Exception exception)
        {
            // Code to execute when a client disconnects from this hub
            // This could involve cleaning up resources, updating status, etc.
            await base.OnDisconnectedAsync(exception);
        }

    }
}
