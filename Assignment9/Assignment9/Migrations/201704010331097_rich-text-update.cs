namespace Assignment9.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class richtextupdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Albums", "Depiction", c => c.String(maxLength: 500));
            AddColumn("dbo.Artists", "Portrayal", c => c.String(maxLength: 500));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Artists", "Portrayal");
            DropColumn("dbo.Albums", "Depiction");
        }
    }
}
