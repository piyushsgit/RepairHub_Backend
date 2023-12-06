using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Helper
{
    public class ErrorMessages
    {
        // General
        public const string Error = "An Error Occured";
        public const string Success = "Success";
        public const string SaveSuccess = "Data save successfully.";
        public const string UpdateSuccess = "Data update successfully.";
        public const string DataNotFound = "Data not found.";
        public const string ImageUploadSuccess = "Images uploded successfully.";
        public const string DeletedSuccess = "Deleted successfully.";
         
        // User
        public const string InvalidEmailPass = "Invalid email id or password";
        public const string InvalidEmailId = "Email id is not valid.";
        public const string FirstNameRequired = "First name is required";
        public const string LastNameRequired = "Last name is required";

        //Login
        public const string LoginSuccess = "Logged In Successfully.";
        public const string LoginError = "Invalid username or password.";
        public const string InActiveUser = "User is not active, please verify OTP for login.";
        public const string DeletedUser = "User is deleted.";


        //Registration
        public const string PhoneNoExist = "Phone no already exist with another user.";
        public const string CustomerRegistrationSuccess = "User registration successfully,Please verify your OTP";
        public const string VendorRegistrationSuccess = "Vendor registration successfully.";
        public const string InvalidUser = "Invalid User.";
        public const string OTPExpired = "OTP expired.";
        public const string InvalildOTP = "Invalid OTP.";
        public const string OTPVerificationSuccess = "OTP successfully verified.";
        public const string OTPSentSuccess = "OTP sent successfully.";

        //Utility
        public const string EncryptionError = "Invalid value";
        public const string DecryptionError = "Invalid value";

        //Ground
        public const string InvalidSport = "Invalid Sport.";
        public const string GroundNotBelongs = "Ground not belongs to login vendor.";

    }
}
