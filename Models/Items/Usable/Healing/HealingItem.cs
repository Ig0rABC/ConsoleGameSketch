using Models.Entities;

namespace Models.Items.Usable
{
    public abstract class HealingItem : UsableItem
    {

        public float Recovery { get; }

        public HealingItem(string name, float recovery) : base(name)
        {
            Recovery = recovery;
        }

        public override void Use(Entity user)
        {
            user.Heal(Recovery);
        }
    }
}
