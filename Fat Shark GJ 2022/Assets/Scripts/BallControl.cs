using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
using System;
//using TreeEditor;

public class BallControl : MonoBehaviour
{
    [SerializeField][Range(0, 1f)] private float _strikeModifier;
    [SerializeField][Range(0, 1f)] private float _strikeIndicatorScaleModifier;
    [SerializeField] private float _minimumVelocity;
    [SerializeField] private float _maxStrikeCharge;
    [SerializeField] private float _strikeIndicatorOffset;
    [SerializeField] private float _scaleModifier;
    [SerializeField] private float _levelFloorHeight;
    [SerializeField] GameObject _strikeIndicator;

    private Vector2 _respawnPosition;
    private Rigidbody2D _rb;
    private bool _isMoving;
    private float _oldSpeed;
    private float _myRadius;

    public UnityEvent onBallStoppedMoving;
    public UnityEvent onBallStartedMoving;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _myRadius = GetComponent<CircleCollider2D>().radius;
    }

    public void Strike(Vector2 strike)
    {
        if (strike.magnitude > _maxStrikeCharge)
        {
            strike = strike.normalized * _maxStrikeCharge;
        }
        _rb.AddForce(strike * _strikeModifier, ForceMode2D.Impulse);
        SoundManager.soundManager.PlaySwing();
    }

    private void FixedUpdate()
    {
        ForceStop();
        CheckForStop();
    }

    private void CheckForStop()
    {
        if (_rb.velocity == Vector2.zero)
        {
            if (_isMoving)
            {
                transform.rotation = Quaternion.identity;
                _isMoving = false;
                onBallStoppedMoving.Invoke();
                Debug.Log("A ball stopped moving");
            }
        }

        else if (_rb.velocity.magnitude > 0 && !_isMoving)
        {
            onBallStartedMoving.Invoke();
            _rb.freezeRotation = false;
            _isMoving = true;
            Debug.Log("A ball started moving");
        }
    }

    private void ForceStop()
    {
        if (_oldSpeed > _rb.velocity.magnitude && _rb.velocity.magnitude < _minimumVelocity)
        {
            _rb.freezeRotation = true;
        }
        _oldSpeed = _rb.velocity.magnitude;
    }

    public void DrawStrikeIndicator(Vector2 origin, Vector2 position)
    {
        Vector2 scaleVector = origin - position;
        float scale = scaleVector.magnitude;
        if (scale > _maxStrikeCharge)
        {
            scale = _maxStrikeCharge;
        }
        scale = scale * _strikeIndicatorScaleModifier + 0.5f;

        scaleVector.Normalize();

        _strikeIndicator.SetActive(true);

        _strikeIndicator.transform.localPosition = - scaleVector * (scale * 0.5f + _strikeIndicatorOffset);

        if (scaleVector.x <= 0)
        {
            _strikeIndicator.transform.rotation = Quaternion.Euler(Vector3.back * Vector2.Angle(Vector2.down, scaleVector));
        }
        else
        {
            _strikeIndicator.transform.rotation = Quaternion.Euler(Vector3.forward * Vector2.Angle(Vector2.down, scaleVector));
        }

        _strikeIndicator.transform.localScale = new Vector2(scale * 0.5f, scale) * _scaleModifier;
    }

    public void HideStrikeIndicator()
    {
        _strikeIndicator.SetActive(false);
    }

    public void SetWeight(float newWeight)
    {
        _rb.mass = newWeight;

    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Level"))
        {
            if (/*other.gameObject.transform.position.y < this.transform.position.y - _myRadius && */transform.position.y < _levelFloorHeight)
            {
                SoundManager.soundManager.PlayHitGrass();
            }
            else
            {
                SoundManager.soundManager.PlayHitWall();
            }
        }

        else if (other.gameObject.CompareTag("Hazard"))
        {
            Die();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Ontriggerenter");
        if (other.gameObject.CompareTag("Respawn"))
        {
            CheckPoint checkPoint = other.gameObject.GetComponent<CheckPoint>();
            _respawnPosition = checkPoint.GetRespawnLocation();

            if (checkPoint.IsWinCheck())
            {
                GameManager.gameManager.Victory();
            }
        }
    }

    private void Die()
    {
        Debug.Log("A ball died");
        SoundManager.soundManager.PlayDeath();
        transform.position = _respawnPosition;
    }

    public bool GetIsMoving()
    {
        return _isMoving;
    }
}

