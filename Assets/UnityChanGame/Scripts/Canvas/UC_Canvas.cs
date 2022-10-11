using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.PlasticSCM.Editor.CollabMigration;
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

        void Initialize()
        {
            image_drift_Fill.color = Color.white;
            image_drift_Effect.color = Color.white;
            image_CooldownEffect.color = new Color(1,1,1,0);
        }
        void Awake()
        {
            _skillManager = this.GetComponent<UC_SkillManager>();
            Initialize();
        }
    }
   
}