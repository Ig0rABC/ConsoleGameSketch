using Models.Entities;

namespace Models.Items
{
    public abstract class UsableItem : InventoryItem
    {
        public UsableItem(string name) : base(name)
        {
        }

        public abstract void Use(Entity user);
    }
}
