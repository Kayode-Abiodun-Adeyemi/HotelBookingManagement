
using AutoMapper;
using HotelBookingApp.Client.Data;
using HotelBookingApp.Client.Models;
using HotelBookingApp.Client.Models.DTO;
using HotelBookingApp.Client.Services.Interfaces;
using System;

namespace HotelBookingApp.Client.Services.Implementations
{
    public class Register : IRegister
    {
        public readonly AppDbContext appDbContext;
        public Register(AppDbContext _appDbContext)
        {
            appDbContext = _appDbContext;
        }

     
        public bool Registration(RegisterViewModel registerViewModel)
        {
            try
            {
                string EncryptedPassword = EncodePasswordToBase64(registerViewModel.Password);
                registerViewModel.Password = EncryptedPassword;
                Mapper.CreateMap<RegisterViewModel, User>();

                User user = Mapper.Map<User>(registerViewModel);

                var result = appDbContext.Users.Add(user);
                appDbContext.SaveChanges();
             
                return true;

            }
            catch (System.Exception)
            {

                return false;
            }

        }

        public bool AdminRegistration(AdminRegisterViewModel adminregisterViewModel)
        {
            try
            {
                string EncryptedPassword = EncodePasswordToBase64(adminregisterViewModel.Password);
                adminregisterViewModel.Password = EncryptedPassword;
                Mapper.CreateMap<AdminRegisterViewModel, User>();

                User user = Mapper.Map<User>(adminregisterViewModel);

                var result = appDbContext.Users.Add(user);
                appDbContext.SaveChanges();

                return true;

            }
            catch (System.Exception)
            {

                return false;
            }

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

        public string DecodeFrom64(string encodedData)
        {
            System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
            System.Text.Decoder utf8Decode = encoder.GetDecoder();
            byte[] todecode_byte = Convert.FromBase64String(encodedData);
            int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
            char[] decoded_char = new char[charCount];
            utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
            string result = new String(decoded_char);
            return result;
        }


    }
}
