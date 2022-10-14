using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace UC
{
    public class UC_AttackManager : MonoBehaviour
    {
        [SerializeField] private GameObject range_NormalAttack;

        private MoveActor _moveActor; 
        // 공격 루틴이 돌아가고 있는지
        private bool isRoutineProgress = false;

        private int routineCount = 0;
        // 공격 가능까지의 딜레이시간
        private WaitForSeconds attackDelay = new WaitForSeconds(0.5f);
        
        // 연속공격 취소까지의 딜레이시간
        private WaitForSeconds cancelDelay = new WaitForSeconds(1f);
        public void NormalAttack()
        {
            if (isRoutineProgress)
            {
                routineCount++;
                Debug.Log("루틴 카운트 : " + routineCount);
               StopCoroutine(AttackRoutine);
                if ( routineCount == 1)
                {
                    AttackRoutine = CRT_SecondAttack();
                }
                else if (routineCount == 2)
                {
                    AttackRoutine = CRT_ThirdAttack();
                }
                else
                {
                    // 일단 발생할 일 없는데 발생하면 버그
                    Debug.Log("<color=red>버그발생!</color>");
                }
                
                //Debug.Log("기본 공격 루틴 종료했고, 다른 루틴 실행해줘야함");
                StartCoroutine(AttackRoutine);
            }
            else
            {
                routineCount = 0;
                isRoutineProgress = true;
                AttackRoutine = CRT_NormalAttack();
                StartCoroutine(AttackRoutine);
            }
        }
        

        private IEnumerator AttackRoutine;
        private IEnumerator CRT_NormalAttack()
         {
             _moveActor.ContinuousProperty = true;
             Debug.Log("<color=yellow>1 : 공격</color>");
             yield return attackDelay;
             
             Debug.Log("<color=green>1 : 공격 가능으로 변환</color>");
             _moveActor.AttackProperty = true;
             
             yield return cancelDelay;
             _moveActor.ContinuousProperty = false;
             isRoutineProgress = false;
             Debug.Log("<color=red>1 : 연속공격취소</color>");
             yield return null;
         }

        private IEnumerator CRT_SecondAttack()
        {
            _moveActor.ContinuousProperty = true;
            Debug.Log("<color=yellow>2 : 공격</color>");
            yield return attackDelay;
             
            Debug.Log("<color=green>2 : 공격 가능으로 변환</color>");
            _moveActor.AttackProperty = true;
             
            yield return cancelDelay;
            _moveActor.ContinuousProperty = false;
            isRoutineProgress = false;
            Debug.Log("<color=red>2 : 연속공격취소</color>");
            yield return null;
        }

        private IEnumerator CRT_ThirdAttack()
        {
            Debug.Log("<color=yellow>3 : 공격</color>");
            yield return attackDelay;

            _moveActor.AttackProperty = true;
            isRoutineProgress = false;
            Debug.Log("<color=green>3 : 마지막 공격이라 머 없음</color>");
            yield return null;
        }

        private void rangeActive(int _num)
        {
            range_NormalAttack.SetActive(false);
            switch(_num)
            {
                case 0:
                    range_NormalAttack.SetActive(true);
                    break;
                case 1:
                    break;
                case 2:
                    break;
            }
        }
        private void Awake()
        {
            _moveActor = this.GetComponent<MoveActor>();
            range_NormalAttack.gameObject.SetActive(false);
            isRoutineProgress = false;
        } 
    }
    
}
