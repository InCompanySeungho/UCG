using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

namespace UC
{
    public class UC_SkillManager : MonoBehaviour
    {
        // 누르고 있는 동 안은 노란색으로?
        /// <param name="_fillImage">쿨타임을 채워지게 하는 이미지</param>
        /// <param name="_effectImage">스킬 누르고 있는 동안 색 변하는 이미지</param>
        ///
        /// _effectImage가 더 뒤에 보일 예정
        public void PushingSkill_Down(Image _fillImage, Image  _effectImage)
        {
            _fillImage.color = Color.white;
            _fillImage.fillAmount = 0f;
            _effectImage.color = Color.yellow;
            _effectImage.transform.DOScale(1.2f, 0.1f).SetEase(Ease.InSine);
        }

        public void PushingSkill_Up(Image _fillImage, Image _effectImage, float _cooldown)
        {
            _effectImage.transform.DOScale(1f, 0.1f).SetEase(Ease.Linear)
                // 사이즈 줄이는 거 끝나면
                .OnComplete(() =>
                    {
                        _effectImage.color = new Color(0, 0, 0, 0.7f);
                        _fillImage.DOFillAmount(1f, _cooldown)
                            .OnComplete(() => // 쿨타임이 다 돌았음
                            {
                                _effectImage.DOFade(1f, 0.2f).SetEase(Ease.Linear);
                                _effectImage.color = Color.white;
                                _fillImage.DOFade(0f, 0.2f).SetEase(Ease.Linear);

                            });
                    }
                );
        }
        // 눌렀을 때 동작
        public void NormalSkill_Down(Image _filliamge, Image _effectImage)
        {
            _filliamge.color = Color.white;
            _filliamge.fillAmount = 0f;
            _effectImage.color = Color.yellow;
            _effectImage.transform.DOScale(1.2f, 0.1f).SetEase(Ease.InSine);
        }

        // 쿨타임 다 돌았을 때 동작
        public void NormalSkill_Up(Image _fillImage, Image _effectImage, float _cooldown)
        {
            _effectImage.color = new Color(0, 0, 0, 0.7f);
            _fillImage.DOFillAmount(1f, _cooldown)
                .OnComplete(() =>
                {
                    _effectImage.DOFade(1f, 0.2f).SetEase(Ease.Linear);
                    _effectImage.color = Color.white;
                    _fillImage.DOFade(0f, 0.2f).SetEase(Ease.Linear);
                });
        }

        //private Sequence mouseSequence = DOTween.Sequence();
        public void mouse_Input_Down(Image[] _images, float _duration)
        {
            //_images[0].gameObject.transform.parent.gameObject.SetActive(true);
            mouseSequence(_images, _duration).Restart();

        }

        public void mouse_Input_Up()
        { 
            // 다른 설정 없이 UC_Canvas의 crosshairSetting 함수에서 비활성화를 시키면될듯
            //_crosshair.gameObject.SetActive(false);
        }

        // 게임오브젝트 활성화 되면 하는거로
        Sequence mouseSequence(Image[] _images, float _duration)
        {
            return DOTween.Sequence()
                .SetAutoKill(false)
                .OnStart(() =>
                {
                    for (int i = 0; i < _images.Length; i++)
                    {
                        _images[i].color = new Color(1, 0, 0, 0.2f);
                    }
                    // 초기값
                })
                .Append(_images[0].DOFade(1f, _duration).SetEase(Ease.InSine)).SetDelay(0.2f)
                .Append(_images[1].DOFade(1f, _duration).SetEase(Ease.InSine))
                .Append(_images[2].DOFade(1f, _duration).SetEase(Ease.InSine));
        }
    }
    
}
