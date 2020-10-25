using UnityEngine;
using System.Collections;
using System;

public class ShakePrefab : MonoBehaviour
{
    [Header("Info")]
    private Vector3 _startPos;
    private float _timer;
    private Vector3 _randomPos;

    [Header("Settings")]
    [Range(0f, 2f)]
    public float _time = 0.2f;
    [Range(0f, 2f)]
    public float _distance = 0.1f;
    [Range(0f, 0.1f)]
    public float _delayBetweenShakes = 0f;

    private void Awake()
    {
        _startPos = transform.position;
    }

    private void OnValidate()
    {
        if (_delayBetweenShakes > _time)
            _delayBetweenShakes = _time;
    }

    public void Begin(Action whenDone)
    {
        if (!this.gameObject.activeInHierarchy)
        {
            whenDone();
            return;
        }
        StartCoroutine(Shake(whenDone));
    }

    private IEnumerator Shake(Action whenDone)
    {
        var start = DateTime.Now;
        Debug.Log("Started shaking");
        _timer = 0f;

        while (_timer < _time)
        {
            _timer += Time.deltaTime;

            _randomPos = _startPos + (UnityEngine.Random.insideUnitSphere * _distance);

            transform.position = _randomPos;

            if (_delayBetweenShakes > 0f)
            {
                yield return new WaitForSeconds(_delayBetweenShakes);
            }
            else
            {
                yield return null;
            }
        }

        Debug.Log("Finisehd shaking after " + (DateTime.Now - start).TotalMilliseconds + " millis");
        transform.position = _startPos;
        whenDone();
        Destroy(this);
    }

}