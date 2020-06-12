using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Weapon : MonoBehaviour
{
    public float fireRate = 0f;
    public Transform fireBorn;
    public GameObject hitEffect;
    public float damage = 10f;
    public LayerMask mask;
    float timeToFire = 0;

    float timeToBullet = 0;
    public float bulletRate = 0;
    public GameObject bulletPrefab;

    public GameObject muzzleFlash;
    public GameObject gameMaster;
    // Start is called before the first frame update
    void Start()
    {
        gameMaster = GameObject.Find("GameMaster");
    }

    // Update is called once per frame
    void Update()
    {
        if (fireRate==0)
        {
            if (Input.GetMouseButton(0))
            {
                Shoot();
            }
        }
        else
        {
            if (Input.GetMouseButton(0) && Time.time>timeToFire)
            {
                timeToFire = Time.time + 1 / fireRate;
                Shoot();
            }
        }
    }
    void Shoot()
    {
        Vector3 mousePos = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        Vector3 fireBornPos = new Vector2(fireBorn.position.x, fireBorn.position.y);
        RaycastHit2D raycastHit= Physics2D.Raycast(fireBornPos, mousePos - fireBornPos, 100, mask);
        if (raycastHit.collider != null)
        {
            Enemy enemy = raycastHit.collider.GetComponent<Enemy>();
            if (enemy != null)
            {         
                enemy.Damage(damage);
            }
            gameMaster.GetComponent<GameMaster>().GetComponent<CameraShake>().Shake(0.1f, 0.2f);
            GameObject hitEffectIns=Instantiate(hitEffect, raycastHit.point, Quaternion.FromToRotation(Vector3.up, raycastHit.normal)) as GameObject;
            Destroy(hitEffectIns, 1f);
        }
        if (Time.time>timeToBullet)
        {
            Vector3 hitPos;
            if (raycastHit.collider==null)
            {
                hitPos = (mousePos - fireBorn.position) * 30;
            }
            else
            {
                hitPos = raycastHit.point;
            }
            Bullet(hitPos);
            timeToBullet = Time.time + 1 / bulletRate;
        } 
        
    }
    void Bullet(Vector3 hitPos)
    {
        GameObject bulletIns = Instantiate(bulletPrefab, fireBorn.position, fireBorn.rotation);
        LineRenderer line = bulletIns.GetComponent<LineRenderer>();
        if (line!=null)
        {
            line.SetPosition(0, fireBorn.position);
            line.SetPosition(1, hitPos);
        }
        Destroy(bulletIns.gameObject, 0.04f);
        GameObject muzzleFlashIns = Instantiate(muzzleFlash, fireBorn.position, fireBorn.rotation);
        muzzleFlashIns.transform.parent = fireBorn;
        Destroy(muzzleFlashIns, 0.2f);
    }
}
