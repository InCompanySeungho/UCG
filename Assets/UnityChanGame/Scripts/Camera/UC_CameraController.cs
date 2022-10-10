using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using Cinemachine;
using UnityEditor;

namespace UC
{
    public class UC_CameraController : MonoBehaviour
    {
        public CinemachineBrain brain;
        //[SerializeField] private CinemachineVirtualCamera _virtualCam;

        private void Awake()
        {
            brain = this.GetComponent<CinemachineBrain>();
        }

        [SerializeField] private GameObject cam_Default;
        [SerializeField] private GameObject cam_Game;
        [SerializeField] private GameObject cam_Back;
        [SerializeField] private GameObject cam_1stSight;
        /// <summary>
        /// 카메라 모드 변경
        /// </summary>
        /// <param name="_Mode">"Game", "Defualt"</param>
        public void ChangeCameraMode(string _Mode)
        {
            switch (_Mode)
            {
                case "Game":
                    cam_Default.SetActive(false);
                    cam_Game.SetActive(true);
                    break;
                case "Default":
                    cam_Default.SetActive(true);
                    cam_Game.SetActive(false);
                    break;
            }
        }

        /// <summary>
        /// 카메라 1인칭 - 3인칭 변경
        /// </summary>
        public void ChangeSight(string _Mode)
        {
            switch (_Mode)
            {
                case "First":
                    cam_1stSight.SetActive(true);
                    cam_Default.SetActive(false);
                    break;
                case "Third":
                    cam_1stSight.SetActive(false);
                    cam_Default.SetActive(true);
                    break;
            }
        }
        
        public void BackShot()
        {
            
            StartCoroutine(CRt_BackShot());
        }

        IEnumerator CRt_BackShot()
        {
            yield return null;
        }
    }

}
