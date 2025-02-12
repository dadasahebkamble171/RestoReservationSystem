namespace RestoReservationSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatedDb_three : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Admins", "AboutYou", c => c.String());
            AddColumn("dbo.Admins", "BannerForResto", c => c.String());
            AddColumn("dbo.Admins", "ProfilePic", c => c.String());
            AddColumn("dbo.Reservations", "RestoId", c => c.Int(nullable: false));
            AddColumn("dbo.Reservations", "UserName", c => c.String());
            AddColumn("dbo.Reservations", "UserEmail", c => c.String());
            AddColumn("dbo.Reservations", "Contact", c => c.Int(nullable: false));
            AddColumn("dbo.Reservations", "Capacity", c => c.Int(nullable: false));
            AddColumn("dbo.Reservations", "Catagory", c => c.String());
            AddColumn("dbo.Reservations", "Day", c => c.String());
            AddColumn("dbo.Tables", "RestoId", c => c.Int(nullable: false));
            AddColumn("dbo.Tables", "Catagory", c => c.String());
            AddColumn("dbo.Tables", "TblFor", c => c.String());
            AddColumn("dbo.Tables", "Day", c => c.String());
            AlterColumn("dbo.Tables", "Status", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Tables", "Status", c => c.String(nullable: false, maxLength: 20));
            DropColumn("dbo.Tables", "Day");
            DropColumn("dbo.Tables", "TblFor");
            DropColumn("dbo.Tables", "Catagory");
            DropColumn("dbo.Tables", "RestoId");
            DropColumn("dbo.Reservations", "Day");
            DropColumn("dbo.Reservations", "Catagory");
            DropColumn("dbo.Reservations", "Capacity");
            DropColumn("dbo.Reservations", "Contact");
            DropColumn("dbo.Reservations", "UserEmail");
            DropColumn("dbo.Reservations", "UserName");
            DropColumn("dbo.Reservations", "RestoId");
            DropColumn("dbo.Admins", "ProfilePic");
            DropColumn("dbo.Admins", "BannerForResto");
            DropColumn("dbo.Admins", "AboutYou");
        }
    }
}
