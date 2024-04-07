namespace WebApiCodeBaseToken.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeEmployeetoProduct : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Product",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Manufacturer = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ImgPath = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            DropTable("dbo.Employee");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Employee",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        DepartmentID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            DropTable("dbo.Product");
        }
    }
}
