using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace RestoReservationSystem.Models
{
    public class RestoResDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Reservation> Reservations { get; set; }

        public DbSet<Table> Tables { get; set; }

        public DbSet<Admin> Admins { get; set; }
    }
}