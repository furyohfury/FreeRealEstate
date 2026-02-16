using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game
{
    public sealed class SwipeHandler : MonoBehaviour
    {
        [SerializeField]
        private InputSystem _inputSystem;
        [SerializeField]
        private LaneSystem _laneSystem;
        [SerializeField]
        private ItemLaneRegistry _itemLaneRegistry;
        [SerializeField] [Range(0, 1f)]
        private float _screenRatioToSwipe = 0.2f;
        [SerializeField]
        private Camera _camera;
        [SerializeField]
        private LayerMask _itemsLayerMask;
        [SerializeField]
        private float _moveDuration = 0.5f;
        private bool _isSwiping;
        private float _initialPos;
        private float _pixelsToSwipe;
        private Item _selectedItem;

        private void OnEnable()
        {
            _inputSystem.OnSwipeStarted += OnSwipeStarted;
            _inputSystem.OnSwipeCancelled += OnSwipeCancelled;
        }

        private void Start()
        {
            int width = Screen.width;
            _pixelsToSwipe = _screenRatioToSwipe * width;
        }

        private void OnSwipeStarted()
        {
            RaycastHit hit;
            bool raycastHit;
#if UNITY_EDITOR || UNITY_STANDALONE_WIN // TODO WEB3
            var ray = _camera.ScreenPointToRay(Mouse.current.position.value);
            raycastHit = Physics.Raycast(ray,
                out hit,
                10000f,
                _itemsLayerMask
                );
  #endif
            if (raycastHit)
            {
                _isSwiping = true;
                _initialPos = _inputSystem.PointerPositionX;
                _selectedItem = hit.collider.GetComponent<Item>();
            }
        }

        private void OnSwipeCancelled()
        {
            _isSwiping = false;
        }

        private void Update()
        {
            if (!_isSwiping)
            {
                return;
            }

            float delta = _inputSystem.PointerPositionX - _initialPos;

            if (Mathf.Abs(delta) >= _pixelsToSwipe)
            {
                _isSwiping = false;
                Lane[] lanes = _laneSystem.Lanes;

                for (int i = 0, count = lanes.Length; i < count; i++)
                {
                    if (lanes[i].LinkedItems.Contains(_selectedItem) == false)
                        continue;

                    if (delta > 0
                        && i < count - 1)
                    {
                        MoveItemToLane(_selectedItem, lanes[i], lanes[i + 1]);
                    }
                    else if (delta < 0
                             && i > 0)
                    {
                        MoveItemToLane(_selectedItem, lanes[i], lanes[i - 1]);
                    }
                }
            }
        }

        private void MoveItemToLane(Item selectedItem, Lane initialLane, Lane newLane)
        {
            _itemLaneRegistry.SwapLane(selectedItem, newLane);
            initialLane.LinkedItems.Remove(selectedItem);
            newLane.LinkedItems.Add(selectedItem);
            selectedItem.IsSwiped = true;

            DOTween.Sequence()
                   .Append(selectedItem.transform.DOMoveX(newLane.transform.position.x, _moveDuration))
                   .AppendCallback(() => selectedItem.IsSwiped = false);
        }

        private void OnDisable()
        {
            _inputSystem.OnSwipeStarted -= OnSwipeStarted;
            _inputSystem.OnSwipeCancelled -= OnSwipeCancelled;
        }
    }
}
