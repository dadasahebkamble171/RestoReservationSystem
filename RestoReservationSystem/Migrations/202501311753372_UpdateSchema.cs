namespace RestoReservationSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateSchema : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "Name", c => c.String());
            AlterColumn("dbo.Users", "Username", c => c.String());
            AlterColumn("dbo.Users", "Email", c => c.String());
            AlterColumn("dbo.Users", "Phone", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "Phone", c => c.String(maxLength: 10));
            AlterColumn("dbo.Users", "Email", c => c.String(maxLength: 150));
            AlterColumn("dbo.Users", "Username", c => c.String(maxLength: 100));
            AlterColumn("dbo.Users", "Name", c => c.String(maxLength: 100));
        }
    }
}
