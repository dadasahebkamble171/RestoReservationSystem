namespace RestoReservationSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedDb_four : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tables", "Date", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tables", "Date");
        }
    }
}
