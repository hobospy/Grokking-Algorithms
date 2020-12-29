using NUnit.Framework;
using QuickSort;

namespace QuickSortTests
{
    public class Tests
    {
        [Test]
        public void QuickSort_ThrowsNullException_WhenInputArrayIsNull()
        {
            long[] testArray = null;

            Assert.That(() => QuickSortFunctions.QuickSort(testArray), Throws.ArgumentNullException);
        }

        [Test]
        public void QuickSort_ReturnsEmptyArray_WhenInputArrayIsEmpty()
        {
            long[] testArray = { };

            var result = QuickSortFunctions.QuickSort(testArray);

            Assert.AreEqual(new long[] { }, result);
        }

        [Test]
        [TestCase(new long[] { 1 }, new long[] { 1 })]
        [TestCase(new long[] { 1, 2 }, new long[] { 1, 2 })]
        [TestCase(new long[] { 2, 1 }, new long[] { 1, 2 })]
        [TestCase(new long[] { 1, 2, 3 }, new long[] { 1, 2, 3 })]
        [TestCase(new long[] { 3, 1, 2 }, new long[] { 1, 2, 3 })]
        [TestCase(new long[] { 10, -1, 7, 24, -17 }, new long[] { -17, -1, 7, 10, 24 })]
        public void QuickSort_ReturnsSortedArray_WhenInputArrayIsValid(long[] inputArray, long[] expectedResult)
        {
            var result = QuickSortFunctions.QuickSort(inputArray);

            Assert.AreEqual(expectedResult, result);
        }
    }
}