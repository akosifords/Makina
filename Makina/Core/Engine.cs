using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Makina.Logging;       // Add using for Logger
using Makina.Rendering;     // Add using for Renderer
using Makina.SceneManagement; // Add using for SceneManager

namespace Makina.Core
{
    public class Engine
    {
        private readonly SceneManager _sceneManager;
        private readonly Renderer _renderer;

        // TODO: Initialize other engine systems (input, etc.)

        public Engine(GraphicsDevice graphicsDevice /*, ContentManager content */)
        {
            _renderer = new Renderer(graphicsDevice);
            _sceneManager = new SceneManager();
            // TODO: Load initial scene
        }

        public void Initialize()
        {
            Logger.Info("Engine Initializing...");
            // Initialization logic for other systems
            Logger.Info("Engine Initialized.");
        }

        public void LoadContent()
        {
            Logger.Info("Engine Loading Content...");
            _renderer.LoadContent();
            // TODO: Load scene content via SceneManager
            Logger.Info("Engine Content Loaded.");
        }

        public void Update(GameTime gameTime)
        {
            // Update other systems (e.g., Input)
            _sceneManager.Update(gameTime); // Delegate update to current scene
        }

        public void Draw(GameTime gameTime)
        {
            _renderer.BeginDraw();
            _sceneManager.Draw(gameTime);
            _renderer.EndDraw();
        }
    }
} 