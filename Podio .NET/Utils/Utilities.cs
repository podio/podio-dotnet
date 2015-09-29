using PodioAPI.Models;
using System.IO;
using System.Threading.Tasks;

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

        private static async Task<byte[]> ReadAllBytesAsync(string path, bool checkHost)
        {
            byte[] bytes;
            using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, true))
            {
                // Do a blocking read
                int index = 0;
                long fileLength = fs.Length;
                
                int count = (int)fileLength;
                bytes = new byte[count];
                while (count > 0)
                {
                    int n = await fs.ReadAsync(bytes, index, count);
                    if (n == 0)
                        throw new EndOfStreamException("EndOfStreamException");

                    index += n;
                    count -= n;
                }
            }
            return bytes;
        }
    }
}