using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creatures : MonoBehaviour
{
    [SerializeField] protected float speed;
    [SerializeField] protected int hp;
    [SerializeField] protected int damage;
    [SerializeField] protected bool isFlying;
    protected bool canMove = true;
    public void SetCanMove (bool val) => canMove = val;

    protected Rigidbody2D rb;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Movement(Vector2 direction) 
    {
        if (canMove)
        {
            rb.velocity = direction * speed;
        }
    }
    public virtual void TakeDamage(int damage)
    {
        hp -= damage;
    }
}
