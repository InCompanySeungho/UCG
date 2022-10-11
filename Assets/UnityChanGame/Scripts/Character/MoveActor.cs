using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Cinemachine;
using DG.Tweening;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.UI;

namespace UC
{
    public class MoveActor : MonoBehaviour
    {
        private CharacterController controller;
        [SerializeField] private CinemachineVirtualCamera _virtualCam;

        [SerializeField] private UC_AnimController _animController;
        [SerializeField] private UC_Canvas _canvas;
        private UC_CameraController cameraController;
        private Rigidbody myRigid;

        
        private enum State
        {
            SIGHT_1,
            SIGHT_3,
            ATTACK,
            JUMP,
        }

        private State _state = State.SIGHT_1;

        [SerializeField] private UC_Mesh meshManager;

        private  IEnumerator IE_Drift;
        
        // 키 입력중인지 
        private bool isDashing;
        private bool isJumping;
        
        
        // 키 입력 가능여부
        private bool canDash;
        private bool canJump;
        
        
        // 움직임 관련
        // [점프]
        private bool canSuperJump = false;
        private float curr_Ypos; // 현재 Y좌표
        private float dest_Ypos; // 목표 Y좌표  
        private bool isLanded =false;

        void Awake()
        {
            controller = this.GetComponent<CharacterController>();
            //myRigid = this.GetComponent<Rigidbody>();
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            //rb = GetComponent<Rigidbody>();
            //rb.freezeRotation = true;

            RotateActor = this.transform.GetChild(0).gameObject;


            horizontal = Input.GetAxisRaw("Horizontal");
            vertical = Input.GetAxisRaw("Vertical");

            cameraController = Camera.main.GetComponent<UC_CameraController>();
            meshManager.MeshControl("First");
            cameraController.ChangeSight("First");

            canDash = true;
            canJump = true;
            IE_Drift = CRT_Skill_Drift_DOWN();
        }

        int count = 0;

        void Update()
        {
            switch (_state)
            {
                case State.SIGHT_1:
                    MouseRot_FirstSight();
                    Movement_FirstSight();
                    break;

                case State.SIGHT_3:
                    Movement();
                    MouseRotator();
                    break;
            }

            if (Input.GetKeyDown(KeyCode.J))
            {
                _animController.Kick();
            }
            else if (Input.GetKeyDown(KeyCode.K))
            {
                _animController.Punch();
            }

            if (Input.GetKeyDown(KeyCode.R)) // [3인칭] close - far 조절
            {
                if (count % 2 == 0)
                {
                    cameraController.ChangeCameraMode("Game");
                }
                else
                {
                    cameraController.ChangeCameraMode("Default");
                }

                count++;
            }

            if (Input.GetKeyDown(KeyCode.Q)) // 1인칭 - 3인칭 변경
            {
                if (count % 2 == 0)
                {
                    cameraController.ChangeSight("First");
                    meshManager.MeshControl("First");
                    _state = State.SIGHT_1;
                }
                else
                {
                    cameraController.ChangeSight("Third");
                    meshManager.MeshControl("Third");
                    _state = State.SIGHT_3;
                }

                count++;
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (!canJump)
                    return;
                curr_Ypos = this.gameObject.transform.position.y;
                
                if (canSuperJump) // 슈퍼점프 
                {
                    
                }
                else // 일반점프
                {
                    //_animController.Jump();
                    this.gameObject.transform.DOLocalJump
                        (Vector3.up,
                            2f,
                            1,
                            0.3f).SetRelative(true).SetEase(Ease.InSine)
                        .OnComplete(() =>
                        {
                            //_state = State.JUMP;
                        });
                }
     
            }
            // 점프 중일 때 안때면 좀 더 올라감
            if(Input.GetKey(KeyCode.Space))
            {
                Debug.Log("스페이스 입력중");
                if (!isLanded) // 내가 지금 지면이 아닐때만
                {

                    if (dest_Ypos - curr_Ypos <= 10f)
                    {
                        
                    }
                    else
                    {
                        curr_Ypos += 0.2f;
                    } 
                    //Debug.Log("이 이상 높아지면 안돼!");
                }
            }
            if (_state == State.JUMP)
            {
                // 점점 위로 올라가도록  (한계점 있음)
                this.transform.
                    Translate(new Vector3(0,dest_Ypos,0), Space.World);
                
                
                if (isLanded) // 처음 위치와 다른 땅.
                {
                    
                }
                else
                {
                    this.gameObject.transform.DOLocalMoveY(
                            curr_Ypos,
                            0.3f)
                        .OnComplete(() =>
                        {
                            _state = State.SIGHT_1;
                        });
                }
                
            }


            #region 특수스킬

            
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                if (canDash)
                {
                    canDash = false;
                    isDashing = true;
                    _canvas.SKILL_DRIFT_DOWN();
                    canSuperJump = true;
                    // 슈퍼점프 가능
                    StartCoroutine(IE_Drift);
                }
                
            }
            else if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                Debug.Log("쉬프트 손 때기");
                if (isDashing == true)
                {
                    Debug.Log("대쉬중에 손 때서 대쉬 취소된다.");
                    StopCoroutine(IE_Drift);
                    isDashing = false;
                    StartCoroutine(CRT_Skill_Drift_UP());
                }
                // Up 하고 0.5초 이내까지는 슈퍼점프 되도록
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                   
            }

