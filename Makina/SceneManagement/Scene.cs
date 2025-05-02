using Microsoft.Xna.Framework;

namespace Makina.SceneManagement
{
    public abstract class Scene
    {
        public abstract void LoadContent();
        public abstract void UnloadContent();
        public abstract void Update(GameTime gameTime);
        public abstract void Draw(GameTime gameTime);
    }
} 