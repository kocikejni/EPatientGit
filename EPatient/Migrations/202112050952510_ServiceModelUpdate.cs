namespace EPatient.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ServiceModelUpdate : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Services", "DoctorId", "dbo.Doctors");
            DropIndex("dbo.Services", new[] { "DoctorId" });
            RenameColumn(table: "dbo.Services", name: "DoctorId", newName: "Doctor_Id");
            AddColumn("dbo.Services", "DoctorName", c => c.String());
            AlterColumn("dbo.Services", "Doctor_Id", c => c.Int());
            CreateIndex("dbo.Services", "Doctor_Id");
            AddForeignKey("dbo.Services", "Doctor_Id", "dbo.Doctors", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Services", "Doctor_Id", "dbo.Doctors");
            DropIndex("dbo.Services", new[] { "Doctor_Id" });
            AlterColumn("dbo.Services", "Doctor_Id", c => c.Int(nullable: false));
            DropColumn("dbo.Services", "DoctorName");
            RenameColumn(table: "dbo.Services", name: "Doctor_Id", newName: "DoctorId");
            CreateIndex("dbo.Services", "DoctorId");
            AddForeignKey("dbo.Services", "DoctorId", "dbo.Doctors", "Id", cascadeDelete: true);
        }
    }
}
