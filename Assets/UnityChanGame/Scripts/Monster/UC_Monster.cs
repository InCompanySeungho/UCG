using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEditor.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.AI;

namespace UC
{
    public class UC_Monster : MonoBehaviour
    {
        // Component
        private Animator animator;
        private Canvas _canvas;
        private NavMeshAgent _agent;
        
        // Property
        private int _hp;
        private const int _maxHP = 3;
        private void Awake()
        {
            animator = this.transform.GetChild(0).GetComponent<Animator>();
            this._canvas = this.transform.GetChild(1).GetComponent<Canvas>();
            _agent = this.GetComponent<NavMeshAgent>();
            _hp = _maxHP;
        }
        
        private void Update()
        {
            // 몬스터 hp UI
            this._canvas.transform.LookAt(Camera.main.transform.position);
        }

        
        public void GetDamage(int _damage)
        {
            _hp -= _damage;
            if (_hp <= 0)
            {
                animator.SetTrigger("Dead");
                Debug.Log("나 주겅");
            }
            else
            {
                Debug.Log("몬스터 피격 남은체력 : " + _hp  + " / " + _maxHP);
            }
        }
    }
}