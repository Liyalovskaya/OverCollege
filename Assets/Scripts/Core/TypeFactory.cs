using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace OC.Core
{
    internal static class TypeFactory<T> where T : class
    {
        // ReSharper disable once StaticMemberInGenericType
        private static readonly Dictionary<string, Type> FullNameTypeDict = new();
        // ReSharper disable once StaticMemberInGenericType
        private static readonly Dictionary<string, Type> TypeDict = new();
        

        private static T InternalCreateInstance(Type type)
        {
            var result = (T)Activator.CreateInstance(type);
            (result as IInitializable)?.Initialize();
            (result as IVerifiable)?.Verify();
            return result;
        }
        
        /// <summary>
        /// Get deriving type of T with name.
        /// </summary>
        /// <param name="id">The deriving type's name.</param>
        /// <returns>If found, return the type, else return null.</returns>
        public static Type TryGetType(string id)
        {
            if (TypeDict.TryGetValue(id, out var type) || FullNameTypeDict.TryGetValue(id, out type))
                return type;
            return null;
        }

        public static Type GetType(string id)
        {
            return TryGetType(id) ?? throw new ArgumentException($"Cannot find {typeof(T).Name} type '{id}'");
        }

        public static T TryCreateInstance(string id)
        {
            var type = TryGetType(id);
            return type == null ? null : InternalCreateInstance(type);
        }

        public static T CreateInstance(string id)
        {
            var result = TryCreateInstance(id);
            if (result != null) return result;
            throw new ArgumentException($"Cannot create instance {id} of type {typeof(T).Name}");
        }

        public static T TryCreateInstance(Type type)
        {
            if (!type.IsSubclassOf(typeof(T)))
                throw new ArgumentException($"Type {type.Name} is not sub-class of {typeof(T).Name}");
            return InternalCreateInstance(type);
        }

        public static T CreateInstance(Type type)
        {
            if (!type.IsSubclassOf(typeof(T)))
                throw new ArgumentException($"Type {type.Name} is not sub-class of {typeof(T).Name}");
            var result = TryCreateInstance(type);
            if (result != null) return result;
            throw new ArgumentException($"Cannot create instance {type.Name} of type {typeof(T).Name}");
        }

        public static TDerived TryCreateInstance<TDerived>() where TDerived : T
            => (TDerived)TryCreateInstance(typeof(TDerived));

        public static TDerived CreateInstance<TDerived>() where TDerived : T
            => (TDerived)CreateInstance(typeof(TDerived));

        public static IEnumerable<Type> AllTypes
            => TypeDict.Values;
        
        public static void RegisterAssembly(Assembly assembly)
        {
            var baseType = typeof(T);

            var subTypes = assembly.GetExportedTypes()
                .Where(type => type.IsSealed && type.IsSubclassOf(baseType))
                .ToList();

            foreach (var type in subTypes)
            {
                // ReSharper disable once AssignNullToNotNullAttribute
                FullNameTypeDict.Add(type.FullName, type);
                TypeDict.TryAdd(type.Name, type);
            }
        }
        
    }
    

    
}
