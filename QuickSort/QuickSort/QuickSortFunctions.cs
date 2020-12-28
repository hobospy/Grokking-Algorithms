using System;
using System.Collections.Generic;
using System.Linq;

namespace QuickSort
{
    public class QuickSortFunctions
    {
        public static long[] QuickSort(long[] inputArray)
        {
            List<long> returnValue = new List<long>();

            if (inputArray != null)
            {
                if (inputArray.Length > 0)
                {
                    if (inputArray.Length == 1)
                    {
                        returnValue.AddRange(inputArray);
                    }
                    else
                    {
                        var pivotValueIndex = inputArray.Length / 2;
                        var pivotValue = inputArray[pivotValueIndex];

                        List<long> lessThanValues = new List<long>();
                        List<long> greaterThanValues = new List<long>();

                        for (int i = 0; i < pivotValueIndex; ++i)
                        {
                            UpdateSortLists(lessThanValues, greaterThanValues, inputArray[i], pivotValue);
                        }

                        for (int i = pivotValueIndex + 1; i < inputArray.Length; ++i)
                        {
                            UpdateSortLists(lessThanValues, greaterThanValues, inputArray[i], pivotValue);
                        }

                        returnValue.AddRange(QuickSort(lessThanValues.ToArray()));
                        returnValue.Add(pivotValue);
                        returnValue.AddRange(QuickSort(greaterThanValues.ToArray()));
                    }
                }
            }
            else
            {
                throw new ArgumentNullException();
            }

            return returnValue.ToArray();
        }

        private static void UpdateSortLists(List<long> lessThanValues, List<long>greaterThanValues, long testingValue, long pivotValue)
        {
            if (testingValue <= pivotValue)
            {
                lessThanValues.Add(testingValue);
            }
            else
            {
                greaterThanValues.Add(testingValue);
            }
        }
    }
}
