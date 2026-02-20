using System.Threading;
using DG.Tweening;
using Unity.VisualScripting;
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
        [SerializeField]
        private float _dragSpeed = 10f;
        [SerializeField]
        private ItemLaneRegistry _itemLaneRegistry;
        [SerializeField]
        private LaneSystem _laneSystem;
        [SerializeField]
        private ItemSystem _itemSystem;
        [field: SerializeField]
        public float DragCloseLaneDistance
        {
            get;
            set;
        } = 3f;
        [SerializeField]
        private Color _ghostItemColor;
        private Camera _cam;
        private CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();
        private Lane _cachedLane;
        private Lane _nearLane;
        private Item _activeItem;
        private Vector3 _dragStartPos;
        private GhostItem _ghostItem;

        private void Awake()
        {
            _cam = Camera.main;
        }

        private void OnEnable()
        {
            _inputSystem.OnDragStarted += OnDragStarted;
            _inputSystem.OnDragCancelled += OnDragCancelled;
        }

        public void CancelDrag()
        {
            _cancellationTokenSource?.Cancel();

            if (_activeItem == null)
            {
                return;
            }

            _activeItem.IsPlayerControlled = false;
            Transform activeItemTransform = _activeItem.transform;

            if (_nearLane != null)
            {
                _itemLaneRegistry.LinkItem(_activeItem, _nearLane);
                Vector3 lanePos = _nearLane.transform.position;
                var targetPos = new Vector3(lanePos.x, activeItemTransform.position.y, activeItemTransform.position.z);
                activeItemTransform.position = targetPos;
                _nearLane.DisableHighlight();
                _nearLane = null;
                _ghostItem.Destroy();
                _ghostItem = null;
            }
            else if (_ghostItem != null)
            {
                activeItemTransform.position = _ghostItem.transform.position;
                _itemLaneRegistry.LinkItem(_activeItem, _cachedLane);
                _ghostItem.Destroy();
                _ghostItem = null;
            }
            else
            {
                DOTween.Sequence()
                       .Append(ItemAnimationSystem.Instance.ScaleOnKnockAnim(activeItemTransform))
                       .AppendCallback(() => _itemSystem.DestroyItem(_activeItem));
                Debug.Log("no lane and no ghost item");
            }
        }

        private void OnDragStarted()
        {
            Ray ray = _cam.ScreenPointToRay(Mouse.current.position.value);
            var raycast = Physics.Raycast(ray, out RaycastHit hit, 1000, _itemLayerMask);

            if (raycast
                && hit.collider.TryGetComponent(out Item item)
                && CanBeDragged(item))
            {
                // TODO VFX
                _activeItem = item;
                _cachedLane = _itemLaneRegistry.GetLane(item);
                _itemLaneRegistry.UnlinkItem(item);
                _activeItem.IsPlayerControlled = true;
                _ghostItem = SpawnGhostItem(item);
                _cancellationTokenSource = new CancellationTokenSource();
                DragItemAsync(item, _cancellationTokenSource.Token);
                MoveGhostItemAsync(_ghostItem, _cancellationTokenSource.Token);
            }
        }

        private async Awaitable MoveGhostItemAsync(GhostItem ghostItem, CancellationToken cancelToken)
        {
            while (!cancelToken.IsCancellationRequested)
            {
                if (ghostItem == null
                    || ghostItem.gameObject.activeInHierarchy == false)
                {
                    break;
                }
                ghostItem.transform.position += Vector3.back * (_cachedLane.Speed * Time.deltaTime);

                await Awaitable.NextFrameAsync(cancelToken);
            }
        }

        private static bool CanBeDragged(Item item)
        {
            return item.IsPlayerControlled == false;
        }

        private GhostItem SpawnGhostItem(Item item)
        {
            Item itemClone = Instantiate(item, item.transform.position, item.transform.rotation);
            itemClone.DisableCollision();
            itemClone.SetColor(_ghostItemColor);
            var ghostItem = itemClone.AddComponent<GhostItem>();

            return ghostItem;
        }

        private async Awaitable DragItemAsync(Item item, CancellationToken cancelToken)
        {
            var tr = item.transform;

            while (!cancelToken.IsCancellationRequested)
            {
                Vector3 itemPos = tr.position;

                // Плоскость, по которой "едет" предмет: XZ на высоте предмета
                var plane = new Plane(Vector3.up, new Vector3(0f, itemPos.y, 0f));

                Vector2 mousePosSS = Mouse.current.position.ReadValue();
                Ray ray = _cam.ScreenPointToRay(mousePosSS);

                if (plane.Raycast(ray, out float enter))
                {
                    Vector3 hit = ray.GetPoint(enter);

                    // Двигаем по XZ, Y фиксируем как был
                    Vector3 targetPos = new Vector3(hit.x, itemPos.y, hit.z);

                    // Если хочешь плавность — раскомментируй:
                    tr.position = Vector3.MoveTowards(itemPos, targetPos, Time.deltaTime * _dragSpeed);

                    // tr.position = targetPos;

                    // Важно: обрабатываем уже НОВУЮ позицию
                    ProcessNearLanes(tr.position);
                }

                await Awaitable.NextFrameAsync(cancelToken);
            }
        }

        private bool IsItemCloseToLane(Lane lane, Vector3 itemPos)
        {
            return Mathf.Abs(lane.transform.position.x - itemPos.x) <= DragCloseLaneDistance;
        }

        private void ProcessNearLanes(Vector3 itemPos)
        {
            Lane[] lanes = _laneSystem.Lanes;
            bool isNearLane = false;

            foreach (Lane lane in lanes)
            {
                if (IsItemCloseToLane(lane, itemPos)
                    && lane != _cachedLane)
                {
                    isNearLane = true;

                    if (_nearLane != null)
                    {
                        _nearLane.DisableHighlight();
                    }

                    _nearLane = lane;
                    lane.Highlight();

                    break;
                }
            }

            if (!isNearLane
                && _nearLane != null)
            {
                _nearLane.DisableHighlight();
                _nearLane = null;
            }
        }

        private void OnDragCancelled()
        {
            CancelDrag();
        }

        private void OnDisable()
        {
            _inputSystem.OnDragStarted -= OnDragStarted;
            _inputSystem.OnDragCancelled -= OnDragCancelled;
        }
    }
}
