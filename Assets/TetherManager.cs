using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetherManager : MonoBehaviour
{
    public float fLinkDistance;

    private GameObject goPlayer1;
    private GameObject goPlayer2;

    public List<GameObject> a_goTetherLinks;

    private GameObject goTetherLink;

    private LineRenderer lrLineRenderer;

    private float fLinkDiamater;
    
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
		
	}

    void CreateLink(int p_iLinkQuantity)
    {
        GameObject link = Instantiate(goTetherLink, transform);
        a_goTetherLinks.Add(link);
        HingeJoint2D linkHinge = link.GetComponent<HingeJoint2D>();

        if (a_goTetherLinks.Count == 1)
        {
            linkHinge.connectedBody = goPlayer1.GetComponent<Rigidbody2D>();
            linkHinge.connectedAnchor = new Vector3((fLinkDistance)/2 + goPlayer1.GetComponent<CircleCollider2D>().radius, 0, 0);
        }
        else
        {
            linkHinge.connectedBody = a_goTetherLinks[a_goTetherLinks.Count - 2].GetComponent<Rigidbody2D>();
            linkHinge.connectedAnchor = new Vector3(fLinkDistance, 0, 0);
        }
    }

    void RemoveLink()
    {

    }

    void TetherRenderer()
    {

    }

    void TetherGenerator()
    {
        float playerDistance = Vector3.Distance(goPlayer1.transform.position, goPlayer2.transform.position);

        int linkQuantity = (int)(playerDistance / (fLinkDiamater + fLinkDistance));
        for (int i = 0; i < linkQuantity; i++)
        {
            CreateLink(1);
        }

        HingeJoint2D tmp = goPlayer2.GetComponent<HingeJoint2D>();
        tmp.connectedBody = a_goTetherLinks[a_goTetherLinks.Count -1].GetComponent<Rigidbody2D>();
        tmp.connectedAnchor = new Vector3(goPlayer2.GetComponent<CircleCollider2D>().radius + fLinkDistance, 0, 0);

    }
}