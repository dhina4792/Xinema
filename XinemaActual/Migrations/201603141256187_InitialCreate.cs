namespace XinemaActual.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cinemas",
                c => new
                    {
                        CinemaID = c.Int(nullable: false, identity: true),
                        CinemaName = c.String(),
                        CinemaAddress = c.String(),
                    })
                .PrimaryKey(t => t.CinemaID);
            
            CreateTable(
                "dbo.Movies",
                c => new
                    {
                        movieID = c.Int(nullable: false, identity: true),
                        movieTitle = c.String(),
                    })
                .PrimaryKey(t => t.movieID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Movies");
            DropTable("dbo.Cinemas");
        }
    }
}
