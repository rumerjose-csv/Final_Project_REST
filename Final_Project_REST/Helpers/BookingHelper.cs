using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using Final_Project_REST.DataModels;
using Final_Project_REST.Helpers;
using Final_Project_REST.Resources;
using Final_Project_REST.Tests.TestData;
using System.Net;

namespace Final_Project_REST.Helpers
{
    public class BookingHelper
    {
        private static async Task<string> GetToken(RestClient restClient)
        {
            restClient = new RestClient();
            restClient.AddDefaultHeader("Accept", "application/json");

            var postRequest = new RestRequest(BookingEndpoints.GenerateToken).AddJsonBody(TokenDetails.credentials());

            var generateToken = await restClient.ExecutePostAsync<TokenModel>(postRequest);

            return generateToken.Data.Token;
        }

        public static async Task<RestResponse<BookingIDModel>> CreateBooking(RestClient restClient)
        {
            restClient = new RestClient();
            restClient.AddDefaultHeader("Accept", "application/json");

            var postRequest = new RestRequest(BookingEndpoints.BaseBooking).AddJsonBody(GenerateBookingDetails.bookingDetails());

            return await restClient.ExecutePostAsync<BookingIDModel>(postRequest);
        }

        public static async Task<RestResponse<BookingDetailsModel>> GetBook(RestClient restClient, int bookingId)
        {
            restClient = new RestClient();
            restClient.AddDefaultHeader("Accept", "application/json");

            var getRequest = new RestRequest(BookingEndpoints.BookingMethodById(bookingId));

            return await restClient.ExecuteGetAsync<BookingDetailsModel>(getRequest);
        }

        public static async Task<RestResponse> DeleteBooking(RestClient restClient, int bookingId)
        {
            var token = await GetToken(restClient);
            restClient = new RestClient();
            restClient.AddDefaultHeader("Accept", "application/json");
            restClient.AddDefaultHeader("Cookie", "token=" + token);

            var getRequest = new RestRequest(BookingEndpoints.BookingMethodById(bookingId));

            return await restClient.DeleteAsync(getRequest);
        }

        public static async Task<RestResponse<BookingDetailsModel>> UpdateBooking(RestClient restClient, BookingDetailsModel booking, int bookingId)
        {
            var token = await GetToken(restClient);
            restClient = new RestClient();
            restClient.AddDefaultHeader("Accept", "application/json");
            restClient.AddDefaultHeader("Cookie", "token=" + token);

            var putRequest = new RestRequest(BookingEndpoints.BookingMethodById(bookingId)).AddJsonBody(booking);

            return await restClient.ExecutePutAsync<BookingDetailsModel>(putRequest);
        }
    }
}
