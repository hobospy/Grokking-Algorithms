using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SelectionSort
{
    public class SelectionSortFunctions
    {
        public static int? FindIndexOfSmallestValue(int[] array)
        {
            int? returnValue = null;

            if (array != null)
            {
                if (array.Length > 0)
                {
                    var smallestValue = array[0];
                    var smallestValueIndex = 0;

                    for (int i = 0; i < array.Length; ++i)
                    {
                        if (array[i] < smallestValue)
                        {
                            smallestValue = array[i];
                            smallestValueIndex = i;
                        }
                    }

                    returnValue = smallestValueIndex;
                }
            }
            else
            {
                throw new ArgumentNullException();
            }

            return returnValue;
        }

        public static int[] SelectionSort(int[] array)
        {
            List<int> sortedList = new List<int>();

            if (array != null)
            {
                int[] sortingArray = new int[array.Length];
                array.CopyTo(sortingArray, 0);
                var arrayLength = sortingArray.Length;

                for (int i = 0; i < arrayLength; ++i)
                {
                    var indexOfSmallest = FindIndexOfSmallestValue(sortingArray);

                    if (indexOfSmallest != null)
                    {
                        sortedList.Add(sortingArray[indexOfSmallest.Value]);

                        List<int> tempArray = new List<int>();
                        for(int x = 0; x < sortingArray.Length; ++x)
                        {
                            if (x != indexOfSmallest)
                            {
                                tempArray.Add(sortingArray[x]);
                            }
                        }

                        if (tempArray.Count > 0)
                        {
                            sortingArray = new int[tempArray.Count];
                            tempArray.CopyTo(sortingArray, 0);
                        }
                    }
                }
            }
            else
            {
                throw new ArgumentNullException();
            }

            return sortedList.Count > 0 ? sortedList.ToArray() : null;
        }
    }
}
