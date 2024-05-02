namespace IAAI_ARM64.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addPassword : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Members", "Password", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Members", "Password");
        }
    }
}
