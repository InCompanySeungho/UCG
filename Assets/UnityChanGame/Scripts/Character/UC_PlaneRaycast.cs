using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UC
{
    public class UC_PlaneRaycast : MonoBehaviour
    {
        public LayerMask planeLayer;
        private MoveActor _moveActor;
        [SerializeField] private Transform tf_Foorpoint;

        private void Awake()
        {
            _moveActor = this.GetComponent<MoveActor>();
        }

        private void Update()
        {
            //CheckPlaneRay();
            _moveActor.isLand(CheckPlaneRay());
        }

        bool CheckPlaneRay()
        {

            Ray ray = new Ray(tf_Foorpoint.position, -transform.up);
            RaycastHit hitData;
            Debug.DrawRay(tf_Foorpoint.position, ray.direction, Color.red);
            if (Physics.Raycast(ray, out hitData, 1f, planeLayer))
            {
                float distance = tf_Foorpoint.position.y - hitData.point.y;
                if (hitData.transform.CompareTag("Plane"))
                {
                    Debug.Log("사이의 거리 : " +
                              (tf_Foorpoint.position.y - hitData.point.y));
                    if (distance <= 0.05f) // 지면 
                    {
                        return true;
                    }
                    else // 공중에 있음
                    {
                        return false;
                    }

                }
            }

            return false;
        }
    }
}
