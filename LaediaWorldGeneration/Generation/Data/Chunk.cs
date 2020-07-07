using System;

namespace LaediaWorldGeneration.Generation.Data
{
    [Serializable]
    public class Chunk
    {
        Tile[,,] m_tiles;

        public Tile[,,] Tiles { get => m_tiles; set => m_tiles = value; }


        public Chunk(int chunkSize)
        {
            m_tiles = new Tile[chunkSize, chunkSize, 0];
        }
    }
}
