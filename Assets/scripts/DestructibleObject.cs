using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleObject : MonoBehaviour
{
    private bool _animationPlaying = false;
    public float FallDistance = 1f;
    public float TippingAngle = 30f;
    private Vector3 _targetPosition;
    private Quaternion _targetRotation;
    public GameObject FireParticles;
    public AudioClip CollapseSound;

    private void Start()
    {
        var health = gameObject.GetComponent<Health>();
        if (health != null)
        {
            health.Death += Death;
        }
    }

    private void Update()
    {
        AnimateDeath();
    }

    private void Death(object sender, EventArgs e)
    {
        var renderer = gameObject.GetComponent<Renderer>();
        if(renderer == null) { return; }
        renderer.material.SetColor("_Color", new Color(0.15f, 0.15f, 0.15f));

        var particles = Instantiate(FireParticles,transform);
        particles.transform.position = transform.position;

        var collider = gameObject.GetComponent<Collider>(); 
        if(collider != null)
        {
            collider.enabled = false;
        }

        var audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.spatialBlend = 0.5f;
        audioSource.PlayOneShot(CollapseSound);

        InitDeathAnimation();
        gameObject.GetComponent<Health>().Death -= Death;
        Destroy(this, 5f);
    }

    private void InitDeathAnimation()
    {
        _targetPosition = transform.position - FallDistance * Vector3.up;
        var rotationAxis = Quaternion.AngleAxis(UnityEngine.Random.Range(0f, 360f), Vector3.up) * Vector3.forward;
        _targetRotation = Quaternion.AngleAxis(TippingAngle, rotationAxis) * transform.rotation;
        _animationPlaying = true;
    }

    private void AnimateDeath()
    {
        if (!_animationPlaying) { return; }
        transform.position = Vector3.Lerp(transform.position, _targetPosition, 2 * Time.deltaTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, _targetRotation, 2 * Time.deltaTime);
        
    }
}
