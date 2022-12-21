using HotelBookingApp.Client.Models.DTO;

namespace HotelBookingApp.Client.Services.Interfaces
{
    public interface IRegister
    {
        bool Registration (RegisterViewModel registerViewModel);
        bool AdminRegistration(AdminRegisterViewModel registerViewModel);
    }   
    
}
