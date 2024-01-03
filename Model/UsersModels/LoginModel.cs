using Azure;
using Microsoft.Identity.Client.Extensions.Msal;
using System.ComponentModel.DataAnnotations;
namespace Model.UsersModels
{
    public class LoginWithEmail // For Admin
    {
        [Required]
        public string? Email { get; set; }
        public string? Password { get; set; }

    }
    public class LoginWithContact // For Customer and Shop keeper
    {
        public string? ContactNo { get; set; }
        public int? Otp { get; set; }

    }

    public class SignInGoogle 
    {
        public string? Email { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? ProfileImage { get; set; }
    }
    public class OtpVerification
    {
        public int UserID { get; set; }
        public int? VerificationCode { get; set; }
        public int? VerificationStatus { get; set; }
    }
    public class OtpVerificationResponse
    {
        public int Id { get; set; }
        public string? otpExpiryTime { get; set; }
        public string? EmailId { get; set; }
        public int? otp { get; set; }
    }

    public class LoginModelResponse
    {
        public int Id { get; set; }
        public string EmailId { get; set; }
        public string JwdToken { get; set; }
        public int UserTypeId { get; set; }
        public string IsVarified { get; set; }
        public string First_Name { get; set; }
        public string tokenExpiration { get; set; }
        public string userTypeId { get; set; }
        public string message { get; set; }
        public string profileImage { get; set; }

    }

    public class ForgotPassword {
        public int Type { get; set; }
        public string? Email { get; set; }
        public string? contact { get; set; }
        public string? Otp { get; set; }
        public string? NewPassword { get; set; }
    }
    public class Message
    {
        public string? message { get; set; }
    }
    public class UserTokenModel
    {
        public decimal UserId { get; set; }
        public decimal RoleId { get; set; }
        public string Role { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PhoneNo { get; set; }
        public string? EmailId { get; set; }
        public string UserLoginId { get; set; }
        public DateTime TokenValidTo { get; set; }
    }
    public class Email
    {
        public int? type { get; set; }
        public string? EmailId { get; set; } = null;
    }
}
