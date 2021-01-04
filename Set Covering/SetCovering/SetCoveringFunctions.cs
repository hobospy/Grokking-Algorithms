using System;
using System.Collections.Generic;

namespace SetCovering
{
    public class SetCoveringFunctions
    {
        public static List<string> GetBestCoverage(List<ValueTuple<string, HashSet<string>>> availableItemSetList, HashSet<string> requiredItemsHashSet)
        {
            List<string> returnValue = null;

            if (requiredItemsHashSet != null && requiredItemsHashSet.Count > 0 && availableItemSetList != null && availableItemSetList.Count > 0 && availableItemSetList[0].Item2.Count > 0)
            {
                returnValue = new List<string>();
                string bestSetName = "";

                while (requiredItemsHashSet.Count > 0)
                {
                    bestSetName = "";
                    var itemsMatched = new HashSet<string>() { };

                    foreach(var availableItemsSet in availableItemSetList)
                    {
                        availableItemsSet.Item2.IntersectWith(requiredItemsHashSet);

                        if (availableItemsSet.Item2.Count > itemsMatched.Count)
                        {
                            bestSetName = availableItemsSet.Item1;
                            itemsMatched = availableItemsSet.Item2;
                        }
                    }

                    if (itemsMatched.Count > 0)
                    {
                        requiredItemsHashSet.ExceptWith(itemsMatched);
                        returnValue.Add(bestSetName);
                    }
                    else
                    {
                        throw new ArgumentOutOfRangeException();
                    }
                }
            }
            else if (requiredItemsHashSet == null || availableItemSetList == null)
            {
                throw new ArgumentNullException();
            }
            else
            {
                throw new ArgumentException();
            }

            return returnValue;
        }
    }
}
