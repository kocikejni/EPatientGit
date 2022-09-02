namespace EPatient.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PatientAllergies : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Patients", "Allergies", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Patients", "Allergies");
        }
    }
}
