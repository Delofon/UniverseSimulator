using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace UniverseSimulator.UniverseSimulatorStuff
{
    public class ParticlePlayground
    {
        Particle[,] particles;

        int particleSize;

        float repelCoeff;
        float repelRadius;

        Color particleColour;
        public Gradient particleGradient;

        public Texture2D square;

        Texture2D makeSquare()
        {
            Texture2D square = new Texture2D(Game1.graphics.GraphicsDevice, 1, 1);

            Color[] data = new Color[1];

            data[0] = Color.White;

            square.SetData(data);

            return square;
        }

        Vector2 calculateParticlePosition(int width, int height, int particlesX, int particlesY, int particleX, int particleY)
        {
            int calculatedX = width / particlesX * particleX;
            int calculatedY = height / particlesY * particleY;

            Vector2 calculated = new Vector2(calculatedX, calculatedY);

            return calculated;
        }

        public ParticlePlayground(Vector2 origin, int width, int height, int particlesX, int particlesY, int particleSize, float repelCoeff, float repelRadius, Color particleColour, int massSeed = 2938)
        {
            particleGradient = new Gradient();
            MakeGradientColours(particleGradient);

            Perlin2D perlin = new Perlin2D(massSeed);

            particles = new Particle[particlesX, particlesY];

            this.particleSize = particleSize;

            this.repelCoeff = repelCoeff;
            this.repelRadius = repelRadius;

            this.particleColour = particleGradient.Evaluate(0.5f);

            square = makeSquare();

            for(int x = 0; x < particlesX; x++)
            {
                for(int y = 0; y < particlesY; y++)
                {
                    Vector2 position = calculateParticlePosition(width, height, particlesX, particlesY, x, y) + origin;

                    float sample = PerlinNoiseHandler.Sample(perlin, position.X, position.Y, width, height, 4f, 0, 0, 8);
                    //float mapped = Utility_Basic.map(sample, -1f, 1f, 0f, 2f);
                    //float mapped = sample + 1f;

                    particles[x, y] = new Particle(position, sample);
                }
            }
        }

        private void MakeGradientColours(Gradient gradient)
        {
            gradient.AddKeyColour(0f, Color.Black);
            gradient.AddKeyColour(0.25f, Color.DarkBlue);
            gradient.AddKeyColour(0.6f, Color.Orange);
            gradient.AddKeyColour(1f, Color.White);
        }
        
        public void Update(GameTime gameTime)
        {
            foreach(Particle particleRepeller in particles)
            {
                foreach(Particle particleRepelled in particles)
                {
                    if(particleRepeller != particleRepelled && Vector2.DistanceSquared(particleRepeller.position, particleRepelled.position) < repelRadius * repelRadius)
                    {
                        Vector2 direction = particleRepelled.position - particleRepeller.position;
                        direction.Normalize();

                        Vector2 velocityChange = direction * (repelCoeff + particleRepeller.mass);

                        particleRepelled.velocity += velocityChange * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    }
                }
            }

            foreach(Particle particle in particles)
            {
                particle.UpdatePos(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Rectangle source = new Rectangle(0, 0, 1, 1);

            foreach(Particle particle in particles)
            {
                Rectangle destination = new Rectangle((int)particle.position.X - particleSize / 2, (int)particle.position.Y - particleSize / 2, particleSize, particleSize);

                //float opacity = Utility_Basic.invLerp(-.5f, .5f, -particle.mass) * .2f;
                //spriteBatch.Draw(square, destination, source, particleGradient.evaluate(Utility_Basic.invLerp(-.5f, .5f, particle.mass), opacity));
                spriteBatch.Draw(square, destination, source, particleColour);
            }
        }
    }
}
