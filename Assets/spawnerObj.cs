using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnerObj : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject[] objs;
    private GameObject player;
    private birdMovimento playerScript;

    private int posInicialPlayer = 0;

    private AudioSource audio;

    //mudanca da probabilidade do spawn dos boosts
    int spawnRate;
    int maxSpawnRate = 11;

    //quantidades de cada objeto na tela
    int obstaculos = 0;
    int qtdParedes;
    int moedas = 0;
    int speedBoosts = 0;
    int extraLife = 0;

    //controlador da altura dos spawns dos objetos
    int alturaMoedaAnt = 0;
    int alturaBoostAnt = 0;
    int alturaExtraLife = 0;

    private int nivelMoeda;
    private Color[] coresMoeda = new Color[9];

    void Start()
    {
        qtdParedes = -2;

        coresMoeda[0] = Color.cyan;
        coresMoeda[1] = Color.blue;
        coresMoeda[2] = Color.green;
        coresMoeda[3] = Color.red;
        coresMoeda[4] = Color.magenta;
        coresMoeda[5] = Color.gray;
        coresMoeda[6] = Color.black;
        coresMoeda[7] = Color.white;
        coresMoeda[8] = Color.white;
        objs = GameObject.FindGameObjectsWithTag("Player");
        player = objs[0];
        playerScript = player.GetComponent<birdMovimento>();
        audio = GetComponent<AudioSource>();

        while (player.transform.position.z - posInicialPlayer - (obstaculos * 10) > -80)
        {
            obstaculos++;
            spawnObstaculo((obstaculos * 10));
        }
  
        while (player.transform.position.z - posInicialPlayer - ((speedBoosts * 10) + 5) > -80)
        {
            speedBoosts++;
            spawnSpeedBoost((speedBoosts*10)+5);
        }

        while (player.transform.position.z - posInicialPlayer - ((extraLife * 10) + 5) > -80)
        {
            extraLife++;
            spawnExtraLife((extraLife*10)+5);
        }

        while (qtdParedes < 8)
        {
            qtdParedes++;
            spawnParedes(qtdParedes * 12);
        }
    }

    public void setNivelMoeda(int nivel)
    {
        nivelMoeda = nivel;
    }

    public void musicHandler(bool state){
        if(state == true){
            audio.Play();
        } else if (state == false){
            audio.Stop();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.z - posInicialPlayer - obstaculos * 10 > -80)
        {
            obstaculos++;
            spawnObstaculo(95+(int)((player.transform.position.z - posInicialPlayer)/100));
        }

        if (player.transform.position.z - posInicialPlayer - ((moedas * 10) + 5) > -80)
        {
            moedas++;
            spawnMoeda(85 + (int)((player.transform.position.z - posInicialPlayer) / 100));
        }

        if (player.transform.position.z - posInicialPlayer - (qtdParedes * 12) > -84)
        {
            qtdParedes++;
            spawnParedes(96);
        }

        if (player.transform.position.z - posInicialPlayer - ((speedBoosts * 10) + 5) > -80)
        {
            speedBoosts++;
            spawnSpeedBoost(85+(speedBoosts * 0.5f));
            
        }

        if (player.transform.position.z - posInicialPlayer - ((extraLife * 10) + 5) > -80)
        {
            extraLife++;
            spawnExtraLife(85+(extraLife * 0.5f));
            
        }
    }

    private void spawnObstaculo(float distObs)
    {
        int obstaculo = Random.Range(1, 18);
        string nome = "ob" + obstaculo.ToString();
        GameObject obstaculoNovo = Instantiate(Resources.Load(nome) as GameObject, new Vector3(0, -5, player.transform.position.z + distObs), Quaternion.identity);
    }

    private void spawnParedes(float distObs)
    {
        int obstaculo = Random.Range(1, 4);
        string nome = "parede" + obstaculo.ToString();
        obstaculo = Random.Range(1, 4);
        Vector3 rotacaoParede = new Vector3(0,90*obstaculo,0);
        GameObject obstaculoNovo = Instantiate(Resources.Load(nome) as GameObject, new Vector3(12.5f, -5, player.transform.position.z + distObs), Quaternion.identity);
        obstaculoNovo.transform.eulerAngles = rotacaoParede;

        GameObject obstaculoNovo2 = Instantiate(Resources.Load(nome) as GameObject, new Vector3(-12.5f, -5, player.transform.position.z + distObs), Quaternion.identity);
        obstaculoNovo2.transform.eulerAngles = rotacaoParede;
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

        GameObject obstaculoNovo = Instantiate(Resources.Load("moeda") as GameObject, new Vector3((moedaLane*5), alturaMoedaAnt, player.transform.position.z + distObj), Quaternion.identity);
        if (nivelMoeda >= 0)
        {
            obstaculoNovo.transform.GetChild(0).gameObject.GetComponent<Renderer>().material.color = coresMoeda[nivelMoeda];
        }
        
    }

    private void spawnExtraLife(float distObj)
    {
        spawnRate = Random.Range(1, maxSpawnRate);
        if(spawnRate == 1){
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

            Vector3 rotacaoExtraLife = new Vector3(-90, 0, 0);
            GameObject obstaculoNovo = Instantiate(Resources.Load("ExtraLife") as GameObject, new Vector3((extraLifeLane*5), alturaExtraLife, player.transform.position.z + distObj), Quaternion.identity);
            obstaculoNovo.transform.eulerAngles = rotacaoExtraLife;
        }
    }

    private void spawnSpeedBoost(float distObj)
    {
        spawnRate = Random.Range(1, maxSpawnRate);
        if(spawnRate == 1){
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

            Vector3 rotacaoSpeedBoost = new Vector3(-90, 0, 0);
            GameObject obstaculoNovo = Instantiate(Resources.Load("SpeedBoost") as GameObject, new Vector3((boostLane*5), alturaBoostAnt, player.transform.position.z + distObj), Quaternion.identity);
            obstaculoNovo.transform.eulerAngles = rotacaoSpeedBoost;
        }
    }

    private void reiniciarObstaculos()
    {
        GameObject[] obstaculosGameObjects = GameObject.FindGameObjectsWithTag("Obstaculos");
        for (int i = 0; i < obstaculosGameObjects.Length; i++)
        {
            Destroy(obstaculosGameObjects[i]);
        }

        obstaculos = 0;

        while (player.transform.position.z - posInicialPlayer - (obstaculos * 10) > -80)
        {
            obstaculos++;
            spawnObstaculo((obstaculos * 10));
        }
    }

    public void setPosInicialPlayer(int posInicial)
    {
        posInicialPlayer = posInicial;

    }

    private void reiniciarParedes()
    {
        GameObject[] paredes = GameObject.FindGameObjectsWithTag("Paredes");
        for (int i = 0; i < paredes.Length; i++)
        {
            Destroy(paredes[i]);
        }

        qtdParedes = -2;

        while (qtdParedes < 8)
        {
            qtdParedes++;
            spawnParedes(qtdParedes * 12);
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

        while (player.transform.position.z - posInicialPlayer - ((moedas * 10) + 5) > -80)
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
        

        while (player.transform.position.z - posInicialPlayer - ((extraLife * 10) + 5) > -80 )
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
        
        while (player.transform.position.z - posInicialPlayer - ((speedBoosts * 10) + 5) > -80)
        {
            speedBoosts++;
            spawnSpeedBoost((speedBoosts * 10) + 5);
        }
    }

    public void reiniciarSpawns()
    {
        reiniciarObstaculos();
        reiniciarMoedas();
        reiniciarSpeedBoosts();
        reiniciarExtraLifes();
        reiniciarParedes();
    }
}
