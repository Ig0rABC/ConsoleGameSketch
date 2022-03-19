using Models.Resistances;

namespace Models.Entities
{
    public sealed class Person : Entity
    {
        public Person(string name, AbilityBoard abilities, EntityResistanceBoard resistances, Inventory inventory) : base(name, abilities, resistances, inventory)
        {

        }
    }
}
