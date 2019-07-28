namespace Exam_Result.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StudentTblmodified : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Students", "isDeleted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Students", "isDeleted");
        }
    }
}
