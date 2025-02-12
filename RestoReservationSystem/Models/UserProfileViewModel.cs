using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestoReservationSystem.Models
{
    public class UserProfileViewModel
    {
        public List<User> UserInfo { get; set; }

        public List<Reservation> ResInfo { get; set; }

        public List<Reservation> HResInfo { get; set; }
    }
}