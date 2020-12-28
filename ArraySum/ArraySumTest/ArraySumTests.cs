using ArraySum;
using NUnit.Framework;

namespace ArraySumTest
{
    public class Tests
    {
        [Test]
        public void ArraySum_ThrowsNullException_IfArrayIsNull()
        {
            long[] testArray = null;

            Assert.That(() => ArraySumFunctions.ArraySum(testArray), Throws.ArgumentNullException);
        }

        [Test]
        public void ArraySum_ThrowsArgumentException_IfArrayIsEmpty()
        {
            long[] testArray = { };

            Assert.That(() => ArraySumFunctions.ArraySum(testArray), Throws.ArgumentException);
        }

        [Test]
        [TestCase(new long[] { 1 }, 1)]
        [TestCase(new long[] { 1, 1 }, 2)]
        [TestCase(new long[] { 1, 1, -1 }, 1)]
        [TestCase(new long[] { 1, 5, 11, 29, -76 }, -30)]
        public void ArraySum_ReturnsSumOfArray_IfArrayValid(long[] inputArray, long sumValue)
        {
            var result = ArraySumFunctions.ArraySum(inputArray);

            Assert.AreEqual(sumValue, result);
        }
    }
}