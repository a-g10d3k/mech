using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickToGround : MonoBehaviour
{
    public LayerMask StickyLayers;
    public Transform MeshPivot;
    private CapsuleCollider _collider;
    public bool Ground { get => GetGround(); }

    private void Start()
    {
        _collider = gameObject.GetComponent<CapsuleCollider>();
    }

    private bool GetGround()
    {
        RaycastHit hit;
        var ret = Physics.Raycast(transform.position,Vector3.down, out hit, _collider.radius * transform.lossyScale.z *2f, StickyLayers);
        return ret;
    }

    private void OnCollisionStay(Collision collision)
    {
        if(((int)StickyLayers & (1 << collision.collider.gameObject.layer)) == 0)
        {
            return;
        }
        Stick(collision);
    }

    private void Stick(Collision collision)
    {
        Vector3 groundNormal = collision.GetContact(0).normal;
        MeshPivot.transform.position = transform.position + groundNormal * _collider.radius;
        MeshPivot.transform.rotation = Quaternion.LookRotation(Vector3.ProjectOnPlane(transform.forward, groundNormal), groundNormal);
    }
}
