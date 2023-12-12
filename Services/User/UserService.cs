using Common.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Model;
using Model.UsersModels;
using Repository.User;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using System.Security.Claims;
using System.Text;


namespace Services.User
{
    public class UserService : IUserService
    {
        private IUserRepository _accountRepository;
        public readonly IOptions<AppSettings> _connectionString;

        #region Constructors
        public UserService(IOptions<AppSettings> connection, IUserRepository accountRepository)
        {
            _connectionString = connection;
            _accountRepository = accountRepository;
        }
        #endregion

        public async Task<ApiPostResponse<LoginModelResponse>> Loginuser(LoginWithContact model)
        {
            var res = new ApiPostResponse<LoginModelResponse>();
            LoginModelResponse loginModelResponse = new LoginModelResponse();
            loginModelResponse = await _accountRepository.UserLogin(model);
            if (loginModelResponse.message == "please enter valid credentials")
            {
                res.Success = false;
                res.Message = ErrorMessages.LoginError;
                return res;
            }
            else if (loginModelResponse.message == "please enter your otp")
            {
                res.Success = false;
                res.Message = loginModelResponse.message;
                return res;
            }
            else if (loginModelResponse != null && loginModelResponse.message != "please enter valid credentials")
            {
                res.Data = new LoginModelResponse
                {
                    JwdToken = Login(model.ContactNo, "Admin")
                };
                res.Success = true;
                res.Message = ErrorMessages.LoginSuccess;
                return res;
            }
            else
            {
                res.Success = false;
                res.Message = ErrorMessages.LoginError;
                return res;
            }
        }
  
        public Task<OtpVerificationResponse> Generateopt(string ContactNo)
        {
            return _accountRepository.Generateopt(ContactNo);
        }
        public string Login(string Data, string Role)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_connectionString.Value.JWT_Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, Data),
                    new Claim(ClaimTypes.Role, Role)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);
            return tokenString;
        }



        //public static string GetHash(string input)
        //{
        //    using (SHA256 sha256Hash = SHA256.Create())
        //    {

        //        byte[] data = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(input));


        //        StringBuilder builder = new StringBuilder();


        //        for (int i = 0; i < data.Length; i++)
        //        {
        //            builder.Append(data[i].ToString("x2"));
        //        }

        //        return builder.ToString();
        //    }
        //}
              public async Task<ApiPostResponse<LoginModelResponse>> AdminLogin(LoginWithEmail model)
        {
            var res = new ApiPostResponse<LoginModelResponse>();
            LoginModelResponse loginModelResponse = new LoginModelResponse();
            loginModelResponse = await _accountRepository.AdminLogin(model);
            if (loginModelResponse.message == "please enter valid credentials")
            {
                res.Success = false;
                res.Message = ErrorMessages.LoginError;
                return res;
            }
            else if (loginModelResponse.message == "email not exists")
            {
                res.Success = false;
                res.Message = loginModelResponse.message;
                return res;
            }
            {
                res.Data = new LoginModelResponse
                {
                    JwdToken = Login(model.Email, "Admin")
                };
                res.Success = true;
                res.Message = ErrorMessages.LoginSuccess;
                return res;
            }
        }
        public async Task<ApiPostResponse<int>> RegisterUser(RegistrationModel regData)
        {
            ApiPostResponse<int> response = new ApiPostResponse<int>();
            var img = regData.Image;

            string imgpath = Path.Combine(Directory.GetCurrentDirectory(), "Asset/Images");
            string destinationPath = Path.Combine(imgpath, img.FileName);
            regData.ProfileImage = img.FileName;

            using (var stream = new FileStream(destinationPath, FileMode.Create))

            {
                img.CopyToAsync(stream);
            }
            //var Id = regData.Id;
            var FirstName = regData.FirstName;
            var LastName = regData.LastName;
            var ContactNo = regData.ContactNo;
            var EmailId = regData.EmailId;
            var UserTypeId = regData.UserTypeId;
            var CreatedBy = regData.CreatedBy;
            var ModifiedBy = regData.ModifiedBy;
            var Password = regData.Password;
            var ProfileImage = regData.ProfileImage;
            var ShopName = regData.ShopName;
            var ShopOwner = regData.ShopOwnerName;
            var AddharNumber = regData.AddharNumber;
            var PanNumber = regData.PanNumber;
            var shopDescription = regData.ShopDescription;
            var ShopRepairType = regData.ShopRepairType;
            var Since = regData.Since;
            var AsociateWith = regData.AsociateWith;
            var Country = regData.Country;
            var State = regData.State;
            var City = regData.City;
            var Address = regData.Address;
            var Rating = regData.Rating;
            var Area = regData.Area;
            var AddressType = regData.AddressType;
            var AccountNo = regData.AccountNo;
            var AccountHolderName = regData.AccountHolderName;
            var BankName = regData.BankName;
            var IFSC_Code = regData.IFSC_Code;
            var UPI_Detail = regData.UPI_Detail;




            var result = await _accountRepository.RegisterUser(regData);
            if (result == 3) { 
                response.Data = result;
                response.Success = true;
                response.Message = "Success";
            }
            else
            {
                response.Success = false;
                response.Message = "Failure";
            }
            return response;

        }
    }
}
