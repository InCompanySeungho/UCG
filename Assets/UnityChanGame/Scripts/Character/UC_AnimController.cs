using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.SubsystemsImplementation;

namespace UC
{
    public class UC_AnimController : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private BlendTree _bt;
        private void Awake()
        {
            _animator  = this.GetComponent<Animator>();
        }
    
        public void Walk(bool _value)
        {
            _animator.SetBool("isMove", _value);
            Debug.Log("Walk");
        }
        public void WalkSetting(float _right , float _forward)
        {
            _animator.SetFloat("PosX", _right);
            _animator.SetFloat("PosY", _forward);
        }
    
        public void TriggerMove()
        {
            _animator.SetTrigger("TriggerMove");
        }
        public void TestDirection(float _value)
        {
            // 일단 부드럽게는 제외
            _animator.SetFloat("Direction", _value);
        }
        public void TestIdle()
        {
            _animator.SetTrigger("Idle");
        }
    
        public void Kick()
        {
            _animator.SetTrigger("Kick");
        }

        public void Punch()
        {
            _animator.SetTrigger("Punch");
        }
    
        public void Stop()
        {
            _animator.SetBool("isMove",false);
        }
        public void Jump()
        {
            _animator.SetTrigger("action_Jump");
        }
    }

}
