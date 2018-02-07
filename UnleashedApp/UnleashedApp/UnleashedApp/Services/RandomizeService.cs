using System;
using System.Collections.Generic;

namespace UnleashedApp.Services
{
    public static class RandomizeService
    {
        private static Random random = new Random();

        #region Unused
        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = random.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
        #endregion

        public static List<T> GetSpecifiedAmountOfRandomObjectsFromList<T>(List<T> list, int amount)
        {
            if (list.Count >= amount)
            {
                List<int> randomIndexes = new List<int>();
                List<T> randomEmployees = new List<T>();
                int first = random.Next(list.Count);
                randomIndexes.Add(first);
                randomEmployees.Add(list[first]);

                bool isDone = false;
                while (!isDone)
                {
                    int index = random.Next(list.Count);
                    if (!randomIndexes.Contains(index))
                    {
                        randomIndexes.Add(index);
                        randomEmployees.Add(list[index]);

                        if (randomIndexes.Count == amount)
                        {
                            isDone = true;
                        }
                    }
                }

                return randomEmployees;
            }
            return null;
        }

        public static T GetRandomObjectFromList<T>(List<T> list)
        {
            int r = random.Next(list.Count);
            return list[r];
        }

        public static char GetRandomGender(int malePercentage = 50)
        {
            if (random.NextDouble() < malePercentage / 100.0)
            {
                return 'M';
            }
            else
            {
                return 'F';
            }
        }
    }
}
