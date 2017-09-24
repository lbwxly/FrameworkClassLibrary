using Ionic.Zlib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCL.Compression
{
    public class GZipCompressor : Compressor
    {
        public static string Name => "GZip";

        public override byte[] Compress(byte[] originalData)
        {
            using (var outputStream = new MemoryStream())
            {
                using (var gzipStream = new GZipStream(outputStream, CompressionMode.Compress, CompressionLevel.BestSpeed))
                {
                    gzipStream.Write(originalData, 0, originalData.Length);
                }

                return outputStream.ToArray();
            }
        }

        public override byte[] Decompress(byte[] compressedData)
        {
            using (var inputStream = new MemoryStream(compressedData))
            {
                var outputStream = new MemoryStream();
                var gzipStream = new GZipStream(inputStream, CompressionMode.Decompress, CompressionLevel.BestSpeed);
                int blockSize = 10 * 1024;
                while (gzipStream.CanRead)
                {
                    byte[] buffer = new byte[blockSize];
                    int readSize = gzipStream.Read(buffer, 0, blockSize);
                    outputStream.Write(buffer, 0, readSize);
                }

                outputStream.Flush();
                return outputStream.ToArray();
            }
        }
    }
}
