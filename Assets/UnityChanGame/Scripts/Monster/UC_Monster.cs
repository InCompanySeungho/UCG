using System;
using System.Collections;
using System.Collections.Generic;
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
       [SerializeField] private int _hp;
        private int _maxHP = 3;
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
            if (_damage <= 0)
                // 활동불가
            {
                animator.SetTrigger("Dead");
            }
            else
            {
                _hp -= _damage;
            }
        }
    }
}