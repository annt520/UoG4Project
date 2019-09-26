namespace Assignment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddYoutubeLinkAndRating : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posts", "YoutubeLink", c => c.String());
            AddColumn("dbo.Posts", "Rating", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Posts", "Rating");
            DropColumn("dbo.Posts", "YoutubeLink");
        }
    }
}
