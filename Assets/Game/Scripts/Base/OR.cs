using System;
using System.Collections.Generic;
using UnityEngine.Assertions;

namespace Game
{
    public sealed class OR
    {
        private static OR _instance;
        private readonly Dictionary<Type, object> _map = new();
        
        private OR(){}

        public static void Init()
        {
            _instance ??= new OR();
        }

        public static void Set<T>(T value) where T : class
        {
            Assert.IsNotNull(value);
            _instance._map[typeof(T)] = value;
        }

        public static T Get<T>() where T : class
        {
            if (_instance._map.TryGetValue(typeof(T), out var value))
                return (T)value;
            
            throw new InvalidOperationException($"No value for type {typeof(T)}");
        }
    }
}
