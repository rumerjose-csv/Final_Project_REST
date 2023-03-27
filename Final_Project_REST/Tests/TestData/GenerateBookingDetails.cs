using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Final_Project_REST.DataModels;

namespace Final_Project_REST.Tests.TestData
{
    public class GenerateBookingDetails
    {
        public static BookingDetailsModel bookingDetails()
        {
            DateTime dateTime = DateTime.UtcNow.AddHours(+8).ToUniversalTime();

            Bookingdates bookingDates = new Bookingdates();
            bookingDates.Checkin = dateTime;
            bookingDates.Checkout = dateTime.AddDays(1);

            return new BookingDetailsModel
            {
                Firstname = "Juan",
                Lastname = "Carlos",
                Totalprice = 1000,
                Depositpaid = true,
                Bookingdates = bookingDates,
                Additionalneeds = "Final Payment"
            };
        }
    }
}
