﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace MonoGames___ProjektMapp
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
        private Texture2D layer1Texture;

        private List<Vector2> cactusis;
        private int cactusTimer = 120;

        private Vector2 position;
        private Vector2 speed;
        private bool isJumping;
        private bool isCrouching;

        //public static int ScreenWidth = 1260;
        //public static int ScreenHeight = 768;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
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
            layer1Texture = Content.Load<Texture2D>("layer1");

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


            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            //GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            _spriteBatch.Draw(backgroundTexture, Vector2.Zero, Color.White);


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