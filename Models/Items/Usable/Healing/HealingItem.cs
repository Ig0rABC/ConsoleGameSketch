using Models.Entities;

namespace Models.Items.Usable
{
    public abstract class HealingItem : UsableItem
    {

        public byte Recovery { get; }

        public HealingItem(string name, byte recovery) : base(name)
        {
            Recovery = recovery;
        }

        public override void Use(Entity user)
        {
            user.Heal(Recovery);
        }
    }
}
