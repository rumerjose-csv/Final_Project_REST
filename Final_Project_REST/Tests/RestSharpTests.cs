using Final_Project_REST.DataModels;
using Final_Project_REST.Helpers;
using Final_Project_REST.Tests.TestData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

[assembly: Parallelize(Workers = 10, Scope = ExecutionScope.MethodLevel)]

namespace Final_Project_REST.Tests
{
    [TestClass]
    public class RestSharpTests : RestBaseTest
    {
        private readonly List<BookingIDModel> cleanupList = new List<BookingIDModel>();

        [TestInitialize]
        public async Task Initialize()
        {
            var restResponse = await BookingHelper.CreateBooking(RestClient);
            BookingDetails = restResponse.Data;

            Assert.AreEqual(restResponse.StatusCode, HttpStatusCode.OK);
        }

        [TestCleanup]
        public async Task TestCleanup()
        {
            foreach (var data in cleanupList)
            {
                var deleteBookingResponse = await BookingHelper.DeleteBooking(RestClient, data.BookingId);
            }
        }

        [TestMethod]
        public async Task CreateBooking()
        {
            var getBookingResponse = await BookingHelper.GetBook(RestClient, BookingDetails.BookingId);

            cleanupList.Add(BookingDetails);

            var expectedBookingDetails = GenerateBookingDetails.bookingDetails();

            Assert.AreEqual(expectedBookingDetails.Firstname, getBookingResponse.Data.Firstname, "Firstname mismatch");
            Assert.AreEqual(expectedBookingDetails.Lastname, getBookingResponse.Data.Lastname, "Lastname mismatch");
            Assert.AreEqual(expectedBookingDetails.Totalprice, getBookingResponse.Data.Totalprice, "Total Price mismatch");
            Assert.AreEqual(expectedBookingDetails.Depositpaid, getBookingResponse.Data.Depositpaid, "Deposit mismatch");
            Assert.AreEqual(expectedBookingDetails.Bookingdates.Checkin.Date, getBookingResponse.Data.Bookingdates.Checkin.Date, "Check in mismatch");
            Assert.AreEqual(expectedBookingDetails.Bookingdates.Checkout.Date, getBookingResponse.Data.Bookingdates.Checkout.Date, "Check out mismatch");
            Assert.AreEqual(expectedBookingDetails.Additionalneeds, getBookingResponse.Data.Additionalneeds, "Additional needs mismatch");
        }

        [TestMethod]
        public async Task UpdateBooking()
        {
            var getBookingResponse = await BookingHelper.GetBook(RestClient, BookingDetails.BookingId);

            cleanupList.Add(BookingDetails);

            var updateBookingDetails = new BookingDetailsModel()
            {
                Firstname = "John",
                Lastname = "Weak",
                Totalprice = getBookingResponse.Data.Totalprice,
                Depositpaid = getBookingResponse.Data.Depositpaid,
                Bookingdates = getBookingResponse.Data.Bookingdates,
                Additionalneeds = getBookingResponse.Data.Additionalneeds
            };
            var updateBooking = await BookingHelper.UpdateBooking(RestClient, updateBookingDetails, BookingDetails.BookingId);

            Assert.AreEqual(HttpStatusCode.OK, updateBooking.StatusCode);

            var getUpdatedBookingResponse = await BookingHelper.GetBook(RestClient, BookingDetails.BookingId);

            Assert.AreEqual(updateBookingDetails.Firstname, getUpdatedBookingResponse.Data.Firstname, "Firstname mismatch");
            Assert.AreEqual(updateBookingDetails.Lastname, getUpdatedBookingResponse.Data.Lastname, "Lastname mismatch");
            Assert.AreEqual(updateBookingDetails.Totalprice, getUpdatedBookingResponse.Data.Totalprice, "Total price mismatch");
            Assert.AreEqual(updateBookingDetails.Depositpaid, getUpdatedBookingResponse.Data.Depositpaid, "Deposit paid mismatch");
            Assert.AreEqual(updateBookingDetails.Bookingdates.Checkin.Date, getUpdatedBookingResponse.Data.Bookingdates.Checkin.Date, "Check in mismatch");
            Assert.AreEqual(updateBookingDetails.Bookingdates.Checkout.Date, getUpdatedBookingResponse.Data.Bookingdates.Checkout.Date, "Check out mismatch");
            Assert.AreEqual(updateBookingDetails.Additionalneeds, getUpdatedBookingResponse.Data.Additionalneeds, "Additional needs mismatch");
        }

        [TestMethod]
        public async Task RemoveBooking()
        {
            var delBooking = await BookingHelper.DeleteBooking(RestClient, BookingDetails.BookingId);

            Assert.AreEqual(HttpStatusCode.Created, delBooking.StatusCode);
        }

        [TestMethod]
        public async Task ValidateBooking()
        {
            var getBooking = await BookingHelper.GetBook(RestClient, 950000050);

            cleanupList.Add(BookingDetails);

            Assert.AreEqual(HttpStatusCode.NotFound, getBooking.StatusCode);


        }
    }
}

