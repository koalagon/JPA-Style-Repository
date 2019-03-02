using System;
using NPA.Repository;
using NUnit.Framework;

namespace NPA.UnitTest
{
    [TestFixture]
    public class UnitOfWorkFixture
    {
        [Test]
        public void Constructor_ShouldThrowException_WhenNameOrConnectionStringIsNull()
        {
            Assert.Throws<InvalidOperationException>(() => new UnitOfWork());
        }
    }
}
