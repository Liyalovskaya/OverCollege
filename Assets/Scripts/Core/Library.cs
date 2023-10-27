using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace OC.Core
{
    public static class Library
    {
        private static bool _registered;

        private static void RegisterTypes<T>(IDictionary<string, Assembly> asmTable) where T : class
        {
            RegisterAssembly<T>(asmTable, "OC.Core");
        }

        private static void RegisterAssembly<T>(IDictionary<string, Assembly> asmTable, string assemblyName)
            where T : class
        {
            if (asmTable.TryGetValue(assemblyName, out var assembly))
            {
                RegisterAssembly<T>(assembly);
            }
        }

        private static void RegisterAssembly<T>(Assembly assembly) where T : class
        {
            TypeFactory<T>.RegisterAssembly(assembly);
        }

        public static async UniTask RegisterAllAsync()
        {
            if (_registered) return;

            var asmTable = (from asm in AppDomain.CurrentDomain.GetAssemblies()
                    where !asm.IsDynamic
                    group asm by asm.GetName().Name
                    into grp
                    select (name: grp.Key, value: grp.First()))
                .ToDictionary(kv => kv.name, kv => kv.value);

            var tasks = new[]
            {
                UniTask.RunOnThreadPool(() => RegisterTypes<Location>(asmTable)),
                UniTask.RunOnThreadPool(() => RegisterTypes<Character>(asmTable)),
            };
            await UniTask.WhenAll(tasks);

            _registered = true;
        }

        public static T CreateLocation<T>() where T : Location
            => TypeFactory<Location>.CreateInstance<T>();

        public static Location CreateLocation(Type locationId)
            => TypeFactory<Location>.CreateInstance(locationId);

        public static Location CreateLocation(string name)
        {
            return TypeFactory<Location>.CreateInstance(name);
        }
        
        public static T CreateCharacter<T>() where T : Character
            => TypeFactory<Character>.CreateInstance<T>();

        public static Character CreateCharacter(Type characterId)
            => TypeFactory<Character>.CreateInstance(characterId);

        public static Character CreateCharacter(string name)
        {
            return TypeFactory<Character>.CreateInstance(name);
        }


    }
    
    public interface IVerifiable
    {
        void Verify();
    }
    
    public interface IInitializable
    {
        void Initialize();
    }
    
    public interface INotifyChanged
    {
        event Action PropertyChanged;
        void NotifyChanged();
    }
    
}