using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Wheel
{
    class DiskGenerator
    {
        public int seedO;
        public string[,] NoGen(Random rand, int rot)
        {
            string[,] alpha = { { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" }, { "-1", "-1", "-1", "-1", "-1", "-1", "-1", "-1", "-1", "-1", "-1", "-1", "-1", "-1", "-1", "-1", "-1", "-1", "-1", "-1", "-1", "-1", "-1", "-1", "-1", "-1" } };
            int seed = rand.Next(99999999);
            seedO = seed;
            bool faker;
            int loco;
            int location;
            Random ran;
            for (int i = 0; i < 26; i++)
            {
                seed += (int)Math.Pow(i, 2);
                ran = new Random(seed);
                location = ran.Next(0, 26);
                loco = 0;
                faker = true;
                if (location + rot > 25)
                {
                    loco = location - 26 + rot;
                }
                else
                {
                    loco = location + rot;
                }
                for (int x = 0; x < 26; x++)
                {
                    if (loco == int.Parse(alpha[1, x]))
                    {
                        i--;
                        faker = false;
                        break;
                    }
                }
                if (faker == true)
                {
                    alpha[1, i] = (loco).ToString();
                }
            }
            return alpha;
        }
        public string[,] Gen(Random rand, int seed, int rot)
        {
            string[,] alpha = { { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" }, { "-1", "-1", "-1", "-1", "-1", "-1", "-1", "-1", "-1", "-1", "-1", "-1", "-1", "-1", "-1", "-1", "-1", "-1", "-1", "-1", "-1", "-1", "-1", "-1", "-1", "-1" } };
            Random ran;
            int location;
            int loco;
            bool faker;
            for (int i = 0; i < 26; i++)
            {
                seed += (int)Math.Pow(i, 2);
                ran = new Random(seed);
                location = ran.Next(0, 26);
                loco = 0;
                faker = true;
                if (location + rot > 25)
                {
                    loco = location - 26 + rot;
                }
                else
                {
                    loco = location + rot;
                }
                for (int x = 0; x < 26; x++)
                {
                    if (loco == int.Parse(alpha[1, x]))
                    {
                        i--;
                        faker = false;
                        break;
                    }
                }
                if (faker == true)
                {
                    alpha[1, i] = (loco).ToString();
                }
            }
            return alpha;
        }
        public string[,] Degen(Random rand, int seed, int rot)
        {
            string[,] alpha = { { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" }, { "-1", "-1", "-1", "-1", "-1", "-1", "-1", "-1", "-1", "-1", "-1", "-1", "-1", "-1", "-1", "-1", "-1", "-1", "-1", "-1", "-1", "-1", "-1", "-1", "-1", "-1" } };
            Random ran;
            int location;
            bool faker;
            int loco;

            for (int i = 0; i < 26; i++)
            {
                seed += (int)Math.Pow(i, 2);
                ran = new Random(seed);
                location = ran.Next(0, 26);
                faker = true;
                loco = 0;
                if (location + rot > 25)
                {
                    loco = location - 26 + rot;
                }
                else
                {
                    loco = location + rot;
                }
                for (int x = 0; x < 26; x++)
                {
                    if (loco == int.Parse(alpha[1, x]))
                    {
                        i--;
                        faker = false;
                        break;
                    }
                }
                if(faker == true)
                {
                    alpha[1, i] = (loco).ToString();
                }
            }
            return alpha;
        }

        public void reset()
        {
            seedO = 0;
        }
    }
}
