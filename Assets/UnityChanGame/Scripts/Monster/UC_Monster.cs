using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UC
{
    public class UC_Monster : MonoBehaviour
    {
        // 
        private Animator animator;
        private void Awake()
        {
            animator = this.transform.GetChild(0).GetComponent<Animator>();
        }
        
        
        
    }
}