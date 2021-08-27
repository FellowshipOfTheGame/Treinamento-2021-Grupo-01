using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pontosControlador : MonoBehaviour
{

    private GameObject[] objs;
    private GameObject player;
    private GameObject camera;
    private uiControler uiControllerScript;

    private int pontos = 0;
    private int obstaculosPassados = 0;
    private int qtdMoedas = 0;
    private int posInicialPlayer = 0;


    public int valorMoedasPontos = 50;
    public int valorObstaculoPts = 50;

    public int multiplicadorPontos = 1;
    // Start is called before the first frame update
    void Start()
    {
        objs = GameObject.FindGameObjectsWithTag("Player");
        player = objs[0];
        objs = GameObject.FindGameObjectsWithTag("MainCamera");
        camera = objs[0];
        uiControllerScript = camera.GetComponent<uiControler>();
    }

    // Update is called once per frame
    void Update()
    {
        pontos = (int)(player.transform.position.z * multiplicadorPontos) + (obstaculosPassados * valorObstaculoPts) - posInicialPlayer + (qtdMoedas * valorMoedasPontos);
        uiControllerScript.atualizarUIPontos();
    }

    public void addObstaculosPassados()
    {
        obstaculosPassados++;
    }

    public void addPontos(int pontosAdd)
    {
        pontos += pontosAdd;
    }

    public void removerPontos(int pontosRemover)
    {
        pontos -= pontosRemover;
    }

    public void addMoedas()
    {
        qtdMoedas++;
        uiControllerScript.atualizarUIMoedas();
    }

    public void attPosInicial()
    {
        posInicialPlayer = (int)player.transform.position.z;
    }

    public void resetObstaculosPassados()
    {
        obstaculosPassados = 0;
    }

    public void resetQtdMoedas()
    {
        qtdMoedas = 0;
    }

    public int getPontos()
    {
        return pontos;
    }

    public int getMoedas()
    {
        return qtdMoedas;
    }
}
