using System;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

namespace _06.Scripts.Utilities
{
    [Serializable]
    public class ObjectToSave<T>
    {
        public T Object;
    }

    public class PopupParameter : MonoBehaviour
    {
        private readonly Dictionary<string, string> _storage = new();

        public void SaveObject<T>(ParamType key, T obj)
        {
            ObjectToSave<T> saveObject = new ObjectToSave<T>();
            saveObject.Object = obj;
            string jsonString = JsonConvert.SerializeObject(saveObject);
            _storage[key.ToString()] = jsonString;
        }

        public void SaveObject<T>(string key, T obj)
        {
            ObjectToSave<T> saveObject = new ObjectToSave<T>();
            saveObject.Object = obj;
            string jsonString = JsonConvert.SerializeObject(saveObject);
            _storage[key] = jsonString;
        }

        public T GetObject<T>(ParamType key)
        {
            if (!_storage.ContainsKey(key.ToString())) return default;
            string jsonString = _storage[key.ToString()];
            ObjectToSave<T> saveObject = JsonConvert.DeserializeObject<ObjectToSave<T>>(jsonString);
            return saveObject.Object;
        }

        public T GetObject<T>(String key)
        {
            if (!_storage.ContainsKey(key)) return default;
            string jsonString = _storage[key];
            ObjectToSave<T> saveObject = JsonConvert.DeserializeObject<ObjectToSave<T>>(jsonString);
            return saveObject.Object;
        }

        void Start()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}