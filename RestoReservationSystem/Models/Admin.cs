using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RestoReservationSystem.Models
{
    public class Admin
    {
        [Key]
        public int AdminId { get; set; }

        public string RestaurantName { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string AboutYou { get; set; }

        public string BannerForResto { get; set; }

        public string ProfilePic { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}