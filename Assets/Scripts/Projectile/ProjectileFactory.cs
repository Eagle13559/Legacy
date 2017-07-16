using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileFactory : MonoBehaviour {

    [SerializeField]
    private GameObject projectileBase;

    public void ShootProjectile(Vector2 forceVelocity, bool facingRight)
    {
       GameObject projectileInstance = Instantiate(projectileBase, this.transform);
       projectileInstance.transform.position = this.transform.position;

       projectileInstance.GetComponent<Projectile>().shootProjectile(forceVelocity, facingRight);
    }
}
