
namespace Models.Entities
{
    public sealed class Person : Entity
    {
        public Person(string name, AbilityBoard abilities, Inventory inventory) : base(name, abilities, inventory)
        {

        }
    }
}
