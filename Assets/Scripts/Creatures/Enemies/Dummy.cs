using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dummy : Enemy
{
    public override void TakeDamage(int damage) 
    {
        Debug.Log(damage);
    }

    private void Update()
    {
        Movement(new Vector2(0, 0));
    }
}
