namespace IAAI_ARM64.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _123 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Members",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Account = c.Int(nullable: false),
                        Name = c.String(nullable: false),
                        Gender = c.Int(nullable: false),
                        Birthday = c.DateTime(nullable: false),
                        ApplyType = c.Int(nullable: false),
                        Address = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        International = c.Boolean(nullable: false),
                        CurrentPosition = c.String(nullable: false),
                        Title = c.String(nullable: false),
                        Education = c.String(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ServiceHistories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MemberId = c.Int(nullable: false),
                        ServiceUnit = c.String(),
                        Title = c.String(),
                        StartYear = c.DateTime(nullable: false),
                        StartMonth = c.DateTime(nullable: false),
                        EndYear = c.DateTime(nullable: false),
                        EndMonth = c.DateTime(nullable: false),
                        ServiceUnit2 = c.String(),
                        Title2 = c.String(),
                        StartYear2 = c.DateTime(nullable: false),
                        StartMonth2 = c.DateTime(nullable: false),
                        EndYear2 = c.DateTime(nullable: false),
                        EndMonth2 = c.DateTime(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                        Members_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Members", t => t.Members_Id)
                .Index(t => t.Members_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ServiceHistories", "Members_Id", "dbo.Members");
            DropIndex("dbo.ServiceHistories", new[] { "Members_Id" });
            DropTable("dbo.ServiceHistories");
            DropTable("dbo.Members");
        }
    }
}
