using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game
{
    public sealed class DragHandler : MonoBehaviour
    {
        [SerializeField]
        private InputSystem _inputSystem;
        [SerializeField]
        private LayerMask _itemLayerMask;
        private Camera _cam;

        private void Awake()
        {
            _cam = Camera.main;
        }

        private void OnEnable()
        {
            _inputSystem.OnDragStarted += OnDragStarted;
            _inputSystem.OnDragCancelled += OnDragCancelled;
        }

        private void OnDragStarted()
        {
            var raycast = Physics.Raycast(_cam.ScreenPointToRay(Mouse.current.position.value)
                , out RaycastHit hit
                , 1000
                , _itemLayerMask);

            if (raycast
                && hit.collider.TryGetComponent(out Item item)
                && CanItemBeDragged(item))
            {
                
            }
        }

        private static bool CanItemBeDragged(Item item)
        {
            return item.IsSwiped == false;
        }

        private async Awaitable DragItemAsync(Item item, CancellationToken cancelToken)
        {
            Vector3 mousePosWS = _cam.ScreenToWorldPoint(Mouse.current.position.value);
            Vector3 itemPos = item.GetPosition();
            var targetPos = new Vector3(mousePosWS.x, itemPos.y, mousePosWS.z);
            item.Move(targetPos); // TODO LERP
            // TODO unlink
        }

        private void OnDragCancelled()
        {
        }

        private void OnDisable()
        {
            _inputSystem.OnDragStarted -= OnDragStarted;
            _inputSystem.OnDragCancelled -= OnDragCancelled;
        }
    }
}
