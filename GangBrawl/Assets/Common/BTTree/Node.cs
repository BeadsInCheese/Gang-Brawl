using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public List<Node> children=new List<Node>();
    public enum Status{SUCCESS,RUNNING,FAILURE};
    public Status status;

    public int currentChild=0;

    public string name;
    public Node()
    {}
    public virtual Status Process()
    {
        return Status.SUCCESS;

    }
    public Node(string n)
    {
        this.name=n;
    }
    public void AddChild(Node child)
    {
        children.Add(child);
    }
    public void print(int level)
    {
        string printString="";
        for (int i=0; i<level; i++)
        {
            printString+="\t";
        }
        printString+="--"+name;
        Debug.Log(printString);
        foreach(Node i in children)
        {
            i.print(level+1);
        }

    }

}
