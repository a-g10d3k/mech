using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public Player Player { get => gameObject.GetComponent<Player>(); }
    private Animator _animator;
    // Start is called before the first frame update
    void Start()
    {
        _animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateAnimatorProperties();
    }

    void UpdateAnimatorProperties()
    {
        var targetSpeed = (float)System.Math.Round(Player.Movement.NormalizedSpeed, 1);
        targetSpeed = targetSpeed > 1f ? 2.5f : targetSpeed;
        var speed = Mathf.Lerp(_animator.GetFloat("Speed"),targetSpeed, 0.5f);
        _animator.SetFloat("Speed", speed);
    }

}
