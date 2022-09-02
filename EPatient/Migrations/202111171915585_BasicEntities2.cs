namespace EPatient.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BasicEntities2 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AspNetUsers", "RegisteredDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "RegisteredDate", c => c.DateTime(nullable: false));
        }
    }
}
