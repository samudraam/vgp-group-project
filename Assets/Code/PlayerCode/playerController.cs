using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PlayerCode{
public class playerController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
     //move up (to be changed to jump)  
     if(Input.GetKey(KeyCode.UpArrow)){
        transform.position += new Vector3(0,0.2f,0);
     }
     //move down (to be changed to fall through platforms/fastfall)  
     if(Input.GetKey(KeyCode.DownArrow)){
        transform.position += new Vector3(0,-0.2f,0);
     }
     //move backwards
     if(Input.GetKey(KeyCode.LeftArrow)){
        transform.position += new Vector3(-0.2f,0,0);
     }
     //move forewards
     if(Input.GetKey(KeyCode.LeftArrow)){
        transform.position += new Vector3(0.2f,0,0);
     }
    }
}
}
