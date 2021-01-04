using NUnit.Framework;
using SetCovering;
using System;
using System.Collections.Generic;

namespace SetCoveringTests
{
    public class Tests
    {
        private static IEnumerable<TestCaseData> _testNullArguments()
        {
            yield return new TestCaseData(new List<ValueTuple<string, HashSet<string>>>() { }, null);
            yield return new TestCaseData(null, new HashSet<string>() { });
            yield return new TestCaseData(null, null);
        }

        private static IEnumerable<TestCaseData> _testEmptyArguments()
        {
            yield return new TestCaseData(new List<ValueTuple<string, HashSet<string>>>() { }, new HashSet<string>() { "A" });
            yield return new TestCaseData(new List<ValueTuple<string, HashSet<string>>>() { new ValueTuple<string, HashSet<string>> ( "TestItem", new HashSet<string>() { "Z" } ) }, new HashSet<string>() { });
            yield return new TestCaseData(new List<ValueTuple<string, HashSet<string>>>() { }, new HashSet<string>() { });
        }

        [Test]
        [TestCaseSource("_testNullArguments")]
        public void GetBestCoverage_ThrowsNullArgumentException_WhenArgumentIsNull(List<ValueTuple<string, HashSet<string>>> testAvailableItemSetList, HashSet<string> testRequiredItemsHashSet)
        {
            Assert.That(() => SetCoveringFunctions.GetBestCoverage(testAvailableItemSetList, testRequiredItemsHashSet), Throws.ArgumentNullException);
        }

        [Test]
        [TestCaseSource("_testEmptyArguments")]
        public void GetBestCoverage_ThrowsArgumentException_WhenArgumentIsEmpty(List<ValueTuple<string, HashSet<string>>> testAvailableItemSetList, HashSet<string> testRequiredItemsHashSet)
        {
            Assert.That(() => SetCoveringFunctions.GetBestCoverage(testAvailableItemSetList, testRequiredItemsHashSet), Throws.ArgumentException);
        }

        [Test]
        public void GetBestCoverage_ThrowsArgumentOutOfRangeException_WhenRequiredItemsNotContained()
        {
            var testAvailableItemSetList = new List<ValueTuple<string, HashSet<string>>>() { ("ValidItem1", new HashSet<string>() { "D" }) };
            var testRequiredItemsHashSet = new HashSet<string>() { "A", "B", "C" };

            Assert.That(() => SetCoveringFunctions.GetBestCoverage(testAvailableItemSetList, testRequiredItemsHashSet), Throws.TypeOf<ArgumentOutOfRangeException>());
        }

        [Test]
        public void GetBestCoverage_ReturnsList_WhenAvailableItemSetContainsRequiredItems()
        {
            var testAvailableItemSetList = new List<ValueTuple<string, HashSet<string>>>() { ("ValidItem1", new HashSet<string>() { "NOR", "DEN", "SWE" }),
                                                                                             ("ValidItem2", new HashSet<string>() { "SCO", "FRA", "NOR" }),
                                                                                             ("ValidItem3", new HashSet<string>() { "EST", "GER", "DEN" }),
                                                                                             ("ValidItem4", new HashSet<string>() { "DEN", "SWE" }),
                                                                                             ("ValidItem5", new HashSet<string>() { "GER", "IT" }) };
            var testRequiredItemsHashSet = new HashSet<string>() { "SCO", "FRA", "EST", "NOR", "DEN", "SWE", "GER", "IT" };
            var expectedResult = new List<string> { "ValidItem1", "ValidItem2", "ValidItem3", "ValidItem5" };

            var result = SetCoveringFunctions.GetBestCoverage(testAvailableItemSetList, testRequiredItemsHashSet);

            Assert.AreEqual(expectedResult, result);
        }
    }
}