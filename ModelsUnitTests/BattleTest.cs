using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using Models.Entities;
using Models.Battle;

namespace ModelsUnitTests
{
    [TestClass]
    public class BattleTest
    {
        [TestMethod]
        public void FirstMoverFromAllies()
        {
            var allies = new[] { new Person("Test Player", AbilityBoard.Empty, Inventory.Empty) };
            var enemies = new[] { new Ogre(Inventory.Empty), new Ogre(Inventory.Empty) };
            var battle = new Battle(new Party(allies), new Party(enemies));

            battle.Next();

            Assert.IsTrue(allies.Contains(battle.Attacker));
        }

        [TestMethod]
        public void EnemiesStartMovingAfterAllAlliesHaveBeenMoved()
        {
            Entity[] allies = new[] { new Person("Test Player", AbilityBoard.Empty, Inventory.Empty) };
            Entity[] enemies = new [] { new Ogre(Inventory.Empty), new Ogre(Inventory.Empty) };
            var battle = new Battle(new Party(allies), new Party(enemies));

            for (byte i = 0; i < allies.Length; i++)
                battle.Next();
            battle.Next();

            Assert.IsTrue(enemies.Contains(battle.Attacker));
        }

        [TestMethod]
        public void EnemiesAreEnemies()
        {
            Entity[] allies = new[] { new Person("Test Player", AbilityBoard.Empty, Inventory.Empty) };
            Entity[] enemies = new[] { new Ogre(Inventory.Empty), new Ogre(Inventory.Empty) };
            var battle = new Battle(new Party(allies), new Party(enemies));

            battle.Next();

            for (byte i = 0; i < 2; i++)
                Assert.AreEqual(enemies[i], battle.Enemies.ElementAt(i));
        }
    }
}
