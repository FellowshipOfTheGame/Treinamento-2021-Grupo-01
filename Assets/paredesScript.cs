using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class paredesScript : MonoBehaviour
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
        if (player.gameObject.transform.position.z - 20 > gameObject.transform.position.z)
        {
            Destroy(gameObject);
        }
    }
}
