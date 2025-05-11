using Microsoft.VisualStudio.TestTools.UnitTesting;
using lab3;

namespace lab3.Tests
{
    [TestClass]
    public class SystemNumTests
    {
        [TestMethod]
        public void TestVerbose()
        {
            var num = new SystemNum("15", NumberSystemType.dec);
            Assert.AreEqual("15(10)", num.Verbose());

            num = new SystemNum("1010", NumberSystemType.bin);
            Assert.AreEqual("1010(2)", num.Verbose());

            num = new SystemNum("17", NumberSystemType.oct);
            Assert.AreEqual("17(8)", num.Verbose());

            num = new SystemNum("1A", NumberSystemType.hex);
            Assert.AreEqual("1A(16)", num.Verbose());
        }

        [TestMethod]
        public void TestDecimalToAnySystem()
        {
            var num = new SystemNum("42", NumberSystemType.dec);
            Assert.AreEqual("101010(2)", num.To(NumberSystemType.bin).Verbose());
            Assert.AreEqual("52(8)", num.To(NumberSystemType.oct).Verbose());
            Assert.AreEqual("2A(16)", num.To(NumberSystemType.hex).Verbose());
        }

        [TestMethod]
        public void TestAnySystemToDecimal()
        {
            var num = new SystemNum("101010", NumberSystemType.bin);
            Assert.AreEqual("42(10)", num.To(NumberSystemType.dec).Verbose());

            num = new SystemNum("52", NumberSystemType.oct);
            Assert.AreEqual("42(10)", num.To(NumberSystemType.dec).Verbose());

            num = new SystemNum("2A", NumberSystemType.hex);
            Assert.AreEqual("42(10)", num.To(NumberSystemType.dec).Verbose());
        }

        [TestMethod]
        public void TestAdditionWithInteger()
        {
            var num = new SystemNum("11", NumberSystemType.oct); // 9 в десятичной
            num = num + 2;
            Assert.AreEqual("13(8)", num.Verbose()); // 11 + 2 = 13 в восьмеричной
        }

        [TestMethod]
        public void TestSubtractionWithInteger()
        {
            var num = new SystemNum("50", NumberSystemType.dec);
            num = num - 20;
            Assert.AreEqual("30(10)", num.Verbose());
        }

        [TestMethod]
        public void TestMultiplicationWithInteger()
        {
            var num = new SystemNum("10", NumberSystemType.dec);
            num = num * 6;
            Assert.AreEqual("60(10)", num.Verbose());
        }

        [TestMethod]
        public void TestDivisionWithInteger()
        {
            var num = new SystemNum("40", NumberSystemType.dec);
            num = num / 5;
            Assert.AreEqual("8(10)", num.Verbose());
        }

        [TestMethod]
        public void TestDivisionGetValue()
        {
            var num = new SystemNum("40", NumberSystemType.dec);
            num = num / 4;
            Assert.AreEqual("10", num.GetValue());
        }

        [TestMethod]
        public void TestAddDecimalAndBinary()
        {
            var dec = new SystemNum("12", NumberSystemType.dec);
            var bin = new SystemNum("10", NumberSystemType.bin); // 2

            Assert.AreEqual("14(10)", (dec + bin).Verbose());
            Assert.AreEqual("1110(2)", (bin + dec).Verbose()); // 2 + 12 = 14 = 1110
        }

        [TestMethod]
        public void TestAddDecimalToOctalAndHex()
        {
            var dec = new SystemNum("9", NumberSystemType.dec);
            var oct = new SystemNum("7", NumberSystemType.oct); // 7 (в 10) = 7

            var hex = new SystemNum("6", NumberSystemType.hex); // 6 (в 10) = 6

            Assert.AreEqual("20(8)", (oct + dec).Verbose()); // 7 + 9 = 16 = 20(8)
            Assert.AreEqual("F(16)", (hex + dec).Verbose()); // 6 + 9 = 15 = F(16)
        }

        [TestMethod]
        public void TestBinaryMinusDecimal()
        {
            var dec = new SystemNum("3", NumberSystemType.dec);
            var bin = new SystemNum("1111", NumberSystemType.bin); // 15

            Assert.AreEqual("1100(2)", (bin - dec).Verbose()); // 15 - 3 = 12 = 1100(2)
        }
    }
}
