namespace DvdLibrary.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Dvds",
                c => new
                    {
                        DvdId = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        ReleaseYear = c.Int(),
                        Director = c.String(maxLength: 20),
                        Rating = c.String(maxLength: 5),
                        Notes = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.DvdId);
        }
        
        public override void Down()
        {
            DropTable("dbo.Dvds");
        }
    }
}
