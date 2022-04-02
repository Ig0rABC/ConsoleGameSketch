﻿using Models.Resistances;

namespace Models.Outfits
{
    public abstract class Outfit : InventoryItem
    {
        public readonly StateBar Condition;
        public readonly OutfitResistanceBoard Resistances;

        public Outfit(string name, OutfitResistanceBoard resistances) : base(name)
        {
            Resistances = resistances;
            Condition = new StateBar();
            Resistances.SetOutfitCondition(Condition);
        }
    }
}
