﻿using System;

namespace MTest.Models
{
    public class AddCustomerRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime DOB { get; set; }
    }
}
