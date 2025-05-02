using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameEngine
{
	public abstract class Entity : MonoBehaviour
	{
		private readonly Dictionary<Type, IComponent> _components = new();

		public new bool TryGetComponent<T>(out T component) where T : IComponent
		{
			if (_components.TryGetValue(typeof(T), out IComponent value))
			{
				component = (T)value;
				return true;
			}

			component = default;
			return false;
		}

		public new T GetComponent<T>() where T : IComponent
		{
			if (_components.TryGetValue(typeof(T), out IComponent value))
			{
				return (T)value;
			}

			throw new NullReferenceException($"No component of type {typeof(T).Name}");
		}

		protected bool AddComponent(IComponent component)
		{
			return _components.TryAdd(component.GetType(), component);
		}
	}
}