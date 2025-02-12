namespace RestoReservationSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class reserv_update : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reservations", "restoName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Reservations", "restoName");
        }
    }
}
