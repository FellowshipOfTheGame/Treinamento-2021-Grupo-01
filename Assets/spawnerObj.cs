using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnerObj : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject[] objs;
    private GameObject player;
    private birdMovimento playerScript;


    int obstaculos = 0;
    int moedas = 0;
    int speedBoosts = 0;
    int extraLife = 0;

    int alturaMoedaAnt = 0;
    int alturaBoostAnt = 0;
    int alturaExtraLife = 0;

    void Start()
    {
        objs = GameObject.FindGameObjectsWithTag("Player");
        player = objs[0];
        playerScript = player.GetComponent<birdMovimento>();

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

        while (player.transform.position.z - ((moedas * 10) + 5) > -80)
        {
            speedBoosts++;
            spawnSpeedBoost((moedas*10)+5);
        }

        while (player.transform.position.z - ((moedas * 10) + 5) > -80)
        {
            extraLife++;
            spawnExtraLife((extraLife*10)+5);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.z - obstaculos * 10 > -80)
        {
            obstaculos++;
            spawnObstaculo(94);
        }

        if (player.transform.position.z - ((moedas * 10) + 5) > -80)
        {
            moedas++;
            spawnMoeda(85+(moedas * 0.5f));
        }

        if (player.transform.position.z - ((moedas * 10) + 5) > -80)
        {
            speedBoosts++;
            spawnSpeedBoost(85+(speedBoosts * 0.5f));
        }

        if (player.transform.position.z - ((moedas * 10) + 5) > -80)
        {
            extraLife++;
            spawnExtraLife(85+(extraLife * 0.5f));
        }
    }

    private void spawnObstaculo(float distObs)
    {
        // int obstaculo = Random.Range(1, 6);
        // string nome = "ob" + obstaculo.ToString();
        // //Debug.Log(nome);
        // GameObject obstaculoNovo = Instantiate(Resources.Load(nome) as GameObject, new Vector3(0, 0, player.transform.position.z + distObs), Quaternion.identity);
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
        
        //Debug.Log(moedaLane);
        GameObject obstaculoNovo = Instantiate(Resources.Load("moeda") as GameObject, new Vector3((moedaLane*5), alturaMoedaAnt, player.transform.position.z + distObj), Quaternion.identity);
    }

    private void spawnExtraLife(float distObj)
    {
        if (extraLife % 5 == 0)
        {
            alturaExtraLife = 0;
        }
        alturaExtraLife += Random.Range(-2, 3);

        if (alturaExtraLife < -4)
            alturaExtraLife = -4;

        if (alturaExtraLife > 5)
            alturaExtraLife = 5;

        int extraLifeLane = Random.Range(-1,2);
        
        //Debug.Log(moedaLane);
        GameObject obstaculoNovo = Instantiate(Resources.Load("ExtraLife") as GameObject, new Vector3((extraLifeLane*5), alturaExtraLife, player.transform.position.z + distObj), Quaternion.identity);
    }

    private void spawnSpeedBoost(float distObj)
    {
        if (speedBoosts % 5 == 0)
        {
            alturaBoostAnt = 0;
        }
        alturaBoostAnt += Random.Range(-2, 3);

        if (alturaBoostAnt < -4)
            alturaBoostAnt = -4;

        if (alturaBoostAnt > 5)
            alturaBoostAnt = 5;

        int boostLane = Random.Range(-1,2);
        
        //Debug.Log(moedaLane);
        GameObject obstaculoNovo = Instantiate(Resources.Load("SpeedBoost") as GameObject, new Vector3((boostLane*5), alturaBoostAnt, player.transform.position.z + distObj), Quaternion.identity);
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
            //Debug.Log(obstaculos);
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

    public void reiniciarExtraLifes()
    {
        GameObject[] ExtraLifeGameObjects = GameObject.FindGameObjectsWithTag("ExtraLife");
        for (int i = 0; i < ExtraLifeGameObjects.Length; i++)
        {
            Destroy(ExtraLifeGameObjects[i]);
        }

        extraLife = 0;

        while (player.transform.position.z - ((extraLife * 10) + 5) > -80)
        {
            extraLife++;
            spawnExtraLife((extraLife * 10) + 5);
        }
    }

    public void reiniciarSpeedBoosts()
    {
        GameObject[] BoostsGameObjects = GameObject.FindGameObjectsWithTag("SpeedBoost");
        for (int i = 0; i < BoostsGameObjects.Length; i++)
        {
            Destroy(BoostsGameObjects[i]);
        }

        speedBoosts = 0;

        while (player.transform.position.z - ((speedBoosts * 10) + 5) > -80)
        {
            speedBoosts++;
            spawnSpeedBoost((speedBoosts * 10) + 5);
        }
    }
}
