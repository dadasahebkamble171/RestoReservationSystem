using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RestoReservationSystem.Models
{
    public class Table
    {
        [Key]
        public int TableId { get; set; }

        public int RestoId { get; set; }

        public int Capacity { get; set; }

        public string Catagory { get; set; }
    }
}