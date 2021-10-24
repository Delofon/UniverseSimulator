using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniverseSimulator.UniverseSimulatorStuff
{
    public static class PerlinNoiseHandler
    {
        public static float Sample(Perlin2D perlin, float x, float y, int width, int height, float scale, float offsetX, float offsetY)
        {
            float xCoord = x / width * scale + offsetX;
            float yCoord = y / height * scale + offsetY;
            return perlin.Noise(xCoord, yCoord);
        }

        public static float Sample(Perlin2D perlin, float x, float y, int width, int height, float scale, float offsetX, float offsetY, int octaves, float perm = 0.5f)
        {
            float xCoord = x / width * scale + offsetX;
            float yCoord = y / height * scale + offsetY;
            return perlin.Noise(xCoord, yCoord, octaves, perm);
        }
    }
}
