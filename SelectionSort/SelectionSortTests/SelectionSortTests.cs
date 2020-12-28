using NUnit.Framework;
using SelectionSort;

namespace SelectionSortTests
{
    public class Tests
    {
        [Test]
        public void FindIndexOfSmallestValue_ReturnsException_WhenArrayNull()
        {
            int[] testArray = null;

            Assert.That(() => SelectionSortFunctions.FindIndexOfSmallestValue(testArray), Throws.ArgumentNullException);
        }

        [Test]
        public void FindIndexOfSmallestValue_ReturnsNull_WhenArrayIsEmpty()
        {
            int[] testArray = { };

            var result = SelectionSortFunctions.FindIndexOfSmallestValue(testArray);

            Assert.IsNull(result);
        }

        [Test]
        public void FindIndexOfSmallestValue_ReturnsSmallestValueIndex_WhenArrayIsValidAndAllPositive()
        {
            int[] testArray = { 5, 2, 9, 0, 10 };

            var result = SelectionSortFunctions.FindIndexOfSmallestValue(testArray);

            Assert.AreEqual(3, result);
        }

        [Test]
        public void FindIndexOfSmallestValue_ReturnsSmallestValueIndex_WhenArrayIsValidAndMixedSigns()
        {
            int[] testArray = { 5, -22, 9, 0, -10 };

            var result = SelectionSortFunctions.FindIndexOfSmallestValue(testArray);

            Assert.AreEqual(1, result);
        }

        [Test]
        public void SelectionSort_ReturnsException_WhenArrayIsNull()
        {
            int[] testArray = null;

            Assert.That(() => SelectionSortFunctions.SelectionSort(testArray), Throws.ArgumentNullException);
        }

        [Test]
        public void SelectionSort_ReturnsNull_WhenArrayIsEmpty()
        {
            int[] testArray = { };

            var result = SelectionSortFunctions.SelectionSort(testArray);

            Assert.IsNull(result);
        }

        [Test]
        public void SelectionSort_ReturnsOrderedArray_WhenArrayIsValid()
        {
            int[] testArray = { 9, -10, 2, -5, 11, 23 };
            int[] expectedResult = { -10, -5, 2, 9, 11, 23 };

            var result = SelectionSortFunctions.SelectionSort(testArray);

            Assert.That(result, Is.EqualTo(expectedResult));
        }
    }
}