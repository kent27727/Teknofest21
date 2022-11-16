using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

public class DraggableUIObject : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private DragNDrop _dragNDrop;

    private RaycastHit _hit;
    private Ray _ray;

    private GameObject _movingObj;
    private Vector3 _startPos;
    private Camera _viewCamera;
    private int _layerMask;

    private void Start()
    {
        _dragNDrop = DragNDropConfig.Instance.GetDragNDrop(transform);
        _layerMask = DragNDropConfig.Instance.DraggingLayerMask;

    }

    public void OnBeginDrag(PointerEventData eventData)
    {

        _viewCamera = DragNDropConfig.Instance.ViewCamera;
        DragNDropConfig.Instance.UpdateDraggingPlanePos(_dragNDrop.Destination.position);
        if (_movingObj == null)
        {
            _ray = _viewCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(_ray, out _hit, Mathf.Infinity, _layerMask))
            {
                _movingObj = Instantiate(_dragNDrop.MovingPrefab, _hit.point, _dragNDrop.Destination.rotation);
                _startPos = _hit.point;
            }
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (_movingObj != null)
        {
            _ray = _viewCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(_ray, out _hit, Mathf.Infinity, _layerMask))
            {
                _movingObj.transform.position = _hit.point;
                if (IsInGoodPosition(_hit.point))
                {
                    if (!DOTween.IsTweening(_dragNDrop.Destination))
                    {
                        _dragNDrop.Destination.DOPunchScale(new Vector3(0.2f, 0.2f, 0.2f), 0.5f, 4).SetRelative(true);
                    }
                }
            }
        }

    }

    public void OnEndDrag(PointerEventData eventData)
    {

        if (_movingObj != null)
        {
            _ray = _viewCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(_ray, out _hit, Mathf.Infinity, _layerMask))
            {
                _movingObj.transform.position = _hit.point;

                if (IsInGoodPosition(_hit.point))
                {
                    _dragNDrop.Destination.GetComponent<MeshRenderer>().material = DragNDropConfig.Instance.RoadRollerMaterial;
                    Destroy(_movingObj);
                    Destroy(gameObject);
                    _movingObj = null;
                }
                else
                {
                    _movingObj.transform.DOMove(_startPos, 0.5f).OnComplete(() =>
                    {
                        gameObject.GetComponent<Image>().material = null;
                        Destroy(_movingObj);
                    });
                }
            }
        }
    }

    private bool IsInGoodPosition(Vector3 pos)
    {
        return Vector3.Distance(pos, GetTargetPoint()) <= DragNDropConfig.Instance.GoodDistance;
    }

    private Vector3 GetTargetPoint()
    {
        Ray ray = _viewCamera.ScreenPointToRay(_viewCamera.WorldToScreenPoint(_dragNDrop.Destination.position));
        Physics.Raycast(ray, out _hit, Mathf.Infinity, _layerMask);
        return _hit.point;
    }
}