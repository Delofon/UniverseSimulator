using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace UniverseSimulator.UniverseSimulatorStuff
{
    public class Particle
    {
        public Vector2 position;
        public Vector2 velocity;

        public float mass;

        public Particle(Vector2 position, float mass)
        {
            this.position = position;

            this.mass = mass;
        }

        public void UpdatePos(GameTime gameTime)
        {
            position += velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }
    }
}
