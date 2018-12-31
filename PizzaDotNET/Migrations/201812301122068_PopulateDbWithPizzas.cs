namespace PizzaDotNET.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class PopulateDbWithPizzas : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Pizzas (Name, Toppings, NormPrice, FamPrice) VALUES('Vegan', 'No meat', 100, 200)");
            Sql("INSERT INTO Pizzas (Name, Toppings, NormPrice, FamPrice) VALUES('Meat lover', 'Meat', 50, 80)");
            Sql("INSERT INTO Pizzas (Name, Toppings, NormPrice, FamPrice) VALUES('Hawaii', 'Pinapple, skinke og ost', 99, 199)");
        }
        
        public override void Down()
        {
            Sql("DELETE From Pizzas WHERE Id IN (2,3,4");
        }
    }
}
