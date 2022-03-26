using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using Models.Entities;
using Models.Resistances;
using Models.Battle;

namespace ModelsUnitTests
{
    [TestClass]
    public class BattleTest
    {
        [TestMethod]
        public void FirstMoverFromAllies()
        {
            var allies = new[] { new Person("Test Player", AbilityBoard.Empty, EntityResistanceBoard.Empty, Inventory.Empty) };
            var enemies = new[] { new Ogre(Inventory.Empty), new Ogre(Inventory.Empty) };
            var battle = new Battle(new Party(allies), new Party(enemies));

            battle.MoveNext();

            Assert.IsTrue(allies.Contains(battle.MovingParty.Current));
        }

        [TestMethod]
        public void EnemiesStartMovingAfterAllAlliesHaveBeenMoved()
        {
            Entity[] allies = new[] { new Person("Test Player", AbilityBoard.Empty, EntityResistanceBoard.Empty, Inventory.Empty) };
            Entity[] enemies = new [] { new Ogre(Inventory.Empty), new Ogre(Inventory.Empty) };
            var battle = new Battle(new Party(allies), new Party(enemies));

            for (byte i = 0; i < allies.Length; i++)
                battle.MoveNext();
            battle.MoveNext();

            Assert.IsTrue(enemies.Contains(battle.MovingParty.Current));
        }

        [TestMethod]
        public void EnemiesAreEnemies()
        {
            Entity[] allies = new[] { new Person("Test Player", AbilityBoard.Empty, EntityResistanceBoard.Empty, Inventory.Empty) };
            Entity[] enemies = new[] { new Ogre(Inventory.Empty), new Ogre(Inventory.Empty) };
            var battle = new Battle(new Party(allies), new Party(enemies));

            battle.MoveNext();

            for (byte i = 0; i < 2; i++)
                Assert.AreEqual(enemies[i], battle.TargetParty.AliveMembers.ElementAt(i));
        }
    }
}
