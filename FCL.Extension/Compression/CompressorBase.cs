using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCL.Compression
{
    /// <summary>
    /// The abstract base class for compression.
    /// </summary>
    public abstract class Compressor
    {
        /// <summary>
        /// Compress the data.
        /// </summary>
        /// <param name="originalData">the original data.</param>
        /// <returns>The compressed binary</returns>
        public abstract byte[] Compress(byte[] originalData);

        /// <summary>
        /// Decompress the data
        /// </summary>
        /// <param name="compressedData">The compressed data.</param>
        /// <returns>The decompressed binary</returns>
        public abstract byte[] Decompress(byte[] compressedData);
    }
}
