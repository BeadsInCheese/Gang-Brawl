using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTTree : Node
{
    public BTTree()
    {
        name="Tree";
    }
    public override Status Process()
    {
        return this.children[currentChild].Process();
    }
}
