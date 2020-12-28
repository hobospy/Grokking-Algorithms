using System;
using System.Linq;

namespace ArraySum
{
    public class ArraySumFunctions
    {
        public static long ArraySum(long[] inputArray)
        {
            var returnValue = -1L;

            if (inputArray != null)
            {
                if (inputArray.Length > 0)
                {
                    if (inputArray.Length == 1)
                    {
                        returnValue = inputArray[0];
                    }
                    else
                    {
                        long[] newArray = inputArray.Skip(1).Take(inputArray.Length - 1).ToArray();
                        returnValue = inputArray[0] + ArraySum(newArray);
                    }
                }
                else
                {
                    throw new ArgumentException();
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
