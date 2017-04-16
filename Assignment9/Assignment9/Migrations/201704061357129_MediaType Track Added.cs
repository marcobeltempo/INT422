namespace Assignment9.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MediaTypeTrackAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tracks", "MediaItem", c => c.Binary());
            AddColumn("dbo.Tracks", "MediaItemType", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tracks", "MediaItemType");
            DropColumn("dbo.Tracks", "MediaItem");
        }
    }
}
