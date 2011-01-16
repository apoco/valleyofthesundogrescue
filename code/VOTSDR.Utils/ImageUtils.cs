using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace VOTSDR.Utils
{
    public class ImageUtils
    {
        public static byte[] GetBytes(Stream stream)
        {
            var fileLength = stream.Length;
            var bits = new byte[fileLength];
            stream.Read(bits, 0, (int)fileLength);
            return bits;
        }

    }
}
