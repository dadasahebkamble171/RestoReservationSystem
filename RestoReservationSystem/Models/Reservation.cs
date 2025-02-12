using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RestoReservationSystem.Models
{
    public class Reservation
    {
        [Key]
        public int ReservationId { get; set; }

        public int RestoId { get; set; }

        public string restoName { get; set; }

        public int UserId { get; set; }

        public int TableId { get; set; }

        public string UserName { get; set; }

        public string UserEmail { get; set; }

        public string Contact { get; set; }

        public int Capacity { get; set; }

        public string Catagory { get; set; }

        public string Day { get; set; }

        public string Date { get; set; }

        public string Time { get; set; }

        public string Status { get; set; }
    }
}