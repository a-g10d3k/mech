using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAiming : MonoBehaviour
{
    public Player Player { get => gameObject.GetComponent<Player>(); }
    public GameObject Recticle;
    public LayerMask LayerMask;

    // Update is called once per frame
    void Update()
    {
        Vector3 point;
        if(Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit ray,Mathf.Infinity, ~LayerMask))
        {
            point = ray.point;
        }
        else
        {
            point = Camera.main.transform.position + Camera.main.transform.forward * 1000;
        }
        Player.AutoGunConvergence.Point = point;
        Recticle.transform.position = point;
    }
}
