using System;
using System.Collections.Generic;

namespace BinarySearch
{
    public class BinarySearchFunctions
    {
        public static int? FindIntValue(List<int> searchList, int searchValue)
        {
            int? returnValue = null;

            if (searchList != null)
            {
                var lowIndex = 0;
                var highIndex = searchList.Count - 1;

                while (lowIndex <= highIndex && returnValue == null)
                {
                    var midIndex = (lowIndex + highIndex) / 2;
                    var guessValue = searchList[midIndex];

                    if (guessValue == searchValue)
                    {
                        returnValue = guessValue;
                    }
                    else
                    {
                        if (guessValue > searchValue)
                        {
                            highIndex = midIndex - 1;
                        }
                        else
                        {
                            lowIndex = midIndex + 1;
                        }
                    }
                }
            }
            else
            {
                throw new ArgumentNullException();
            }

            return returnValue;
        }
    }
}
