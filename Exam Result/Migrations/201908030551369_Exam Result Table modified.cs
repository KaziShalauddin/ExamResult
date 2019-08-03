namespace Exam_Result.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ExamResultTablemodified : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ExamResults", "StudentSubjectId", c => c.Int(nullable: false));
            CreateIndex("dbo.ExamResults", "StudentSubjectId");
            AddForeignKey("dbo.ExamResults", "StudentSubjectId", "dbo.StudentSubjects", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ExamResults", "StudentSubjectId", "dbo.StudentSubjects");
            DropIndex("dbo.ExamResults", new[] { "StudentSubjectId" });
            DropColumn("dbo.ExamResults", "StudentSubjectId");
        }
    }
}
