namespace PodioAPI.Utils
{
    internal class Utilities
    {
        internal static string ArrayToCSV(int[] array, string splitter = ",")
        {
            if (array != null && array.Length > 0)
                return string.Join(splitter, array);

            return string.Empty;
        }

        internal static string ArrayToCSV(string[] array, string splitter = ",")
        {
            if (array != null && array.Length > 0)
                return string.Join(splitter, array);

            return string.Empty;
        }
    }
}