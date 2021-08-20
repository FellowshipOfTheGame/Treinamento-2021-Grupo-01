using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moedaScript : MonoBehaviour
{
    private bool animacaoStart = false;
    private int subidaAnim = 0;
    private int descidaAnim = 0;

    private GameObject[] objs;
    private GameObject camera;
    // Start is called before the first frame update
    void Start()
    {
        objs = GameObject.FindGameObjectsWithTag("MainCamera");
        camera = objs[0];
    }

    // Update is called once per frame
    void Update()
    {
        if (animacaoStart == true)
        {
            if (subidaAnim < 60)
            {
                gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 0.01f, gameObject.transform.position.z);
                subidaAnim++;
            }
            else
            {
                if (descidaAnim < 20)
                {
                    gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - 0.01f, gameObject.transform.position.z);
                    descidaAnim++;
                }
                else
                {
                    Destroy(gameObject);
                }
            }
            
        }

        if (camera.gameObject.transform.position.z > gameObject.transform.position.z)
        {
            Destroy(gameObject);
        }
    }

    public void moedaPega()
    {
        animacaoStart = true;
        gameObject.GetComponent<SphereCollider>().enabled = false;
    }
}
