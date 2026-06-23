using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Codomaster.Extensions
{
    public static class ComponentExtensions
    {
        /// <summary>
        /// Copy <see cref="Component"/> and transfer it to selected <see cref="GameObject"/>.
        /// <br></br>
        /// https://discussions.unity.com/t/copy-a-component-at-runtime/71172/3
        /// </summary>
        /// <param name="component"></param>
        /// <param name="destination"></param>
        /// <returns>The copy</returns>
        public static Component CopyComponentToGameObject
            (this Component component, GameObject destination)
        {

            System.Type type = component.GetType();
            Component copy = destination.AddComponent(type);

            // Copied fields can be restricted with BindingFlags
            System.Reflection.FieldInfo[] fields = type.GetFields();
            foreach (System.Reflection.FieldInfo field in fields)
                field.SetValue(copy, field.GetValue(component));
            
            return copy;
        }
        
        /// <summary>
        /// Gets <typeparamref name="T"/> component from <paramref name="component"/>'s game object. If it no exist, new one will be created.
        /// </summary>
        /// <typeparam name="T">Component type.</typeparam>
        /// <param name="component">The component.</param>
        /// <returns>Game object's <typeparamref name="T"/> component.</returns>
        public static T GetOrAddComponent<T>(this Component component) where T : Component
        {
            return component.gameObject.GetOrAddComponent<T>();
        }

        /// <summary>
        /// Trying to get component in this or any it's children.
        /// </summary>
        /// <typeparam name="T">Component type.</typeparam>
        /// <param name="sourceComponent">The component.</param>
        /// <param name="component">Target component.</param>
        /// <param name="includeInactive">Should we find component on inactive game objects?</param>
        /// <returns><see langword="true"/> if component was found.</returns>
        public static bool TryGetComponentInChildren<T>(this Component sourceComponent, out T component, bool includeInactive = false) where T : Component
        {
            return component = sourceComponent.gameObject.GetComponentInChildren<T>(includeInactive);
        }

        /// <summary>
        /// Trying to get component in this or any it's parent.
        /// </summary>
        /// <typeparam name="T">Component type.</typeparam>
        /// <param name="sourceComponent">The component.</param>
        /// <param name="component">Target component.</param>
        /// <param name="includeInactive">Should we find component on inactive game objects?</param>
        /// <returns><see langword="true"/> if component was found.</returns>
        public static bool TryGetComponentInParent<T>(this Component sourceComponent, out T component, bool includeInactive = false) where T : Component
        {
            return component = sourceComponent.gameObject.GetComponentInParent<T>(includeInactive);
        }
    }
}
