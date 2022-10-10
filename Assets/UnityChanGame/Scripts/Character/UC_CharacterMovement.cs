using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace UC
{
    public class UC_CharacterMovement : MonoBehaviour
    {
        private GameObject go_ModelCharacter;


        // Value
        private float _moveSpeed;
        [SerializeField] private float _posX;
        [SerializeField] private float _posZ;

        private bool _isMoving_X;
        private bool _isMoving_Z;

        private void Awake()
        {
            go_ModelCharacter = this.transform.GetChild(0).gameObject;
            _moveSpeed = 0.02f;
        }


        private void Update()
        {
            CharacterMovement();
        }

        void SightAboutMouse()
        {
            
        }
        void CharacterMovement()
        {
            //this.gameObject.transform.position = new Vector3(_posX, 0, _posZ);

            #region 누르기

            if (Input.GetKey(KeyCode.A)) // Left
            {
                _isMoving_X = true;
                _posX -= _moveSpeed;
            }
            else if (Input.GetKey(KeyCode.D)) // Right
            {
                _isMoving_X = true;
                _posX += _moveSpeed;
            }

            if (Input.GetKey(KeyCode.W)) // Forward
            {
                _isMoving_Z = true;
                _posZ += _moveSpeed;
            }
            else if (Input.GetKey(KeyCode.S)) // Back
            {
                _isMoving_Z = true;
                _posZ -= _moveSpeed;
            }

            #endregion

            #region 때기

            if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
            {
                _isMoving_X = false;
            }
            else if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S))
            {
                _isMoving_Z = false;
            }

            #endregion


            if (_isMoving_X || _isMoving_Z)
            {
                this.transform.Translate(new Vector3(_posX, 0, _posZ) * Time.deltaTime, Space.Self);
            }

        }
    }
}