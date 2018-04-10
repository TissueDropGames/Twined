using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetherLink : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        StartCoroutine("TetherLinkUpScaler");
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void StartTetherLinkDownScaler()
    {
        StartCoroutine("TetherLinkDownScaler");
    }

    IEnumerator TetherLinkUpScaler()
    {
        for (float i = 0; i <= 1; i += 0.1f)
        {
            transform.localScale = new Vector3(i, i, 0);
            yield return null;
        }
    }

    IEnumerator TetherLinkDownScaler()
    {
        for (float i = 1; i >= 0; i -= 0.1f)
        {
            transform.localScale = new Vector3(i, i, 0);
            yield return null;
        }
        Destroy(gameObject);
    }
}
