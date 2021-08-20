using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnerObj : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject[] objs;
    private GameObject player;


    int obstaculos = 0;
    int moedas = 0;

    int alturaMoedaAnt = 0;

    void Start()
    {
        objs = GameObject.FindGameObjectsWithTag("Player");
        player = objs[0];

        while (player.transform.position.z - (obstaculos * 10) > -80)
        {
            obstaculos++;
            spawnObstaculo((obstaculos * 10));
        }

        while (player.transform.position.z - ((moedas * 10) + 5) > -80)
        {
            moedas++;
            spawnMoeda((moedas*10)+5);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.z - (obstaculos * 10) > -80)
        {
            obstaculos++;
            spawnObstaculo(94 + player.gameObject.GetComponent<birdMovimento>().velocidade - 2);
        }

        if (player.transform.position.z - ((moedas * 10) + 5) > -80)
        {
            moedas++;
            spawnMoeda(85 + player.gameObject.GetComponent<birdMovimento>().velocidade - 2);
        }
    }

    private void spawnObstaculo(float distObs)
    {
        int obstaculo = Random.Range(1, 6);
        string nome = "ob" + obstaculo.ToString();
        //Debug.Log(nome);
        GameObject obstaculoNovo = Instantiate(Resources.Load(nome) as GameObject, new Vector3(0, 0, player.transform.position.z + distObs), Quaternion.identity);
    }

    private void spawnMoeda(float distObj)
    {
        if (moedas % 5 == 0)
        {
            alturaMoedaAnt = 0;
        }
        alturaMoedaAnt += Random.Range(-2, 3);

        if (alturaMoedaAnt < -4)
            alturaMoedaAnt = -4;

        if (alturaMoedaAnt > 5)
            alturaMoedaAnt = 5;

        int moedaLane = Random.Range(-1,2);
        Debug.Log(moedaLane);
        GameObject obstaculoNovo = Instantiate(Resources.Load("moeda") as GameObject, new Vector3((moedaLane*5), alturaMoedaAnt, player.transform.position.z + distObj), Quaternion.identity);
    }

    public void reiniciarObstaculos()
    {
        GameObject[] obstaculosGameObjects = GameObject.FindGameObjectsWithTag("Obstaculos");
        for (int i = 0; i < obstaculosGameObjects.Length; i++)
        {
            Destroy(obstaculosGameObjects[i]);
        }

        obstaculos = 0;

        while (player.transform.position.z - (obstaculos * 10) > -80)
        {
            obstaculos++;
            spawnObstaculo((obstaculos * 10));
        }
    }

    public void reiniciarMoedas()
    {
        GameObject[] moedasGameObjects = GameObject.FindGameObjectsWithTag("Moeda");
        for (int i = 0; i < moedasGameObjects.Length; i++)
        {
            Destroy(moedasGameObjects[i]);
        }

        moedas = 0;

        while (player.transform.position.z - ((moedas * 10) + 5) > -80)
        {
            moedas++;
            spawnMoeda((moedas * 10) + 5);
        }
    }
}