namespace Auction.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedCategories : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            AddColumn("dbo.Auctions", "CategoryID", c => c.Int(nullable: false));
            CreateIndex("dbo.Auctions", "CategoryID");
            AddForeignKey("dbo.Auctions", "Category", "dbo.Categories", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Auctions", "CategoryID", "dbo.Categories");
            DropForeignKey("dbo.AuctionPictures", "AuctionID", "dbo.Auctions");
            DropForeignKey("dbo.AuctionPictures", "PictureID", "dbo.Pictures");
            DropIndex("dbo.Auctions", new[] { "CategoryID" });
            DropIndex("dbo.AuctionPictures", new[] { "PictureID" });
            DropIndex("dbo.AuctionPictures", new[] { "AuctionID" });
            DropTable("dbo.Categories");
            DropTable("dbo.Auctions");
            DropTable("dbo.Pictures");
            DropTable("dbo.AuctionPictures");
        }
    }
}
