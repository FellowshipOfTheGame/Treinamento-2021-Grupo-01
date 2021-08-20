using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obstaculosScript : MonoBehaviour
{
    private bool passou;
    private GameObject[] player;

    // Start is called before the first frame update
    void Start()
    {
        passou = false;
        player = GameObject.FindGameObjectsWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (this.transform.position.z < player[0].transform.position.z-1.5f)
        {
            //Debug.Log("a");
            passou = true;
            //Destroy(gameObject);
        }

        if (passou == true)
        {
            if (this.transform.position.x > -14)
            {
                moverObstaculo();
            }
            else
            {
                player[0].GetComponent<birdMovimento>().addPtsObstaculo();
                Destroy(gameObject);
            }
        }
    }

    private void moverObstaculo()
    {
        gameObject.transform.position = new Vector3(gameObject.transform.position.x-0.1f, gameObject.transform.position.y, gameObject.transform.position.z);
    }

}
