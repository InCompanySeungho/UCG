using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.PlasticSCM.Editor.CollabMigration;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace UC
{
    public class UC_Canvas : MonoBehaviour
    {
        private UC_SkillManager _skillManager;

        [SerializeField] private Image image_drift_Fill;
        [SerializeField] private Image image_drift_Effect;
        [SerializeField] private Image image_CooldownEffect;



        private GameObject crosshair_Charging;
        [SerializeField] private GameObject crosshair_Normal;
        [Header("0이 가장 아래가 되도록")]
        [SerializeField] private Image[] image_Charging;
        
        
        
        
        public void SKILL_DRIFT_DOWN()
        {
            _skillManager.PushingSkill_Down(image_drift_Fill, image_drift_Effect);
        }

        public void SKILL_DRIFT_UP(float _cooldown) // 쿨타임은 메인에서 설정하도록
        {
            _skillManager.PushingSkill_Up(image_drift_Fill, image_drift_Effect,_cooldown);
        }

        public void COOL_DOWN(int _num)
        {
            image_CooldownEffect.DOFade(0f, 0.15f).SetEase(Ease.InSine).From(1f);
            image_CooldownEffect.transform.DOScale(3f, 0.15f).SetEase(Ease.InSine).From(1f);
        }


        // 우클릭 누름. 차징 시작
        public void mouse_Right_DOWN()
        {
            crosshairSetting(1);
            _skillManager.mouse_Input_Down(image_Charging, 0.3f);
        }

        // 우클릭 땜. 차징 취소 밑 초기화
        public void mouse_Right_UP()
        {
            crosshairSetting(0);
            _skillManager.mouse_Input_Up();
        }


        public void mouse_Left_DOWN()
        {
            crosshair_Normal.transform.DOScale(1.2f, 0.12f).SetEase(Ease.OutCubic)
                .OnComplete(() =>
                {
                    crosshair_Normal.transform.DOScale(1f, 0.2f).SetEase(Ease.Linear);
                    //1f 로 사이즈 조절 뒤, Delegate를 넣던 콜백을 넣던 공격 가능상태임을 반환해줘야함
                });
        }

        private void crosshairFade(Image _image)
        {
            
        }

        private void crosshairFade(Image[] _images)
        {
            
        }
        /// <summary>
        /// 현재 상태의 크로스헤어를 활성화한다.
        /// </summary>
        /// <param name="_num">0 : normal, 1 : charging<:/param>
        private void crosshairSetting(int _num)
        {
            crosshair_Charging.gameObject.SetActive(false);
            crosshair_Normal.gameObject.SetActive(false);
            switch (_num)
            {
                case 0:
                    crosshair_Normal.SetActive(true);
                    break;
                case 1:
                    crosshair_Charging.SetActive(true);
                    break;
                case 2:
                    break;
                default:
                    // 0과 동일한 로직
                    break;
            }
        }
        
        /// <summary>
        /// 초기값 설정
        /// </summary>
        void Initialize()
        {
            image_drift_Fill.color = Color.white;
            image_drift_Effect.color = Color.white;
            image_CooldownEffect.color = new Color(1,1,1,0);
            
            
            //mouse_Right_UP();
            crosshairSetting(0);
        }
        void Awake()
        {
            crosshair_Charging = image_Charging[0].transform.parent.gameObject;
            _skillManager = this.GetComponent<UC_SkillManager>();
            Initialize();
            
        }
    }
   
}