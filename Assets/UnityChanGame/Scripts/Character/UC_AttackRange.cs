using System;
using System.Collections;
using System.Collections.Generic;
using UC;
using UnityEngine;

public class UC_AttackRange : MonoBehaviour
{
    //private MoveActor _moveActor;

    private void Awake()
    {
        //_moveActor = this.transform.parent.parent.GetComponent<MoveActor>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Monster"))
        {
            Debug.Log("큐에 포함시키기 : " + other.transform.name);
            MonsterPool.getHitMonster(other.gameObject.GetComponent<UC_Monster>());
        }
    }
}
