using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace UniverseSimulator.UniverseSimulatorStuff
{
    public class Gradient
    {
        Dictionary<float, Color> keyColours;

        public Gradient()
        {
            keyColours = new Dictionary<float, Color>();
        }

        public Color Evaluate(float time, float alpha = 1)
        {
            time = Math.Clamp(time, 0f, 1f);

            if(keyColours.ContainsKey(time))
            {
                //Console.WriteLine(new Color(GetValueFromDictionary(time), Utility_Basic.lerp(0, 255, alpha)));
                return new Color(GetValueFromDictionary(time), Utility_Basic.lerp(0, 255, alpha));
            }

            else
            {
                float leftKey = float.NegativeInfinity;
                float rightKey = float.PositiveInfinity;

                foreach(float key in keyColours.Keys)
                {
                    if (key < time)
                    {
                        if (key > leftKey)
                        {
                            leftKey = key;
                        }
                    }
                    else
                    {
                        if (key < rightKey)
                        {
                            rightKey = key;
                        }
                    }
                }

                Color leftColour = GetValueFromDictionary(leftKey);
                Color rightColour = GetValueFromDictionary(rightKey);

                //Console.WriteLine(leftColour);
                //Console.WriteLine(rightColour);

                Vector3 leftInvLerp = (leftColour * Utility_Basic.invLerp(rightKey, leftKey, time)).ToVector3();
                Vector3 rightInvLerp = (rightColour * Utility_Basic.invLerp(leftKey, rightKey, time)).ToVector3();

                //Console.WriteLine(leftInvLerp);
                //Console.WriteLine(rightInvLerp);
                //
                //Console.WriteLine("----------");
                //
                //Console.WriteLine(invLerp(leftKey, rightKey, time));
                //Console.WriteLine(invLerp(rightKey, leftKey, time));

                Vector3 output = leftInvLerp + rightInvLerp;

                Color outputColour = new Color(output);

                //Console.WriteLine(output);
                //
                //Console.WriteLine("======================================");

                //Console.WriteLine(new Color(outputColour.R, outputColour.G, outputColour.B, Utility_Basic.lerp(0, 255, alpha)));

                return new Color(outputColour.R, outputColour.G, outputColour.B, Utility_Basic.lerp(0, 255, alpha));
            }
        }

        public Texture2D GetTexture(int resolution)
        {
            Texture2D gradient = new Texture2D(Game1.graphics.GraphicsDevice, resolution, 1);
            Color[] data = new Color[resolution];

            for(int i = 0; i < resolution; i++)
            {
                data[i] = Evaluate((float)i / resolution);
            }

            gradient.SetData(data);
            return gradient;
        }

        public void AddKeyColour(float time, Color colour)
        {
            KeyValuePair<float, Color> pair = new KeyValuePair<float, Color>(time, colour);
            if(!keyColours.Contains(pair))
            {
                keyColours.Add(time, colour);
            }
        }

        public void RemoveKeyColour(float time)
        {
            if(keyColours.ContainsKey(time))
            {
                keyColours.Remove(time);
            }
        }

        private Color GetValueFromDictionary(float key)
        {
            Color output;
            keyColours.TryGetValue(key, out output);
            return output;
        }
    }
}
