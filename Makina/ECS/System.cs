using Microsoft.Xna.Framework;

namespace Makina.ECS
{
    /// <summary>
    /// Base class for all systems. Systems contain the logic that operates on
    /// entities possessing specific sets of components.
    /// </summary>
    public abstract class System
    {
        protected World World { get; private set; }

        internal void SetWorld(World world)
        {
            World = world;
        }

        /// <summary>
        /// Called once when the system is registered with the World.
        /// </summary>
        public virtual void Initialize() { }

        /// <summary>
        /// Called once per frame to update the system's logic.
        /// </summary>
        /// <param name="gameTime">Provides snapshot of timing values.</param>
        public abstract void Update(GameTime gameTime);

        /// <summary>
        /// Optional: Called once per frame for drawing logic related to this system.
        /// Often, a dedicated RenderingSystem handles all drawing.
        /// </summary>
        /// <param name="gameTime">Provides snapshot of timing values.</param>
        public virtual void Draw(GameTime gameTime) { }
    }
} 