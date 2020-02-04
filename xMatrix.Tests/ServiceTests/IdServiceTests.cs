using AutoFixture;
using AutoFixture.AutoMoq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xMatrix.Core.Interfaces;
using xMatrix.Core.Models;
using xMatrix.Core.Services;

namespace xMatrix.Tests.ServiceTests
{
    public class IdServiceTests
    {
        private IFixture _fixture;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization());
        }

        [Test]
        public void GetFreeIdTests_ShouldReturnOne()
        {
            var goals = new List<Goal>();
            var sut = _fixture.Create<IdService>();
            var result = sut.GetFreeId(goals);
            Assert.AreEqual(1, result);
        }

        [Test]
        public void GetFreeIdTests_ShouldReturnFour()
        {
            var goals = new List<Goal>();
            goals.Add(new Goal() { Id = 1 });
            goals.Add(new Goal() { Id = 2 });
            goals.Add(new Goal() { Id = 3 });
            var sut = _fixture.Create<IdService>();
            var result = sut.GetFreeId(goals);
            Assert.AreEqual(4, result);
        }
    }
}
