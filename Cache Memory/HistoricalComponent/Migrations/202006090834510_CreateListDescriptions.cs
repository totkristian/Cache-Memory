namespace HistoricalComponent.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateListDescriptions : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ListDescriptions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.HistoricalDescriptions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Dataset = c.Int(nullable: false),
                        ListDescription_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ListDescriptions", t => t.ListDescription_Id)
                .Index(t => t.ListDescription_Id);
            
            CreateTable(
                "dbo.HistoricalProperties",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Codes = c.Int(nullable: false),
                        HistoricalValue_Timestamp = c.DateTime(nullable: false),
                        HistoricalValue_GeographicalLocationId = c.Int(nullable: false),
                        HistoricalValue_Consumption = c.Double(nullable: false),
                        Time = c.DateTime(nullable: false),
                        HistoricalDescription_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.HistoricalDescriptions", t => t.HistoricalDescription_Id)
                .Index(t => t.HistoricalDescription_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.HistoricalDescriptions", "ListDescription_Id", "dbo.ListDescriptions");
            DropForeignKey("dbo.HistoricalProperties", "HistoricalDescription_Id", "dbo.HistoricalDescriptions");
            DropIndex("dbo.HistoricalProperties", new[] { "HistoricalDescription_Id" });
            DropIndex("dbo.HistoricalDescriptions", new[] { "ListDescription_Id" });
            DropTable("dbo.HistoricalProperties");
            DropTable("dbo.HistoricalDescriptions");
            DropTable("dbo.ListDescriptions");
        }
    }
}
