using Microsoft.Xna.Framework;

namespace Makina.SceneManagement
{
    public class SceneManager
    {
        private Scene _currentScene;

        public Scene CurrentScene => _currentScene; // Read-only property

        public void LoadScene(Scene newScene)
        {
            if (_currentScene != null)
            {
                _currentScene.UnloadContent();
            }

            _currentScene = newScene;
            _currentScene.LoadContent();
        }

        public void Update(GameTime gameTime)
        {
            _currentScene?.Update(gameTime);
        }

        public void Draw(GameTime gameTime)
        {
            _currentScene?.Draw(gameTime);
        }
    }
} 