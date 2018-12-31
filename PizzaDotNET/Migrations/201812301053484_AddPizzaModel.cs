namespace PizzaDotNET.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPizzaModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Pizzas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Toppings = c.String(),
                        NormPrice = c.Int(nullable: false),
                        FamPrice = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Pizzas");
        }
    }
}
