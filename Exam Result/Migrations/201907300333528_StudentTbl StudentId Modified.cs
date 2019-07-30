namespace Exam_Result.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StudentTblStudentIdModified : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Students", new[] { "StudentId" });
            AddColumn("dbo.Students", "Student_Id", c => c.String(maxLength: 10));
            CreateIndex("dbo.Students", "Student_Id", unique: true);
            DropColumn("dbo.Students", "StudentId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Students", "StudentId", c => c.String(maxLength: 10));
            DropIndex("dbo.Students", new[] { "Student_Id" });
            DropColumn("dbo.Students", "Student_Id");
            CreateIndex("dbo.Students", "StudentId", unique: true);
        }
    }
}
