using kolveniershofBackend.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace KolveniershofBackendTests.UnitTests.DomainTests
{
    public class DagPlanningDomainTest
    {
        [Theory]
        [InlineData(2019, 10, 1)]
        [InlineData(2017, 10, 1)]
        [InlineData(2016, 10, 1)]
        [InlineData(2019, 10, 23)]
        [InlineData(1823, 10, 1)]
        public void TestOpInvalidDatum(int year, int month, int day)
        {
            DateTime date = new DateTime(year, month, day);
            Assert.Throws<ArgumentException>(() =>
            {
                DagPlanning planning = new DagPlanning(1, date, "eten");
            });
        }

        [Theory]
        [InlineData(2019, 11, 1)]
        [InlineData(2020, 10, 1)]
        [InlineData(2021, 10, 1)]
        [InlineData(3803, 10, 23)]
        [InlineData(4000, 10, 1)]
        public void TestOpValidDatum(int year, int month, int day)
        {
            DateTime date = new DateTime(year, month, day);
            Assert.Equal(new DateTime(year, month, day), date.Date);
        }

        [Theory]
        [InlineData("eten")]
        [InlineData("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")]
        public void TestOpMeegevenEten(string eten)
        {
            DagPlanning planning = new DagPlanning(1, DateTime.Today, eten);
            Assert.Equal(eten, planning.Eten);
        }
    }
}
