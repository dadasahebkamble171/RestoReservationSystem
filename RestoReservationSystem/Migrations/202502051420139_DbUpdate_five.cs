namespace RestoReservationSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DbUpdate_five : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Tables", "TblFor");
            DropColumn("dbo.Tables", "Day");
            DropColumn("dbo.Tables", "Date");
            DropColumn("dbo.Tables", "Status");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tables", "Status", c => c.String());
            AddColumn("dbo.Tables", "Date", c => c.String());
            AddColumn("dbo.Tables", "Day", c => c.String());
            AddColumn("dbo.Tables", "TblFor", c => c.String());
        }
    }
}
