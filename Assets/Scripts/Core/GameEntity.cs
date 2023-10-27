using System;
using UnityEngine;
using UnityEngine.Scripting;

namespace OC.Core
{
    [RequireDerived]
    public abstract class GameEntity : IInitializable, INotifyChanged
    {

        public string Id { get; private set; }

        public virtual string DebugName => $"<{Id}>({Name})";
        
        protected string BaseName { get; set; }

        protected string BaseDescription { get; set; }

        /// <summary>
        /// Base description for runtime formatter (i.e. with {args}) 
        /// </summary>
        protected virtual string GetBaseDescription()
            => BaseDescription;

        public virtual string Name
            => BaseName;

        public virtual string Description
        {
            get
            {
                try
                {
                    var baseDescription = GetBaseDescription();
                    if (baseDescription == null)
                    {
                        Debug.LogError($"<{DebugName}>.GetBaseDescription() returns null");
                        return "";
                    }

                    return baseDescription;
                }
                catch (Exception exc)
                {
                    Debug.LogException(exc);
                    return "<Error>";
                }
            }
        }

        // ReSharper disable once MemberCanBeProtected.Global

        public virtual void Initialize()
        {
            Id = GetType().Name;
        }

        public event Action PropertyChanged;

        public virtual void NotifyChanged()
        {
            PropertyChanged?.Invoke();
        }
    }
}