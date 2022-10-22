using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private float _smoothness;
    [SerializeField] private Transform _targetTransform;
    private Vector3 _zOffset;

    private void Start()
    {
        _zOffset = new Vector3(0, 0, -10);
    }

    void Update()
    {
        Move();
    }

    public void SetTarget(Transform newTarget)
    {
        _targetTransform = newTarget;
    }

    private void Move()
    {
        //Debug.Log(_targetTransform.position + _zOffset);
        transform.position = Vector3.MoveTowards(transform.position, _targetTransform.position + _zOffset, _smoothness * Time.deltaTime);
    }
}
