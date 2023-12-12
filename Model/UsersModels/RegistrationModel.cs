using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Data;

namespace Model.UsersModels
{
    public class RegistrationModel
    {
 
        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? ContactNo { get; set; }

        public string? EmailId { get; set; }

        public int? UserTypeId { get; set; }
       // public bool IsShopKeeper { get; set; } = false;
        public long? CreatedBy { get; set; }

        public string? ModifiedBy { get; set; }
        public string? Password { get; set; }

        public string? ProfileImage { get; set; }

        public IFormFile Image { get; set; }

        public string? ShopName { get; set; }

        public string? ShopOwnerName { get; set; }

        public string? AddharNumber { get; set; }

        public string? PanNumber { get; set; }

        public string? ShopDescription { get; set; }

        public int? ShopRepairType { get; set; }

        public DateTime? Since { get; set; }

        public string? AsociateWith { get; set; }

        public string? Country { get; set; }

        public string? State { get; set; }

        public string? City { get; set; }

        public string? Address { get; set; }

        public decimal? Rating { get; set; }

        public string? Area { get; set; }

        public string? AddressType { get; set; }

        public string? AccountNo { get; set; }

        public string? AccountHolderName { get; set;}

        public string? BankName { get; set; }

        public string? IFSC_Code { get; set; }

        public string? UPI_Detail { get; set; }




    }

    
}
