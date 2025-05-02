namespace Makina.ECS
{
    /// <summary>
    /// Represents a unique game object. Contains no data or logic itself,
    /// acting as a container for components.
    /// </summary>
    public readonly struct Entity
    {
        /// <summary>
        /// The unique identifier for this entity.
        /// </summary>
        public readonly int Id;

        internal Entity(int id)
        {
            Id = id;
        }

        // Optional: Overload equality operators for easier comparison
        public override bool Equals(object obj)
        {
            return obj is Entity other && Id == other.Id;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public static bool operator ==(Entity left, Entity right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Entity left, Entity right)
        {
            return !(left == right);
        }
    }
} 