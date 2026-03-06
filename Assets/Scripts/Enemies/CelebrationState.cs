using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CelebrationState : MonoBehaviour
{
    private Animator _animator;

        private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _animator.Play("Celebration");
    }

    private void OnDisable()
    {
        _animator.StopPlayback();
    }
}
