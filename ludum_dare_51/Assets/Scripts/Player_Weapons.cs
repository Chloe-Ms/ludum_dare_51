using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class Player_Weapons : MonoBehaviour
{
    [SerializeField] 
    private GameObject bulletPrefab;
    public float bulletSpeed = 20f;

    public GameObject currentWeapon;

    public GameObject PistolModel;
    public GameObject AssaultRifleModel;
    public GameObject ShotGunModel;
    public GameObject SabreModel;

    public GameObject Enemy;
    
    public GameObject pistol;
    public float damageWeaponPistol;
    public float rangeWeaponPistol;
    public float fireRateWeaponPistol;
    public float impactForceWeaponPistol;
    public float reloadTimeWeaponPistol;
    public int maxAmmoWeaponPistol;
    public int currentAmmoWeaponPistol;
    public bool isReloadingWeaponPistol;

    public GameObject weaponAssaultRifle;
    public float damageWeaponAssaultRifle;
    public float rangeWeaponAssaultRifle;
    public float fireRateWeaponAssaultRifle;
    public float reloadTimeWeaponAssaultRifle;
    public int maxAmmoWeaponAssaultRifle;
    public int currentAmmoWeaponAssaultRifle;
    public bool isReloadingWeaponAssaultRifle;
    
    public GameObject weaponShotGun;
    public float damageWeaponShotGun;
    public float rangeWeaponShotGun;
    public float fireRateWeaponShotGun;
    public float reloadTimeWeaponShotGun;
    public int maxAmmoWeaponShotGun;
    public int currentAmmoWeaponShotGun;
    public bool isReloadingWeaponShotGun;

    public GameObject weaponSabre;
    public float damageWeaponSabre;
    public float rangeWeaponSabre;
    public float fireRateWeaponSabre;
    public float reloadTimeWeaponSabre;
    public int maxAmmoWeaponSabre;
    public int currentAmmoWeaponSabre;
    public bool isReloadingWeaponSabre;

    // Update is called once per frame
    void Update()
    {

        IEnumerator Reload()
        {
            isReloadingWeaponPistol = true;
            Debug.Log("Reloading...");
            yield return new WaitForSeconds(reloadTimeWeaponPistol);
            currentAmmoWeaponPistol = maxAmmoWeaponPistol;
            isReloadingWeaponPistol = false;
        }

        IEnumerator ReloadAssaultRifle()
        {
            isReloadingWeaponAssaultRifle = true;
            Debug.Log("Reloading...");
            yield return new WaitForSeconds(reloadTimeWeaponAssaultRifle);
            currentAmmoWeaponAssaultRifle = maxAmmoWeaponAssaultRifle;
            isReloadingWeaponAssaultRifle = false;
        }

        IEnumerator ReloadShotGun()
        {
            isReloadingWeaponShotGun = true;
            Debug.Log("Reloading...");
            yield return new WaitForSeconds(reloadTimeWeaponShotGun);
            currentAmmoWeaponShotGun = maxAmmoWeaponShotGun;
            isReloadingWeaponShotGun = false;
        }

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.1f);

        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject.tag == "Weapon")
            {
                if (collider.gameObject.name == "Pistol")
                {
                    pistol.SetActive(false);
                    gameObject.GetComponent<Player_Weapons>().currentWeapon = pistol;
                    PistolModel.SetActive(true);
                    AssaultRifleModel.SetActive(false);
                    ShotGunModel.SetActive(false);
                    SabreModel.SetActive(false);
                }
                if (collider.gameObject.name == "AssaultRifle")
                {
                    weaponAssaultRifle.SetActive(false);
                    gameObject.GetComponent<Player_Weapons>().currentWeapon = weaponAssaultRifle;
                    PistolModel.SetActive(false);
                    AssaultRifleModel.SetActive(true);
                    ShotGunModel.SetActive(false);
                    SabreModel.SetActive(false);
                }
                if (collider.gameObject.name == "ShotGun")
                {
                    weaponShotGun.SetActive(false);
                    gameObject.GetComponent<Player_Weapons>().currentWeapon = weaponShotGun;
                    PistolModel.SetActive(false);
                    AssaultRifleModel.SetActive(false);
                    ShotGunModel.SetActive(true);
                    SabreModel.SetActive(false);
                }
                if (collider.gameObject.name == "Sabre")
                {
                    weaponSabre.SetActive(false);
                    gameObject.GetComponent<Player_Weapons>().currentWeapon = weaponSabre;
                    PistolModel.SetActive(false);
                    AssaultRifleModel.SetActive(false);
                    ShotGunModel.SetActive(false);
                    SabreModel.SetActive(true);
                }
            }
        }

        if (currentWeapon == pistol && !isReloadingWeaponPistol)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                currentAmmoWeaponPistol--;
                
                if (currentAmmoWeaponPistol > 0)
                {
                    shoot();
                    
                }
                else
                {
                    StartCoroutine(Reload());
                }
            }
        }
    }
    void shoot()
    {
        if (Player_move.facingRight)
        {
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(bulletSpeed, 0);
        }
        else
        {
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(-bulletSpeed, 0);
        }
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.right * transform.localScale.y, rangeWeaponPistol);
        Debug.DrawRay(transform.position, Vector2.right * transform.localScale.y * rangeWeaponPistol, Color.red, 100f);
        if (hitInfo)
        {
            Enemy enemy = hitInfo.transform.GetComponent<Enemy>();
            Destroy(bullet, 2f);
            if (enemy != null)
            {
                enemy.TakeDamage(damageWeaponPistol);
            }

        }
    }
}
