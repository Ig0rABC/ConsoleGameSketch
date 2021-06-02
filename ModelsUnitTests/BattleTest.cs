using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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
            var allies = new[] { new Player("Test Player", 30) };
            var enemies = new[] { new Ogre(), new Ogre() };
            var battle = new Battle(allies, enemies);

            battle.Next();

            Assert.IsTrue(allies.Contains(battle.Attacker));
        }

        [TestMethod]
        public void EnemiesStartMovingAfterAllAlliesHaveBeenMoved()
        {
            Entity[] allies = new[] { new Player("Test Player", 30) };
            Entity[] enemies = new [] { new Ogre(), new Ogre() };
            var battle = new Battle(allies, enemies);

            for (byte i = 0; i < allies.Length; i++)
                battle.Next();
            battle.Next();

            Assert.IsTrue(enemies.Contains(battle.Attacker));
        }

        [TestMethod]
        public void EnemiesAreEnemies()
        {
            Entity[] allies = new[] { new Player("Test Player", 30) };
            Entity[] enemies = new[] { new Ogre(), new Ogre() };
            var battle = new Battle(allies, enemies);

            battle.Next();

            for (byte i = 0; i < 2; i++)
                Assert.AreEqual(enemies[i], battle.Enemies[i]);
        }
    }
}
