using Models.Resistances;

namespace Models.Outfits
{
    public class RagOutfit : Outfit
    {
        public RagOutfit() : base("Rag", new OutfitResistanceBoard(0.15f, 0.1f, 0.05f))
        {

        }
    }
}
