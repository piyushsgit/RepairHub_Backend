using Common.CommonMethods;
using Common.Helper;
using Dapper;
using Data;
using Model.CaseRequestResponse;
using Model.ConnectionInfo;
using Model.dbModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.ManageRequest
{
    public class ManageRequestRepo : BaseRepository, IManageRequestRepo
    {

        private readonly INonStaticCommonMethods nonStatic;
        public ManageRequestRepo(INonStaticCommonMethods config) : base(config)
        {
            this.nonStatic = nonStatic;
        }

        public async Task<int> UpsertConnectionIds(ConnectionModel con)
        {
            DynamicParameters parameters = new();
            parameters.Add("@UserId", con.UserId);
            parameters.Add("@ShopId", con.ShopId);
            parameters.Add("@LoginConnectionId", con.LoginConnectionId);
            parameters.Add("@RequestConnectionId", con.RequestConnectionId);
            parameters.Add("@ChatConnectionId", con.ChatConnectionId);
            parameters.Add("@IsActive", con.IsActive);
           
            return await ExecuteAsync<int>("[UpsertRealTimeConnectionId]", parameters, commandType: CommandType.StoredProcedure);
        }
        public async Task<List<ConnectionModel>> GetConnectionInfo()
        {
            return (await QueryAsync<ConnectionModel>("Sp_GetConnectionInfo", null, commandType: CommandType.StoredProcedure)).ToList();

        }
        public async Task<NewCaseResponse> InsertRequest(InsertRequestmodel req)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@UserId", Convert.ToInt32(StaticMethods.GetDecrypt(req.UserId)));
            parameters.Add("@tittle", req.Tittle);
            parameters.Add("@ShopId", Convert.ToInt32(StaticMethods.GetDecrypt(req.ShopId)));
            parameters.Add("@Description", req.Description);
            parameters.Add("@UserAddressId", req.UserAddressId);
            parameters.Add("@RequestImageList", string.Join(",", req.RequestImageName));

            return await QueryFirstOrDefaultAsync<NewCaseResponse>("InsertRequest", parameters, commandType: CommandType.StoredProcedure);

        }
        public async Task<CaseDetailOnShopAcceptanceResonse> InsertCaseDetailOnShopAcceptance(CaseDetailOnShopAcceptance caseDetail)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@RequestId",Convert.ToInt32(StaticMethods.GetDecrypt(caseDetail.RequestId)));   
            parameters.Add("@StatusName", caseDetail.StatusName);
            parameters.Add("@EstimatedTime", caseDetail.EstimatedTime);
            parameters.Add("@Description", caseDetail.Description);
            parameters.Add("@RepairType", caseDetail.RepairType);
            parameters.Add("@EstimatedPrice", caseDetail.EstimatedPrice);

            return await QueryFirstOrDefaultAsync<CaseDetailOnShopAcceptanceResonse>("ManageRequest", parameters,commandType: CommandType.StoredProcedure);
        }

        public async Task<RequestUpdateResponse> SimpleRequestUpdate(RequestUpdate caseDetail)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@RequestId", Convert.ToInt32(StaticMethods.GetDecrypt(caseDetail.RequestId)));
            parameters.Add("@StatusName", caseDetail.StatusName);
            parameters.Add("@Description", caseDetail.Description);
           

            var data= await QueryFirstOrDefaultAsync<RequestUpdateResponse>("ManageRequest", parameters, commandType: CommandType.StoredProcedure);
            return data;
        }
        public async Task<BillResponse> BillGenetaion(BillRequest caseDetail)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@RequestId", Convert.ToInt32(StaticMethods.GetDecrypt(caseDetail.RequestId)));
            parameters.Add("@StatusName", caseDetail.StatusName);
            parameters.Add("@Description", caseDetail.Description);
            parameters.Add("@FinalPrice", caseDetail.FinalPrice);


            return await QueryFirstOrDefaultAsync<BillResponse>("ManageRequest", parameters, commandType: CommandType.StoredProcedure);
        }
    }
}
