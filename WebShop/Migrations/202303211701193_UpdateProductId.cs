namespace WebShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateProductId : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.tb_OrderDetail", "Product_Id", "dbo.tb_Product");
            DropIndex("dbo.tb_OrderDetail", new[] { "Product_Id" });
            DropColumn("dbo.tb_OrderDetail", "ProductId");
            RenameColumn(table: "dbo.tb_OrderDetail", name: "Product_Id", newName: "ProductId");
            AlterColumn("dbo.tb_OrderDetail", "ProductId", c => c.Int(nullable: false));
            AlterColumn("dbo.tb_OrderDetail", "ProductId", c => c.Int(nullable: false));
            CreateIndex("dbo.tb_OrderDetail", "ProductId");
            AddForeignKey("dbo.tb_OrderDetail", "ProductId", "dbo.tb_Product", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tb_OrderDetail", "ProductId", "dbo.tb_Product");
            DropIndex("dbo.tb_OrderDetail", new[] { "ProductId" });
            AlterColumn("dbo.tb_OrderDetail", "ProductId", c => c.Int());
            AlterColumn("dbo.tb_OrderDetail", "ProductId", c => c.String());
            RenameColumn(table: "dbo.tb_OrderDetail", name: "ProductId", newName: "Product_Id");
            AddColumn("dbo.tb_OrderDetail", "ProductId", c => c.String());
            CreateIndex("dbo.tb_OrderDetail", "Product_Id");
            AddForeignKey("dbo.tb_OrderDetail", "Product_Id", "dbo.tb_Product", "Id");
        }
    }
}
