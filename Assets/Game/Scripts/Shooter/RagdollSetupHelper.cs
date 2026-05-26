using TriInspector;
using UnityEditor;
using UnityEngine;

namespace Game.Scripts.Shooter
{
    // Этот скрипт нужен ТОЛЬКО в редакторе для заполнения массива
    public class RagdollSetupHelper : MonoBehaviour
    {
        [SerializeField]
        private RagdollComponent _ragdollComponent;

        [Button("Собрать коллайдеры в RagdollComponent")]
        private void GetColliders()
        {
            if (_ragdollComponent == null)
                _ragdollComponent = GetComponent<RagdollComponent>();

            if (_ragdollComponent != null)
            {
                // Находим коллайдеры и передаем их в сетевой компонент
                Collider[] colliders = GetComponentsInChildren<Collider>();
                _ragdollComponent.Colliders = colliders;

                // Помечаем сцену/префаб измененными, чтобы Unity сохранила данные
                EditorUtility.SetDirty(_ragdollComponent);
            }
        }

        [Button("Add proxy component")]
        private void AddProxyComponent()
        {
            Collider[] colliders = _ragdollComponent.Colliders;

            foreach (Collider collider in colliders)
            {
                var ragdollPartProxy = collider.gameObject.AddComponent<RagdollPartProxy>();
                ragdollPartProxy.RagdollComponent = _ragdollComponent;
            }

            EditorUtility.SetDirty(_ragdollComponent);
        }
    }
}
