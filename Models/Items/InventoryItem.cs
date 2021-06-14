
namespace Models
{
    public abstract class InventoryItem
    {
        public string Name { get; }

        public InventoryItem(string name)
        {
            Name = name;
        }
    }
}
