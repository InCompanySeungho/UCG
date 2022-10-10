using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.UI;
using Random = UnityEngine.Random;


public class ImpulseObject : MonoBehaviour
{
    public float force = 1f;

    private Rigidbody rigidbody;
    private Cinemachine.CinemachineImpulseSource source;


    private void Awake()
    {
        source = this.GetComponent<Cinemachine.CinemachineImpulseSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(CRT_Down());
    }

    IEnumerator CRT_Down()
    {
        yield return new WaitForSeconds(1f);
        Down();
    }
    void Down()
    {
        Debug.Log("Down!!");
        //rigidbody.AddForce(transform.up * (100 * Random.Range(0.9f, 2f)), ForceMode.Impulse);
        //source.GenerateImpulse(Camera.main.transform.forward);    
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
