using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using Makina.Logging; // For logging errors/info

namespace Makina.Assets
{
    /// <summary>
    /// Manages loading and storage of game assets, wrapping MonoGame's ContentManager.
    /// </summary>
    public class AssetManager : IDisposable
    {
        private readonly ContentManager _contentManager;
        private readonly Dictionary<string, object> _loadedAssets = new();
        private bool _isDisposed = false;

        public AssetManager(ContentManager contentManager)
        {
            _contentManager = contentManager ?? throw new ArgumentNullException(nameof(contentManager));
            Logger.Info($"AssetManager initialized with Content Root: {_contentManager.RootDirectory}");
        }

        /// <summary>
        /// Loads an asset of a specific type.
        /// If the asset is already loaded, returns the cached instance.
        /// </summary>
        /// <typeparam name="T">The type of asset to load.</typeparam>
        /// <param name="assetName">The name of the asset (usually the file path relative to the Content root, without extension).</param>
        /// <returns>The loaded asset, or null if loading fails.</returns>
        public T Load<T>(string assetName) where T : class
        {
            if (_isDisposed) throw new ObjectDisposedException(nameof(AssetManager));
            if (string.IsNullOrEmpty(assetName)) throw new ArgumentNullException(nameof(assetName));

            // Check cache first
            if (_loadedAssets.TryGetValue(assetName, out var cachedAsset))
            {
                if (cachedAsset is T typedAsset)
                {
                    // Logger.Debug($"Asset cache hit: {assetName} ({typeof(T).Name})");
                    return typedAsset;
                }
                else
                {
                    Logger.Error($"Asset cache conflict: Requested type {typeof(T).Name} for asset '{assetName}', but cached type is {cachedAsset?.GetType().Name}. Returning null.");
                    return null;
                }
            }

            // Load using ContentManager
            try
            {
                Logger.Debug($"Loading asset: {assetName} ({typeof(T).Name})");
                T loadedAsset = _contentManager.Load<T>(assetName);
                if (loadedAsset != null)
                {
                    _loadedAssets[assetName] = loadedAsset;
                    Logger.Info($"Asset loaded and cached: {assetName} ({typeof(T).Name})");
                    return loadedAsset;
                }
                else
                {
                     Logger.Warning($"ContentManager returned null for asset: {assetName} ({typeof(T).Name})");
                     return null;
                }
            }
            catch (ContentLoadException ex)
            {
                Logger.Error($"Failed to load asset '{assetName}' ({typeof(T).Name}): {ex.Message}");
                // Optionally log ex.ToString() for more details
                return null;
            }
            catch (Exception ex)
            {
                 Logger.Error($"An unexpected error occurred loading asset '{assetName}' ({typeof(T).Name}): {ex.Message}");
                 return null;
            }
        }

        /// <summary>
        /// Removes the asset from the cache, allowing it to be potentially garbage collected
        /// if not referenced elsewhere. Does not call ContentManager.Unload().
        /// </summary>
        /// <param name="assetName">The name of the asset to remove from the cache.</param>
        public void UnloadFromCache(string assetName)
        {
            if (_isDisposed) return;
            if (_loadedAssets.Remove(assetName))
            {
                Logger.Info($"Asset removed from cache: {assetName}");
            }
        }

        /// <summary>
        /// Unloads all content loaded by the underlying ContentManager and clears the cache.
        /// </summary>
        public void UnloadAll()
        {
            if (_isDisposed) return;
            Logger.Info("Unloading all assets and clearing cache...");
            _contentManager.Unload();
            _loadedAssets.Clear();
            Logger.Info("Asset unload complete.");
        }

        // Implement IDisposable
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_isDisposed)
            {
                if (disposing)
                {
                    // Dispose managed state (managed objects).
                    UnloadAll();
                }
                // Free unmanaged resources (unmanaged objects) and override finalizer
                // Set large fields to null
                _isDisposed = true;
            }
        }

        // Finalizer (optional)
         ~AssetManager()
         {
             Dispose(false);
         }
    }
} 