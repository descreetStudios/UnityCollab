using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{

    public float damage = 10f;
    public float range = 100f;
    private float timer = 0f;
    private float impactForce = 60f;
   // private PlayerMovement speed;
    public Camera fpsCam;
    public ParticleSystem muzzleFlash;

    public GameObject impactEffect;

    private bool canShoot = true;
    private float cooldown = 0.175f;

    void Update()
    {
        timer += Time.deltaTime;
        //Debug.Log(timer);

        if (timer >= cooldown)
        {
            //Debug.Log("pass");
            timer = 0f;
            canShoot = true;
        }

        if (Input.GetButton("Fire1") & canShoot == true)
        {
            //Debug.Log("Called");
            Shoot();
        }

    }

    void Shoot()
    {
        //Debug.Log("Executed");
        canShoot = false;
        muzzleFlash.Play();

        RaycastHit hit;
       if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);

            Target target = hit.transform.GetComponent<Target>();
            if(target != null)
            {
                target.TakeDamage(damage);
                //Debug.Log("Dealed Damage");
            }

            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }

            if (hit.transform.name != "First Person Player")
            {
               GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
               Destroy(impactGO, 2f);
            }
            

        }
        
    }

}
