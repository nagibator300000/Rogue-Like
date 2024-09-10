using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.AI;

public class MelleEnemy : Enemy
{
    [SerializeField] private string playerTag;
    [SerializeField] private float attackRange; 

    protected GameObject player;
    protected Seeker seeker;

    protected override void Awake()
    {
        base.Awake();
        player = GameObject.FindGameObjectWithTag(playerTag);
        seeker = gameObject.GetComponent<Seeker>();

    }

    private void Update()
    {
        if (player == null) return; 

        FollowPlayer();
    }

    protected void FollowPlayer()
    {
        var direction = seeker?.GetCurrentPath()?.vectorPath[0];
        if (direction == null) return;
        Movement((Vector2)direction);
        /*        if (1000000 > attackRange)
                {
                    agent.isStopped = false; 
                }
                else
                {
                    agent.isStopped = true; 
                    Attack();
                }*/

        LookAtPlayer();
    }

    protected void Attack()
    {
        Debug.Log("Attack player!");
    }


    protected void LookAtPlayer()
    {
        Vector3 direction = (player.transform.position - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
    }
}
