﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetherManager : MonoBehaviour
{
    public float fLinkDistance;

    //[HideInInspector]
    public List<GameObject> a_goTetherLinks;


    private GameObject goPlayer1;
    private GameObject goPlayer2;
    private GameObject goTetherLink;

    private LineRenderer lrLineRenderer;

    private float fLinkDiamater;

    public int iLinkQuantity = 0;

    // Use this for initialization
    void Start ()
    {
        goPlayer1 = GameObject.Find("Player1");
        goPlayer2 = GameObject.Find("Player2");
        lrLineRenderer = GetComponent<LineRenderer>();
        goTetherLink = Resources.Load("Prefabs/TetherLink") as GameObject;
        fLinkDiamater = (goTetherLink.GetComponent<CircleCollider2D>().radius) * 2;
        TetherGenerator();
    }

    // Update is called once per frame
    void Update ()
	{
	    

        if (Input.GetKey(KeyCode.E))
	    {
            RemoveLink();
	    }

	    if (Input.GetKeyDown(KeyCode.R))
	    {
            CreateLink(1);
	    }
	    TetherRenderer();
    }

    public void CreateLink(int p_iLinkQuantity)
    {
        //TODO: create links in the middle of the player.
        GameObject link;
        if (a_goTetherLinks.Count == 0)
        { 
            link = Instantiate(goTetherLink, goPlayer2.transform.position + goPlayer1.transform.position, Quaternion.identity);

        }
        else
        {
            link = Instantiate(goTetherLink, a_goTetherLinks[a_goTetherLinks.Count / 2].transform.position, Quaternion.identity);
        }
        a_goTetherLinks.Add(link);
        HingeJoint2D linkHinge = link.GetComponent<HingeJoint2D>();


        //Thought Create the enitre rope before conecting the two players
        if (a_goTetherLinks.Count > 1)
        {
            linkHinge.connectedBody = a_goTetherLinks[a_goTetherLinks.Count - 2].GetComponent<Rigidbody2D>();
            linkHinge.connectedAnchor = new Vector3(fLinkDistance, 0, 0);
        }

        // Have to fix Add the new link in the middle


        //if (a_goTetherLinks.Count == 1)
        //{
        //    linkHinge.connectedBody = goPlayer1.GetComponent<Rigidbody2D>();
        //    linkHinge.connectedAnchor = new Vector3((fLinkDistance) / 2 + goPlayer1.GetComponent<CircleCollider2D>().radius, 0, 0);
        //}
        //else
        //{
        //    linkHinge.connectedBody = a_goTetherLinks[a_goTetherLinks.Count - 2].GetComponent<Rigidbody2D>();
        //    linkHinge.connectedAnchor = new Vector3(fLinkDistance, 0, 0);
        //}
        //HingeJoint2D tmp = goPlayer2.GetComponent<HingeJoint2D>();
        //tmp.connectedBody = a_goTetherLinks[a_goTetherLinks.Count - 1].GetComponent<Rigidbody2D>();
        //tmp.connectedAnchor = new Vector3(goPlayer2.GetComponent<CircleCollider2D>().radius + fLinkDistance, 0, 0);

        link.GetComponent<CircleCollider2D>().enabled = true; 
        
        iLinkQuantity++;
    }

    void RemoveLink()
    {
        if (a_goTetherLinks.Count > 1)
        {
            if (a_goTetherLinks[a_goTetherLinks.Count / 2] != null)
            {
                a_goTetherLinks[a_goTetherLinks.Count / 2].GetComponent<TetherLink>().StartTetherLinkDownScaler();
            }
            iLinkQuantity--;
        }

        
    }

    public int i = 0;

    void TetherRenderer()
    {
        lrLineRenderer.positionCount = iLinkQuantity+2;
        i = 0;

        lrLineRenderer.SetPosition(i, goPlayer1.transform.position);

        for (i = 1; i < a_goTetherLinks.Count; i++)
        {
            if (a_goTetherLinks[i - 1] != null)
            {
                lrLineRenderer.SetPosition(i, a_goTetherLinks[i - 1].transform.position);
            }
        }

        lrLineRenderer.SetPosition(i, goPlayer2.transform.position);
    }

    void TetherGenerator()
    {
        float playerDistance = Vector3.Distance(goPlayer1.transform.position, goPlayer2.transform.position);

        int generationQuantity = (int)(playerDistance / (fLinkDiamater + fLinkDistance));
        for (int i = 0; i < generationQuantity; i++)
        {
            CreateLink(1);
        }

        a_goTetherLinks[0].GetComponent<HingeJoint2D>().connectedBody = goPlayer1.GetComponent<Rigidbody2D>();
        a_goTetherLinks[0].GetComponent<HingeJoint2D>().connectedAnchor = new Vector3((fLinkDistance) / 2 + goPlayer1.GetComponent<CircleCollider2D>().radius, 0, 0);

        HingeJoint2D tmp = goPlayer2.GetComponent<HingeJoint2D>();
        tmp.connectedBody = a_goTetherLinks[a_goTetherLinks.Count - 1].GetComponent<Rigidbody2D>();
        tmp.connectedAnchor = new Vector3(goPlayer2.GetComponent<CircleCollider2D>().radius + fLinkDistance, 0, 0);
    }

    public List<GameObject> GetLinkList()
    {
        return a_goTetherLinks;
    }
    public void SetLinkList(List<GameObject> p_a_goLinkList)
    {
        a_goTetherLinks = p_a_goLinkList;
    }
}
