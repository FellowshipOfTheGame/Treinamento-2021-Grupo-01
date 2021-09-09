using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moedaScript : MonoBehaviour
{
    private GameObject[] objs;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        objs = GameObject.FindGameObjectsWithTag("Player");
        player = objs[0];
    }

    // Update is called once per frame
    void Update()
    {
        if (player.gameObject.transform.position.z - 10 > gameObject.transform.position.z)
        {
            Destroy(gameObject);
        }
        else if (Vector3.Distance(player.transform.position, gameObject.transform.position) < 1.1f)
        {
            player.GetComponent<birdMovimento>().addMoeda();
            Destroy(gameObject);
        }

    }
}
