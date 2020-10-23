using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Utilities
{
    public class SerializableList<T>
    {
        public List<T> Items;

        public SerializableList()
        {
            Items = new List<T>();
        }

        public SerializableList(T defaultValue)
        {
            Items = new List<T>() {defaultValue};
        }

        public SerializableList(List<T> defaultValues)
        {
            Items = new List<T>(defaultValues);
        }
    
        public SerializableList(T[] defaultValues)
        {
            Items = new List<T>(defaultValues.ToList());
        }
    }

    /// <summary>
    /// liste şeklinde veri tutan player prefler için bazı işlemleri yapar.
    /// </summary>
    public class PlayerPrefList
    {
        public static List<T> GetList<T>(string key)
        {
            if (!PlayerPrefs.HasKey(key))
            {
                return new List<T>();
            }

            var items = PlayerPrefs.GetString(key);
            if (string.IsNullOrWhiteSpace(items))
            {
                return new List<T>();
            }

            var list = JsonUtility.FromJson<SerializableList<T>>(items);
            if (list == null || list.Items == null)
            {
                return new List<T>();
            }

            return list.Items;
        }

        public static void AddOrUpgradeArray<T>(string key, T[] items)
        {
            PlayerPrefs.SetString(key, JsonUtility.ToJson(new SerializableList<T>(items)));
        }

        public static void AddOrUpgradeList<T>(string key, List<T> items)
        {
            AddOrUpgradeArray(key, items.ToArray());
        }

        public static void AddOrUpgrade<T>(string key, T item)
        {
            var allItems = GetList<T>(key);
            if (allItems.Contains(item))
            {
                return;
            }

            allItems.Add(item);
            AddOrUpgradeList(key, allItems);
        }
    }
}