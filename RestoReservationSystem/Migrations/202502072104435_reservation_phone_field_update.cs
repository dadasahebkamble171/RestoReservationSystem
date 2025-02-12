namespace RestoReservationSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class reservation_phone_field_update : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Reservations", "Contact", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Reservations", "Contact", c => c.Int(nullable: false));
        }
    }
}
