using System;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraHandler : MonoBehaviour
{
    [SerializeField] private Vector3 _offset;
    [SerializeField] private float _expectedBoundsMaxHeight = 30f;
    [SerializeField] private float _heightThreshold = 9f;

    [Header("Orthographic half-size limiters")]
    [SerializeField] private float _minSize = 5;
    [SerializeField] private float _maxSize = 17;

    [Header("Bounds targets")]
    [SerializeField] private Transform _defaultCamTarget;
    [SerializeField] private Transform _ballTransform;

    private Camera _cam;
    private Bounds _bounds;

    private void Awake()
    {
        _cam = GetComponent<Camera>();
    }

    private void LateUpdate()
    {
        if (_ballTransform.position.y >= _heightThreshold)
        {
            UpdateBounds();
            UpdateSize();
            Move();
        }
    }

    private void UpdateBounds()
    {
        _bounds = new Bounds(_defaultCamTarget.position, Vector3.zero);

        _bounds.Encapsulate(_defaultCamTarget.position);
        _bounds.Encapsulate(_ballTransform.position);
    }

    private void UpdateSize()
    {
        _cam.orthographicSize = Mathf.Lerp(_minSize, _maxSize, _bounds.size.y / _expectedBoundsMaxHeight);
    }

    private void Move()
    {
        transform.position = new Vector3(transform.position.x, _bounds.center.y, 0f) + _offset;
    }
}
