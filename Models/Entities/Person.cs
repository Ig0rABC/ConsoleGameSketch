
namespace Models.Entities
{
    public sealed class Person : Entity
    {
        public Person(string name, AbilityBoard abilities, Resistances resistances, Inventory inventory) : base(name, abilities, resistances, inventory)
        {

        }
    }
}
