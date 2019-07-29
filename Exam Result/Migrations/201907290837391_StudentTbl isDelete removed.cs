namespace Exam_Result.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StudentTblisDeleteremoved : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Students", "isDeleted");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Students", "isDeleted", c => c.Boolean(nullable: false));
        }
    }
}
