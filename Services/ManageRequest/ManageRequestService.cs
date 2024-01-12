using Common.CommonMethods;
using Common.Helper;
using Model.CaseRequestResponse;
using Model.ConnectionInfo;
using Org.BouncyCastle.Ocsp;
using Repository.ManageRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.ManageRealtime
{
    public class ManageRequestService:IManageRequestService
    {
        private readonly IManageRequestRepo _manageRequest;
        public ManageRequestService(IManageRequestRepo manageRequet) { 
        _manageRequest = manageRequet;
        }


        public async Task<ApiPostResponse<int>> UpsertConnectionIds(ConnectionModel ids)
        {
            ApiPostResponse<int> response = new();

            var data = await _manageRequest.UpsertConnectionIds(ids);

            if (data != null)
            {
                response.Success = true;
                response.Message = "Upsert SuccessfullY";
            }
            else
            {
                response.Success = false;
                response.Message = "Something went Wrong";
            }
            return response;
        }
        public async Task<List<ConnectionModel>> GetConnectionInfo()
        {
            return await _manageRequest.GetConnectionInfo();
        }

        public async Task<ApiPostResponse<NewCaseResponse>> InsertRequest(InsertRequestmodel req)
        {
            ApiPostResponse<NewCaseResponse> response = new ();

            var result = await _manageRequest.InsertRequest(req);
            if (result != null)
            {
                result.EncryptRequstId = StaticMethods.GetEncrypt(result.RequestId.ToString());
                result.RequestId = 0;
                response.Data = result;
                response.Success = true;
                response.Message = ErrorMessages.RequestGenrated;
            }
            else
            {
                response.Success = false;
                response.Message = ErrorMessages.FailToGenerateRequest; 
            }
            return response;
        }
        public async Task<ApiPostResponse<CaseDetailOnShopAcceptanceResonse>> InsertCaseDetailOnShopAcceptance(CaseDetailOnShopAcceptance caseDetail)
        {
            ApiPostResponse<CaseDetailOnShopAcceptanceResonse> response = new();

            var result = await _manageRequest.InsertCaseDetailOnShopAcceptance(caseDetail);
            if (result != null)
            {
                response.Data = result;
                response.Success = true;
                response.Message = ErrorMessages.Success;
            }
            else
            {
                response.Success = false;
                response.Message = ErrorMessages.Error;
            }
            return response;
        }
        public async Task<ApiPostResponse<RequestUpdateResponse>> SimpleRequestUpdate(RequestUpdate caseDetail)
        {
            ApiPostResponse<RequestUpdateResponse> response = new();

            var result = await _manageRequest.SimpleRequestUpdate(caseDetail);
            if (result != null)
            {
                response.Data = result;
                response.Success = true;
                response.Message = ErrorMessages.Success;
            }
            else
            {
                response.Success = false;
                response.Message = ErrorMessages.Error;
            }
            return response;

        }
        public async Task<ApiPostResponse<BillResponse>> BillGenetaion(BillRequest caseDetail)
        {
            ApiPostResponse<BillResponse> response = new();

            var result = await _manageRequest.BillGenetaion(caseDetail);
            if (result != null)
            {
                response.Data = result;
                response.Success = true;
                response.Message = ErrorMessages.Success;
            }
            else
            {
                response.Success = false;
                response.Message = ErrorMessages.Error;
            }
            return response;
        }


    }
}
