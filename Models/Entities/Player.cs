
namespace Models.Entities
{
    public sealed class Player : Entity
    {
        public Player(string name, AbilityBoard abilities, Inventory inventory) : base(name, abilities, inventory)
        {

        }
    }
}
