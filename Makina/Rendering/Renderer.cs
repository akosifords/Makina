using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Makina.Logging;

namespace Makina.Rendering
{
    public class Renderer
    {
        private readonly GraphicsDevice _graphicsDevice;
        private SpriteBatch _spriteBatch;

        public Renderer(GraphicsDevice graphicsDevice)
        {
            _graphicsDevice = graphicsDevice;
        }

        public void LoadContent()
        {
            _spriteBatch = new SpriteBatch(_graphicsDevice);
            Logger.Info("Renderer content loaded.");
        }

        public void BeginDraw(Color? clearColor = null)
        {
            _graphicsDevice.Clear(clearColor ?? Color.CornflowerBlue);
            _spriteBatch.Begin();
        }

        public void EndDraw()
        {
            _spriteBatch.End();
        }

        // Wrapper draw methods
        public void Draw(Texture2D texture, Vector2 position, Color color)
        {
            _spriteBatch.Draw(texture, position, color);
        }

        public void Draw(Texture2D texture, Vector2 position, Rectangle? sourceRectangle, Color color)
        {
             _spriteBatch.Draw(texture, position, sourceRectangle, color);
        }

        public void Draw(Texture2D texture, Vector2 position, Rectangle? sourceRectangle, Color color, float rotation, Vector2 origin, float scale, SpriteEffects effects, float layerDepth)
        {
            _spriteBatch.Draw(texture, position, sourceRectangle, color, rotation, origin, scale, effects, layerDepth);
        }

         public void Draw(Texture2D texture, Vector2 position, Rectangle? sourceRectangle, Color color, float rotation, Vector2 origin, Vector2 scale, SpriteEffects effects, float layerDepth)
        {
            _spriteBatch.Draw(texture, position, sourceRectangle, color, rotation, origin, scale, effects, layerDepth);
        }

        // Add other SpriteBatch.Draw overloads as needed
    }
} 