namespace Assignment9.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AudioPropertChangeinTrack : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tracks", "AudioContentType", c => c.String(maxLength: 200));
            AddColumn("dbo.Tracks", "Audio", c => c.Binary());
            DropColumn("dbo.Tracks", "MediaItem");
            DropColumn("dbo.Tracks", "MediaItemType");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tracks", "MediaItemType", c => c.String());
            AddColumn("dbo.Tracks", "MediaItem", c => c.Binary());
            DropColumn("dbo.Tracks", "Audio");
            DropColumn("dbo.Tracks", "AudioContentType");
        }
    }
}
