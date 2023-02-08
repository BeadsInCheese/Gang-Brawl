using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeAnchor : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody2D hook;
    public GameObject linkPrefab;
    public int links = 7;
    public bool hasObjectAtEnd = true;
    public GameObject ObjectAtEnd;
    GameObject ropeContainer;
    void Start()
    {
        GenerateRope();
    }
    void GenerateRope()
    {
        DestroyRopeContainer();
        ropeContainer = new GameObject("RopeContainer");
        GameObject ObjectAtEndInstance = Instantiate(ObjectAtEnd);
        //ObjectAtEnd.transform.position = new Vector3(ObjectAtEnd.transform.position.z, ObjectAtEnd.transform.position.y, ObjectAtEnd.transform.position.z);
        Rigidbody2D prevRb = hook;
        for (int i = 0; i < links; i++)
        {
            GameObject link = Instantiate(linkPrefab, ropeContainer.transform);
            ObjectAtEnd.transform.position = new Vector3(link.transform.position.x, link.transform.position.y, link.transform.position.z);
            HingeJoint2D joint = link.GetComponent<HingeJoint2D>();

            joint.connectedBody = prevRb;
            prevRb = link.GetComponent<Rigidbody2D>();

        }

        ObjectAtEndInstance.GetComponentInChildren<HingeJoint2D>().connectedBody = prevRb;
        ObjectAtEndInstance.GetComponentInChildren<DiesAndRespawnsOnRope>().anchor = this;

    }

    void DestroyRopeContainer()
    {
        if (ropeContainer != null)
        {
            Destroy(ropeContainer);
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
}
