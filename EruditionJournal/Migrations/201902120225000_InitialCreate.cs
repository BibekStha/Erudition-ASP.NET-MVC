namespace EruditionJournal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Category",
                c => new
                    {
                        CategoryId = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.CategoryId);
            
            CreateTable(
                "dbo.Publication",
                c => new
                    {
                        PublicationId = c.Int(nullable: false, identity: true),
                        PublicationTitle = c.String(nullable: false),
                        PublicationAbstract = c.String(nullable: false, maxLength: 750),
                        PublishedDate = c.DateTime(nullable: false),
                        HasManuscript = c.Int(nullable: false),
                        Category_CategoryId = c.Int(),
                        Publisher_PublisherId = c.Int(),
                    })
                .PrimaryKey(t => t.PublicationId)
                .ForeignKey("dbo.Category", t => t.Category_CategoryId)
                .ForeignKey("dbo.Publisher", t => t.Publisher_PublisherId)
                .Index(t => t.Category_CategoryId)
                .Index(t => t.Publisher_PublisherId);
            
            CreateTable(
                "dbo.Publisher",
                c => new
                    {
                        PublisherId = c.Int(nullable: false, identity: true),
                        PublisherFName = c.String(nullable: false),
                        PublisherLName = c.String(nullable: false),
                        PublisherDisplayName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.PublisherId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Publication", "Publisher_PublisherId", "dbo.Publisher");
            DropForeignKey("dbo.Publication", "Category_CategoryId", "dbo.Category");
            DropIndex("dbo.Publication", new[] { "Publisher_PublisherId" });
            DropIndex("dbo.Publication", new[] { "Category_CategoryId" });
            DropTable("dbo.Publisher");
            DropTable("dbo.Publication");
            DropTable("dbo.Category");
        }
    }
}
