using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestoReservationSystem.Models
{
    public class ManageResViewModel
    {
        public ChangeReservationStatus crs {  get; set; }

        public List<Reservation> reserv { get; set; }
    }
}