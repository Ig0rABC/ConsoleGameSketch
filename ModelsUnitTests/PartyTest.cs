using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models.Entities;
using Models.Battle;

namespace ModelsUnitTests
{
    [TestClass]
    public class PartyTest
    {
        [TestMethod]
        public void AfterOneMovePartyWithOneMemberIncrementingIterationsCount()
        {
            var player = new Player("Test Player", 30);
            var party = new Party(new Entity[] { player });

            party.Next();

            Assert.AreEqual(1, party.IterationsCount);
        }
    }
}
