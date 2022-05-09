using Models.Entities;
using Models.Effects;

namespace Models.Items.Usable
{
    public abstract class HealingItem : UsableItem
    {
        public byte Duration { get; }
        public float Recovery { get; }

        public HealingItem(string name, byte duration, float recovery) : base(name)
        {
            Duration = duration;
            Recovery = recovery;
        }

        public override void Use(Entity user)
        {
            var effect = new Healing(Duration, Recovery);
            user.Effector.Add(effect);
        }
    }
}
