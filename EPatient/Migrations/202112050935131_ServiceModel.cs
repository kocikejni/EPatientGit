namespace EPatient.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ServiceModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Services",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        DoctorId = c.Int(nullable: false),
                        Price = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Doctors", t => t.DoctorId, cascadeDelete: true)
                .Index(t => t.DoctorId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Services", "DoctorId", "dbo.Doctors");
            DropIndex("dbo.Services", new[] { "DoctorId" });
            DropTable("dbo.Services");
        }
    }
}
