using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System.Linq;

/// <summary>This component adds basic Pitch/Yaw controls to the current GameObject using mouse or touch input. This requires the P3dInputManager to be in your scene.</summary>
[ExecuteInEditMode]
public class DragPitchYaw : MonoBehaviour
{
    public static float ScaleFactor { get { return (400f / Screen.dpi); } }

    [Range(0, 1)]
    public float PitchSensitivity = 0.1f;

    [Range(-180, 0)]
    public float PitchMin = -90.0f;

    [Range(0, 180)]
    public float PitchMax = 90.0f;

    [Range(0, 1)]
    public float YawSensitivity = 0.1f;

    [Range(1, 100)]
    public float Dampening = 10.0f;

    private float _currentPitch;
    private float _currentYaw;

    private bool _allowRotation = false;
    private bool _isDragging = false;
    private float _pitch = 0;
    private float _yaw = 0;
    private List<Vector2> _deltaPositions = new List<Vector2>();
    private Vector2 _lastPos = Vector2.zero;

    protected virtual void Update()
    {

        // Touch support
        if (Input.touchCount > 0)
        {
            Touch tch = Input.GetTouch(0);
            if (tch.phase == TouchPhase.Began)
            {
                _allowRotation = !EventSystem.current.IsPointerOverGameObject(tch.fingerId);
            }

            if (_allowRotation)
            {
          
                for (var i = 0; i < Input.touchCount; i++)
                {
                    var touch = Input.GetTouch(i);

                    if (touch.phase == TouchPhase.Moved)
                    {
                        _deltaPositions.Add(Input.GetTouch(i).deltaPosition);
                    }

                }

                AdjustCameraPivot(_deltaPositions);
            }
            if (tch.phase == TouchPhase.Ended)
            {
                _deltaPositions.Clear();
            }

        }

        //Mouse support
        else
        {
            if (Input.GetMouseButtonDown(0) == true && !_isDragging)
            {
                if (!EventSystem.current.IsPointerOverGameObject())
                {
                    _lastPos = Input.mousePosition;
                    _isDragging = true;
                }

            }

            if (_isDragging)
            {
                Vector2 pos = Input.mousePosition;
                _deltaPositions.Add(pos - _lastPos);

                AdjustCameraPivot(_deltaPositions);

                _lastPos = Input.mousePosition;

            }

            if (Input.GetMouseButtonUp(0) == true)
            {
                _isDragging = false;
                Debug.Log("Size: " + _deltaPositions.Count);
                _deltaPositions.Clear();
            }
        }
    }

    private void AdjustCameraPivot(List<Vector2> deltas)
    {
        var average = new Vector2(deltas.Average(d => d.x), deltas.Average(d => d.y)) * ScaleFactor;

        _pitch -= average.y * PitchSensitivity;
        _yaw += average.x * YawSensitivity;
        _pitch = Mathf.Clamp(_pitch, PitchMin, PitchMax);

        // Smoothly dampen values
        var factor = DampenFactor(Dampening, Time.deltaTime);

        _currentPitch = Mathf.Lerp(_currentPitch, _pitch, factor);
        _currentYaw = Mathf.Lerp(_currentYaw, _yaw, factor);

        transform.localRotation = Quaternion.Euler(_currentPitch, _currentYaw, 0.0f);
    }

    private float DampenFactor(float dampening, float elapsed)
    {
        return 1.0f - Mathf.Pow((float)System.Math.E, -dampening * elapsed);
    }
}

