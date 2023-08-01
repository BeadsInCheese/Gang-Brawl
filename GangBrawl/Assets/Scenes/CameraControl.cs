using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    // Start is called before the first frame update
    public List<Transform> players;
    Vector2 center=new Vector3(0,0);
    Camera cam;
    void Start()
    {
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        float maX = 0 ;
        float minx=0;
        float maxy=0;
        float miny=0;
        center.x =  0;
        center.y = 0;
        int p = 0;
        foreach (Transform i in players)
        {

            if (i.position.y>-30&& i.position.y < 30)
            {
                maX = Mathf.Max(i.position.x, maX);
                maxy = Mathf.Max(i.position.y, maxy);
                minx = Mathf.Min(i.position.x, minx);
                miny = Mathf.Max(i.position.y, miny);
                center.x += i.position.x;
                center.y += i.position.y;
                p++;
            }

        }
        center=center / Mathf.Max(1,p);
        var temp= Vector2.Lerp(transform.position, center, 0.2f);
        transform.position =new  Vector3(temp.x, temp.y, transform.position.z);
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize,new Vector2(Mathf.Max(5,Mathf.Min(maX - minx,10)), Mathf.Max(8, Mathf.Min(maxy - miny,10))).magnitude,0.2f);
    }
}
