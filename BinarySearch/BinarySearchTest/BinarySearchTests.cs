using NUnit.Framework;
using BinarySearch;
using System.Collections.Generic;

namespace BinarySearchTest
{
    public class Tests
    {
        [Test]
        public void FindIntValue_ThrowsException_IfArrayIsNull()
        {
            List<int> nullList = null;

            Assert.That(() => BinarySearchFunctions.FindIntValue(nullList, 0), Throws.ArgumentNullException);
        }

        [Test]
        public void FindIntValue_ReturnsNull_IfListIsEmpty()
        {
            var emptyList = new List<int>();

            var result = BinarySearchFunctions.FindIntValue(emptyList, 0);

            Assert.IsNull(result);
        }

        [Test]
        public void FindIntValue_ReturnsNull_IfValueNotFound()
        {
            var testList = new List<int> { 0, 1, 2, 3, 4, 5 };

            var result = BinarySearchFunctions.FindIntValue(testList, 9);

            Assert.IsNull(result);
        }

        [Test]
        public void FindIntValue_ReturnsValue_IfValueFound()
        {
            var testList = new List<int> { 0, 1, 2, 3, 4, 5 };
            var searchValue = 3;

            var result = BinarySearchFunctions.FindIntValue(testList, searchValue);

            Assert.AreEqual(searchValue, result);
        }
    }
}