namespace RestoReservationSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReservationModelUpdate : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Reservations", "Date", c => c.String());
            AlterColumn("dbo.Reservations", "Time", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Reservations", "Time", c => c.Time(nullable: false, precision: 7));
            AlterColumn("dbo.Reservations", "Date", c => c.DateTime(nullable: false));
        }
    }
}
