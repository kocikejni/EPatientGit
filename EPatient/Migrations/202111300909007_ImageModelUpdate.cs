namespace EPatient.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ImageModelUpdate : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Images", "PatientId", "dbo.Patients");
            DropIndex("dbo.Images", new[] { "PatientId" });
            RenameColumn(table: "dbo.Images", name: "PatientId", newName: "Patient_Id");
            AddColumn("dbo.Images", "PatientFullName", c => c.String());
            AlterColumn("dbo.Images", "Patient_Id", c => c.Int());
            CreateIndex("dbo.Images", "Patient_Id");
            AddForeignKey("dbo.Images", "Patient_Id", "dbo.Patients", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Images", "Patient_Id", "dbo.Patients");
            DropIndex("dbo.Images", new[] { "Patient_Id" });
            AlterColumn("dbo.Images", "Patient_Id", c => c.Int(nullable: false));
            DropColumn("dbo.Images", "PatientFullName");
            RenameColumn(table: "dbo.Images", name: "Patient_Id", newName: "PatientId");
            CreateIndex("dbo.Images", "PatientId");
            AddForeignKey("dbo.Images", "PatientId", "dbo.Patients", "Id", cascadeDelete: true);
        }
    }
}
