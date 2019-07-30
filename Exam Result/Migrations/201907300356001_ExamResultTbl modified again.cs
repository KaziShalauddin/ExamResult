namespace Exam_Result.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ExamResultTblmodifiedagain : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ExamResults", "StudentId", c => c.Int(nullable: false));
            AddColumn("dbo.ExamResults", "SubjectId", c => c.Int(nullable: false));
            CreateIndex("dbo.ExamResults", "StudentId");
            CreateIndex("dbo.ExamResults", "SubjectId");
            AddForeignKey("dbo.ExamResults", "StudentId", "dbo.Students", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ExamResults", "SubjectId", "dbo.Subjects", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ExamResults", "SubjectId", "dbo.Subjects");
            DropForeignKey("dbo.ExamResults", "StudentId", "dbo.Students");
            DropIndex("dbo.ExamResults", new[] { "SubjectId" });
            DropIndex("dbo.ExamResults", new[] { "StudentId" });
            DropColumn("dbo.ExamResults", "SubjectId");
            DropColumn("dbo.ExamResults", "StudentId");
        }
    }
}
