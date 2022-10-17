using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceNode : Node
{
    public SequenceNode(string name){
        this.name=name;
    }
    // Start is called before the first frame update
    public override Status Process()
    {
        Status childStatus=children[currentChild].Process();
        if(childStatus==Status.RUNNING)
        {
            return Status.RUNNING;
        }
        if(childStatus==Status.FAILURE)
        {
            return Status.FAILURE;
        }
        currentChild++;
        if(currentChild>=children.Count){
            currentChild=0;
            return Status.SUCCESS;
        }
        return Status.RUNNING;
    }
}
