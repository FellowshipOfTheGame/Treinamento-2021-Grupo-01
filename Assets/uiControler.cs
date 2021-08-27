using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class uiControler : MonoBehaviour
{

    private pontosControlador pontosController;

    public GameObject morreuTextGameObject;
    private Text morreuText;
    public GameObject scoreTextGameObject;
    private Text scoreText;
    public GameObject moedasTextGameObject;
    private Text moedasText;
    public Button btn_play;
    public Button btn_abrirLoja;

    public Image img_backgroundLoja;


    // Start is called before the first frame update
    void Start()
    {
        scoreText = scoreTextGameObject.GetComponent<Text>();
        morreuText = morreuTextGameObject.GetComponent<Text>();
        moedasText = moedasTextGameObject.GetComponent<Text>();

        pontosController = gameObject.GetComponent<pontosControlador>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void mostrarTelaFinal()
    {
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

        btn_play.gameObject.SetActive(false);
        morreuTextGameObject.gameObject.SetActive(false);
        btn_abrirLoja.gameObject.SetActive(false);
    }

    public void abrirLoja()
    {
        //btn_abrirLoja.gameObject.SetActive(false);
        img_backgroundLoja.gameObject.SetActive(true);
    }

    public void fecharLoja()
    {
        img_backgroundLoja.gameObject.SetActive(false);
    }
}
