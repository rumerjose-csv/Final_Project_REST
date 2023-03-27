using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using Final_Project_REST.DataModels;

namespace Final_Project_REST.Tests
{
    public class RestBaseTest
    {
        public RestClient RestClient { get; set; }
        public BookingIDModel BookingDetails { get; set; }

        [TestInitialize]
        public void Initialize()
        {
            RestClient = new RestClient();
        }
    }
}
