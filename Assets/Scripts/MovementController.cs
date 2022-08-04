using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{


    [Header("Movement")]
    [SerializeField] public float playerSpeed = 10f;
    [SerializeField] public float xSpeed = 10f;
    [SerializeField] public float xClamp = 3.5f;



    float _playerSpeed;
    float _xSpeed;
    public Rigidbody rb;
    public Camera mainCamera;
    private float _distanceToScreen;
    private Vector3 _mousePos;
    private void Start()
    {
     
       
       StopMovement();
        rb = GetComponent<Rigidbody>();
    }


    private void FixedUpdate()
    {

         Movement(); //x axis movement
        transform.Translate(0, 0, Time.deltaTime * _playerSpeed); // forward movement
   
    }

    void Movement()
    {

        if (Input.GetMouseButton(0))
         { 
         
          var position = Input.mousePosition;
         
            _distanceToScreen = mainCamera.WorldToScreenPoint(gameObject.transform.position).z;
          _mousePos = mainCamera.ScreenToWorldPoint(new Vector3(position.x, position.y, _distanceToScreen));
          float direction = _xSpeed;
          direction = _mousePos.x > transform.position.x ? direction : -direction;

          if (Math.Abs(_mousePos.x - transform.position.x) > 0.5f )
            {
            transform.Translate(Time.deltaTime * direction, 0, 0);
            var pos = transform.position;
            pos.x = Mathf.Clamp(transform.position.x, -xClamp, xClamp); // x axis movement limit
            transform.position = pos;
            }
            
        }

    }
    public void StopMovement()
    {
        _xSpeed = 0f;
        _playerSpeed = 0f;
    }
    public void StartMovement()
    {
        _xSpeed = xSpeed;
        _playerSpeed = playerSpeed;
    }


  




}
