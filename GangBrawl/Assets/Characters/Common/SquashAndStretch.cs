using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquashAndStretch : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    float time=1f;
    public IEnumerator anim(){
        time=0f;
        float t2;
        while(time<0.1f){
            t2=time/0.1f;
            transform.localScale=new Vector3(1-(Mathf.Lerp(1,1.2f,time)-1),Mathf.Lerp(1f,1.2f,t2),1);
            time+=Time.deltaTime;
        yield return 0;
        }
        time=0;
        while(time<0.3f){
            t2=time/0.3f;
            transform.localScale=new Vector3(1-(Mathf.Lerp(1.2f,1f,t2)-1),Mathf.Lerp(1.2f,1f,t2),1);
            time+=Time.deltaTime;
        yield return 0;
        }
        transform.localScale=Vector3.one;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
