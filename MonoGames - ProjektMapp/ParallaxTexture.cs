using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGames_ProjektMapp
{
    internal class ParallaxTexture
    {
        private Texture2D _texture;
        private int _positionY;
        
        public float OffsetX { get; set; }

        public ParallaxTexture (Texture2D Texture, int PositionY)
        {
            _positionY = PositionY;
            _texture = Texture;
        }

        public void Draw(SpriteBatch _spriteBatch)
        {
            int width = _spriteBatch.GraphicsDevice.Adapter.CurrentDisplayMode.Width;
            int textureStarX = (int)(OffsetX % _texture.Width);
            int textureWidth = _texture.Width - textureStarX;
            int startX = 0;

            while (startX < width)
            {
                _spriteBatch.Draw(_texture, new Vector2(startX, _positionY), new Rectangle(textureStarX, 0, textureWidth, _texture.Height), Color.White);
                startX += textureWidth;
                textureStarX = 0;
                textureWidth = _texture.Width;
            }
        }



    }
}
