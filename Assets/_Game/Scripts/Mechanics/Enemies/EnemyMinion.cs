﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMinion : EnemyBase
{
    private GameObject playerReference = null;

    [Header("Enemy Minion Attack Rate")]
    [SerializeField] private float attackRate = 0;

    [Header("Enemy Minion Bullet Prefab")]
    [SerializeField] private GameObject bullet;

    private float shotTime;

    private void Start()
    {
        playerReference = GameManager.player.obj;
    }

    private void FixedUpdate()
    {
        UpdateState();
    }

    protected override void UpdateState()
    {
        switch (currentState)
        {
            case EnemyState.Passive:
                Passive();
                break;
            case EnemyState.Attacking:
                Attacking();
                break;
            default:
                break;
        }
    }

    protected override void Passive()
    {
        if (Vector3.Distance(transform.position, playerReference.transform.position) < EnemyDetectionRadius)
        {
            transform.LookAt(playerReference.transform.position);

            Debug.Log("Detect range reached");
            currentState = EnemyState.Attacking;
        }
    }

    protected override void Attacking()
    {
        transform.LookAt(playerReference.transform.position);
        bullet.GetComponent<EnemyProjectile>().SetDamage(AttackDamage);

        if (shotTime <= 0)
        {
            shotTime = attackRate;
            Instantiate(bullet, transform.position, Quaternion.identity);
        }
        else
        {
            shotTime -= Time.deltaTime;
        }
    }

    public override void Dead()
    {
        Debug.Log("Enemy destroyed");
        Destroy(transform.parent.gameObject);
    }
}