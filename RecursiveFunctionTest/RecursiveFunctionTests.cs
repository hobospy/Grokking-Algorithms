using NUnit.Framework;
using RecursiveFunctions;

namespace RecursiveFunctionTest
{
    public class Tests
    {
        [Test]
        [TestCase(1, 1)]
        [TestCase(2, 2)]
        [TestCase(3, 6)]
        public void Factorial_WhenPassedPositiveValue_ReturnsFactorialValue(long inputValue, long expectedValue)
        {
            var result = RecursiveFunctionFunctions.Factorial(inputValue);

            Assert.AreEqual(expectedValue, result);
        }

        [Test]
        public void Factorial_WhenPassedZero_Returns1()
        {
            var result = RecursiveFunctionFunctions.Factorial(0);

            Assert.AreEqual(1, result);
        }

        [Test]
        [TestCase(-1)]
        [TestCase(-6)]
        [TestCase(-19)]
        public void Factorial_WhenPassedNegativeValue_RaisesArgumentException(long inputValue)
        {
            Assert.That(() => RecursiveFunctionFunctions.Factorial(inputValue), Throws.ArgumentException);
        }
    }
}