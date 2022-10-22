using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private float _smoothness;
    [SerializeField] private Transform _targetTransform;
    private Vector3 _zOffset;

    private CameraShake _cameraShake;

    private void Start()
    {
        _zOffset = new Vector3(0, 0, -10);
        _cameraShake = new CameraShake();
    }

    void Update()
    {
        Move();
    }

    public void SetTarget(Transform newTarget)
    {
        _targetTransform = newTarget;
    }

    public void ApplyCameraShake()
    {
        _cameraShake.ApplyScreenShake();
    }

    private void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, _targetTransform.position + _zOffset, _smoothness * Time.deltaTime);
        transform.position += _cameraShake.UpdateScreenShake();
    }
}
