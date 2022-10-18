using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace UC
{
    public class UC_AttackManager : MonoBehaviour
    {
        private UC_CharacterBase main; 
        // 공격 루틴이 돌아가고 있는지
        private bool isRoutineProgress = false;

        private int routineCount = 0;
        // 공격 가능까지의 딜레이시간
        private WaitForSeconds attackDelay = new WaitForSeconds(0.5f);
        
        // 연속공격 취소까지의 딜레이시간
        private WaitForSeconds cancelDelay = new WaitForSeconds(1f);




        [Header("AttackRange")]
        [SerializeField] private UC_AttackRange range_NormalAttack;
        public void NormalAttack()
        {
            if (isRoutineProgress)
            {
                routineCount++;
                //Debug.Log("루틴 카운트 : " + routineCount);
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
             main.ContinuousProperty = true;
             //range_NormalAttack.ColliderActive(true); // 공격범위 활성화
             MonsterPool.MonsterDamaged(1); // 공격 모션 뒤 공격하도록?

             yield return attackDelay; // [DELAY] 다음 공격 가능시간
             
             main.AttackProperty = true;
             
             yield return cancelDelay; // [DELAY] 연속공격 취소지연시간
             
             main.ContinuousProperty = false;
             isRoutineProgress = false;
             //range_NormalAttack.ColliderActive(false);
             yield return null;
         }

        private IEnumerator CRT_SecondAttack()
        {
            main.ContinuousProperty = true;
            //Debug.Log("<color=yellow>2 : 공격</color>");
            yield return attackDelay;
             
            //Debug.Log("<color=green>2 : 공격 가능으로 변환</color>");
            main.AttackProperty = true;
             
            yield return cancelDelay;
            main.ContinuousProperty = false;
            isRoutineProgress = false;
            //Debug.Log("<color=red>2 : 연속공격취소</color>");
            //range_NormalAttack.ColliderActive(false);
            yield return null;
        }

        private IEnumerator CRT_ThirdAttack()
        {
            //Debug.Log("<color=yellow>3 : 공격</color>");
            yield return attackDelay;

            main.AttackProperty = true;
            isRoutineProgress = false;
            //Debug.Log("<color=green>3 : 마지막 공격이라 머 없음</color>");
            //range_NormalAttack.ColliderActive(false);
            yield return null;
        }
        private void Awake()
        {
            main = this.GetComponent<UC_CharacterBase>();
            isRoutineProgress = false;
            
        } 
    }
    
}
