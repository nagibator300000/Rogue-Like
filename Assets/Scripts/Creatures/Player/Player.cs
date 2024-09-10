using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Creatures
{
    [SerializeField] private GameObject bulletPref;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private Transform shootPos;
    [SerializeField] private float kickForce = 30f;
    [SerializeField] private float kickDuration = 0.5f;
    [SerializeField] private float kickCooldown = 0.5f;
    [SerializeField] private Collider2D kickHitbox;

    bool canKick = true;

    private PropAnimation anim;

    private void Awake()
    {
        base.Awake();
        anim = GetComponent<PropAnimation>(); 
    }

    private void Update()
    {
        Scope();
    }

    private void Scope()
    {
        var mousePos = Input.mousePosition;
        var worldPos = Camera.main.ScreenToWorldPoint(mousePos);
        var direction = worldPos - transform.position;
        var angle = (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg) - 90;
        switch (angle)
        {
            case 0:
                anim.SetClip("up");
                return;
            case 90:
                anim.SetClip("left");
                return;
            case -180:
                anim.SetClip("down");
                return;
            case -90:
                anim.SetClip("right");
                return;
        }
    }

    public void Shoot()
    {
        var bullet = Instantiate(bulletPref, shootPos.position, shootPos.transform.rotation);
        var bulletScript = bullet.GetComponent<Bullet>();

        bulletScript.SetDamage(damage);
        var bulletRb = bullet.GetComponent<Rigidbody2D>();

        bulletRb.velocity = shootPos.transform.up * bulletSpeed;
    }

    public void TryKick()
    {
        if (canKick) 
        { 
            Kick();
            StartCoroutine(StartKickCooldown());
        } 
    }

    private void Kick()
    {
        Collider2D[] hitColliders = new Collider2D[10];
        ContactFilter2D filter = new ContactFilter2D();
        int count = kickHitbox.OverlapCollider(filter,hitColliders);

        for (int i =0; i < count; i++ )
        {
            var hitCollider = hitColliders[i];

            if (hitCollider.gameObject == gameObject) continue;

            Rigidbody2D rb = hitCollider.GetComponent<Rigidbody2D>();
            Creatures creatures = hitCollider.GetComponent<Creatures>();
            if (rb != null)
            {
                Vector2 direction = (hitCollider.transform.position - transform.position).normalized;
                rb.AddForce(direction * kickForce, ForceMode2D.Impulse);
                creatures?.SetCanMove(false);
                StartCoroutine(StopForce(rb, creatures));
            }
        }
    }
    
    IEnumerator StartKickCooldown ()
    {
        canKick = false;
        yield return new WaitForSeconds(kickCooldown);
        canKick = true;
    }

    IEnumerator StopForce(Rigidbody2D rb,Creatures creatures) 
    {
        yield return new WaitForSeconds(kickDuration);
        rb.velocity = Vector2.zero;
        creatures?.SetCanMove(true);
    }

}
