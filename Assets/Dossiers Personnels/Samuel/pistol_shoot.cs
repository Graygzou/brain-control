using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pistol_shoot : MonoBehaviour {

    // Use this for initialization
    public GameObject prefab;
    public GameObject muzzle;

    //Reaload
    [SerializeField]
    private int MaxAmmo;
    private int currentAmmo;
    [SerializeField]
    private float reloadTime;
    private Animator animator;

    public float Attackrate = 10;
    private float nextTimeToAttack = 0;

    public float BulletSpeed;

    public AudioSource bruitage;
    private void Start()
    {
        currentAmmo = MaxAmmo;
        animator = GetComponentInParent<Animator>();
        bruitage = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if(currentAmmo == 0)
        {
            StartCoroutine(Reload());
        }
    }


    public void Fire(Vector3 dir)
    {
        
        if(Time.time >= nextTimeToAttack && currentAmmo > 0)
        {
            nextTimeToAttack = (Time.time + 1f / Attackrate);
            GameObject projectile = Instantiate(prefab) as GameObject;
            projectile.transform.rotation = muzzle.transform.rotation;
            projectile.transform.position = muzzle.transform.position;
            projectile.GetComponent<basic_hit>().damage = 10;
            projectile.GetComponent<basic_hit>().player = transform.parent.GetComponent<PlayerStatus>();
            bruitage.Play();
            projectile.GetComponent<Rigidbody2D>().AddForce(dir * BulletSpeed);
            currentAmmo--;
        }

    }

    public IEnumerator Reload()
    {
        animator.SetBool("isReloading", true);
        animator.SetBool("isShooting", false);
        animator.SetBool("isMoving", false);             
        yield return new WaitForSecondsRealtime(reloadTime);
        animator.SetBool("isReloading", false);
        currentAmmo = MaxAmmo;
    }

    #region Get/Set
    public int GetCurrentAmmo()
    {
        return currentAmmo;
    }
    public int GetMaxAmmo()
    {
        return MaxAmmo;
    }
    #endregion

}



