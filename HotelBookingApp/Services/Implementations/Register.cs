using AutoMapper;
using HotelBookingApp.Data;
using HotelBookingApp.Models;
using HotelBookingApp.Models.DTO;
using HotelBookingApp.Services.Interfaces;

namespace HotelBookingApp.Services.Implementations
{
    public class Register : IRegister
    {
        public readonly AppDbContext appDbContext;
        public Register(AppDbContext _appDbContext)
        {
            appDbContext = _appDbContext;
        }
        bool IRegister.Register(RegisterViewModel registerViewModel)
        {
            try
            {
                Mapper.CreateMap<RegisterViewModel, User>();

                User user = Mapper.Map<User>(registerViewModel);

                var result = appDbContext.Users.Add(user);
                return true;

            }
            catch (System.Exception ex)
            {

                return false;
            }
            
        }
    }
}
