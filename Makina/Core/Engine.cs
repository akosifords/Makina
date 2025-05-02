using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Makina.Logging;       // Add using for Logger
using Makina.Rendering;     // Add using for Renderer
using Makina.SceneManagement; // Add using for SceneManager
using Makina.Input; // Add Input using
using Makina.Assets; // Add Assets using
// using Makina.Input; // Example for future input system

namespace Makina.Core
{
    // Add IDisposable if Engine owns disposable resources like AssetManager
    public class Engine : IDisposable
    {
        private readonly SceneManager _sceneManager;
        private readonly Renderer _renderer;
        private readonly InputManager _inputManager;
        private readonly AssetManager _assetManager; // Add AssetManager instance
        private bool _isDisposed = false;

        // TODO: Initialize other engine systems (audio? physics?)

        // Accept ContentManager in constructor
        public Engine(GraphicsDevice graphicsDevice, ContentManager content)
        {
            _assetManager = new AssetManager(content); // Instantiate AssetManager
            _renderer = new Renderer(graphicsDevice);
            _sceneManager = new SceneManager();
            _inputManager = new InputManager(); // Instantiate InputManager
            // _inputManager = new InputManager(); // Example

            // TODO: Load initial scene - Needs a concrete Scene class (e.g., MainMenuScene)
            // Example:
            // _sceneManager.LoadScene(new GameplayScene());
        }

        public void Initialize()
        {
            Logger.Info("Engine Initializing...");
            _inputManager.Initialize(); // Initialize InputManager
            // Initialize SceneManager with needed references (now including AssetManager)
            _sceneManager.Initialize(_assetManager, _renderer);
            // Initialize other systems (Input, etc.)
            // _inputManager.Initialize();
            Logger.Info("Engine Initialized.");
        }

        public void LoadContent()
        {
            Logger.Info("Engine Loading Content...");
            _renderer.LoadContent();
            // Core engine assets could be loaded via _assetManager here if needed
            Logger.Info("Engine Content Loaded.");
            // Load Initial Scene *after* core content (renderer) is loaded and SceneManager is initialized.
            // This needs a default scene implementation.
            LoadInitialScene();
        }

        private void LoadInitialScene()
        {
            // TODO: Replace this with your actual starting scene
            // Example: _sceneManager.LoadScene(new GameplayScene());
            Logger.Warning("No initial scene loaded. Create a Scene implementation and load it in Engine.LoadInitialScene().");
        }

        public void Update(GameTime gameTime)
        {
            if (_isDisposed) return;
            // Update core systems first
            _inputManager.Update(gameTime); // Update InputManager state

            // Then update the current scene (which updates its ECS systems)
            _sceneManager.Update(gameTime);
        }

        public void Draw(GameTime gameTime)
        {
            if (_isDisposed) return;
            _renderer.BeginDraw(); // Handled by Renderer
            _sceneManager.Draw(gameTime); // Scene draws its ECS systems (like RenderingSystem)
            _renderer.EndDraw(); // Handled by Renderer
        }

        // Implement IDisposable to dispose owned resources like AssetManager
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_isDisposed)
            {
                if (disposing)
                {
                    // Dispose managed state (managed objects).
                    _assetManager?.Dispose(); // Dispose the AssetManager
                    // Dispose other managed resources owned by Engine here
                }
                _isDisposed = true;
                Logger.Info("Engine disposed.");
            }
        }
    }
} 