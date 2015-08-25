using NUnit.Framework;
using SwissKnife.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwissKnife.Tests.Utils
{
    [TestFixture]
    public class HashUtilTests
    {
        protected Dictionary<HashAlgorithmType, Dictionary<string, string>> algorithmTextAndHashes;     

        public class ComputeHashStream : HashUtilTests
        {                 
            [Test]
            public void Parameter_checks()
            {
                Stream stream = null;
                var ane = Assert.Throws<ArgumentNullException>(delegate { HashUtil.ComputeHash(stream); });
                Assert.That(ane.Message, Contains.Substring("stream"));
            }

            [Test]
            public void Validate_hash()
            {
                foreach (var algorithmType in this.algorithmTextAndHashes.Keys)
                {
                    var textsAndHashes = this.algorithmTextAndHashes[algorithmType];

                    foreach (var keyValuePair in textsAndHashes)
                    {
                        using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(keyValuePair.Key)))
                        {
                            Assert.AreEqual(keyValuePair.Value, HashUtil.ComputeHash(ms, algorithmType));
                        }
                    }
                }
            }
        }

        public class ComputeHashBytes : HashUtilTests
        {
            [Test]
            public void Parameter_checks()
            {
                byte[] bytes = null;
                var ane = Assert.Throws<ArgumentNullException>(delegate { HashUtil.ComputeHash(bytes); });
                Assert.That(ane.Message, Contains.Substring("bytes"), "Test 1");

                bytes = new byte[0];
                Assert.DoesNotThrow(delegate { HashUtil.ComputeHash(bytes); }, "Test 2");
            }

            [Test]
            public void Validate_hash()
            {
                foreach (var algorithmType in this.algorithmTextAndHashes.Keys)
                {
                    var textsAndHashes = this.algorithmTextAndHashes[algorithmType];

                    foreach (var keyValuePair in textsAndHashes)
                    {
                        byte[] bytes = Encoding.UTF8.GetBytes(keyValuePair.Key);
                        Assert.AreEqual(keyValuePair.Value, HashUtil.ComputeHash(bytes, algorithmType));
                    }
                }
            }
        }

        public class ComputeHashString : HashUtilTests
        {
            [Test]
            public void Parameter_checks()
            {
                string text = null;
                var ane = Assert.Throws<ArgumentNullException>(delegate { HashUtil.ComputeHash(text); });
                Assert.That(ane.Message, Contains.Substring("text"), "Test 1");

                text = string.Empty;
                Assert.DoesNotThrow(delegate { HashUtil.ComputeHash(text); }, "Test 2");

                text = "  ";
                Assert.DoesNotThrow(delegate { HashUtil.ComputeHash(text); }, "Test 3");
            }

            [Test]
            public void Validate_hash()
            {
                foreach (var algorithmType in this.algorithmTextAndHashes.Keys)
                {
                    var textsAndHashes = this.algorithmTextAndHashes[algorithmType];

                    foreach (var keyValuePair in textsAndHashes)
                    {                       
                        Assert.AreEqual(keyValuePair.Value, HashUtil.ComputeHash(keyValuePair.Key, algorithmType));
                    }
                }
            }
        }

        // TODO Write tests for FileHashing

        // TODO Write tests for CompareHasing

        [TestFixtureSetUp]
        public void Init()
        {
            this.algorithmTextAndHashes = new Dictionary<HashAlgorithmType, Dictionary<string, string>>();
            var MD5TextsAndHashes = new Dictionary<string, string>();
            MD5TextsAndHashes.Add("", "D41D8CD98F00B204E9800998ECF8427E");
            MD5TextsAndHashes.Add("A simple string", "DCAAE4B618EB72F747FFBF82DD96CB92");
            MD5TextsAndHashes.Add("Duis accumsan dapibus ligula ut sollicitudin. Sed quis velit ut enim tincidunt viverra sed vel quam. Aliquam eget tristique lectus. Duis nulla urna, dignissim eget lacinia quis, consectetur ac erat. Vivamus efficitur nec erat nec elementum. Aliquam enim metus, lacinia ut sem nec, volutpat sagittis lectus. Nulla facilisi.", "A9FB3C0E880BA83EB25AD47485250AAE");
            this.algorithmTextAndHashes.Add(HashAlgorithmType.MD5, MD5TextsAndHashes);
            var SHA1TextsAndHashes = new Dictionary<string, string>();
            SHA1TextsAndHashes.Add("", "DA39A3EE5E6B4B0D3255BFEF95601890AFD80709");
            SHA1TextsAndHashes.Add("A simple string", "49B3C749032304BF0663F8A2D5E0ADE4FDF8BB96");
            SHA1TextsAndHashes.Add("Duis accumsan dapibus ligula ut sollicitudin. Sed quis velit ut enim tincidunt viverra sed vel quam. Aliquam eget tristique lectus. Duis nulla urna, dignissim eget lacinia quis, consectetur ac erat. Vivamus efficitur nec erat nec elementum. Aliquam enim metus, lacinia ut sem nec, volutpat sagittis lectus. Nulla facilisi.", "B604E583EC1D67637FCF2DE9B9B921CFEFFC33A1");
            this.algorithmTextAndHashes.Add(HashAlgorithmType.SHA1, SHA1TextsAndHashes);
            var SHA256TextsAndHashes = new Dictionary<string, string>();
            SHA256TextsAndHashes.Add("", "E3B0C44298FC1C149AFBF4C8996FB92427AE41E4649B934CA495991B7852B855");
            SHA256TextsAndHashes.Add("A simple string", "D33538266532340DC6342A505BB026A9F5B6F200FD13F6D27F6395D3F3FD950D");
            SHA256TextsAndHashes.Add("Duis accumsan dapibus ligula ut sollicitudin. Sed quis velit ut enim tincidunt viverra sed vel quam. Aliquam eget tristique lectus. Duis nulla urna, dignissim eget lacinia quis, consectetur ac erat. Vivamus efficitur nec erat nec elementum. Aliquam enim metus, lacinia ut sem nec, volutpat sagittis lectus. Nulla facilisi.", "C29135B1CF8BF381A042EF6B7A7A0F10E12B4533FFE2B322778230E38444BCD3");
            this.algorithmTextAndHashes.Add(HashAlgorithmType.SHA256, SHA256TextsAndHashes);
            var SHA384TextsAndHashes = new Dictionary<string, string>();
            SHA384TextsAndHashes.Add("", "38B060A751AC96384CD9327EB1B1E36A21FDB71114BE07434C0CC7BF63F6E1DA274EDEBFE76F65FBD51AD2F14898B95B");
            SHA384TextsAndHashes.Add("A simple string", "3A619ED365F291206EB52BB82BDBA4363E728FC48002018071015CDAD53630261DC62AC2A02616897FB6B97A76836020");
            SHA384TextsAndHashes.Add("Duis accumsan dapibus ligula ut sollicitudin. Sed quis velit ut enim tincidunt viverra sed vel quam. Aliquam eget tristique lectus. Duis nulla urna, dignissim eget lacinia quis, consectetur ac erat. Vivamus efficitur nec erat nec elementum. Aliquam enim metus, lacinia ut sem nec, volutpat sagittis lectus. Nulla facilisi.", "8A16CDB653308EF33E02C20CD6D561AB1BF996C01E3E37E46BCD0A9206432B8D6D2076A31AC7ED631918CDB6A134A19C");
            this.algorithmTextAndHashes.Add(HashAlgorithmType.SHA384, SHA384TextsAndHashes);
            var SHA512TextsAndHashes = new Dictionary<string, string>();
            SHA512TextsAndHashes.Add("", "CF83E1357EEFB8BDF1542850D66D8007D620E4050B5715DC83F4A921D36CE9CE47D0D13C5D85F2B0FF8318D2877EEC2F63B931BD47417A81A538327AF927DA3E");
            SHA512TextsAndHashes.Add("A simple string", "CD53687F49740995BB80A627B75F291F49C476133BAB268B360A0D0689027B7F1F8F69BA7EB92169B7129320B867427E2196364DF931A4CD00695504628D2E09");
            SHA512TextsAndHashes.Add("Duis accumsan dapibus ligula ut sollicitudin. Sed quis velit ut enim tincidunt viverra sed vel quam. Aliquam eget tristique lectus. Duis nulla urna, dignissim eget lacinia quis, consectetur ac erat. Vivamus efficitur nec erat nec elementum. Aliquam enim metus, lacinia ut sem nec, volutpat sagittis lectus. Nulla facilisi.", "4741EDEE2D515E836444395B217873248E877589C5D1547454F788155D71A6DC51560C4AD6525C154EAD4AE22D1797318055DC7046FE153B9F147C5BA8BC9F56");
            this.algorithmTextAndHashes.Add(HashAlgorithmType.SHA512, SHA512TextsAndHashes);

            #region Code to generate the above
            //this.algorithmTextAndHashes = new Dictionary<HashAlgorithmType, Dictionary<string, string>>();

            //List<string> values = new List<string>();
            //values.Add(string.Empty);
            //values.Add("A simple string");
            //values.Add("Duis accumsan dapibus ligula ut sollicitudin. Sed quis velit ut enim tincidunt viverra sed vel quam. Aliquam eget tristique lectus. Duis nulla urna, dignissim eget lacinia quis, consectetur ac erat. Vivamus efficitur nec erat nec elementum. Aliquam enim metus, lacinia ut sem nec, volutpat sagittis lectus. Nulla facilisi.");

            //foreach (HashAlgorithmType type in Enum.GetValues(typeof(HashAlgorithmType)))
            //{
            //    string textsAndHashesKeyName = string.Format("{0}TextsAndHashes", type);
            //    Debug.WriteLine(string.Format("var {0} = new Dictionary<string, string>();", textsAndHashesKeyName));

            //    var textsAndHashes = new Dictionary<string, string>();

            //    foreach (var value in values)
            //    {
            //        Debug.WriteLine("{0}.Add(\"{1}\", \"{2}\");", textsAndHashesKeyName, value, HashUtil.ComputeHash(value, type));
            //    }

            //    Debug.WriteLine("this.algorithmTextAndHashes.Add(HashAlgorithmType.{0}, {1});", type, textsAndHashesKeyName);

            //}
            #endregion
        }

        [TestFixtureTearDown]
        public void Cleanup()
        {

        }
    }
}
