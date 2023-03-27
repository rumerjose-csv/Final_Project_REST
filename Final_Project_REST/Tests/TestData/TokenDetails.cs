using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Final_Project_REST.DataModels;

namespace Final_Project_REST.Tests.TestData
{
    public class TokenDetails
    {
        public static TokenDetailsModel credentials()
        {
            return new TokenDetailsModel
            {
                Username = "admin",
                Password = "password123"
            };
        }
    }
}
