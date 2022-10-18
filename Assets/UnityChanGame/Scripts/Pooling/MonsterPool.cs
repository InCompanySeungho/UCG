using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using DG.Tweening;
using UnityEngine;

namespace  UC
{
    public class MonsterPool : MonoBehaviour
    {
        private static MonsterPool _Instance;

        public static MonsterPool instance
        {
            get
            {
                return _Instance;
            }
            set
            {
                _Instance = value;
                DontDestroyOnLoad(_Instance);
            }
        }

        private void Awake()
        {
            instance = this;
        }

        [SerializeField] private Transform ActivePool;
        [SerializeField] private Transform DeActivePool;
        [SerializeField] private GameObject monsterPrefab;
        private Queue<UC_Monster> monsterQueue = new Queue<UC_Monster>();
    
        // [Queue]
        // Dequeue : 큐에 추가한다.
        // Enqueue : 큐에서 뺀다.
        
        /// <summary>
        /// 초기 생성
        /// </summary>
        /// <param name="_initCount">생성시킬 몬스터 수</param>
        public void Init(int _initCount)
        {
            Initialize(_initCount);
        }

        void Initialize(int _initCount) 
        {
            for (int i = 0; i < _initCount; i++)
            { 
                monsterQueue.Enqueue(CreateNewMonster());
            }
        }

        UC_Monster CreateNewMonster() // 새롭게 생성
        {
            var newMonster = Instantiate(monsterPrefab).GetComponent<UC_Monster>();
            newMonster.transform.SetParent(DeActivePool);
            //newMonster.transform.position = DeActivePool.position;
            newMonster.transform.position = Vector3.zero;
            newMonster.gameObject.SetActive(false);
            return newMonster;
        }

        public static UC_Monster GetMonster()
        {
            if (instance.monsterQueue.Count > 0) // 큐에 내용이 존재함
            {
                var obj = instance.monsterQueue.Dequeue();
                obj.transform.SetParent(instance.ActivePool);
                obj.gameObject.SetActive(true);
                return obj;
            }
            else // 초기 생성량보다 더 생성하려고 하는거 차라리 초기화하고 다시 만드는게?
            {
                var newobj = instance.CreateNewMonster();
                newobj.gameObject.SetActive(true);
                newobj.transform.SetParent(instance.ActivePool);
                return newobj;
            }
        }
        
        /// <summary>
        /// 반환(몬스터 완전 처리 후 사용)
        /// </summary>
        /// <param name="_monster"></param>
        public static void ReturnObject(UC_Monster _monster)
        {
            _monster.gameObject.SetActive(false);
            _monster.transform.SetParent(instance .DeActivePool);
            instance.monsterQueue.Enqueue(_monster);
        }
        
        
        // 피격 당할 예정인 몬스터들
       [SerializeField] private List<UC_Monster> inAttackRangeMonsterList = new List<UC_Monster>();
        
        public static void in_HitMonster(UC_Monster _monster)
        {
            if (instance.inAttackRangeMonsterList.Contains(_monster)) // 이미 담고있는 상태라면.
                return;
            
            //Debug.Log("큐에 몬스터 추가");
            instance.inAttackRangeMonsterList.Add(_monster);
        }
        public static void Out_HitMonster(UC_Monster _monster)
        {
            //Debug.Log("큐에 몬스터 감소");
            instance.inAttackRangeMonsterList.Remove(_monster);
        }
        public static void MonsterDamaged(int _damage)
        {
            
            foreach (var monster in instance.inAttackRangeMonsterList)
            {
                monster.GetDamage(_damage);
            }
        }
    }
    
}
