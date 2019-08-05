namespace Exam_Result.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ExamResultTablemodifiedagain : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.ExamResults", "Status");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ExamResults", "Status", c => c.String());
        }
    }
}
