using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseCursor : MonoBehaviour
{
    // Start is called before the first frame update
    
    public PlayerInput playerInput;
    public float speed=5;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!PauseMenu.isPaused){
            Destroy(gameObject);
        }

        Vector2 dir=playerInput.actions["Aim"].ReadValue<Vector2>();
        Vector2 bounds=new Vector2(Camera.main.orthographicSize*Camera.main.aspect,Camera.main.orthographicSize );
                
        transform.position=new Vector3(Mathf.Clamp(transform.position.x+dir.x*speed*(1.0f/60),Camera.main.transform.position.x-bounds.x,Camera.main.transform.position.x+bounds.x),Mathf.Clamp(transform.position.y+dir.y*speed*(1.0f/60),Camera.main.transform.position.y-bounds.y,Camera.main.transform.position.y+bounds.y),transform.position.z);
     RaycastHit2D[] hitInfo=Physics2D.RaycastAll(transform.position,new Vector2(-1,1),3f);
     //Debug.DrawRay(transform.position,new Vector2(-1,1),Color.green,3f);
    foreach(RaycastHit2D i in hitInfo){
               //Debug.Log(i.collider.name);
     if (i)
     {
         if (i.collider.gameObject.tag.Equals("cursor"))
         {
            var invoker=i.collider.gameObject.GetComponent<ButtonInvoker>();
            invoker.btSelect();
            if(playerInput.actions["LightAttack"].triggered){
                invoker.btOnClick.Invoke();
            }
          //Debug.Log("Button Hover");
        }
     }
    }}
}
