using Makina.ECS;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Makina.Rendering; // For potentially passing Renderer

namespace Makina.SceneManagement
{
    public abstract class Scene
    {
        public World World { get; protected set; }
        protected ContentManager Content { get; private set; } // Give scenes access to ContentManager
        protected Renderer Renderer { get; private set; } // Give scenes access to Renderer

        // Called by SceneManager when loading the scene
        internal void InitializeScene(ContentManager content, Renderer renderer)
        {
            Content = content;
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
        /// Load scene-specific content and create initial entities.
        /// </summary>
        public abstract void LoadContent();

        /// <summary>
        /// Unload scene-specific content.
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