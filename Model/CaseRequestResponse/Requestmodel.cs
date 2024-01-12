using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Model.dbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.CaseRequestResponse
{
    public class InsertRequestmodel
    {
        public string? UserId { get; set; }
        public string? ShopId { get; set; }
        public string? Description { get; set; }
        public string? Tittle { get; set; }
        public long? UserAddressId { get; set; }

        public List<string>? RequestImageName { get; set; }
    }
    public class RequestResponsemodel
    {
        public long RequestId { get; set; }
        public string? UserId { get; set; }
        public string? Description { get; set; }
        public string? Tittle { get; set; }
        public string? EncryptRequstId { get; set; }
        public string? First_Name { get; set; }
        public string? Last_Name { get; set; }
        public string? profileImage { get; set; }
        public DateTime CreatedOn { get; set; }
        public string? StatusName { get; set; }
        public string? StatusDesc { get; set; }
        public List<string>? CaseImages { get; set; }
        public string? CaseImage { get; set; }
        public long? TotalRequest { get; set; }
        public bool? IsOpen { get; set; }
        public string? FullAddress { get; set; }
        public List<string>? RequestImageName { get; set; }


    }

    public class NewCaseResponse
    {
        public long RequestId { get; set; }
        public string? UserId { get; set; }
        public string? Tittle { get; set; }
        public string? EncryptRequstId { get; set; }
        public string? First_Name { get; set; }
        public string? Last_Name { get; set; }
        public string? profileImage { get; set; }
        public DateTime CreatedOn { get; set; }
        public string? StatusName { get; set; }
        public string? StatusDesc { get; set; }
        public bool? IsOpen { get; set; }

    }



    public class ShopRequestQueryModel
    {
        public long? DecryptUserId { get; set; }
        public string? UserId { get; set; }
        public string? Search { get; set; }
        public string? OrderBy { get; set; }
        public int? PageSize { get; set; }
        public int? PageNumber { get; set; }
    }

    public class CaseDetailOnShopAcceptance
    {
        public string RequestId { get; set; }
        public string StatusName { get; set; }
        public DateTime? EstimatedTime { get; set; }
        public string? Description { get; set; }
        public string RepairType { get; set; }
        public string EstimatedPrice { get; set; }

    }
    public class CaseDetailOnShopAcceptanceResonse
    {
        public string RequestConnectionId { get; set; }
        public string StatusName { get; set; }
        public DateTime? EstimatedTime { get; set; }
        public string? Description { get; set; }
        public string RepairType { get; set; }
        public string EstimatedPrice { get; set; }
        public DateTime? CreatedOn { get; set; }
        public  string? DisplayName { get; set; }

    }

    public class RequestUpdate
    {
        public string RequestId { get; set; }
        public string? Description { get; set; }
        public string StatusName { get; set; }
        
    }
    public class RequestUpdateResponse
    {
        public string RequestConnectionId { get; set; }
        public string RequestId { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedOn { get; set; }
        public string StatusName { get; set; }
        public string DisplayName { get; set; }
    }
    public class BillRequest
    {
        public string RequestId { get; set; }
        public string? Description { get; set; }
        public string StatusName { get; set; }
        public string FinalPrice { get; set; }

    }
    public class BillResponse
    {
        public string RequestConnectionId { get; set; }
        public string RequestId { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedOn { get; set; }
        public string StatusName { get; set; }
        public string FinalPrice { get; set; }
        public string DisplayName { get; set; }
    }
}
