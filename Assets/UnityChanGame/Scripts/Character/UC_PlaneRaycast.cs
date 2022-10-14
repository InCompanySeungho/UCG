using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using TMPro;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

namespace UC
{
    public class UC_PlaneRaycast : MonoBehaviour
    {
        public LayerMask planeLayer;
        private MoveActor _moveActor;
        [SerializeField] private Transform tf_Foorpoint; // PlaneRay's point
        [SerializeField] [NotNull] private Transform tf_Viewpoint;   // ViewpointRay's point
        private Color _color;
        private Color frayColor;
        private void Awake()
        {
            _moveActor = this.GetComponent<MoveActor>();
            _color = Color.red;
            frayColor = Color.green;
        }

        private void Update()
        {
            CheckViewpointRay();
            if (_moveActor.JumpProperty) // 점프중인지 검사먼저
                return;
            //Debug.Log("지면검사 하고있음");
            
            CheckPlaneRay();
        }


        private void CheckViewpointRay()
        {
            Ray ray = new Ray(tf_Viewpoint.position,
                tf_Viewpoint.forward);
            RaycastHit hitdata;
            Debug.DrawRay(tf_Viewpoint.position, ray.direction, frayColor);
            if(Physics.Raycast(ray, out hitdata, 2f))
            {
                if (hitdata.transform.CompareTag("Monster"))
                {
                    //Debug.Log("몬스터 사정거리 안쪽");
                    _moveActor.TargetingProperty = true;
                    frayColor = Color.red;
                }
                else
                {
                    //Debug.Log("몬스터 사정거리 바깥쪽");
                    _moveActor.TargetingProperty = false;
                    frayColor = Color.green;
                }
            }
            else // 사정거리에 찍히는게 없어도 Targeting = false 상태임.
            {
                frayColor = Color.green;
                _moveActor.TargetingProperty = false;
            }
        }
       private void CheckPlaneRay()
        {
            Ray ray = new Ray(tf_Foorpoint.position, -transform.up);
            RaycastHit hitData;
          
            Debug.DrawRay(tf_Foorpoint.position, ray.direction, _color);
            if (Physics.Raycast(ray, out hitData, 5f, planeLayer))
            {
                // distance : 발 - 땅 사이의 거리
                if (hitData.transform.CompareTag("Plane")) // 태그로 구분함!
                {
                    _color= Color.blue;
                    if (!_moveActor.JumpProperty) // 점프가 아닐때를 가져온다
                    { 
                        float distance = tf_Foorpoint.position.y - hitData.point.y;
                        //Debug.Log("점프상태가 아님. 거리 : " + distance);
                        if (distance <= 0.1f) // 지면 
                        {
                            _moveActor.LandProperty = true;
                            //_moveActor.set_thisPositionisPlane();
                        }
                        else // 공중에 있음
                        {
                            _moveActor.LandProperty =false;
                        }
                    }
                }
            }
            else
            {
                _color = Color.red;
            }
        }
    }
}
