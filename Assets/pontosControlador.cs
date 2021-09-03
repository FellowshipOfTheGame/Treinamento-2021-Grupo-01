using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pontosControlador : MonoBehaviour
{
    private int topRun = 0;
    private int qtdMoedasBolso;
    public int qtdUpgrades = 1;
    private int[] upgradesNiveis = new int[4];
    private int qtdDistanciaPercorrida;
    private int qtdObstaculosDesviados;
    private int qtdVezesJogadas;
    private int qtdMoedasTotal;

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
        Array.Resize(ref upgradesNiveis, qtdUpgrades);

        objs = GameObject.FindGameObjectsWithTag("Player");
        player = objs[0];
        objs = GameObject.FindGameObjectsWithTag("MainCamera");
        camera = objs[0];
        uiControllerScript = camera.GetComponent<uiControler>();

        for (int i = 0; i < qtdUpgrades; i++)
        {
            upgradesNiveis[i] = PlayerPrefs.GetInt("upgrade" + i.ToString(), 0);
        }

        qtdMoedasBolso = PlayerPrefs.GetInt("moedas", 0);
        qtdMoedasTotal = PlayerPrefs.GetInt("moedasTotais", 0);
        qtdDistanciaPercorrida = PlayerPrefs.GetInt("distanciaPercorrida", 0);
        qtdObstaculosDesviados = PlayerPrefs.GetInt("obstaculosDesviados", 0);
        qtdVezesJogadas = PlayerPrefs.GetInt("vezesJogadas", 0);
        topRun = PlayerPrefs.GetInt("topRun", 0);

        gameObject.GetComponent<lojaScript>().attNivelUpgrades();

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
        qtdMoedasBolso++;
        qtdMoedasTotal++;
        uiControllerScript.atualizarUIMoedas();
    }

    public void setPosInicial(float posAtualizada)
    {
        qtdDistanciaPercorrida += (int)(posAtualizada - posInicialPlayer);
        posInicialPlayer = (int)posAtualizada;
    }

    public int getPosInicial()
    {
        return posInicialPlayer;
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

    public int getMoedasBolso()
    {
        return qtdMoedasBolso;
    }

    public void setMoedasBolso(int novaQtd)
    {
        qtdMoedasBolso = novaQtd;
    }

    public void salvarDados()
    {
        qtdObstaculosDesviados += obstaculosPassados;
        qtdVezesJogadas++;
        if (topRun < pontos)
        {
            PlayerPrefs.SetInt("topRun", pontos);
            topRun = pontos;
        }

        for (int i = 0; i < qtdUpgrades; i++)
        {
            PlayerPrefs.SetInt("upgrade" + i.ToString(), upgradesNiveis[i]);
        }

        PlayerPrefs.SetInt("moedas", qtdMoedasBolso);
        PlayerPrefs.SetInt("moedasTotais", qtdMoedasTotal);
        PlayerPrefs.SetInt("distanciaPercorrida", qtdDistanciaPercorrida);
        PlayerPrefs.SetInt("obstaculosDesviados", qtdObstaculosDesviados);
        PlayerPrefs.SetInt("vezesJogadas", qtdVezesJogadas);
        PlayerPrefs.Save();
    }

    public void mostrarRecordes()
    {
        uiControllerScript.loadRecordes(topRun,qtdMoedasTotal,qtdDistanciaPercorrida,qtdObstaculosDesviados,qtdVezesJogadas);
    }

    public int[] getUpgradesLvls()
    {
        return upgradesNiveis;
    }

    public int getqtdUpgrades()
    {
        return qtdUpgrades;
    }

    public void attNivelUpgrade(int index)
    {
        upgradesNiveis[index]++;
        salvarDados();
    }

    public void formatarJogo()
    {
        PlayerPrefs.SetInt("moedas", 0);
        PlayerPrefs.SetInt("moedasTotais", 0);
        PlayerPrefs.SetInt("distanciaPercorrida", 0);
        PlayerPrefs.SetInt("obstaculosDesviados", 0);
        PlayerPrefs.SetInt("vezesJogadas", 0);
        PlayerPrefs.SetInt("topRun", 0);

        for (int i = 0; i < qtdUpgrades; i++)
        {
            upgradesNiveis[i] = 0;
            PlayerPrefs.SetInt("upgrade" + i.ToString(), 0);
        }

        qtdObstaculosDesviados = 0;
        qtdVezesJogadas = 0;
        topRun = 0;
        qtdMoedasBolso = 0;
        qtdMoedasTotal = 0;
        qtdDistanciaPercorrida = 0;
        PlayerPrefs.Save();

        uiControllerScript.fecharRecordes();
    }


}
