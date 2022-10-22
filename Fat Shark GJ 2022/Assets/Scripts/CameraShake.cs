using UnityEngine;

public class CameraShake
{
    private Vector3 _shakeOffset = Vector3.zero;
    private float _shakeMaxValue = 0;
    private float _shakeTimer = 0;
    private float _shakeDecay = 0;

    public Vector3 UpdateScreenShake()
    {
        if (_shakeTimer > 0)
        {
            _shakeMaxValue -= _shakeTimer * _shakeDecay;
            _shakeOffset = new Vector3(Random.Range(-_shakeMaxValue, _shakeMaxValue), Random.Range(-_shakeMaxValue, _shakeMaxValue), 0);
            _shakeTimer -= Time.deltaTime;
        }
        else
        {
            _shakeOffset = Vector3.zero;
        }
        return _shakeOffset;
    }

    public void ApplyScreenShake(float intensity = 1f, float time = 0.2f, float decay = 0.1f)
    {
        _shakeTimer = time;
        _shakeMaxValue = intensity;
        _shakeDecay = decay;
    }
}
