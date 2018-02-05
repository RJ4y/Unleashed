using System;
using System.Collections.Generic;

namespace UnleashedApp.Services
{
    internal static class RandomizeService
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

        public static List<T> GetSpecifiedAmountOfRandomObjectsFromList<T>(List<T> employees, int amount)
        {
            if (employees.Count >= amount)
            {
                List<int> randomIndexes = new List<int>();
                int first = random.Next(employees.Count);
                randomIndexes.Add(first);

                bool isDone = false;
                while (!isDone)
                {
                    int index = random.Next(employees.Count);
                    if (!randomIndexes.Contains(index))
                    {
                        randomIndexes.Add(index);
                        if (randomIndexes.Count == amount)
                        {
                            isDone = true;
                        }
                    }
                }
                List<T> randomEmployees = new List<T>();
                foreach (int i in randomIndexes)
                {
                    randomEmployees.Add(employees[i]);
                }
                return randomEmployees;
            }
            else
            {
                return null;
            }
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
