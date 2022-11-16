using System.Collections.Generic;
using UnityEngine;

public class DragNDropConfig : MonoBehaviour
{
    public const string DragNDropLayerName = "DragNDrop";

    public static DragNDropConfig Instance;

    public Camera ViewCamera;

    [Range(0,10)]
    public float GoodDistance = 0.5f;

    public Material RoadRollerMaterial;

    public List<DragNDrop> DragNDrops;

    public int DraggingLayerMask { get { return ~_draggingPlane.layer; } }


    private GameObject _draggingPlane;

    private void Awake()
    {

        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(this);

        if (ViewCamera == null)
        {
            ViewCamera = Camera.main;
        }

        InitDraggingPlane();

    }



    void Start()
    {
      
    }

    private void InitDraggingPlane()
    {
        _draggingPlane = new GameObject("DraggingPlane");
        _draggingPlane.AddComponent<BoxCollider>();
        BoxCollider boxCollider = _draggingPlane.GetComponent<BoxCollider>();
        _draggingPlane.layer = LayerMask.NameToLayer(DragNDropLayerName);
        boxCollider.isTrigger = true;
        boxCollider.size = new Vector3(200, 200, 1);
        _draggingPlane.transform.parent = ViewCamera.transform;
        _draggingPlane.transform.localRotation = Quaternion.Euler(0, 0, 0);
        _draggingPlane.transform.localPosition = new Vector3(0, 0, 9);

    }


    public DragNDrop GetDragNDrop(Transform source)
    {
        return DragNDrops.Find(dragNDrop => (dragNDrop.Source == source));
    }

    public void UpdateDraggingPlanePos(Vector3 desPos)
    {
        _draggingPlane.transform.position = desPos;
    }
}
