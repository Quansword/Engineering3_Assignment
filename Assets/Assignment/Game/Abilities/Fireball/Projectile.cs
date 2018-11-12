using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Projectile : MonoBehaviour {

    public class CollisionEvent : UnityEvent<Projectile, Collider> {}
    public class ProjectileEvent : UnityEvent<Projectile> {}

    [SerializeField]
    private float speed = 5f;

    [SerializeField]
    private float maxLifetime = 5f;

    [SerializeField]
    private float collisionForce = 100f;    // Newtons

    public UnityEvent OnCollision = null;

    private float lifetime = 0f;
    private bool isAlive = true;
    public bool IsAlive { get { return isAlive; } }

	void Update () {
        if (!isAlive)
            return;

        lifetime += Time.deltaTime;
        if(lifetime >= maxLifetime) {
            isAlive = false;
        }

        transform.position += transform.forward * (speed * Time.deltaTime);
	}

    private void OnTriggerEnter(Collider coll) {
        bool validCollision = false;
        Health health = coll.GetComponentInParent<Health>();
        if(health != null) {
            Damage damage = GetComponent<Damage>();
            damage.DoDamage(health);
            validCollision = true;
        }

        Rigidbody rb = coll.GetComponentInParent<Rigidbody>();
        if(rb != null) {
            Vector3 delta = coll.transform.position - transform.position;
            Vector3 dir = delta.normalized;
            Vector3 force = collisionForce * dir;
            rb.AddForce(force);
            validCollision = true;
        }

        if(validCollision) {
            isAlive = false;
        }
    }
}
