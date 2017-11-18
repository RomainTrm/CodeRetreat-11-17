using NUnit.Framework;
using NFluent;

namespace CodeRetreat
{
    [TestFixture]
    public class GolShould
    {
        [Test]
        [TestCase(0)]
        [TestCase(1)]
        public void DieFromUnderPopulation(int nbNeighbours)
        {
            const bool livingCell = true;
            Check.That(IsDead(livingCell, nbNeighbours)).IsTrue();
        }

        [Test]
        [TestCase(2)]
        [TestCase(3)]
        public void StayAliveWhenPopulationIsStable(int nbNeighbours)
        {
            const bool livingCell = true;
            Check.That(IsDead(livingCell, nbNeighbours)).IsFalse();
        }

        [Test]
        [TestCase(4)]
        [TestCase(5)]
        [TestCase(6)]
        [TestCase(7)]
        [TestCase(8)]
        public void DieFromOverPopulation(int nbNeighbours)
        {
            const bool livingCell = true;
            Check.That(IsDead(livingCell, nbNeighbours)).IsTrue();
        }

        [Test]
        public void StayDeadWhenHaveTwoNeighbours()
        {
            const bool deadCell = false;
            const int nbNeighbours = 2;
            Check.That(IsDead(deadCell, nbNeighbours)).IsTrue();
        }

        [Test]
        public void ReviveWithPerfectPopulation()
        {
            const int perfectPopulation = 3;
            const bool deadCell = false;
            Check.That(IsDead(deadCell, perfectPopulation)).IsFalse();
        }

        [Test]
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(4)]
        [TestCase(5)]
        [TestCase(6)]
        [TestCase(7)]
        [TestCase(8)]
        public void StayDeadWhenDontHavePerfectPopulation(int nbNeighbours)
        {
            const bool deadCell = false;
            Check.That(IsDead(deadCell, nbNeighbours)).IsTrue();
        }
        
        private bool IsDead(bool isAlive, int nbNeighbours)
        {
            if (IsGenerativePopulation(nbNeighbours)) return false;
            if (IsUnstablePopulation(nbNeighbours)) return true;
            return !isAlive;
        }

        private bool IsUnstablePopulation(int nbNeighbours) 
            => IsUnderPopulation(nbNeighbours) || IsOverPopulation(nbNeighbours);

        private bool IsOverPopulation(int nbNeighbours) => nbNeighbours > 3;

        private bool IsUnderPopulation(int nbNeighbours) => nbNeighbours < 2;

        private bool IsGenerativePopulation(int nbNeighbours) => nbNeighbours == 3;
    }
}