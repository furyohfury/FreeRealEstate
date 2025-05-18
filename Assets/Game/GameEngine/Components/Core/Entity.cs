using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameEngine
{
	public abstract class Entity : MonoBehaviour
	{
		private readonly Dictionary<Type, IComponent> _components = new();
		private readonly List<IComponentUpdate> _componentUpdates = new();

		private void Start()
		{
			foreach (var component in _components.Values)
			{
				if (component is IComponentInit componentInit)
				{
					componentInit.Initialize();
				}

				if (component is IComponentUpdate componentUpdate)
				{
					_componentUpdates.Add(componentUpdate);
				}
			}
		}

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

		public bool HasComponent<T>()
		{
			return _components.ContainsKey(typeof(T));
		}

		protected bool AddComponent(IComponent component)
		{
			if (_components.TryAdd(component.GetType(), component) == false)
			{
				return false;
			}

			if (component is IComponentInit componentInit)
			{
				componentInit.Initialize();
			}

			if (component is IComponentUpdate componentUpdate)
			{
				_componentUpdates.Add(componentUpdate);
			}

			return true;
		}

		protected virtual void Update()
		{
			float deltaTime = Time.deltaTime;
			for (int i = 0, count = _componentUpdates.Count; i < count; i++)
			{
				_componentUpdates[i].Update(deltaTime);
			}
		}

		private void OnDestroy()
		{
			foreach (var component in _components.Values)
			{
				if (component is IComponentDispose componentDispose)
				{
					componentDispose.Dispose();
				}
			}
		}
	}
}