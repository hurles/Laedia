using System;

namespace LaediaWorldGeneration.Generation.Data
{
    [Serializable]
    public class Tile
    {
        TileTypes m_tileType;

        public TileTypes TileType { get => m_tileType; set => m_tileType = value; }
    }

    public enum TileTypes
    {
        TT_Dirt,
        TT_Stone
    }
}
