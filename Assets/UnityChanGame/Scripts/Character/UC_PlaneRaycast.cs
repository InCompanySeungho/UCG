using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

namespace UC
{
    public class UC_PlaneRaycast : MonoBehaviour
    {
        public LayerMask planeLayer;
        private MoveActor _moveActor;
        [SerializeField] private Transform tf_Foorpoint;
        private Color _color;
        private void Awake()
        {
            _moveActor = this.GetComponent<MoveActor>();
            _color = Color.red;
        }

        private void Update()
        {
            if (_moveActor.JumpProperty) // 점프중인지 검사먼저
                return;
            //Debug.Log("지면검사 하고있음");
            
            CheckPlaneRay();
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
