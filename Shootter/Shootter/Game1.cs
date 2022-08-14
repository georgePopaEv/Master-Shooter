using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Shootter
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Texture2D target_value;
        Texture2D background;
        Texture2D crossfire;
        SpriteFont gameFont;
        MouseState mState;
        String score_string = "";
        String final_text = "";
        int score = 0, radius = 45;
        bool ispressed = true;
        float times = 5f, distance;
        Vector2 target_position = new Vector2(45, 45);


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _graphics.PreferredBackBufferWidth = 1280;
            _graphics.PreferredBackBufferHeight = 700;
            _graphics.ApplyChanges();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            target_value = Content.Load<Texture2D>("target");
            background = Content.Load<Texture2D>("sky"); 
            crossfire = Content.Load<Texture2D>("crosshairs");
            gameFont = Content.Load<SpriteFont>("galleryFont");

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            mState = Mouse.GetState();
            distance = Vector2.Distance(target_position, new Vector2(mState.X, mState.Y));
            score_string = "Score : " + score;

            if (times > 0)
            {
                times -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (mState.LeftButton == ButtonState.Pressed && ispressed == true)

                {
                    ispressed = false;
                    if (distance < 50)
                    {
                        score++;
                        Random rnd = new Random();
                        target_position = new Vector2(rnd.Next(radius, 1280 - radius), rnd.Next(radius, 700 - radius));
                    }
                }
                else if (mState.LeftButton == ButtonState.Released)
                    ispressed = true; 
            }
            else 
            {
                final_text = "Felicitari ai reusit sa tintesti " + score;
                score = 0;
            }





            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();

            _spriteBatch.Draw(background, new Vector2(0, 0), Color.White);
            _spriteBatch.Draw(target_value, new Vector2(target_position.X-radius, target_position.Y-radius), Color.White);
            _spriteBatch.Draw(crossfire, new Vector2(mState.X-25, mState.Y-25), Color.White);
            
            _spriteBatch.DrawString(gameFont, score_string, new Vector2(_graphics.PreferredBackBufferWidth - 200 , 5), Color.Red);
            _spriteBatch.DrawString(gameFont, "Time : " + times.ToString("0.00"), new Vector2(0, 5), Color.Red);
            if ((int)times == 0)
            {
                _spriteBatch.DrawString(gameFont, final_text, new Vector2(1280 / 2, 720 / 2), Color.Green);
                //_spriteBatch.DrawString(gameFont, "Time : " + times.ToString("0.00"), new Vector2(0, 5), Color.Red);
                
            }
            
            
            
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
