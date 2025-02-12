﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RestoReservationSystem.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        public string Name { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        internal object Split(char v)
        {
            throw new NotImplementedException();
        }
    }
}