using HotelBookingApp.Client.Data;using HotelBookingApp.Client.Models;
using HotelBookingApp.Client.Models.DTO;
using HotelBookingApp.Client.Services.Interfaces;using System;
using System.Collections.Generic;
using System.Linq;

namespace HotelBookingApp.Client.Services.Implementations{    public class RoomRepo : IRoom    {        public AppDbContext appDbContext { get; set; }        public RoomRepo(AppDbContext _appDbContext)        {            appDbContext = _appDbContext;        }   
        public string AddRooms(Room room)
        {
            try
            {
                if (room == null)
                    return "Fail";

                appDbContext.Rooms.Add(room);
                appDbContext.SaveChanges();
                return "Success";

            }
            catch (System.Exception ex)
            {

                throw ex;
            }

        }

        public string Book(BookingModel model)
        {
            try
            {

                if (model == null)
                    return "Fail";

                var RoomModel = new Booking()
                {
                    RoomId = model.RoomId,
                    UserId = model.UserId,
                    StartDate = model.StartDate,
                    EndDate = model.EndDate
                };


                appDbContext.Bookings.Add(RoomModel);
                appDbContext.SaveChanges();
                return "Success";

            }
            catch (System.Exception ex)
            {

                throw ex;
            }
        }

        public string Cancel(int Id)
        {
            try
            {
                Booking model = appDbContext.Bookings.FirstOrDefault(x => x.Id == Id);
                if (model == null)
                    return "Not Found";
                 appDbContext.Bookings.Remove(model);
                appDbContext.SaveChanges();
                return "Success";
            }
            catch (System.Exception ex)
            {

                throw ex;
            }
        }

        public IEnumerable<Booking> History(int Id)
        {
            IEnumerable<Booking> History = appDbContext.Bookings.Where(a => a.UserId == Id && a.EndDate >= DateTime.Now).ToList();

            if (History.Count() > 0)
                return History;
            return null;    


        }

        public IEnumerable<Booking> CancelRoom(int UserId)
        {
            try
            {
              IEnumerable<Booking> bookings  = appDbContext.Bookings.Where(m => m.UserId == UserId && m.StartDate >= DateTime.Now).ToList();

                return bookings;
            }
            catch (System.Exception ex)
            {

                throw ex;
            }
        }

        public IEnumerable<Room> ViewRooms(int Id)
        {
            try
            {
                var NumberofRooms = appDbContext.Rooms.Where(a => a.CategoryId == Id).ToList();

                if (NumberofRooms.Any())
                    return NumberofRooms;
                return null;

            }
            catch (Exception)
            {

                throw;
            }
        }
    }}