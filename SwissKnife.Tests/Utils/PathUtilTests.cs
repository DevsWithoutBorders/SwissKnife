using NUnit.Framework;
using SwissKnife.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwissKnife.Tests.Utils
{
    [TestFixture]
    public class PathUtilTests
    {
        public class GetApplicationPath
        {
            // It's a bit of hard to test, but at least test it a little

            [Test]
            public void Validate_path()
            {                
                string expectedPath = AppDomain.CurrentDomain.BaseDirectory;
                Assert.AreEqual(expectedPath, PathUtil.GetApplicationPath());
            }

            [Test]
            public void Combine_path()
            {
                string expectedPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Test.file");
                Assert.AreEqual(expectedPath, PathUtil.GetApplicationPath("Test.file"));
            }
        }
    }
}
