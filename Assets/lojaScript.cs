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

    private int[] upgradesLvls;
    private int qtdUpgrades;
    private upgradeScript[] upgrades;

    public Text tituloItem;
    public Text descItem;
    public Text precoMoedas;
    public Image imgItem;



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

    }

    public void seekCompra(int indexCompra)
    {
        tituloItem.text = upgrades[indexCompra].getTitulo();
        descItem.text = upgrades[indexCompra].getDesc();
        precoMoedas.text = "Preco : " + upgrades[indexCompra].getprecoProxNivel().ToString() + " moedas";

    }

    public void comprarUpgrade(int indexCompra)
    {
        if (ptsController.getMoedasBolso() >= upgrades[indexCompra].getprecoProxNivel())
        {
            ptsController.setMoedasBolso(ptsController.getMoedasBolso() - upgrades[indexCompra].getprecoProxNivel());
            upgrades[indexCompra].setNivel(upgrades[indexCompra].getNivelAtual()+1);
            ptsController.attNivelUpgrade(indexCompra);

            //todo 
        }
    }

    public void attNivelUpgrades()
    {
        qtdUpgrades = ptsController.getqtdUpgrades();
        Debug.Log(qtdUpgrades);
        upgradesLvls = ptsController.getUpgradesLvls();
    }

    private void criarUpgrades()
    {
        Array.Resize(ref upgrades, qtdUpgrades);

        upgrades[0] = new upgradeScript();
        upgrades[0].setTitulo("Pontuacao");
        upgrades[0].setDescUpgrade("A cada nivel aumenta o multiplicador de pontos ganhos pela distanci percorrida.");
        upgrades[0].setPrecoMult(1);
        upgrades[0].setNivel(upgradesLvls[0]);

        upgrades[1] = new upgradeScript();
        upgrades[1].setTitulo("Multiplicador Moedas");
        upgrades[1].setDescUpgrade("A cada nivel aumenta o multiplicador de moedas ganhas.");
        upgrades[1].setPrecoMult(10);
        upgrades[1].setNivel(upgradesLvls[1]);

        upgrades[2] = new upgradeScript();
        upgrades[2].setTitulo("PowerUp Invincibilidade");
        upgrades[2].setDescUpgrade("A cada nivel aumenta a duracao do powerup de invincibilidade(+1 seg/lvl).");
        upgrades[2].setPrecoMult(4);
        upgrades[2].setNivel(upgradesLvls[2]);

        upgrades[3] = new upgradeScript();
        upgrades[3].setTitulo("PowerUp Ima");
        upgrades[3].setDescUpgrade("A cada nivel aumenta a duracao do powerup de Ima(+1 seg/lvl).");
        upgrades[3].setPrecoMult(3);
        upgrades[3].setNivel(upgradesLvls[3]);

    }
}
