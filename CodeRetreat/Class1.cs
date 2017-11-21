using NUnit.Framework;
using System;
using Moq;

namespace CodeRetreat
{
    public interface INextGeneration
    {
        void SetNextGeneration(CellState cellState);
    }

    [TestFixture]
    public class CellShould
    {
        private const int StablePopulation = 2;
        private const int NativePopulation = 3;
        private Mock<INextGeneration> _mock;

        [SetUp]
        public void Setup() => _mock = new Mock<INextGeneration>();

        [Test]
        [TestCase(0)]
        [TestCase(1)]
        public void BeDeadWhenUnderPopulated(int nbNeighbours)
        {
            NextGeneration(It.IsAny<CellState>(), nbNeighbours, _mock.Object.SetNextGeneration);
            _mock.Verify(x => x.SetNextGeneration(CellState.Dead));
        }

        [Test]
        [TestCase(2)]
        [TestCase(3)]
        public void StayAlive_WhenAliveAndOnSustainablePopulation(int nbNeighbours)
        {
            NextGeneration(CellState.Living, nbNeighbours, _mock.Object.SetNextGeneration);
            _mock.Verify(x => x.SetNextGeneration(CellState.Living));
        }

        [Test]
        public void ShouldBecomeAliveWhen3LivingNeighbours()
        {
            NextGeneration(CellState.Dead, 3, _mock.Object.SetNextGeneration);
            _mock.Verify(x => x.SetNextGeneration(CellState.Living));
        }

        [Test]
        public void ShouldStayDeadWhen2LivingNeighbours()
        {
            NextGeneration(CellState.Dead, 2, _mock.Object.SetNextGeneration);
            _mock.Verify(x => x.SetNextGeneration(CellState.Dead));
        }

        [Test]
        [TestCase(4)]
        [TestCase(5)]
        [TestCase(6)]
        [TestCase(7)]
        [TestCase(8)]
        public void BeDeadWhenOverPopulated(int nbNeighbours)
        {
            NextGeneration(It.IsAny<CellState>(), nbNeighbours, _mock.Object.SetNextGeneration);
            _mock.Verify(x => x.SetNextGeneration(CellState.Dead));
        }

        public static void NextGeneration(CellState state, int nbNeighbours, Action<CellState> nextStateCallback)
        {
            switch (nbNeighbours)
            {
                case StablePopulation:
                    nextStateCallback(state);
                    break;
                case NativePopulation:
                    nextStateCallback(CellState.Living);
                    break;
                default:
                    nextStateCallback(CellState.Dead);
                    break;
            }
        }
    }

    public enum CellState
    {
        Living, Dead
    }
}