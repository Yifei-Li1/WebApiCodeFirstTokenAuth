namespace WebApiCodeBaseToken.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddErrorLogger : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ErrorLog",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Message = c.String(),
                        RequestMethod_ = c.String(),
                        RequestUri_ = c.String(),
                        TimeUtc_ = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ErrorLog");
        }
    }
}
