using System.IO;

namespace mLetsTatoo.Helpers
{
    public class FileHelper
    {
        public static byte[] ReadFully(Stream input)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                input.CopyTo(ms);
                input.Dispose();
                return ms.ToArray();
            }
        }
    }
}
