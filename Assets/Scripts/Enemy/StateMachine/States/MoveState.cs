using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : State
{
    [SerializeField] private float _minimumSpeed;
    [SerializeField] private float _maximumSpeed;

    private float _speed;

    private void Start()
    {
        _speed = Random.Range(_minimumSpeed, _maximumSpeed);
    }

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, Target.transform.position, _speed * Time.deltaTime);
    }
}
