#if UNITY_EDITOR
using System;
using System.Linq;
using UnityEditor;
using Object = UnityEngine.Object;

namespace AD.ToolsCollection
{
    public class EditorUtils
    {
        public static T LoadAsset<T>(params string[] filterPaths) where T : Object
        {
            return LoadAsset(typeof(T), filterPaths) as T;
        }

        public static object LoadAsset(Type type, params string[] filterPaths)
        {
            var typeName = type.Name;
            var filter = GetValidFilter(filterPaths);

            var assetGUID = AssetDatabase
                .FindAssets($"t:{typeName}", filter)
                .FirstOrDefault();

            if (!assetGUID.IsNullOrEmpty())
            {
                return LoadAsset(assetGUID, type);
            }
            else
            {
                return null;
            }
        }

        public static T LoadAsset<T>(string guid) where T : Object
        {
            return LoadAsset(guid, typeof(T)) as T;
        }

        public static object LoadAsset(string guid, Type type)
        {
            var assetPath = AssetDatabase.GUIDToAssetPath(guid);

            return AssetDatabase.LoadAssetAtPath(assetPath, type);
        }

        private static string[] GetValidFilter(params string[] filterPaths)
        {
            filterPaths = filterPaths
                .Where(x => !x.IsNullOrEmpty())
                .ToArray();

            return filterPaths.Length > 0
                ? filterPaths
                : null;
        }
    }
}
#endif