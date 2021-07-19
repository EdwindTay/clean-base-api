using System;
using System.IO;

namespace Clean.Seed.Utils
{
    internal static class Data
    {
        internal static string GetSeedDataFromFile(string fileName)
        {
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"Data\\{fileName}");
        }
    }
}
