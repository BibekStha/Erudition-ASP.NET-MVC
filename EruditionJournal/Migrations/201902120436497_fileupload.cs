namespace EruditionJournal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fileupload : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Publication", "File", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Publication", "File");
        }
    }
}
