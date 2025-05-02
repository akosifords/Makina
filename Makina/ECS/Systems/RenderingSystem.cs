using Makina.Components;
using Makina.ECS;
using Makina.Rendering;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Makina.ECS.Systems
{
    /// <summary>
    /// Processes entities with TransformComponent and SpriteComponent to draw them.
    /// </summary>
    public class RenderingSystem : System
    {
        private Renderer _renderer; // We need access to the engine's renderer

        // We can get the Renderer instance via the World or pass it explicitly.
        // For simplicity now, let's assume we retrieve it somehow in Initialize or require it in the constructor.
        // A better approach might involve dependency injection or a service locator.

        public override void Initialize()
        {
            // TODO: Get the Renderer instance. How depends on engine architecture.
            // Example: _renderer = World.GetService<Renderer>();
            // For now, we'll assume it's manually set or passed elsewhere.
            // If not set, Draw will throw an exception.
        }

        public void SetRenderer(Renderer renderer) // Simple manual setting for now
        {
             _renderer = renderer;
        }

        public override void Update(GameTime gameTime)
        {
            // Rendering is usually done in the Draw phase, so Update might be empty.
        }

        public override void Draw(GameTime gameTime)
        {
            if (_renderer == null)
            {
                // Makina.Logging.Logger.Error("RenderingSystem requires a Renderer instance.");
                 throw new InvalidOperationException("Renderer not set for RenderingSystem.");
                // return;
            }

            // Note: World.GetEntitiesWithComponents can be inefficient on every frame.
            // Optimizations like caching entity lists based on component queries (archetypes)
            // are common in more complex ECS implementations.
            var entitiesToDraw = World.GetEntitiesWithComponents(typeof(TransformComponent), typeof(SpriteComponent));

            foreach (var entity in entitiesToDraw)
            {
                var transform = World.GetComponent<TransformComponent>(entity);
                var sprite = World.GetComponent<SpriteComponent>(entity);

                if (sprite.Texture == null) continue; // Skip if no texture assigned

                _renderer.Draw(
                    sprite.Texture,
                    transform.Position,
                    sprite.SourceRectangle,
                    sprite.Color,
                    transform.Rotation,
                    sprite.Origin,
                    transform.Scale, // Use Vector2 scale
                    sprite.Effects,
                    sprite.LayerDepth
                );
            }
        }
    }
} 