using Models.Resistances;

namespace Models.Outfits
{
    public class LeatherJacket : Outfit
    {
        public LeatherJacket() : base("Leather jacket", new OutfitResistanceBoard(0.33f, 0.2f, 0.1f))
        {

        }
    }
}
