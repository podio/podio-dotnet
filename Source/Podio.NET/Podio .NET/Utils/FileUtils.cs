using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PodioAPI.Utils
{
    internal static class FileUtils
    {
        internal static async Task<byte[]> ReadAllBytesAsync(string path)
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
