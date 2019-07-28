namespace Exam_Result.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TableStudentSubjectAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.StudentSubjects",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Student_Id = c.Int(),
                        Subject_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Students", t => t.Student_Id)
                .ForeignKey("dbo.Subjects", t => t.Subject_Id)
                .Index(t => t.Student_Id)
                .Index(t => t.Subject_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StudentSubjects", "Subject_Id", "dbo.Subjects");
            DropForeignKey("dbo.StudentSubjects", "Student_Id", "dbo.Students");
            DropIndex("dbo.StudentSubjects", new[] { "Subject_Id" });
            DropIndex("dbo.StudentSubjects", new[] { "Student_Id" });
            DropTable("dbo.StudentSubjects");
        }
    }
}
