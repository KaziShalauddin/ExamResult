namespace Exam_Result.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class studentTblmodifiedagain : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Students", "StudentId", c => c.String(maxLength: 10));
            CreateIndex("dbo.Students", "StudentId", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.Students", new[] { "StudentId" });
            AlterColumn("dbo.Students", "StudentId", c => c.String());
        }
    }
}