            #endregion
        }


        IEnumerator CRT_Skill_Drift_DOWN() // IE_Drift 에 할당
        {
            Debug.Log("드리프트 다운");
            int count = 0;
            while (isDashing)
            {
                if (count == 2)
                {
                    Debug.Log("대쉬 자동 해제");
                    isDashing = false;
                    canDash = true;
                    StopCoroutine(IE_Drift);
                    StartCoroutine(CRT_Skill_Drift_UP());
                    count = 0;
                }
                yield return new WaitForSeconds(0.5f);
                count++;
                Debug.Log("카운트 증가, 현재 카운트 = " + count);
            }
            Debug.Log("와일문 탈출");
            yield return null;
        }
        IEnumerator CRT_Skill_Drift_UP()
        {
            float _jumpdelay = 0.5f;
            float _cooltime = 1.5f;
            _canvas.SKILL_DRIFT_UP(_cooltime);
            yield return new WaitForSeconds(_jumpdelay);
            canSuperJump = false;
            yield return new WaitForSeconds(_cooltime - _jumpdelay);
            _canvas.COOL_DOWN(0);
            canDash = true;
            yield return null;
        }

        float horizontal; // = Input.GetAxisRaw("Horizontal");
        float vertical; // = Input.GetAxisRaw("Vertical");
        private float speed = 4f;

        bool MoveStart = false;

        void Movement()
        {
            horizontal = Input.GetAxisRaw("Horizontal");
            vertical = Input.GetAxisRaw("Vertical");

            Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized; // 이동 각도 설정
            if (direction.magnitude >= 0.1f)
            {
                if (!MoveStart)
                {
                    MoveStart = true;
                    _animController.TriggerMove();
                }

                controller.Move(RotateActor.transform.localRotation * direction * speed * Time.deltaTime);

                _animController.TestDirection(horizontal);
            }
            else
            {
                if (MoveStart)
                {
                    MoveStart = false;
                    _animController.Walk(false);
                    _animController.TestIdle();
                }
            }
        }

        void Movement_FirstSight()
        {
            horizontal = Input.GetAxisRaw("Horizontal");
            vertical = Input.GetAxisRaw("Vertical");


            Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

            if (direction.magnitude >= 0.1f)
            {
                if (!MoveStart)
                {
                    // 1인칭에서는 애니메이션 안보임
                    MoveStart = true;
                }

                controller.Move(RotateActor.transform.localRotation * direction * speed * Time.deltaTime);
            }
            else
            {
                if (MoveStart)
                {
                    MoveStart = false;
                }
            }
        }

        [SerializeField] private CinemachineVirtualCamera cam_1stSight;
        [SerializeField] private Transform tf_1stCamTf;

        void MouseRot_FirstSight()
        {
            mouseX = Input.GetAxisRaw("Mouse X") * _sensitivity;
            mouseY = Input.GetAxisRaw("Mouse Y") * _sensitivity;

            cam_1stSight.transform.position = tf_1stCamTf.position;

            cam_1stSight.transform.eulerAngles +=
                new Vector3(
                    -mouseY,
                    mouseX,
                    0);

            // RotateActor(캐릭터 회전을 위해서) 도 카메라 회전에 맞춰서 회전
            RotateActor.transform.eulerAngles =
                new Vector3(
                    0, cam_1stSight.transform.eulerAngles.y, 0);
        }

        float LIMIT_mouseY(float _value)
        {
            return Mathf.Clamp(_value, -30f, 30f);
        }

        IEnumerator CRT_ChangeDirecitonValue(float _startValue, float _endValue, float _duration)
        {
            float elapsed = 0.0f;
            while (elapsed < _duration)
            {
                Debug.Log("<color=red> StartValue : " + _startValue + "</color>");
                Debug.Log("<color=yellow> EndValue : " + _endValue + "</color>");
                horizontal = Mathf.Lerp(_startValue, _endValue, elapsed / _duration);
                elapsed += Time.deltaTime;
                yield return null;
            }

            horizontal = _endValue;
        }

        private float mouseX;
        private float mouseY;
        private float _sensitivity = 0.8f;

        private GameObject RotateActor;

        void MouseRotator()
        {
            mouseX = Input.GetAxis("Mouse X") * _sensitivity;
            mouseY = Input.GetAxis("Mouse Y") * _sensitivity;

            RotateActor.transform.eulerAngles +=
                new Vector3(
                    //-Mathf.Clamp(mouseY, -10f, 10f),
                    -0,
                    mouseX,
                    0);
        }

        public void isLand(bool _island)
        {
            
        }
    }
}