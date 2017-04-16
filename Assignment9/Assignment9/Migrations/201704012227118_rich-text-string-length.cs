namespace Assignment9.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class richtextstringlength : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Albums", "Depiction", c => c.String(maxLength: 1000));
            AlterColumn("dbo.Artists", "Portrayal", c => c.String(maxLength: 1000));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Artists", "Portrayal", c => c.String(maxLength: 500));
            AlterColumn("dbo.Albums", "Depiction", c => c.String(maxLength: 500));
        }
    }
}
