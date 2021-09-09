using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class uiControler : MonoBehaviour
{

    private pontosControlador pontosController;
    private lojaScript lojaScript;

    //ui da gameplay
    public GameObject morreuTextGameObject;
    private Text morreuText;
    public GameObject scoreTextGameObject;
    private Text scoreText;
    public GameObject moedasTextGameObject;
    private Text moedasText;
    public Button btn_play;
    public Image imgExtraLife;
    
    //ui da loja
    public Button btn_abrirLoja;
    public Image img_backgroundLoja;
    public Text qtdMoedasLoja;

    //textos dos recordes
    public Button btn_highscore;
    public GameObject panelRecordes;
    public Text txtTopRun;
    public Text txtTotalMoedasColetadas;
    public Text txtTotalObstaculosDesviados;
    public Text txtTotalVezesJogadas;
    public Text txtTotalDistanciaPercorrida;

    // Start is called before the first frame update
    void Start()
    {
        scoreText = scoreTextGameObject.GetComponent<Text>();
        morreuText = morreuTextGameObject.GetComponent<Text>();
        moedasText = moedasTextGameObject.GetComponent<Text>();

        pontosController = gameObject.GetComponent<pontosControlador>();
        lojaScript = gameObject.GetComponent<lojaScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void mostrarTelaFinal()
    {
        btn_highscore.gameObject.SetActive(true);
        scoreText.text = "Total Pontos : " + pontosController.getPontos().ToString();
        btn_play.gameObject.SetActive(true);
        morreuTextGameObject.gameObject.SetActive(true);
        scoreTextGameObject.gameObject.SetActive(true);
        btn_abrirLoja.gameObject.SetActive(true);
        scoreTextGameObject.gameObject.transform.position = new Vector3(536, scoreText.gameObject.transform.position.y, scoreText.gameObject.transform.position.z);
        moedasTextGameObject.gameObject.transform.position = new Vector3(536, moedasTextGameObject.gameObject.transform.position.y, moedasTextGameObject.gameObject.transform.position.z);

    }

    public void atualizarUIPontos()
    {
        scoreText.text = "Total Pontos : " + pontosController.getPontos().ToString();
    }

    public void atualizarUIMoedas()
    {
        moedasText.text = "Moedas : " + pontosController.getMoedas().ToString();
    }

    public void iniciarJogoUI()
    {
        scoreTextGameObject.gameObject.SetActive(true);
        moedasTextGameObject.gameObject.SetActive(true);

        moedasTextGameObject.gameObject.transform.position = new Vector3(136, moedasTextGameObject.gameObject.transform.position.y, moedasTextGameObject.gameObject.transform.position.z);
        scoreTextGameObject.gameObject.transform.position = new Vector3(136, scoreTextGameObject.gameObject.transform.position.y, scoreTextGameObject.gameObject.transform.position.z);

        moedasText.text = "Moedas : " + pontosController.getMoedas().ToString();

        btn_highscore.gameObject.SetActive(false);
        btn_play.gameObject.SetActive(false);
        morreuTextGameObject.gameObject.SetActive(false);
        btn_abrirLoja.gameObject.SetActive(false);
    }

    public void abrirLoja()
    {
        img_backgroundLoja.gameObject.SetActive(true);
        qtdMoedasLoja.text = pontosController.getMoedasBolso().ToString();
        btn_highscore.gameObject.SetActive(false);
        lojaScript.lojaAberta();

    }

    public void attMoedasLoja()
    {
        qtdMoedasLoja.text = pontosController.getMoedasBolso().ToString();
    }

    public void fecharLoja()
    {
        img_backgroundLoja.gameObject.SetActive(false);
        btn_highscore.gameObject.SetActive(true);
    }

    public void loadRecordes(int topRun, int qtdMoedas, int qtdDist, int qtdObst, int qtdJogos)
    {
        txtTopRun.text = "Melhor Run - " + topRun.ToString() + " pontos";
        txtTotalMoedasColetadas.text = "Moedas coletadas - " + qtdMoedas.ToString() + " moedas";
        txtTotalObstaculosDesviados.text = "Obstaculos Desviados - " + qtdObst.ToString();
        txtTotalVezesJogadas.text = "Vezes Jogadas - " + qtdJogos.ToString();
        txtTotalDistanciaPercorrida.text = "Distancia Percorrida - " + qtdDist.ToString();
    }

    public void abrirRecordes()
    {
        panelRecordes.SetActive(true);
        btn_abrirLoja.gameObject.SetActive(false);
        btn_highscore.gameObject.SetActive(false);
        btn_play.gameObject.SetActive(false);
        pontosController.mostrarRecordes();
    }

    public void fecharRecordes()
    {
        panelRecordes.SetActive(false);
        btn_abrirLoja.gameObject.SetActive(true);
        btn_highscore.gameObject.SetActive(true);
        btn_play.gameObject.SetActive(true);
    }

    public void imgExtraLifeOn()
    {
        imgExtraLife.gameObject.SetActive(true);
    }

    public void imgExtraLifeOff()
    {
        imgExtraLife.gameObject.SetActive(false);
    }

}
