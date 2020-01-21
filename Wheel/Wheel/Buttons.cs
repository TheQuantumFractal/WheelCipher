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
    class bTon
    {
        int RealTexture = 1;
        Vector2 Position;
        bool hovering = false;
        bool hover;

        private Texture2D text1, text2;

        public bTon(Vector2 position, Texture2D text1, Texture2D text2, bool hover)
        {
            this.text1 = text1;
            this.text2 = text2;
            Position = position;
            this.hover = hover;
        }
        public int Hover(MouseState ms)
        {
            if (hover == true)
            {
                if (new Rectangle((int)(Position.X), (int)(Position.Y), text1.Width, text1.Height).Contains(new Rectangle(ms.X, ms.Y, 10, 10)) && hovering == false)
                {
                    if (RealTexture == 2)
                    {
                        RealTexture = 1;
                    }
                    else
                    {
                        RealTexture = 2;
                    }
                    hovering = true;
                }
                if ((!new Rectangle((int)(Position.X), (int)(Position.Y), text1.Width, text1.Height).Contains(new Rectangle(ms.X, ms.Y, 5, 5)) && hovering == true))
                {
                    hovering = false;
                }
                if (ms.LeftButton == ButtonState.Pressed && new Rectangle((int)(Position.X), (int)(Position.Y), text1.Width, text1.Height).Contains(new Rectangle(ms.X, ms.Y, 10, 10)))
                {
                    if (RealTexture == 2)
                    {
                        return 0;
                    }
                    else
                    {
                        return 1;
                    }
                }
                return -1;
            }
            return -2;
        }
        public bool Click(MouseState ms)
        {
            if(ms.LeftButton == ButtonState.Pressed && new Rectangle((int)(Position.X), (int)(Position.Y), text1.Width, text1.Height).Contains(new Rectangle(ms.X, ms.Y, 10, 10)))
            {
                return true;
            }
            return false;
        }
        public void Draw(SpriteBatch sprite)
        {
            if (hover == true)
            {
                if (RealTexture == 2)
                {
                    sprite.Draw(text2, Position, Color.White);
                }
                else
                {
                    sprite.Draw(text1, Position, Color.White);
                }
            }
            else
            {
                sprite.Draw(text1, Position, Color.White);
            }
        }
    }
}
