namespace RestoReservationSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Updateforadmin_table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Admins",
                c => new
                    {
                        AdminId = c.Int(nullable: false, identity: true),
                        RestaurantName = c.String(nullable: false, maxLength: 100),
                        Email = c.String(nullable: false, maxLength: 150),
                        Phone = c.String(nullable: false, maxLength: 15),
                        Password = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.AdminId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Admins");
        }
    }
}
