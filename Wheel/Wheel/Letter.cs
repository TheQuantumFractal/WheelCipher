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
    public class Letter
    {
        public double angle;
        public Vector2 position;
        SpriteFont font;
        char letter;
        public Letter(double angle, Vector2 position, SpriteFont font,  char letter)
        {
            this.angle = angle;
            this.position = position;
            this.letter = letter;
            this.font = font;
        }
        public void Draw(SpriteBatch sprite)
        {
            Vector2 measure = font.MeasureString(letter.ToString());
            sprite.DrawString(font, letter.ToString(), position, Color.White, 0, measure/2, 1, SpriteEffects.None, 1);
        }
    }
}
