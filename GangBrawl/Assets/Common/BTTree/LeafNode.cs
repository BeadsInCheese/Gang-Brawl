using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeafNode : Node
{
    // Start is called before the first frame update
    public delegate Status Tick();

    public Tick ProcessMethod;
public LeafNode(string name,Tick pm)
{
    this.name=name;
    this.ProcessMethod=pm;
}
    public override Status Process()
    {
        return ProcessMethod!=null?ProcessMethod():Status.FAILURE;
    }


}
