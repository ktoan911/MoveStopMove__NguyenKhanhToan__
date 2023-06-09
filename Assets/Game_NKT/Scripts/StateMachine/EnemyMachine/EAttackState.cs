﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EAttackState : IState<Enemy>
{
    private float timeAttack;

    public void OnEnter(Enemy t)
    {
        t.IsMoving = false;

        timeAttack = 0.5f;

        t.ChangeAnim("Attack");
   
    }

    public void OnExecute(Enemy t)
    {
        timeAttack -= Time.deltaTime;
        if (timeAttack > 0) return;

        if (t.characterInRange.Count > 0)
        {
            WeaponSpawner.Instance.SpawnEnemyWeapon(t);
        }

        t.IsAttack = false;
        t.currentState.ChangeState(new EIdleState());

        return;
    }

    public void OnExit(Enemy t)
    {
        
    }
}
