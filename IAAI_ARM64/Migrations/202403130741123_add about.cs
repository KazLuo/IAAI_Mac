namespace IAAI_ARM64.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addabout : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.KnowledgeBases",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        InitDate = c.DateTime(nullable: false),
                        Title = c.String(nullable: false),
                        Download = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.KnowledgeBases");
        }
    }
}
