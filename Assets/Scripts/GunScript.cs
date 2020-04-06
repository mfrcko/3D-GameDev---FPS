using UnityEngine;

public class GunScript : MonoBehaviour
{
    //Public
    public float damage = 10f;
    public float range = 100f;
    public float fireRate = 15f;

    public Camera fpsCam;
    public ParticleSystem muzzle;
    public GameObject impactEffect;

    //Private
    private float nextTimeToFire = 0f;

    void Update()
    {
        //Shoots with the desired fire rate
        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
        }
    }

    void Shoot()
    {
        muzzle.Play(); //Plays muzzle particle
        RaycastHit hit;
        if(Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name); //Displays what object was hit

            GameObject impactGameObject = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal)); //Instantiates impact particle
            Destroy(impactGameObject, 6f); //Destroys impact particle after time
        }

    }
}
