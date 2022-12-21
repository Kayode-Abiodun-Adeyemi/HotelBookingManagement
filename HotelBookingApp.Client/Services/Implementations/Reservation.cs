using HotelBookingApp.Client.Data;
using HotelBookingApp.Client.Models;
using HotelBookingApp.Client.Models.DTO;
using HotelBookingApp.Client.Services.Interfaces;
using System.Linq;

namespace HotelBookingApp.Client.Services.Implementations
{
    public class Reservation : IReservation
    {
        public AppDbContext appDbContext { get; set; }
        public Reservation(AppDbContext _appDbContext)
        {
            appDbContext = _appDbContext;
        }
        public bool CheckAvailability(AvailabilityDTO availabilityDTO)
        {
            try
            {
                var ListofBookings = appDbContext.Bookings.Where(x => x.RoomId == availabilityDTO.Id && x.StartDate >= availabilityDTO.StartDate && x.EndDate <= availabilityDTO.EndDate).ToList();

                int numberofRooms = appDbContext.Rooms.FirstOrDefault(a => a.Id == availabilityDTO.Id).TotalNumberAvailable;

                if (numberofRooms == ListofBookings.Count)
                    return false;
                return true;
            }
            catch (System.Exception ex)
            {

                throw ex;
            }
        }

        public bool BookRoom(RoomBookingDto roomBookingDto)
        {
            try
            {
                var _roomBooking = new Booking()
                {
                    UserId = roomBookingDto.UserId,
                    RoomId = roomBookingDto.RoomId,
                    StartDate = roomBookingDto.StartDate,
                    EndDate = roomBookingDto.EndDate
                };

                appDbContext.Bookings.Add(_roomBooking);
                appDbContext.SaveChanges();
                return true;
            }
            catch (System.Exception ex)
            {
                return false;
                throw ex;
            }
        }

        public bool CancelReservation(int BookingId)
        {
            try
            {
                var model = appDbContext.Bookings.FirstOrDefault(x => x.Id == BookingId);
                if (model != null)
                {
                    appDbContext.Bookings.Remove(model);
                    return true;
                }

                return false;

            }
            catch (System.Exception ex)
            {
                return false;
                throw ex;
            }
        }

        public bool ReduceNumberofRoomsAfterBooking(AvailabilityDTO availabilityDTO)
        {
            try
            {
                var numberRec = appDbContext.Rooms.FirstOrDefault(a => a.Id == availabilityDTO.Id);

                int newValue = numberRec.TotalNumberAvailable - 1;

                numberRec.TotalNumberAvailable = newValue;

                appDbContext.Rooms.Update(numberRec);

                return true;
            }
            catch (System.Exception ex)
            {
                return false;
                throw ex;
            }
        }
    }
}
