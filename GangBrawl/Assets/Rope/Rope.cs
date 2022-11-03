using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
    // Start is called before the first frame update
 
  LineRenderer lnrdr;
  private float lineWidth=0.1f;
  List<RopeSegment> ropeSegments=new List<RopeSegment>();
  public Vector3 ropeStart;
  float ropeSegLen=0.25f;   
  int segmetLenghth=35;

    void Start()
    {
        ropeStart=this.transform.position;
        this.lnrdr=this.GetComponent<LineRenderer>();
        Vector3 ropeStartPoint=ropeStart;
        for(int i=0; i<segmetLenghth; i++){
            this.ropeSegments.Add(new RopeSegment(ropeStartPoint));
            ropeStartPoint.y-=ropeSegLen;

        }
    }
    private void DrawRope(){
        float lineWidth=this.lineWidth;
        lnrdr.startWidth=lineWidth;
        lnrdr.endWidth=lineWidth;

        Vector3[] ropePositions=new Vector3[this.segmetLenghth];
        for(int i=0; i<segmetLenghth; i++){
            ropePositions[i]=this.ropeSegments[i].posNow;

        }
        lnrdr.positionCount=ropePositions.Length;
        lnrdr.SetPositions(ropePositions);

    }
private void FixedUpdate()
    {
        this.Simulate();
    }
    private void ApplyConstraint()
    {
        //Constrant to Mouse
        RopeSegment firstSegment = this.ropeSegments[0];
        firstSegment.posNow = transform.position;
        this.ropeSegments[0] = firstSegment;

        for (int i = 0; i < this.segmetLenghth - 1; i++)
        {
            RopeSegment firstSeg = this.ropeSegments[i];
            RopeSegment secondSeg = this.ropeSegments[i + 1];

            float dist = (firstSeg.posNow - secondSeg.posNow).magnitude;
            float error = Mathf.Abs(dist - this.ropeSegLen);
            Vector2 changeDir = Vector2.zero;

            if (dist > ropeSegLen)
            {
                changeDir = (firstSeg.posNow - secondSeg.posNow).normalized;
            } else if (dist < ropeSegLen)
            {
                changeDir = (secondSeg.posNow - firstSeg.posNow).normalized;
            }

            Vector2 changeAmount = changeDir * error;
            if (i != 0)
            {
                firstSeg.posNow -= changeAmount * 0.5f;
                this.ropeSegments[i] = firstSeg;
                secondSeg.posNow += changeAmount * 0.5f;
                this.ropeSegments[i + 1] = secondSeg;
            }
            else
            {
                secondSeg.posNow += changeAmount;
                this.ropeSegments[i + 1] = secondSeg;
            }
        }
    }
    private void Simulate()
    {
        // SIMULATION
        Vector2 forceGravity = new Vector2(0f, -1.5f);

        for (int i = 1; i < this.segmetLenghth; i++)
        {
            RopeSegment firstSegment = this.ropeSegments[i];
            Vector2 velocity = firstSegment.posNow - firstSegment.posOld;
            firstSegment.posOld = firstSegment.posNow;
            firstSegment.posNow += velocity;
            firstSegment.posNow += forceGravity * Time.fixedDeltaTime;
            this.ropeSegments[i] = firstSegment;
        }

        //CONSTRAINTS
        for (int i = 0; i < 50; i++)
        {
            this.ApplyConstraint();
        }
    }
public struct RopeSegment{
public Vector2 posNow;
public Vector2 posOld;
public RopeSegment(Vector2 pos){
    this.posNow=pos;
    this.posOld=pos;
}

}
    // Update is called once per frame
    void Update()
    {
        DrawRope();        
    }
}
