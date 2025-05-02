using Microsoft.Xna.Framework;
using Makina.Rendering;
using Makina.Assets;

namespace Makina.SceneManagement
{
    public class SceneManager
    {
        private Scene _currentScene;
        private AssetManager _assetManager;
        private Renderer _renderer;

        public Scene CurrentScene => _currentScene;

        public void Initialize(AssetManager assetManager, Renderer renderer)
        {
            _assetManager = assetManager;
            _renderer = renderer;
        }

        public void LoadScene(Scene newScene)
        {
            if (_assetManager == null || _renderer == null)
            {
                throw new InvalidOperationException("SceneManager must be initialized before loading a scene.");
            }

            var previousScene = _currentScene;
            _currentScene = newScene;

            _currentScene.InitializeScene(_assetManager, _renderer);
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