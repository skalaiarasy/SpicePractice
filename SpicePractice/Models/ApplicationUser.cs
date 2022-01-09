﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpicePractice.Models
{
    public class ApplicationUser:IdentityUser
    {
        public string Name { get; set; }
        public string StreetAddress { get; set; }        
        public string city { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
    }
}
