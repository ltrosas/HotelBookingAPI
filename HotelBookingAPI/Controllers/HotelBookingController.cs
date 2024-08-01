using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HotelBookingAPI.Models;
using HotelBookingAPI.Data;
using System.Diagnostics.CodeAnalysis;

namespace HotelBookingAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class HotelBookingController : ControllerBase
    {
        private readonly ApiContext _context;

        public HotelBookingController(ApiContext context)
        {
            _context = context;
        }

        //Create
        [HttpPost]
        public IActionResult Create(HotelBooking booking)
        {
            var BookingInDb = _context.Bookings.Find(booking.Id);

            if (BookingInDb == null)
            {
                _context.Bookings.Add(booking);
                _context.SaveChanges();
                return Ok(booking);
            }
            else
            {
                string messageExists = "Booking already exists.";
                return NotFound(new { message = messageExists });
            }
        }

        //Edit
        [HttpPut]
        public IActionResult Update(HotelBooking booking)
        {
            var BookingInDb = _context.Bookings.Find(booking.Id);

            if(BookingInDb == null)
            {
                string messageNotExists = "Booking does not exist.";
                return NotFound(new { message = messageNotExists });
            } else
            {
                BookingInDb = booking;
                _context.SaveChanges();
                return Ok(booking);
            }
        }

        //Get
        [HttpGet]
        public IActionResult Get(int id)
        {
            var result = _context.Bookings.Find(id);

            if (result == null)
            {

                string messageNotExists = "Booking does not exist.";
                return NotFound(new { message = messageNotExists });
            }
            else
            {
                return Ok(result);
            }
        }

        //GetAll
        [HttpGet()]
        public IActionResult GetAll()
        {
            var result = _context.Bookings.ToList();
            return Ok(result);
        }

        //Delete
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var result = _context.Bookings.Find(id);

            if (result == null)
            {

                string messageNotFound= "Booking does not exist.";
                return NotFound(new { message = messageNotFound });
            }
            else
            {
                _context.Remove(result);
                _context.SaveChanges();

                string messageDeleted = "Booking deleted.";
                return Ok(new { message = messageDeleted, booking = result });
            }
        }




    }
}
