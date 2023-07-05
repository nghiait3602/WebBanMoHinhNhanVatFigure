namespace WebShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class db1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tb_Order", "userdangnhap", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.tb_Order", "userdangnhap");
        }
    }
}
