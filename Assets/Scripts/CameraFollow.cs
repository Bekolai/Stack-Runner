using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset,offset2;
    public bool finish;

    // Update is called once per frame
    void Update()
    {
        
        if(!finish)
        {
         
          transform.position = new Vector3(0, 0, target.position.z) + offset;
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(0,0,target.transform.position.z) + offset2, Time.deltaTime * 2);
           
        }
        
       
    }
    private void Start()
    {
     
    }
}
