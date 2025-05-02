namespace Makina.ECS
{
    /// <summary>
    /// Base class for all components. Components are data containers
    /// that define the properties or aspects of an entity.
    /// </summary>
    public abstract class Component
    {
        // Components are typically data-only, so this base class is often empty.
        // Specific data fields will be in derived component classes.
    }
} 