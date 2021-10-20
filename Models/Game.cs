using System.Collections.Generic;
using Models.Entities;

namespace Models
{
    public static class Game
    {
        public static readonly List<Entity> Guided = new() { };

        public static bool IsGuided(Entity entity)
        {
            return Guided.Contains(entity);
        }
    }
}
