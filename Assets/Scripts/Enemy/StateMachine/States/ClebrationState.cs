using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]

public class ClebrationState : State
{
    private const string Celebration = "Celebration";

    private Animator _animator;

    private void Awake()
    {
        _animator=GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _animator.Play(Celebration);
    }

    private void OnDisable()
    {
        _animator.StopPlayback();
    }
}
