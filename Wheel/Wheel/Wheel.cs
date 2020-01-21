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
    public class Wheel
    {
        double radius;
        public double speed;
        char[] alpha;
        Vector2 position;
        Letter[] letters;
        public double rotation;
        SpriteFont font;
        public Wheel(Vector2 position, double radius, double speed, SpriteFont font, ref char[] alpha)
        {
            this.radius = radius;
            this.speed = speed;
            this.alpha = alpha;
            this.position = position;
            this.font = font;
            letters = new Letter[alpha.Length];

            double angle = -(Math.PI)/2;
            for (int i = 0; i < alpha.Length; i++)
            {
                double x = Math.Cos(angle) * radius + position.X;
                double y = Math.Sin(angle) * radius + position.Y;
                letters[i] = new Letter(angle, new Vector2((float)x, (float)y), font, alpha[i]);
                angle += Math.PI * 2 / 26;
            }
        }
        public void Reset()
        {
            letters = new Letter[alpha.Length];

            double angle = -(Math.PI) / 2;
            for (int i = 0; i < alpha.Length; i++)
            {
                double x = Math.Cos(angle) * radius + position.X;
                double y = Math.Sin(angle) * radius + position.Y;
                letters[i] = new Letter(angle, new Vector2((float)x, (float)y), font, alpha[i]);
                angle += Math.PI * 2 / 26;
            }
        }
        public void Move()
        {
            double angle = letters[0].angle;
            rotation += speed/13;
            angle += MathHelper.ToRadians((float)speed);
            for (int i = 0; i < alpha.Length; i++)
            {
                double x = Math.Cos(angle) * radius + position.X;
                double y = Math.Sin(angle) * radius + position.Y;
                letters[i].position = new Vector2((float)x, (float)y);
                letters[i].angle = angle;
                angle += Math.PI * 2 / 26;
            }
        }
        public void Draw(SpriteBatch sprite)
        {
            foreach (Letter letter in letters)
            {
                letter.Draw(sprite);
            }
        }
    }
}
