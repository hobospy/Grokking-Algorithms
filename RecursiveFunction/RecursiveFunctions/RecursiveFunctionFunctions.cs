using System;

namespace RecursiveFunctions
{
    public class RecursiveFunctionFunctions
    {
        public static long Factorial(long input)
        {
            if (input >= 0)
            {
                if (input == 0 || input == 1)
                {
                    return 1;
                }
                else
                {
                    return input * Factorial(input - 1);
                }
            }
            else
            {
                throw new ArgumentException();
            }
        }
    }
}
