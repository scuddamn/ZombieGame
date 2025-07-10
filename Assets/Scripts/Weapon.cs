using System.Collections;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] Camera camera;
    [SerializeField] float range = 100f;
    [SerializeField] float damage = 30f;
    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] private GameObject bulletHit;
    [SerializeField] private Ammo ammoSlot;
    [SerializeField] private float timeBetweenShots = 0.5f;
    [SerializeField] private AmmoType ammoType;
    private bool canShoot = true;

    private void OnEnable()
    {
        canShoot = true;
    }
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && canShoot == true)
        {
            StartCoroutine(Shoot());
        }
    }

    IEnumerator Shoot()
    {
        canShoot = false;
        
        if (ammoSlot.GetCurrentAmmo(ammoType) > 0)
        {
            PlayMuzzleFlash();
            ProcessRaycast();
            ammoSlot.ReduceCurrentAmmo(ammoType);
        }

        yield return new WaitForSeconds(timeBetweenShots);
        canShoot = true;
    }

    void ProcessRaycast()
    {
        RaycastHit hit;
        if (Physics.Raycast(camera.transform.position, camera.transform.forward, out hit, range))
        {
            CreateHitImpact(hit);
            Debug.Log(hit.transform.name + "hit");
            EnemyHealth target = hit.transform.GetComponent<EnemyHealth>();
            if (target == null) return;
            target.TakeDamage(damage);
        }
        else
        {
            return;
        }
    }

    private void PlayMuzzleFlash()
    {
        muzzleFlash.Play();
    }

    void CreateHitImpact(RaycastHit hit)
    {
        GameObject impact = Instantiate(bulletHit, hit.point, Quaternion.LookRotation(hit.normal));
        Destroy(impact, 0.1f);
    }
}
