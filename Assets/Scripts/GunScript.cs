using System;
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
    private GameManager gameManager;
    private bool singleShoot = false;

    //sounds
    public SoundManager soundManager;
    public AudioClip shoot;
    public AudioClip au;


    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag(Tags.GameManager).GetComponent<GameManager>();
    }

    void Update()
    {
        //Shoots with the desired fire rate
        if (Input.GetKeyDown("x"))
        {
            singleShoot = !singleShoot;
        }

        if (singleShoot && Input.GetButton("Fire1"))
        {
            Shoot();
        }

        else
        {
            if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
            {
                nextTimeToFire = Time.time + 1f / fireRate;
                Shoot();
            }
        }

    }

    void Shoot()
    {
        muzzle.Play(); //Plays muzzle particle
        soundManager.PlaySingle(shoot);
        RaycastHit hit;
        if(Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name); //Displays what object was hit
            IsTargetHit(hit); 

            GameObject impactGameObject = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal)); //Instantiates impact particle
            Destroy(impactGameObject, 6f); //Destroys impact particle after time
        }
    }

    private void IsTargetHit(RaycastHit hit)
    {
        if (hit.transform.tag == Tags.ENEMY || hit.transform.tag == Tags.PERSON) {
            soundManager.RandomizeSfx(au);
            gameManager.TargetHit(hit.transform.gameObject);
        }
    }
}
