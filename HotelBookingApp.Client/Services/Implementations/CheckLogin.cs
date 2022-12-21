using HotelBookingApp.Client.Data;
using HotelBookingApp.Client.Models;
using HotelBookingApp.Client.Models.DTO;
using HotelBookingApp.Client.Services.Interfaces;
using System;
using System.Linq;

namespace HotelBookingApp.Client.Services.Implementations
{
    public class CheckLogin : ICheckLogin
    {
        public readonly AppDbContext appDbContext;

        public CheckLogin(AppDbContext _appDbContext)
        {
            appDbContext = _appDbContext;
        }
        public User ValidateLogin(LoginViewModel model)
        {
            string EncodedPassword = EncodePasswordToBase64(model.Password);

            var result = appDbContext.Users.FirstOrDefault(a => a.EmailAddress == model.EmailAddress && a.Password == EncodedPassword);
            if (result != null)
                return result;
            return null;

        }

        private string EncodePasswordToBase64(string Password)
        {
            try
            {
                byte[] encData_byte = new byte[Password.Length];
                encData_byte = System.Text.Encoding.UTF8.GetBytes(Password);
                string encodedData = Convert.ToBase64String(encData_byte);
                return encodedData;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in base64Encode" + ex.Message);
            }
        }
    }
}
