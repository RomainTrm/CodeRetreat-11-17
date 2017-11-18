using NUnit.Framework;
using NFluent;
using System;
using Moq;
using System.Collections.Generic;

namespace CodeRetreat
{
    [TestFixture]
    public class GolShould
    {
        [Test]
        public void BeDeadWhenHaveLessThanTwoLivingNeighbours()
        {
            Check.That(IsAlive(new LivingCell(), new UnderPopulated())).IsFalse();
        }

        [Test]
        public void BeDeadWhenHaveMoreThanThreeLivingNeighbours()
        {
            Check.That(IsAlive(new LivingCell(), new OverPopulated())).IsFalse();
        }

        [Test]
        public void BecomeAliveWhenHaveThreeLivingNeighbours()
        {
            Check.That(IsAlive(new DeadCell(), new GenetivePopulation())).IsTrue();
        }

        [Test]
        public void StayDeadWhenHaveNotThreeLivingNeighbours()
        {
            Check.That(IsAlive(new DeadCell(), new UnderPopulated())).IsFalse();
            Check.That(IsAlive(new DeadCell(), new StablePopulation())).IsFalse();
            Check.That(IsAlive(new DeadCell(), new OverPopulated())).IsFalse();
        }

        [Test]
        public void StayAliveWhenHaveTwoOrThreeLivingNeighbours()
        {
            Check.That(IsAlive(new LivingCell(), new StablePopulation())).IsTrue();
            Check.That(IsAlive(new LivingCell(), new GenetivePopulation())).IsTrue();
        }

        public interface Cell
        {
        }

        public class LivingCell : Cell
        {
        }

        public class DeadCell : Cell
        {
        }

        public abstract class Population
        {
            private readonly Dictionary<Type, Func<object, bool>> _mapper;

            public Population()
            {
                _mapper = new Dictionary<Type, System.Func<object, bool>>
                {
                    { typeof(LivingCell), cell => this.IsAlive((LivingCell)cell) },
                    { typeof(DeadCell), cell => this.IsAlive((DeadCell)cell) }
                };
            }

            public bool IsAlive(Cell cell) => _mapper[cell.GetType()](cell);

            public abstract bool IsAlive(LivingCell cell);
            public abstract bool IsAlive(DeadCell cell);
        }

        public class UnderPopulated : Population
        {
            public override bool IsAlive(LivingCell cell) => false;

            public override bool IsAlive(DeadCell cell) => false;
        }

        public class OverPopulated : Population
        {
            public override bool IsAlive(LivingCell cell) => false;

            public override bool IsAlive(DeadCell cell) => false;
        }

        public class StablePopulation : Population
        {
            public override bool IsAlive(LivingCell cell) => true;

            public override bool IsAlive(DeadCell cell) => false;
        }

        public class GenetivePopulation : Population
        {
            public override bool IsAlive(LivingCell cell) => true;

            public override bool IsAlive(DeadCell cell) => true;
        }

        private bool IsAlive(Cell cell, Population neighbours)
        {
            return neighbours.IsAlive(cell);
        }
    }
}