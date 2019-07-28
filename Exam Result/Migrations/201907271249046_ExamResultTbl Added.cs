namespace Exam_Result.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ExamResultTblAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ExamResults",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StudentSubjectId = c.Int(nullable: false),
                        Status = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.StudentSubjects", t => t.StudentSubjectId, cascadeDelete: true)
                .Index(t => t.StudentSubjectId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ExamResults", "StudentSubjectId", "dbo.StudentSubjects");
            DropIndex("dbo.ExamResults", new[] { "StudentSubjectId" });
            DropTable("dbo.ExamResults");
        }
    }
}
