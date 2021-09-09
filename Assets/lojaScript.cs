using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class lojaScript : MonoBehaviour
{
    private GameObject[] objs;
    private GameObject player;
    private GameObject camera;
    private uiControler uiControlador;
    private pontosControlador ptsController;

    //niveis dos upgrades
    private int[] upgradesLvls;
    private int qtdUpgrades;
    private upgradeScript[] upgrades;

    //UIs do upgrade da loja
    public Text tituloItem;
    public Text descItem;
    public Text precoMoedas;
    public Image imgItem;
    private int indexItemTela = 0;

    // Start is called before the first frame update
    void Start()
    {
        objs = GameObject.FindGameObjectsWithTag("Player");
        player = objs[0];
        objs = GameObject.FindGameObjectsWithTag("MainCamera");
        camera = objs[0];

        uiControlador = camera.GetComponent<uiControler>();
        ptsController = camera.GetComponent<pontosControlador>();
        attNivelUpgrades();
        criarUpgrades();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void lojaAberta()
    {
        seekCompra(0);
        attNivelUpgrades();
    }

    public void seekCompra(int indexCompra)
    {
        indexItemTela = indexCompra;
        tituloItem.text = upgrades[indexCompra].getTitulo();
        descItem.text = upgrades[indexCompra].getDesc();
        imgItem.sprite = upgrades[indexCompra].getImage();
        precoMoedas.text = "Preco : " + upgrades[indexCompra].getprecoProxNivel().ToString() + " moedas";

    }

    public void comprarUpgrade()
    {
        
        if (ptsController.getMoedasBolso() >= upgrades[indexItemTela].getprecoProxNivel())
        {
            ptsController.setMoedasBolso(ptsController.getMoedasBolso() - upgrades[indexItemTela].getprecoProxNivel());
            upgrades[indexItemTela].setNivel(upgrades[indexItemTela].getNivelAtual()+1);
            ptsController.attNivelUpgrade(indexItemTela);
            uiControlador.attMoedasLoja();

            if (indexItemTela == 1)
            {
                upgradeMoeda();
            }
            else
                if (indexItemTela == 0)
            {
                upgradePontos();
            }
            else
                if (indexItemTela == 2)
            {
                upgradeObstaculosPontos();
            }
            else
                if (indexItemTela == 3)
            {
                upgradeMoedasPontos();
            }

            seekCompra(indexItemTela);
        }
    }

    private void upgradeMoeda()
    {
        ptsController.setMultMoedas(upgrades[indexItemTela].getNivelAtual() + 1);
    }

    private void upgradePontos()
    {
        ptsController.setMultPontos(upgrades[indexItemTela].getNivelAtual() + 1);
    }

    private void upgradeMoedasPontos()
    {
        ptsController.setMultMoedasPontos(upgrades[indexItemTela].getNivelAtual() + 1);
    }

    private void upgradeObstaculosPontos()
    {
        ptsController.setMultObstaculosPontos(upgrades[indexItemTela].getNivelAtual() + 1);
    }

    public void attNivelUpgrades()
    {
        qtdUpgrades = ptsController.getqtdUpgrades();
        upgradesLvls = ptsController.getUpgradesLvls();
    }

    public void reloadLoja()
    {
        attNivelUpgrades();
        criarUpgrades();
    }

    private void criarUpgrades()
    {
        Array.Resize(ref upgrades, qtdUpgrades);

        upgrades[0] = new upgradeScript();
        upgrades[0].setTitulo("Pontuacao");
        upgrades[0].setDescUpgrade("A cada nivel aumenta o multiplicador de pontos ganhos pela distancia percorrida.");
        upgrades[0].setPrecoMult(3);
        upgrades[0].setNivel(upgradesLvls[0]);
        upgrades[0].setImage("pontos");

        upgrades[1] = new upgradeScript();
        upgrades[1].setTitulo("Multiplicador Moedas");
        upgrades[1].setDescUpgrade("A cada nivel aumenta o multiplicador de moedas ganhas.");
        upgrades[1].setPrecoMult(10);
        upgrades[1].setNivel(upgradesLvls[1]);
        upgrades[1].setImage("coin");

        upgrades[2] = new upgradeScript();
        upgrades[2].setTitulo("Pontuacao Obstaculos");
        upgrades[2].setDescUpgrade("A cada nivel aumenta  o multiplicador de pontos ganhos pelos obstaculos desviados.");
        upgrades[2].setPrecoMult(2);
        upgrades[2].setNivel(upgradesLvls[2]);
        upgrades[2].setImage("pontos");

        upgrades[3] = new upgradeScript();
        upgrades[3].setTitulo("Pontuacao Moedas");
        upgrades[3].setDescUpgrade("A cada nivel aumenta  o multiplicador de pontos ganhos pelas moedas pegas.");
        upgrades[3].setPrecoMult(1);
        upgrades[3].setNivel(upgradesLvls[3]);
        upgrades[3].setImage("coin");

    }
}
