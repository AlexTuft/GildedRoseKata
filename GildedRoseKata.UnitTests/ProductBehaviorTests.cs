using FluentAssertions;
using GildedRoseKata.AgeingBehavior;
using GildedRoseKata.QualityBehaviors;
using Moq;
using System;
using Xunit;

namespace GildedRoseKata.UnitTests
{
    public class ProductBehaviorTests
    {
        
        [Fact]
        void ThrowsExceptionIfAgingBehavoirIsNull()
        {
            Action act = () => new ProductBehavior(null, new NullQualityBehavior());

            act.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        void ThrowsExceptionIfQualityBehavoirIsNull()
        {
            Action act = () => new ProductBehavior(new NullAgeingBehavior(), null);

            act.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        void ElapsingADayCallsQualityBehaviour()
        {
            var mockAgingBehavior = new Mock<IAgeingBehavior>();
            var productBehavior = new ProductBehavior(mockAgingBehavior.Object, new NullQualityBehavior());

            productBehavior.ElapseDay(null);

            mockAgingBehavior.Verify(m => m.ChangeAge(It.IsAny<Product>()), Times.Once);
        }

        [Fact]
        void ElapsingADayCallsAgingBehavior()
        {
            var mockQualityBehavior = new Mock<IQualityBehavior>();
            var productBehavior = new ProductBehavior(new NullAgeingBehavior(), mockQualityBehavior.Object);

            productBehavior.ElapseDay(null);

            mockQualityBehavior.Verify(m => m.ChangeQuality(It.IsAny<Product>()), Times.Once);
        }

        [Fact]
        void ElapsingADayCallsQualityBehvaiorBeforeAgingBehavior()
        {
            int callOrder = 0;

            var mockAgingBehavior = new Mock<IAgeingBehavior>();
            mockAgingBehavior.Setup(m => m.ChangeAge(It.IsAny<Product>())).Callback(() => Assert.Equal(1, callOrder++));

            var mockQualityBehavior = new Mock<IQualityBehavior>();
            mockQualityBehavior.Setup(m => m.ChangeQuality(It.IsAny<Product>())).Callback(() => Assert.Equal(0, callOrder++));

            var productBehavior = new ProductBehavior(mockAgingBehavior.Object, mockQualityBehavior.Object);

            productBehavior.ElapseDay(null);
        }
    }
}
