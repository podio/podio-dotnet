
namespace PodioAPI.Utils
{
    internal class Utilities
    {
        internal static string ArrayToCSV(int[] array)
        {
            var csv = "";
            if (array != null && array.Length > 0)
                csv = string.Join(",", array);

            return csv;
        }

        internal static string ArrayToCSV(string[] array)
        {
            var csv = "";
            if (array != null && array.Length > 0)
                csv = string.Join(",", array);

            return csv;
        }
    }
}
