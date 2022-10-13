using System;
using System.Collections;
using System.Collections.Generic;
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

        public static void ReturnObject(UC_Monster _monster)
        {
            _monster.gameObject.SetActive(false);
            _monster.transform.SetParent(instance .DeActivePool);
            instance.monsterQueue.Enqueue(_monster);
        }
        


    }
    
}
