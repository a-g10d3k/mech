using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class RocketCarAI : MonoBehaviour
{
    public float OrbitDistance;
    [Range(0f,1f)]
    public float OrbitDirectionChangeChance;
    public float Speed;
    public float CollisionAvoidanceRadius;
    public LayerMask CollisionAvoidanceMask;
    private Vector3 _orbitTarget;
    private int _orbitDirection = 1;


    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawSphere(_orbitTarget, 1f);
    }

    private void Start()
    {
        InvokeRepeating("ChangeOrbitDirection", 0f, Random.Range(4f, 8f));
    }

    private void Update()
    {
        var player = GameManager.I.Player.transform;
        Orbit(player);
        AvoidCollision();
        UpdateRotation();
        
    }

    private void ChangeOrbitDirection()
    {
        if(Random.Range(0f,1f) < OrbitDirectionChangeChance)
        {
            _orbitDirection = -_orbitDirection;
        }
    }

    private void Orbit(Transform target)
    {
        Orbit(target, OrbitDistance);
    }

    private void Orbit(Transform target, float distance)
    {
        if (!gameObject.GetComponent<StickToGround>().Ground) { return; }
        var rb = gameObject.GetComponent<Rigidbody>();
        var targetPos = target.position + Quaternion.AngleAxis(90,Vector3.up) * (target.position - transform.position).normalized * distance * _orbitDirection;
        _orbitTarget = targetPos;
        var velocity = (targetPos - transform.position).normalized * Speed;
        velocity = new Vector3(velocity.x, rb.velocity.y, velocity.z);
        rb.velocity = Vector3.Lerp(rb.velocity, velocity, 3f * Time.deltaTime);
    }

    private void AvoidCollision()
    {
        if (!gameObject.GetComponent<StickToGround>().Ground) { return; }
        var colliders = Physics.OverlapSphere(transform.position, CollisionAvoidanceRadius, CollisionAvoidanceMask);
        var rb = gameObject.GetComponent<Rigidbody>();
        var vel = Vector3.zero;
        foreach (Collider collider in colliders)
        {
            var colliderV = collider.transform.position - transform.position;
            colliderV = Vector3.ProjectOnPlane(colliderV, Vector3.down);
            vel -= colliderV.normalized * Mathf.Pow(3/colliderV.magnitude,4) * (rb.velocity.magnitude + 0.1f);
        }
        vel = (rb.velocity.normalized + vel.normalized).normalized * (rb.velocity.magnitude + 0.1f);
        var vmag = rb.velocity.magnitude;
        rb.velocity = Vector3.Lerp(rb.velocity, vel, 6f * Time.deltaTime).normalized * vmag;
    }
    
    private void UpdateRotation()
    {
        var rb = gameObject.GetComponent<Rigidbody>();
        transform.forward = new Vector3(rb.velocity.x, 0, rb.velocity.z);
    }

    private void TargetPlayer()
    {

    }
}
