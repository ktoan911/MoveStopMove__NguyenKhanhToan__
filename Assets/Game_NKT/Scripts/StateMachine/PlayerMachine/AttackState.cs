﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class AttackState : IState<Player>
{

    private float attackDelayTime;
    public void OnEnter(Player t)
    {
        t.IsAttack = true;
        t.IsMoving = false;

        attackDelayTime = 0.35f;

        t.ChangeAnim("Attack");

       

    }

    public void OnExecute(Player t)
    {
        if (t.IsMoving)
        {
            t.IsAttack = false;
            t.currentState.ChangeState(new IdleState());

            return;
        }
        else
        {
            attackDelayTime -= Time.deltaTime;
            if (attackDelayTime > 0f) return;

            if (t.characterInRange.Count > 0)
            {
                WeaponSpawner.Instance.SpawnPlayerWeapon(t);
                SoundManager.Ins.ThrowWeaponMusic();
            }
            t.IsAttack = false;

            t.currentState.ChangeState(new IdleState());


            return;
        }
        
        

    }

    public void OnExit(Player t)
    {

    }





    // if (!t.IsMoving)
    //    {
    //        //t.ChangeRotation();
    //        t.transform.LookAt(t.Target.transform.position);
    //        Debug.Log(t.Target.transform.position);
    //        Vector3 direction = new(t.Target.transform.position.x - t.transform.position.x,
    //            0,
    //            t.Target.transform.position.z - t.transform.position.z);
    //attackTime -= Time.deltaTime;
    //        if (attackTime <= 0) 
    //        {
    //            attackTime = 0.5f;
    //            Debug.Log(direction);
    //            WeaponSpawner.Instance.SpawnWeapon(direction, t.transform.position + Vector3.up, Quaternion.identity);
    //            t.IsAttack = false;
    //            t.currentState.ChangeState(new IdleState());
    //            return;
    //        }

    //    }

}
