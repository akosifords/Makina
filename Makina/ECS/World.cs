using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Makina.ECS
{
    /// <summary>
    /// Manages entities, components, and systems within the ECS architecture.
    /// </summary>
    public class World
    {
        private readonly Dictionary<int, Dictionary<Type, Component>> _components = new();
        private readonly List<System> _systems = new();
        private readonly HashSet<int> _entities = new();
        private readonly Queue<int> _recycledEntityIds = new();
        private int _nextEntityId = 0;

        // --- Entity Management ---

        public Entity CreateEntity()
        {
            int id = _recycledEntityIds.Count > 0 ? _recycledEntityIds.Dequeue() : _nextEntityId++;
            var entity = new Entity(id);
            _entities.Add(id);
            _components[id] = new Dictionary<Type, Component>(); // Initialize component map for the new entity
            // Logger.Debug($"Entity Created: {id}"); // Optional logging
            return entity;
        }

        public void DestroyEntity(Entity entity)
        {
            if (!_entities.Contains(entity.Id)) return;

            _components.Remove(entity.Id); // Remove all components associated with the entity
            _entities.Remove(entity.Id);
            _recycledEntityIds.Enqueue(entity.Id); // Allow ID reuse
            // Logger.Debug($"Entity Destroyed: {entity.Id}"); // Optional logging
        }

        // --- Component Management ---

        public T AddComponent<T>(Entity entity, T component) where T : Component
        {
            if (!_entities.Contains(entity.Id)) throw new ArgumentException("Entity does not exist.", nameof(entity));

            _components[entity.Id][typeof(T)] = component;
            // Logger.Debug($"Component {typeof(T).Name} added to Entity {entity.Id}"); // Optional logging
            return component;
        }

        public void RemoveComponent<T>(Entity entity) where T : Component
        {
            if (!_entities.Contains(entity.Id)) return; // Or throw?
            if (_components[entity.Id].ContainsKey(typeof(T)))
            {
                _components[entity.Id].Remove(typeof(T));
                // Logger.Debug($"Component {typeof(T).Name} removed from Entity {entity.Id}"); // Optional logging
            }
        }

        public T GetComponent<T>(Entity entity) where T : Component
        {
            if (!_entities.Contains(entity.Id)) throw new ArgumentException("Entity does not exist.", nameof(entity));
            if (_components[entity.Id].TryGetValue(typeof(T), out var component))
            {
                return (T)component;
            }
            return null; // Component not found
        }

        public bool HasComponent<T>(Entity entity) where T : Component
        {
            if (!_entities.Contains(entity.Id)) return false;
            return _components[entity.Id].ContainsKey(typeof(T));
        }

        // --- System Management ---

        public T RegisterSystem<T>(T system) where T : System
        {
            if (_systems.Any(s => s.GetType() == typeof(T)))
            {
                // Logger.Warning($"System {typeof(T).Name} already registered."); // Optional logging
                return (T)_systems.First(s => s.GetType() == typeof(T)); // Return existing instance
            }

            system.SetWorld(this); // Provide system with access to this world
            _systems.Add(system);
            system.Initialize(); // Call system's initialization logic
            // Logger.Info($"System {typeof(T).Name} registered and initialized."); // Optional logging
            return system;
        }

        public void UpdateSystems(GameTime gameTime)
        {
            foreach (var system in _systems)
            {
                system.Update(gameTime);
            }
        }

        public void DrawSystems(GameTime gameTime)
        {
             foreach (var system in _systems)
            {
                system.Draw(gameTime);
            }
        }

        // --- Entity Querying (Example for systems) ---

        /// <summary>
        /// Gets all entities that possess all the specified component types.
        /// </summary>
        public IEnumerable<Entity> GetEntitiesWithComponents(params Type[] componentTypes)
        {
            if (componentTypes == null || componentTypes.Length == 0)
            {
                // Return all entities if no specific components are requested
                return _entities.Select(id => new Entity(id));
            }

            List<Entity> result = new List<Entity>();
            foreach (int entityId in _entities)
            {
                if (_components.TryGetValue(entityId, out var entityComponents))
                {
                    bool hasAllComponents = true;
                    foreach (Type type in componentTypes)
                    {
                        if (!entityComponents.ContainsKey(type))
                        {
                            hasAllComponents = false;
                            break;
                        }
                    }

                    if (hasAllComponents)
                    {
                        result.Add(new Entity(entityId));
                    }
                }
            }
            return result;
        }
    }
} 