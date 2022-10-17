using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectorNode : Node
{
    // Start is called before the first frame Update
    public SelectorNode(string name){
        this.name=name;
    }
    public override Status Process()
    {
        Status childStatus=children[currentChild].Process();
        if(childStatus==Status.RUNNING)
        {
            return Status.RUNNING;
        }
        if(childStatus==Status.SUCCESS)
        {
            currentChild=0;
            return Status.SUCCESS;
        }
        currentChild++;
        if(currentChild>=children.Count){
            currentChild=0;
            return Status.FAILURE;
        }
        return Status.RUNNING;
    }
    
}
