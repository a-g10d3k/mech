using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatlingProjectile : MonoBehaviour
{
    public GameObject OnHitParticle;

    private void OnCollisionEnter(Collision collision)
    {
        var particle = Instantiate(OnHitParticle, transform.position, Quaternion.identity);
        Destroy(particle, 3f);
        particle.GetComponent<AudioSource>().pitch += Random.Range(-0.1f, 0.1f);

        var health = collision.collider.gameObject.GetComponent<Health>();
        if (health != null)
        {
            health.HP--;
        }

        Destroy(gameObject, Time.fixedDeltaTime);
    }
}
