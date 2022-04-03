using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    [SerializeField] private Vector3 _movementVector;
    [SerializeField] private float period = 2f;
    private float _movementFactor;
    private const float tau = Mathf.PI * 2;
    private Vector3 _startPosition;

    void Start()
    {
        _startPosition = transform.position;
    }


    void Update()
    {
        if(period <= Mathf.Epsilon) { return; }

        float cycles = Time.time / period; //grows over time

        float rawSinWave = Mathf.Sin(cycles * tau); //Sin is going from -1 to 1

        _movementFactor = (rawSinWave + 1f) / 2f; // recalculation for getting convenience


        Vector3 offset = _movementVector * _movementFactor;
        transform.position = _startPosition + offset;
    }
}
