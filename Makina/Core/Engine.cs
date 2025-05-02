using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Makina.Logging;       // Add using for Logger
using Makina.Rendering;     // Add using for Renderer
using Makina.SceneManagement; // Add using for SceneManager
// using Makina.Input; // Example for future input system

namespace Makina.Core
{
    public class Engine
    {
        private readonly SceneManager _sceneManager;
        private readonly Renderer _renderer;
        private readonly ContentManager _content; // Store ContentManager
        // private readonly InputManager _inputManager; // Example

        // TODO: Initialize other engine systems (input, etc.)

        // Accept ContentManager in constructor
        public Engine(GraphicsDevice graphicsDevice, ContentManager content)
        {
            _content = content;
            _renderer = new Renderer(graphicsDevice);
            _sceneManager = new SceneManager();
            // _inputManager = new InputManager(); // Example

            // TODO: Load initial scene - Needs a concrete Scene class (e.g., MainMenuScene)
            // Example:
            // _sceneManager.LoadScene(new GameplayScene());
        }

        public void Initialize()
        {
            Logger.Info("Engine Initializing...");
            // Initialize SceneManager with needed references
            _sceneManager.Initialize(_content, _renderer);
            // Initialize other systems (Input, etc.)
            // _inputManager.Initialize();
            Logger.Info("Engine Initialized.");
        }

        public void LoadContent()
        {
            Logger.Info("Engine Loading Content...");
            _renderer.LoadContent();
            // Scene content is now loaded via SceneManager.LoadScene -> Scene.LoadContent
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
            // Update core systems first (like Input)
            // _inputManager.Update(gameTime);

            // Then update the current scene (which updates its ECS systems)
            _sceneManager.Update(gameTime);
        }

        public void Draw(GameTime gameTime)
        {
            _renderer.BeginDraw(); // Handled by Renderer
            _sceneManager.Draw(gameTime); // Scene draws its ECS systems (like RenderingSystem)
            _renderer.EndDraw(); // Handled by Renderer
        }
    }
} 