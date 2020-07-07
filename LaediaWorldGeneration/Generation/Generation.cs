using LaediaWorldGeneration.Generation.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaediaWorld
{
    public static class Generation
    {
        public static World GenerateWorld(long xChunks, long yChunks, int chunksize)
        {
            var world = new World(chunksize);

            world.Chunks = new Chunk[xChunks, yChunks];

            for (int x = 0; x < xChunks; x++)
            {
                for (int y = 0; y < yChunks; y++)
                {
                    GenerateChunk(chunksize, world, x, y);
                }
            }

            return world;
        }

        private static void GenerateChunk(int chunksize, World world, int x, int y)
        {
            world.Chunks[x, y] = new Chunk(chunksize);

            for (int tileX = 0; tileX < chunksize; tileX++)
            {
                for (int tileY = 0; tileY < chunksize; tileY++)
                {

                }
            }
        }
    }
}
