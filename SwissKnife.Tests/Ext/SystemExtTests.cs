using System;
using NUnit.Framework;
using SwissKnife.Ext;

namespace SwissKnife.Tests.Ext
{
    [TestFixture]
    public class SystemExtTests
    {
        [Test]
        public void ThrowIfNull_Null()
        {
            Assert.Throws<ArgumentNullException>(() => SystemExt.ThrowIfNull(null));
        }

        [Test]
        public void ThrowIfNull_NotNull()
        {
            var notNullObject = new object();
            Assert.DoesNotThrow(() => notNullObject.ThrowIfNull());
        }
    }
}
