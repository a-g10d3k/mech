using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ExtensionMethods;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float _maxSpeed;
    public float MaxSpeed { get => GetMaxSpeed(); set => _maxSpeed = value; }
    public float StopFriction;
    public float JumpForce;
    public float NormalizedSpeed { get => GetNormalizedSpeed(); }
    [Range(0f,1f)]
    public float TimeToAccelerate = 0.2f;
    public Player Player { get => gameObject.GetComponent<Player>(); }
    public Vector3 Acceleration { get => GetAcceleration();}
    private Rigidbody _rb;

    private bool GetGround()
    {
        RaycastHit hit;
        var ret = Physics.Raycast(transform.position, Vector3.down, out hit, 10.5f);
        return ret;
    }

    void Start()
    {
        _rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateRigidbody();
    }

    private Vector3 GetAcceleration()
    {
        var input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        var ret = transform.TransformDirection(input.ToVector3()) * MaxSpeed * (Time.deltaTime / TimeToAccelerate);
        if (Input.GetButtonDown("Jump"))
        {
            ret += JumpForce/2 * Vector3.up + transform.TransformDirection(input.ToVector3()) * JumpForce;
        }
        return ret;
    }
    
    private float GetNormalizedSpeed()
    {
        return _rb.velocity.ToVector2().magnitude / _maxSpeed;
    }

    private void UpdateRigidbody()
    {
        if (!GetGround()) { return; }

        _rb.velocity += Acceleration;
        var horizontalVelocity = _rb.velocity.ToVector2();
        if(horizontalVelocity.magnitude > MaxSpeed)
        {
            horizontalVelocity = horizontalVelocity.normalized * MaxSpeed;
            _rb.velocity = new Vector3(horizontalVelocity.x,_rb.velocity.y,horizontalVelocity.y);
        }
        UpdateFriction();
    }

    private void UpdateFriction()
    {
        if (Acceleration.x + Acceleration.y == 0f)
        {
            gameObject.GetComponent<Collider>().material.dynamicFriction = StopFriction;
        }
        else
        {
            gameObject.GetComponent<Collider>().material.dynamicFriction = 0f;
        }
    }

    private float GetMaxSpeed()
    {
        if (Input.GetButton("Run"))
        {
            return _maxSpeed * 1.75f;
        }
        else
        {
            return _maxSpeed;
        }
    }
}
