using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{

    public float damage = 10f;
    public float range = 100f;

    private float timer = 0f;

    public Camera fpsCam;
    public ParticleSystem muzzleFlash;

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
            //Debug.Log(hit.transform.name);

            Target target = hit.transform.GetComponent<Target>();
            if(target != null)
            {
                target.TakeDamage(damage);
                //Debug.Log("Dealed Damage");
            }
        }
        
    }

}
