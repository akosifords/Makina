using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Makina.Rendering;

namespace Makina.SceneManagement
{
    public class SceneManager
    {
        private Scene _currentScene;
        private ContentManager _content;
        private Renderer _renderer;

        public Scene CurrentScene => _currentScene;

        public void Initialize(ContentManager content, Renderer renderer)
        {
            _content = content;
            _renderer = renderer;
        }

        public void LoadScene(Scene newScene)
        {
            if (_content == null || _renderer == null)
            {
                throw new InvalidOperationException("SceneManager must be initialized before loading a scene.");
            }

            var previousScene = _currentScene;
            _currentScene = newScene;

            _currentScene.InitializeScene(_content, _renderer);
            _currentScene.LoadContent();

            previousScene?.UnloadContent();
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