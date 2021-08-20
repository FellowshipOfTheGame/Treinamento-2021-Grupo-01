using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class birdMovimento : MonoBehaviour
{
    public GameObject player;
    public GameObject chao;
    public GameObject parede1;
    public GameObject parede2;
    public GameObject paredeFim;
    public GameObject camera;

    public GameObject morreuText;
    public GameObject scoreText;
    public GameObject moedasText;

    public float velocidade;
    bool vivo = false;
    public bool jogoComecou = false;
    int lane = 0;
    private Button btn_play = null;

    float timer = 0;
    int tempoAttVelocidade = 1;

    int pontos = 0;
    int obstaculosPassados = 0;
    int posInicialPlayer = 0;
    int valorObstaculoPts = 50;

    int qtdMoedas = 0;

    // Start is called before the first frame update
    void Start()
    {
        Physics.gravity = new Vector3(0, 0, 0);
        velocidade = 2;

    }

    // Update is called once per frame
    void Update()
    {
        if (jogoComecou == true)
        {
            if (vivo == true)
            {
                movimentoPlayer();
                attPontos();
            }
            mudaVel();
            playerEstaVivo();


            //player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z + 0.01f);
            player.GetComponent<Rigidbody>().velocity = new Vector3(0, player.GetComponent<Rigidbody>().velocity.y, velocidade);

            if (player.GetComponent<Rigidbody>().velocity.z != 0)
            {
                camera.transform.position = new Vector3(camera.transform.position.x, camera.transform.position.y, player.transform.position.z - 10);
                chao.transform.position = new Vector3(chao.transform.position.x, chao.transform.position.y, player.transform.position.z + 45);
                parede1.transform.position = new Vector3(parede1.transform.position.x, parede1.transform.position.y, player.transform.position.z + 44);
                parede2.transform.position = new Vector3(parede2.transform.position.x, parede2.transform.position.y, player.transform.position.z + 44);
                paredeFim.transform.position = new Vector3(paredeFim.transform.position.x, paredeFim.transform.position.y, player.transform.position.z + 94);
            }

            //camera.transform.position = new Vector3(camera.transform.position.x, camera.transform.position.y, camera.transform.position.z + 0.01f);

        }
    }

    private void movimentoPlayer()
    {
        timer += Time.deltaTime;

        if ((Input.GetMouseButtonDown(0) || Input.GetKeyDown("space")))
        {
            if (player.transform.position.y < 6)
            {
                player.GetComponent<Rigidbody>().velocity = new Vector3(0, 5, 0);
            }
        }

        if (Input.GetKeyDown("a"))
        {
            switch (lane)
            {
                case 0:
                    player.transform.position = new Vector3(-5, player.transform.position.y, player.transform.position.z);
                    lane = -1;
                    break;

                case 1:
                    player.transform.position = new Vector3(0, player.transform.position.y, player.transform.position.z);
                    lane = 0;
                    break;
            }

        }

        if (Input.GetKeyDown("d"))
        {
            switch (lane)
            {
                case -1:
                    player.transform.position = new Vector3(0, player.transform.position.y, player.transform.position.z);
                    lane = 0;
                    break;

                case 0:
                    player.transform.position = new Vector3(5, player.transform.position.y, player.transform.position.z);
                    lane = 1;
                    break;
            }

        }
    }


    private void playerEstaVivo()
    {
        if (player.transform.position.y > 6)
        {
            player.transform.position = new Vector3(player.transform.position.x, 6, player.transform.position.z);
        }
        else
        if (player.transform.position.y <= -4.5f)
        {
            //Destroy(this);
            mostrarTelaFinal();
            vivo = false;
                timer = 0;
        }
    }

    private void mostrarTelaFinal()
    {
        scoreText.GetComponent<Text>().text = "Total Pontos : " + pontos.ToString();
        btn_play.gameObject.SetActive(true);
        morreuText.gameObject.SetActive(true);
        scoreText.gameObject.SetActive(true);
        scoreText.gameObject.transform.position = new Vector3(536, scoreText.gameObject.transform.position.y, scoreText.gameObject.transform.position.z);
        moedasText.gameObject.transform.position = new Vector3(536, moedasText.gameObject.transform.position.y, moedasText.gameObject.transform.position.z);
    }

    private void mudaVel()
    {
        if (timer > tempoAttVelocidade && vivo == true)
        {
            Debug.Log(velocidade);
            velocidade += 0.1f;
            timer = 0;
        }
        else
        {
            if (vivo == false && timer > 0.1f && velocidade > 0)
            {
                velocidade -= 0.1f;
                timer = 0;
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Moeda")
        {
            Debug.Log("moeda");
            collision.gameObject.GetComponent<moedaScript>().moedaPega();
            qtdMoedas++;
            moedasText.GetComponent<Text>().text = "Moedas : "+ qtdMoedas.ToString();
            return;
        }
        else
        {
            vivo = false;
            timer = 0;
            velocidade = 0;
            mostrarTelaFinal();
        }


    }

    private void attPontos()
    {
        pontos = (int)(player.transform.position.z) + (obstaculosPassados * valorObstaculoPts) - posInicialPlayer + (qtdMoedas * 50);
        scoreText.GetComponent<Text>().text = "Score : " + pontos.ToString();
    }

    public void addPtsObstaculo()
    {
        obstaculosPassados ++;
    }

    private void setUpJogo(bool primeiraVez)
    {
        scoreText.gameObject.SetActive(true);
        moedasText.gameObject.SetActive(true);

        moedasText.gameObject.transform.position = new Vector3(136, moedasText.gameObject.transform.position.y, moedasText.gameObject.transform.position.z);
        scoreText.gameObject.transform.position = new Vector3(136, scoreText.gameObject.transform.position.y, scoreText.gameObject.transform.position.z);

        Physics.gravity = new Vector3(0, -9.81f, 0);
        velocidade = 2;
        vivo = true;
        lane = 0;
        jogoComecou = true;
        tempoAttVelocidade = 1;
        timer = 0;
        obstaculosPassados = 0;
        posInicialPlayer = (int)player.transform.position.z;
        qtdMoedas = 0;

        player.transform.position = new Vector3(0, 1.5f, player.transform.position.z);
        camera.transform.position = new Vector3(camera.transform.position.x, camera.transform.position.y, player.transform.position.z - 10);
        player.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, velocidade);
        player.GetComponent<Rigidbody>().angularVelocity = new Vector3(0, 0, 0);
        player.transform.rotation = new Quaternion(0, 0, 0,0);

        if (primeiraVez == false)
        {
            camera.gameObject.GetComponent<spawnerObj>().reiniciarObstaculos();
            camera.gameObject.GetComponent<spawnerObj>().reiniciarMoedas();
        }
            
    }

    public void comecouJogo(Button btn)
    {
        btn_play = btn;
        btn_play.gameObject.SetActive(false);
        morreuText.gameObject.SetActive(false);
        scoreText.gameObject.SetActive(false);

        if (player.transform.position.z != 0)
        {
            setUpJogo(false);
        }
        else
            setUpJogo(true);

    }
}
