using Makina.ECS;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Makina.Components
{
    /// <summary>
    /// Stores data needed to render a sprite for an entity.
    /// </summary>
    public class SpriteComponent : Component
    {
        public Texture2D Texture { get; set; }
        public Color Color { get; set; }
        public Rectangle? SourceRectangle { get; set; } // Optional: for sprite sheets
        public Vector2 Origin { get; set; } // Rotation/scaling pivot, often center
        public SpriteEffects Effects { get; set; }
        public float LayerDepth { get; set; } // For sorting sprites

        public SpriteComponent(Texture2D texture,
                               Color? color = null,
                               Rectangle? sourceRectangle = null,
                               Vector2? origin = null,
                               SpriteEffects effects = SpriteEffects.None,
                               float layerDepth = 0f)
        {
            Texture = texture;
            Color = color ?? Color.White;
            SourceRectangle = sourceRectangle;
            // Default origin to texture center if not specified
            Origin = origin ?? (sourceRectangle.HasValue
                        ? new Vector2(sourceRectangle.Value.Width / 2f, sourceRectangle.Value.Height / 2f)
                        : (texture != null ? new Vector2(texture.Width / 2f, texture.Height / 2f) : Vector2.Zero));
            Effects = effects;
            LayerDepth = layerDepth;
        }
    }
} 