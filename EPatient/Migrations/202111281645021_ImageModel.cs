namespace EPatient.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ImageModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Images",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        ImagePath = c.String(),
                        PatientId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Patients", t => t.PatientId, cascadeDelete: true)
                .Index(t => t.PatientId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Images", "PatientId", "dbo.Patients");
            DropIndex("dbo.Images", new[] { "PatientId" });
            DropTable("dbo.Images");
        }
    }
}
