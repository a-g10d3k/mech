using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatlingGun : MonoBehaviour
{
    public bool Fire;
    public GameObject Projectile;
    public GameObject Barrel;
    public float ProjectileVelocity;
    public float Inaccuracy = 1f;
    public float SpinupTime = 1f;
    public float RPM = 750;
    public float spinSpeed = 0.0f;
    public AudioSource SpinSound;
    private bool _spinPlaying = false;
    public float SpinPitch = 1f;
    public float SpinVolume = 0.5f;
    public AudioSource FireSound;
    private bool _firePlaying = false;

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            SpinGatling(true);
        }
        else
        {
            SpinGatling(false);
        }
    }

    private void SpinGatling(bool spin)
    {
        if (spin)
        {
            spinSpeed += Time.deltaTime / SpinupTime;
            if(spinSpeed > 1f) 
            { 
                spinSpeed = 1f;
                if (!_firePlaying)
                {
                    FireSound.Play();
                    _firePlaying = true;
                }
            }
            if (!_spinPlaying) 
            {
                SpinSound.Play();
                _spinPlaying = true; 
            }
        }
        else
        {
            spinSpeed -= Time.deltaTime / SpinupTime;
            if (spinSpeed < 0f) 
            { 
                spinSpeed = 0f;
                if (_spinPlaying)
                {
                    SpinSound.Stop();
                    _spinPlaying = false;
                }
            }
            if (_firePlaying)
            {
                FireSound.Stop();
                _firePlaying = false;
            }
        }
        Barrel.transform.rotation *= Quaternion.Euler(6 * RPM * spinSpeed * Time.deltaTime, 0, 0);
        SpinSound.volume = spinSpeed*SpinVolume;
        SpinSound.pitch = spinSpeed*SpinPitch;
    }

    private void FixedUpdate()
    {
        if (spinSpeed >= 1f)
        {
            var inaccuracyX = Quaternion.AngleAxis(Random.Range(-Inaccuracy, Inaccuracy), transform.forward);
            var inaccuracyY = Quaternion.AngleAxis(Random.Range(-Inaccuracy, Inaccuracy), transform.up);
            var projectileRotation = Quaternion.LookRotation(transform.right, Vector3.up) * inaccuracyX * inaccuracyY;
            var projectile = Instantiate(Projectile, transform.position - 3.5f * transform.right, projectileRotation);
            projectile.GetComponent<Rigidbody>().velocity = projectileRotation * Vector3.forward * ProjectileVelocity;
            Physics.IgnoreCollision(projectile.GetComponent<Collider>(), GameManager.I.Player.GetComponent<Collider>(), true);
            Destroy(projectile, 4f);
        }
    }
}
