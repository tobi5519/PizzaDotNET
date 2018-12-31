namespace PizzaDotNET.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class overrideConventionsInEF : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Pizzas", "Name", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Pizzas", "Toppings", c => c.String(maxLength: 255));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Pizzas", "Toppings", c => c.String());
            AlterColumn("dbo.Pizzas", "Name", c => c.String());
        }
    }
}
