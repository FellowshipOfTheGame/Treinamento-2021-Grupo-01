using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pontosControlador : MonoBehaviour
{
    //informacoes a serem salvas no playerPrefs
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

    //contadores dos objetivos do jogo
    private int pontos = 0;
    private int obstaculosPassados = 0;
    private int qtdMoedas = 0;
    private int posInicialPlayer = 0;

    //verificador se o player esta vivo
    private bool playerVivo = false;

    //multiplicadores para os upgrades
    public int valorMoedasPontos = 50;
    public int valorObstaculoPts = 50;
    public int multiplicadorPontos = 1;
    public int multiplicadorMoedas;

    // Start is called before the first frame update
    void Start()
    {
        multiplicadorPontos = 1;
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

        multiplicadorMoedas = upgradesNiveis[1] + 1;
        multiplicadorPontos = upgradesNiveis[0] + 1;
        valorMoedasPontos = upgradesNiveis[3] + 1;
        valorObstaculoPts = upgradesNiveis[2] + 1;
        setMultMoedas(multiplicadorMoedas);
        setMultPontos(multiplicadorPontos);
        setMultMoedasPontos(valorMoedasPontos);
        setMultObstaculosPontos(valorObstaculoPts);

    }

    // Update is called once per frame
    void Update()
    {
        if (playerVivo== true)
        {
            pontos = (int)((player.transform.position.z - posInicialPlayer) * multiplicadorPontos) + (obstaculosPassados * valorObstaculoPts) + (qtdMoedas * valorMoedasPontos);
            uiControllerScript.atualizarUIPontos();
        }
    }

    public void setPlayerVivo(bool estado)
    {
        playerVivo = estado;
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

    public int getMultMoedas()
    {
        return multiplicadorMoedas;
    }

    public void setMultMoedas(int mult)
    {
        multiplicadorMoedas = mult;
        gameObject.GetComponent<spawnerObj>().setNivelMoeda(multiplicadorMoedas-2);
        gameObject.GetComponent<spawnerObj>().reiniciarMoedas();

    }

    public void setMultPontos(int mult)
    {
        multiplicadorPontos = mult;
    }

    public void setMultObstaculosPontos(int mult)
    {
        valorObstaculoPts = mult * 50;
    }

    public void setMultMoedasPontos(int mult)
    {
        valorMoedasPontos = mult * 50;
    }


    public void addMoedas()
    {
        qtdMoedas += multiplicadorMoedas;
        qtdMoedasBolso += multiplicadorMoedas;
        qtdMoedasTotal += multiplicadorMoedas;
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
        setMultMoedas(1);
        setMultPontos(1);
        setMultObstaculosPontos(1);
        setMultMoedasPontos(1);

        uiControllerScript.fecharRecordes();
        gameObject.GetComponent<lojaScript>().reloadLoja();
    }


}
