namespace RestoReservationSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Updateforadmin_table_2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Admins", "RestaurantName", c => c.String());
            AlterColumn("dbo.Admins", "Email", c => c.String());
            AlterColumn("dbo.Admins", "Phone", c => c.String());
            AlterColumn("dbo.Admins", "Password", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Admins", "Password", c => c.String(nullable: false));
            AlterColumn("dbo.Admins", "Phone", c => c.String(nullable: false, maxLength: 15));
            AlterColumn("dbo.Admins", "Email", c => c.String(nullable: false, maxLength: 150));
            AlterColumn("dbo.Admins", "RestaurantName", c => c.String(nullable: false, maxLength: 100));
        }
    }
}
