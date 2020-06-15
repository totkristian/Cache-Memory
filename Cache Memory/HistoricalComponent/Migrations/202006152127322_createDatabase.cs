namespace HistoricalComponent.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class createDatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.HistoricalDescriptions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Dataset = c.Int(nullable: false),
                        ListDescriptionId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ListDescriptions", t => t.ListDescriptionId)
                .Index(t => t.ListDescriptionId);
            
            CreateTable(
                "dbo.HistoricalProperties",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Code = c.Int(nullable: false),
                        HistoricalValue_Timestamp = c.DateTime(),
                        HistoricalValue_GeographicalLocationId = c.String(),
                        HistoricalValue_Consumption = c.Single(nullable: false),
                        Time = c.DateTime(nullable: false),
                        HistoricalDescriptionId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.HistoricalDescriptions", t => t.HistoricalDescriptionId)
                .Index(t => t.HistoricalDescriptionId);
            
            CreateTable(
                "dbo.ListDescriptions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.HistoricalDescriptions", "ListDescriptionId", "dbo.ListDescriptions");
            DropForeignKey("dbo.HistoricalProperties", "HistoricalDescriptionId", "dbo.HistoricalDescriptions");
            DropIndex("dbo.HistoricalProperties", new[] { "HistoricalDescriptionId" });
            DropIndex("dbo.HistoricalDescriptions", new[] { "ListDescriptionId" });
            DropTable("dbo.ListDescriptions");
            DropTable("dbo.HistoricalProperties");
            DropTable("dbo.HistoricalDescriptions");
        }
    }
}
