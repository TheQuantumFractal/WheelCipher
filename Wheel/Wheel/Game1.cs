using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Threading;

namespace Wheel
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        KeyboardState k;
        DiskGenerator disk;
        Texture2D text1;
        Texture2D text2;
        Texture2D clear;
        int seed;
        int UltimateBottomIndex;
        int index;
        int TotallinesCount;
        int linecount;
        int uptimes;
        bTon ClearBton;
        MouseState ms;
        double RotationValue;
        Random rand;
        bool right = false;
        bool left = false;
        Wheel wheel;
        Wheel cipherW;
        bTon BTon;
        string userInput = string.Empty;
        string storageUserInput = string.Empty;
        double cRadius = 175;
        double radius = 215;
        char[] alpha = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
        char[] cipher = new char[26];
        TimeSpan KeyboardTime = TimeSpan.Zero;
        TimeSpan BackTime = new TimeSpan(0, 0, 0, 0, 110);
        TimeSpan Btime = TimeSpan.Zero;
        TimeSpan PressTime = new TimeSpan(0, 0, 0, 0, 250);
        TimeSpan time = TimeSpan.Zero;
        TimeSpan enterTime = new TimeSpan(0, 0, 0, 0, 500);
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            this.IsMouseVisible = true;
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            text1 = Content.Load<Texture2D>("Encrypt");
            text2 = Content.Load<Texture2D>("Decrypt");
            clear = Content.Load<Texture2D>("Clear");
            rand = new Random();
            disk = new DiskGenerator();
            BTon = new bTon(new Vector2(10, 10), text1, text2, true);
            string [,] temp = disk.NoGen(rand, 0);
            seed = disk.seedO;
            disk.reset();
            ClearBton = new bTon(new Vector2(GraphicsDevice.Viewport.Width - clear.Width, 0), clear, null, false);
            for (int x = 0; x < cipher.Length; x++)
            {
                cipher[Convert.ToInt32(temp[1, x])] = alpha[x];
            }
            cipherW = new Wheel(new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2), cRadius, 0, Content.Load<SpriteFont>("SpriteFont1"), ref cipher);
            wheel = new Wheel(new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2), radius, 0, Content.Load<SpriteFont>("SpriteFont1"), ref alpha);
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }


        KeyboardState lastKS;
        protected override void Update(GameTime gameTime)
        {
            int seedO = seed;
            string[,] temp2 = disk.Gen(rand, seed, Convert.ToInt32(Math.Round(RotationValue)));
            for (int x = 0; x < cipher.Length; x++)
            {
                cipher[Convert.ToInt32(temp2[1, x])] = alpha[x];
            }
            ms = Mouse.GetState();
            time += gameTime.ElapsedGameTime;
            linecount = 0;
            if(ClearBton.Click(ms))
            {
                storageUserInput = string.Empty;
            }
            int returned = BTon.Hover(ms);
            if (returned != -1)
            {
                string outputString = string.Empty;
                if (returned == 1)
                {
                    //Encrypt
                    foreach (char c in storageUserInput)
                    {
                        int cipherPlace = 0;
                        if (char.IsLetter(c))
                        {
                            for (int x = 0; x < cipher.Count(); x++)
                            {
                                if (cipher[x] == c)
                                {
                                    cipherPlace = x;
                                    break;
                                }
                            }
                            outputString += alpha[cipherPlace];
                        }
                        else if (c != '\n')
                        {
                            outputString += c;
                        }
                    }
                }
                else
                {
                    //Decrypt
                    foreach (char c in storageUserInput)
                    {
                        if (char.IsLetter(c))
                        {
                            int alphaPlace = Convert.ToInt32(c) - 65;
                            outputString += cipher[alphaPlace];
                        }
                        else if (c != '\n')
                        {
                            outputString += c;
                        }
                    }
                }
                if (outputString == string.Empty)
                {
                    outputString = " ";
                }
                System.Windows.Forms.Clipboard.SetText(outputString, System.Windows.Forms.TextDataFormat.UnicodeText);
            }
            if (uptimes == 0)
            {
                userInput = storageUserInput;
            }
            foreach (char letter in userInput)
            { 
                if(letter == '\n')
                {
                    linecount++;
                }
            }
            if (linecount > 12)
            {
                int LinePosition = 0;
                int Lines = 0;
                for(int x = storageUserInput.Count() - 1; x > 0; x--)
                {
                    if(storageUserInput[x] == '\n')
                    {
                        Lines++;
                        if(Lines == 12)
                        {
                            LinePosition = x;
                        }
                    }
                }
                userInput = storageUserInput.Substring(LinePosition + 1, storageUserInput.Count() - LinePosition - 1);
            }
            index = userInput.LastIndexOf(' ');
            int index2 = storageUserInput.LastIndexOf(' ');
            string part = userInput.Substring(index + 1, userInput.Count() - (index + 1));
            if ((userInput.Count() - userInput.LastIndexOf('\n')) >= 29 && part.Contains('\n'))
            {
                storageUserInput += '\n';
                TotallinesCount++;
            }
            else if ((userInput.Count() - userInput.LastIndexOf('\n')) >= 29 && userInput.Count() >= 0 && index > 0)
            {
                string tempString = string.Empty;
                tempString = storageUserInput.Substring(0, index2);
                tempString += '\n';
                tempString += storageUserInput.Substring(index2 + 1, storageUserInput.Count() - (index2 + 1));

                storageUserInput = tempString;
                TotallinesCount++;
            }
            else if((userInput.Count() - userInput.LastIndexOf('\n')) >= 29 && userInput.Count() >= 29)
            {
                storageUserInput += '\n';
                TotallinesCount++;
            }
            KeyboardTime += gameTime.ElapsedGameTime;
            k = Keyboard.GetState();
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            Keys[] pressedKeys = k.GetPressedKeys();
            if (!pressedKeys.Contains(Keys.Back))
            {
                Btime = TimeSpan.Zero;
            }
            Keys[] lastPressedKeys = lastKS.GetPressedKeys();
            bool ctrl = false;
            if (pressedKeys.Contains(Keys.LeftControl))
            {
                ctrl = true;
            }
            for (int i = 0; i < pressedKeys.Length; i++)
            {
                if (!lastPressedKeys.Contains(pressedKeys[i]))
                {
                    //did the last frame detect the same key? If it did then the person is holding it down, do don't add the key
                    switch (pressedKeys[i])
                    {
                        case Keys.A:
                            storageUserInput += "A";
                            break;
                        case Keys.B:
                            storageUserInput += "B";
                            break;
                        case Keys.C:
                            storageUserInput += "C";
                            break;
                        case Keys.D:
                            storageUserInput += "D";
                            break;
                        case Keys.E:
                            storageUserInput += "E";
                            break;
                        case Keys.F:
                            storageUserInput += "F";
                            break;
                        case Keys.G:
                            storageUserInput += "G";
                            break;
                        case Keys.H:
                            storageUserInput += "H";
                            break;
                        case Keys.I:
                            storageUserInput += "I";
                            break;
                        case Keys.J:
                            storageUserInput += "J";
                            break;
                        case Keys.K:
                            storageUserInput += "K";
                            break;
                        case Keys.L:
                            storageUserInput += "L";
                            break;
                        case Keys.M:
                            storageUserInput += "M";
                            break;
                        case Keys.N:
                            storageUserInput += "N";
                            break;
                        case Keys.O:
                            storageUserInput += "O";
                            break;
                        case Keys.P:
                            storageUserInput += "P";
                            break;
                        case Keys.Q:
                            storageUserInput += "Q";
                            break;
                        case Keys.R:
                            storageUserInput += "R";
                            break;
                        case Keys.S:
                            storageUserInput += "S";
                            break;
                        case Keys.T:
                            storageUserInput += "T";
                            break;
                        case Keys.U:
                            storageUserInput += "U";
                            break;
                        case Keys.W:
                            storageUserInput += "W";
                            break;
                        case Keys.X:
                            storageUserInput += "X";
                            break;
                        case Keys.Y:
                            storageUserInput += "Y";
                            break;
                        case Keys.Z:
                            storageUserInput += "Z";
                            break;
                        case Keys.Space:
                            storageUserInput += " ";
                            break;
                        case Keys.OemQuotes:
                            storageUserInput += "\"";
                            break;
                        case Keys.OemQuestion:
                            storageUserInput += "?";
                            break;
                        case Keys.OemPeriod:
                            storageUserInput += ".";
                            break;
                        case Keys.OemComma:
                            storageUserInput += ",";
                            break;
                        case Keys.NumPad0:
                            if(ctrl == false)
                            {
                                storageUserInput += '0';
                            }
                            else if (seed < 99999999)
                            {
                                seed = Convert.ToInt32(Convert.ToString(seed) + "0");
                            }
                            break;
                        case Keys.NumPad1:
                            if (ctrl == false)
                            {
                                storageUserInput += '1';
                            }
                            else if (seed < 99999999)
                            {
                                seed = Convert.ToInt32(Convert.ToString(seed) + "1");
                            }
                            break;
                        case Keys.NumPad2:
                            if (ctrl == false)
                            {
                                storageUserInput += '2';
                            }
                            else if (seed < 99999999)
                            {
                                seed = Convert.ToInt32(Convert.ToString(seed) + "2");
                            }
                            break;
                        case Keys.NumPad3:
                            if (ctrl == false)
                            {
                                storageUserInput += '3';
                            }
                            else if (seed < 99999999)
                            {
                                seed = Convert.ToInt32(Convert.ToString(seed) + "3");
                            }
                            break;
                        case Keys.NumPad4:
                            if (ctrl == false)
                            {
                                storageUserInput += '4';
                            }
                            else if (seed < 99999999)
                            {
                                seed = Convert.ToInt32(Convert.ToString(seed) + "4");
                            }
                            break;
                        case Keys.NumPad5:
                            if (ctrl == false)
                            {
                                storageUserInput += '5';
                            }
                            else if (seed < 99999999)
                            {
                                seed = Convert.ToInt32(Convert.ToString(seed) + "5");
                            }
                            break;
                        case Keys.NumPad6:
                            if (ctrl == false)
                            {
                                storageUserInput += '6';
                            }
                            else if (seed < 99999999)
                            {
                                seed = Convert.ToInt32(Convert.ToString(seed) + "6");
                            }
                            break;
                        case Keys.NumPad7:
                            if (ctrl == false)
                            {
                                storageUserInput += '7';
                            }
                            else if (seed < 99999999)
                            {
                                seed = Convert.ToInt32(Convert.ToString(seed) + "7");
                            }
                            break;
                        case Keys.NumPad8:
                            if (ctrl == false)
                            {
                                storageUserInput += '8';
                            }
                            else if (seed < 99999999)
                            {
                                seed = Convert.ToInt32(Convert.ToString(seed) + "8");
                            }
                            break;
                        case Keys.NumPad9:
                            if (ctrl == false)
                            {
                                storageUserInput += '9';
                            }
                            else if (seed < 99999999)
                            {
                                seed = Convert.ToInt32(Convert.ToString(seed) + "9");
                            }
                            break;
                        case Keys.Enter:
                            storageUserInput += '\n';
                            TotallinesCount++;
                            break;
                        case Keys.V:
                            if (ctrl == false)
                            {
                                storageUserInput += "V";
                            }
                            else
                            {
                                storageUserInput += System.Windows.Forms.Clipboard.GetData(System.Windows.Forms.DataFormats.UnicodeText);
                            }
                            break;
                        case Keys.Up:
                            if (uptimes < TotallinesCount - 12)
                            {
                                uptimes++;
                            }
                            break;
                        case Keys.Down:
                            if(uptimes > 0)
                            {
                                uptimes--;
                            }
                            break;
                    }
                }
                //BackSpace
                if (pressedKeys[i] == Keys.Back)
                {
                    if (ctrl == true)
                    {
                        seed = 0;
                    }
                    else
                    {
                        if (!lastPressedKeys.Contains(Keys.Back) || Btime >= BackTime)
                        {
                            if (userInput.Count() >= 1)
                            {
                                if (UltimateBottomIndex > 1 && storageUserInput[UltimateBottomIndex - 1] == '\n')
                                {
                                    string sub1 = storageUserInput.Substring(0, UltimateBottomIndex - 1);
                                    string sub2 = string.Empty;
                                    if (UltimateBottomIndex != storageUserInput.Count() - 1)
                                    {
                                        sub2 = storageUserInput.Substring(UltimateBottomIndex , storageUserInput.Count() - UltimateBottomIndex - 1);
                                    }
                                    storageUserInput = sub1 + sub2;
                                    TotallinesCount--;
                                }
                                else
                                {
                                    
                                    string sub1 = storageUserInput.Substring(0, UltimateBottomIndex);
                                    string sub2 = string.Empty;
                                    if (UltimateBottomIndex != storageUserInput.Length - 1)
                                    {
                                        sub2 = storageUserInput.Substring(UltimateBottomIndex, storageUserInput.Count() - UltimateBottomIndex - 1);
                                    }
                                    storageUserInput = sub1 + sub2;
                                }
                            }
                            if (linecount < 12 && storageUserInput.Count() > userInput.Count())
                            {
                                string sub1 = storageUserInput.Substring(0, UltimateBottomIndex);
                                string sub2 = string.Empty;
                                if (UltimateBottomIndex != storageUserInput.Count() - 1)
                                {
                                    sub2 = storageUserInput.Substring(UltimateBottomIndex + 1, storageUserInput.Count() - UltimateBottomIndex - 2);
                                }
                                storageUserInput = sub1 + sub2;
                            }
                            Btime = TimeSpan.Zero;
                        }
                        Btime += gameTime.ElapsedGameTime;
                    }
                }
            }
            disk.seedO = seed;
            if (uptimes < TotallinesCount - 11 && uptimes > 0)
            {
                //Number of times '\n' is found in storageuserinput
                int enterCount = 0;
                //The bottom index for userinput substring of storageuserinput (bottom of screen)
                int bottomindex = 0;
                //The top index for userinput substring of storageuserinput (top of screen)
                int topindex = 0;
                bool topper = true;
                //For loop through all characters of storageuserinput
                if(TotallinesCount - uptimes - 12 == 0)
                {
                    topindex = 0;
                    topper = false;
                }
                for (int x = 0; x < storageUserInput.Count(); x++)
                {
                    //Finds if the current char is '\n'
                    if (storageUserInput[x] == '\n')
                    {
                        //Increments the number of times '\n' is found
                        enterCount++;
                        if (enterCount == 12 && !topper)
                        {
                            bottomindex = x;
                            UltimateBottomIndex = x;
                            break;
                        }
                        if (enterCount == TotallinesCount - uptimes - 12 && topper)
                        {
                            topindex = x + 1;
                        }
                        //Finds if the current entercount is equal to the total # of lines minus the number of times the up key is pressed (therefore bottom line of userinput)
                        else if (enterCount == TotallinesCount - uptimes + 1 && topper)
                        {
                            bottomindex = x;
                            break;
                        }
                        //Finds if the current entercount is equal to the total # of lines minus the number of times the up key is pressed - 12 (therefore top line of userinput)

                    }
                }
                userInput = storageUserInput.Substring(topindex, bottomindex - topindex);
            }
            if (KeyboardTime >= PressTime)
            {
                if (k.IsKeyDown(Keys.Left))
                {
                    left = true;
                    cipherW.speed = -360d / 13d/2;
                    KeyboardTime = TimeSpan.Zero;
                }

                else if (k.IsKeyDown(Keys.Right))
                {
                    right = true;
                    cipherW.speed = 360d / 13d/2;
                    KeyboardTime = TimeSpan.Zero;
                }
            }
            else
            {
                cipherW.speed = 0;
            }
            if (k.IsKeyUp(Keys.Left) && left)
            {
                cipherW.speed = 0;
                KeyboardTime = TimeSpan.Zero;
                left = false;
            }
            if (k.IsKeyUp(Keys.Right) && right)
            {
                cipherW.speed = 0;
                KeyboardTime = TimeSpan.Zero;
                right = false;
            }
            // TODO: Add your update logic here
            cipherW.Move();
            if(Convert.ToInt32(cipherW.rotation) < 0)
            {
                cipherW.rotation += 26;
            }
            if(Convert.ToInt32(cipherW.rotation) > 25)
            {
                cipherW.rotation -= 26;
            }
            RotationValue = cipherW.rotation;
            lastKS = k;
            base.Update(gameTime);
            if(seedO != seed)
            {
                string[,] temp = disk.Gen(rand, seed, Convert.ToInt32(Math.Round(RotationValue)));
                disk.reset();
                for (int x = 0; x < cipher.Length; x++)
                {
                    cipher[Convert.ToInt32(temp[1, x])] = alpha[x];
                }
                cipherW.Reset();
            }

            if (uptimes == 0)
            {
                UltimateBottomIndex = storageUserInput.Count() - 1;
            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            spriteBatch.Begin();
            wheel.Draw(spriteBatch);
            cipherW.Draw(spriteBatch);
            spriteBatch.DrawString(Content.Load<SpriteFont>("SpriteFont2"), "ROT: " + Convert.ToString(Math.Round(RotationValue)), new Vector2(GraphicsDevice.Viewport.Width * 5 / 6 , GraphicsDevice.Viewport.Height / 2), Color.White);
            spriteBatch.DrawString(Content.Load<SpriteFont>("SpriteFont2"), "Seed: " + Convert.ToString(seed), new Vector2(GraphicsDevice.Viewport.Width * 5 / 6, GraphicsDevice.Viewport.Height * 4 / 9), Color.White);
            spriteBatch.DrawString(Content.Load<SpriteFont>("SpriteFont2"), userInput, new Vector2((GraphicsDevice.Viewport.Width-235)/2, (GraphicsDevice.Viewport.Height-240)/2), Color.White);
            BTon.Draw(spriteBatch);
            ClearBton.Draw(spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
