namespace IAAI_ARM64.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _11 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.KnowledgeBases", "Download", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.KnowledgeBases", "Download", c => c.String(nullable: false));
        }
    }
}
