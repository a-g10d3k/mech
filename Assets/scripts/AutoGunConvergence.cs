using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoGunConvergence : MonoBehaviour
{
    public Player Player { get => gameObject.GetComponent<Player>(); }
    public Transform Gun1;
    public Transform Gun2;
    public float MinimumDistance;
    public float Lerp = 3f;
    public Vector3 Point { get => GetPoint(); set { _point = value; } }
    private Vector3 _point;

    private void Update()
    {
        Aim(Gun1);
        Aim(Gun2);
    }

    private void Aim(Transform gun)
    {
        Quaternion direction = Quaternion.LookRotation(Point - gun.position, Vector3.up) * Quaternion.AngleAxis(-90, Vector3.up);
        gun.rotation = Quaternion.Lerp(gun.rotation, direction, Lerp * Time.deltaTime);
    }

    private Vector3 GetPoint()
    {
        var distance = _point - transform.position;
        if (distance.magnitude < MinimumDistance)
        {
            _point = distance.normalized * MinimumDistance + transform.position;
        }

        return _point;
    }

}
