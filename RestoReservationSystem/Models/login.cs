﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RestoReservationSystem.Models
{
    public class login
    {
        public string EmailOrUsername { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; } 

        public string Role { get; set; }
    }
}