using Common.Helper;
using Model.CaseRequestResponse;
using Model.ConnectionInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.ManageRealtime
{
    public interface IManageRequestService
    {
        public Task<ApiPostResponse<int>> UpsertConnectionIds(ConnectionModel ids);
        public Task<List<ConnectionModel>> GetConnectionInfo();
        public Task<ApiPostResponse<NewCaseResponse>> InsertRequest(InsertRequestmodel req);
        public Task<ApiPostResponse<CaseDetailOnShopAcceptanceResonse>> InsertCaseDetailOnShopAcceptance(CaseDetailOnShopAcceptance caseDetail);
        public Task<ApiPostResponse<RequestUpdateResponse>> SimpleRequestUpdate(RequestUpdate caseDetail);
        public Task<ApiPostResponse<BillResponse>> BillGenetaion(BillRequest caseDetail);
    }
}
