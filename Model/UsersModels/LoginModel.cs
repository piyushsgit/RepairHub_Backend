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
        public int? otp { get; set; }
    }

    public class LoginModelResponse
    {
        public int Id { get; set; }
        public string EmailId { get; set; }
        public string JwdToken { get; set; }
        public string IsVarified { get; set; }
        public string message { get; set; }
        
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

    public class Email
    {
        public int? type { get; set; }
        public string? EmailId { get; set;}
    }
}
