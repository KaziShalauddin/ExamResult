namespace Exam_Result.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StudentSubjectTblmodified : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.StudentSubjects", "Student_Id", "dbo.Students");
            DropForeignKey("dbo.StudentSubjects", "Subject_Id", "dbo.Subjects");
            DropIndex("dbo.StudentSubjects", new[] { "Student_Id" });
            DropIndex("dbo.StudentSubjects", new[] { "Subject_Id" });
            RenameColumn(table: "dbo.StudentSubjects", name: "Student_Id", newName: "StudentId");
            RenameColumn(table: "dbo.StudentSubjects", name: "Subject_Id", newName: "SubjectId");
            AlterColumn("dbo.StudentSubjects", "StudentId", c => c.Int(nullable: false));
            AlterColumn("dbo.StudentSubjects", "SubjectId", c => c.Int(nullable: false));
            CreateIndex("dbo.StudentSubjects", "StudentId");
            CreateIndex("dbo.StudentSubjects", "SubjectId");
            AddForeignKey("dbo.StudentSubjects", "StudentId", "dbo.Students", "Id", cascadeDelete: false);
            AddForeignKey("dbo.StudentSubjects", "SubjectId", "dbo.Subjects", "Id", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StudentSubjects", "SubjectId", "dbo.Subjects");
            DropForeignKey("dbo.StudentSubjects", "StudentId", "dbo.Students");
            DropIndex("dbo.StudentSubjects", new[] { "SubjectId" });
            DropIndex("dbo.StudentSubjects", new[] { "StudentId" });
            AlterColumn("dbo.StudentSubjects", "SubjectId", c => c.Int());
            AlterColumn("dbo.StudentSubjects", "StudentId", c => c.Int());
            RenameColumn(table: "dbo.StudentSubjects", name: "SubjectId", newName: "Subject_Id");
            RenameColumn(table: "dbo.StudentSubjects", name: "StudentId", newName: "Student_Id");
            CreateIndex("dbo.StudentSubjects", "Subject_Id");
            CreateIndex("dbo.StudentSubjects", "Student_Id");
            AddForeignKey("dbo.StudentSubjects", "Subject_Id", "dbo.Subjects", "Id");
            AddForeignKey("dbo.StudentSubjects", "Student_Id", "dbo.Students", "Id");
        }
    }
}
