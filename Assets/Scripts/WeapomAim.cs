using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeapomAim : MonoBehaviour
{
    private Transform aimTransform;
    public Camera camera;

    public Transform firePoint;
    public GameObject bulletPrefab;
    public float bulletForce = 20f;
    private void Awake()
    {
        aimTransform = transform.Find("Aim");
    }

    private void Update()
    {
        HandleAiming();
        HandleShooting();
    }

    private void HandleAiming()
    {
        Vector3 mousePos = GetMousePos();

        Vector3 aimDirection = (mousePos - transform.position).normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        aimTransform.eulerAngles = new Vector3(0, 0, angle);

        Vector3 localScale = Vector3.one;
        if(angle > 90 || angle < -90)
            localScale.y =  -1f;
        else
            localScale.y = +1f;
        aimTransform.localScale = localScale;
    }

    private void HandleShooting()
    {
        if(Input.GetButtonDown("Fire1") && this.gameObject.GetComponent<Status>().ShootRifle())
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            bullet.tag = "Bullet";
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(firePoint.right * bulletForce, ForceMode2D.Impulse);
            Destroy(bullet, 1);
        }
    }

    private Vector3 GetMousePos()
    {
        return camera.ScreenToWorldPoint(Input.mousePosition);
    }
}
