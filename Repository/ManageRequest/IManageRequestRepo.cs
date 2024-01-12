using Common.Helper;
using Model.CaseRequestResponse;
using Model.ConnectionInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.ManageRequest
{
    public interface IManageRequestRepo
    {
        public Task<int> UpsertConnectionIds(ConnectionModel ids);
        public Task<List<ConnectionModel>> GetConnectionInfo();
        public Task<NewCaseResponse> InsertRequest(InsertRequestmodel req);
        public Task<CaseDetailOnShopAcceptanceResonse> InsertCaseDetailOnShopAcceptance(CaseDetailOnShopAcceptance caseDetail);
        public Task<RequestUpdateResponse> SimpleRequestUpdate(RequestUpdate caseDetail);
        public Task<BillResponse> BillGenetaion(BillRequest caseDetail);
    }
}
