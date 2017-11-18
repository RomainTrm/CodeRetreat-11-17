using System;
using NUnit.Framework;

namespace CodeRetreat
{
    [TestFixture]
    public class GolShould
    {
        [Test]
        [TestCase(0)]
        [TestCase(1)]
        public void DieWhenHaveLessThanTwoNeighbours(int livingNeighbours)
        {
            var nbLivingNeighbours = new NbLivingNeighbours(livingNeighbours);
            var nextState = Cell.GetNextGeneration(CellState.Living, nbLivingNeighbours);
            Assert.AreEqual(nextState, CellState.Dead);
        }

        [Test]
        [TestCase(2)]
        [TestCase(3)]
        public void StayAliveWhenHave2Or3Neighbours(int livingNeighbours)
        {
            var nbLivingNeighbours = new NbLivingNeighbours(livingNeighbours);
            var nextState = Cell.GetNextGeneration(CellState.Living, nbLivingNeighbours);
            Assert.AreEqual(CellState.Living, nextState);
        }

        [Test]
        [TestCase(4)]
        [TestCase(5)]
        [TestCase(6)]
        [TestCase(7)]
        [TestCase(8)]
        public void DieWhenHaveMoreThanThreeNeighbours(int livingNeighbours)
        {
            var nbLivingNeighbours = new NbLivingNeighbours(livingNeighbours);
            var nextState = Cell.GetNextGeneration(CellState.Living, nbLivingNeighbours);
            Assert.AreEqual(CellState.Dead, nextState);
        }

        [Test]
        [TestCase(2)]
        public void StayDeadWhenHave2Neighbours(int livingNeighbours)
        {
            var nbLivingNeighbours = new NbLivingNeighbours(livingNeighbours);
            var nextState = Cell.GetNextGeneration(CellState.Dead, nbLivingNeighbours);
            Assert.AreEqual(CellState.Dead, nextState);
        }

        [Test]
        [TestCase(3)]
        public void BecomeAliveWhenHaveThreeLivingNeighbours(int livingNeighbours)
        {
            var nbLivingNeighbours = new NbLivingNeighbours(livingNeighbours);
            var nextState = Cell.GetNextGeneration(CellState.Dead, nbLivingNeighbours);
            Assert.AreEqual(CellState.Living, nextState);
        }
    }

    internal enum CellState
    {
        Living,
        Dead
    }

    internal class NbLivingNeighbours
    {
        private readonly int _value;

        public NbLivingNeighbours(int value)
        {
            _value = value;
        }

        private bool IsUnderPopulated => _value < 2;

        private bool IsOverPopulated => _value > 3;

        private bool IsUnstablePopulation => IsOverPopulated || IsUnderPopulated;

        private bool IsGenerativePopulation => _value == 3;

        public IPopulation GetPopulation()
        {
            if (IsUnstablePopulation) return new UnstablePopulation();
            if (IsGenerativePopulation) return new GenerativePopulation();
            return new StablePopulation();
        }
    }

    internal class GenerativePopulation : IPopulation
    {
        public CellState NextState(CellState state) => CellState.Living;
    }

    internal class UnstablePopulation : IPopulation
    {
        public CellState NextState(CellState state) => CellState.Dead;
    }

    internal class StablePopulation : IPopulation
    {
        public CellState NextState(CellState state) => state;
    }

    internal interface IPopulation
    {
        CellState NextState(CellState state);
    }

    internal class Cell
    {
        internal static CellState GetNextGeneration(CellState state, NbLivingNeighbours nbOfLivingNeighbours)
        {
            var population = nbOfLivingNeighbours.GetPopulation();
            return population.NextState(state);
        }
    }
}
