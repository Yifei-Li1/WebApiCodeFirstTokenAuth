namespace WebApiCodeBaseToken.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NotSureWhy : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.User", "Username", c => c.String(nullable: false));
            AlterColumn("dbo.User", "Password", c => c.String(nullable: false, maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.User", "Password", c => c.String());
            AlterColumn("dbo.User", "Username", c => c.String());
        }
    }
}
