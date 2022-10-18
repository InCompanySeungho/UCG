using System;
using System.Collections;
using System.Collections.Generic;
using UC;
using UnityEngine;

public class UC_AttackRange : MonoBehaviour
{
    private UC_CharacterBase _base;
    private BoxCollider _collider;

    private void Awake()
    {
        // 부모관계 변경 시 수정해줘야함
        _collider = this.GetComponent<BoxCollider>();
        _base = this.transform.parent.parent.GetComponent<UC_CharacterBase>();
        ColliderActive(true);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Monster"))
        {
            MonsterPool.in_HitMonster(other.GetComponent<UC_Monster>());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Monster"))
        {
            MonsterPool.Out_HitMonster(other.GetComponent<UC_Monster>());
        }
    }

    public void ColliderActive(bool isOn)
    {
        _collider.enabled = isOn;
    }
}
