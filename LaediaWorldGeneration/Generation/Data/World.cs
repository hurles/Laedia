using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaediaWorldGeneration.Generation.Data
{
    [Serializable]
    public class World
    {
        public readonly int ChunkSize = 4;

        Chunk[,] m_chunks;

        public World(int chunkSize)
        {
            ChunkSize = chunkSize;
        }

        public Chunk[,] Chunks { get => m_chunks; set => m_chunks = value; }
    }
}
