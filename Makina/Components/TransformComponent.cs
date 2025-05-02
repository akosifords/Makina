using Makina.ECS;
using Microsoft.Xna.Framework;

namespace Makina.Components
{
    /// <summary>
    /// Stores position, rotation, and scale data for an entity.
    /// </summary>
    public class TransformComponent : Component
    {
        public Vector2 Position { get; set; }
        public float Rotation { get; set; }
        public Vector2 Scale { get; set; }

        public TransformComponent(Vector2? position = null, float rotation = 0f, Vector2? scale = null)
        {
            Position = position ?? Vector2.Zero;
            Rotation = rotation;
            Scale = scale ?? Vector2.One; // Default scale to 1,1
        }
    }
} 