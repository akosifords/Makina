using Makina.ECS;
using Microsoft.Xna.Framework;
// using Microsoft.Xna.Framework.Content; // No longer needed directly
using Makina.Rendering;
using Makina.Assets; // Add Assets using

namespace Makina.SceneManagement
{
    public abstract class Scene
    {
        public World World { get; protected set; }
        // protected ContentManager Content { get; private set; } // Use AssetManager instead
        protected AssetManager Assets { get; private set; } // Give scenes access to AssetManager
        protected Renderer Renderer { get; private set; } // Still useful for systems like RenderingSystem

        // Called by SceneManager when loading the scene
        internal void InitializeScene(AssetManager assetManager, Renderer renderer)
        {
            Assets = assetManager;
            Renderer = renderer;
            World = new World();
            // Optionally register core systems common to all scenes here?
            RegisterSystems();
        }

        /// <summary>
        /// Register scene-specific ECS systems.
        /// </summary>
        protected virtual void RegisterSystems() { }

        /// <summary>
        /// Load scene-specific content (using this.Assets) and create initial entities.
        /// </summary>
        public abstract void LoadContent();

        /// <summary>
        /// Unload scene-specific content.
        /// (Note: AssetManager.UnloadAll() is usually called by Engine/SceneManager if needed)
        /// </summary>
        public abstract void UnloadContent();

        /// <summary>
        /// Update the scene's ECS world.
        /// </summary>
        public virtual void Update(GameTime gameTime)
        {
            World?.UpdateSystems(gameTime);
        }

        /// <summary>
        /// Draw the scene's ECS world.
        /// </summary>
        public virtual void Draw(GameTime gameTime)
        {
            World?.DrawSystems(gameTime);
        }
    }
} 