namespace IAAI_ARM64.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixaccount : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Members", "Account", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Members", "Account", c => c.Int(nullable: false));
        }
    }
}
