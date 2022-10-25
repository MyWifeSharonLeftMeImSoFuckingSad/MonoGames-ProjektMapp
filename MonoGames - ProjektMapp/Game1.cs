using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace MonoGames_ProjektMapp
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Texture2D normalTexture;
        private Texture2D jumpingTexture;
        private Texture2D crouchTexture;
        private Texture2D cactus1Texture;
        private Texture2D backgroundTexture;
        private Texture2D layer1_oneTexture;
        private ParallaxTexture layer1_one;

        private Texture2D currenTexture;

        private List<Vector2> cactusis;
        private int cactusTimer = 120;

        private Vector2 position;
        private Vector2 speed;
        private bool isJumping;
        private bool isCrouching;

        private SpriteFont font;

        //public static int ScreenWidth = 1260;
        //public static int ScreenHeight = 768;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = false;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            position = new Vector2(200, 300);

            cactusis = new List<Vector2>();
            

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            normalTexture = Content.Load<Texture2D>("normal");
            jumpingTexture = Content.Load<Texture2D>("jump");
            crouchTexture = Content.Load<Texture2D>("crouch");
            cactus1Texture = Content.Load<Texture2D>("cactus1");

            backgroundTexture = Content.Load<Texture2D>("background");
            layer1_oneTexture = Content.Load<Texture2D>("layer1");
            layer1_one = new ParallaxTexture(layer1_oneTexture, 230);

            font = Content.Load<SpriteFont>("font");

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            position += speed;

            if(position.Y > 200)
            {
                position = new Vector2(position.X, 200);
                speed = Vector2.Zero;
                isJumping = false;
            }
            speed += new Vector2(0, 1f);

            var state = Keyboard.GetState();
            if (state.IsKeyDown(Keys.W) && !isJumping)
            {
                speed = new Vector2(0, -17.0f);
                isJumping = true;
            }

            if (state.IsKeyDown(Keys.S) && !isJumping)
            {
                isCrouching = true;
            }
            else
            {
                isCrouching = false;
            }

            

            //CactusTimer
            cactusTimer--;
            if(cactusTimer <= 0)
            {
                cactusTimer = 120;
                cactusis.Add(new Vector2( 800, 230));
            }

            for(int i = 0; i < cactusis.Count; i++)
            {
                cactusis[i] = cactusis[i] + new Vector2(-5, 0);
            }

            layer1_one.OffsetX += 3.0f;

            //collision

            if (isJumping)
            {
                _spriteBatch.Draw(jumpingTexture, position, Color.White);
            }
            else if (isCrouching)
            {
                _spriteBatch.Draw(crouchTexture, position, Color.White);
            }
            else
            {
                _spriteBatch.Draw(normalTexture, position, Color.White);
            }

            //Rectangle playerBox = new Rectangle((int)position.X, (int)position.Y, 40, 58);
            //Rectangle cactusBox = new Rectangle((int)position.X, (int)position.Y, 40, 58);


            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            //GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            _spriteBatch.Draw(backgroundTexture, Vector2.Zero, Color.White);
            layer1_one.Draw(_spriteBatch);


            _spriteBatch.DrawString(font, ((int)gameTime.TotalGameTime.TotalSeconds).ToString(),
                new Vector2(10, 20), Color.Black);

           

            foreach(var cactus in cactusis)
            {
                _spriteBatch.Draw(cactus1Texture, cactus, Color.White);
            }
            _spriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}