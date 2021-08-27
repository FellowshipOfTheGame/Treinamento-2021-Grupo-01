using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class birdMovimento : MonoBehaviour
{
    public GameObject player;
    private Rigidbody playerRigidBody;

    public GameObject chao;
    public GameObject parede1;
    public GameObject parede2;
    public GameObject paredeFim;

    public GameObject camera;
    private pontosControlador ptsControl;
    private uiControler uiConttrollerScript;

    public float velocidade;
    bool vivo = false;
    public bool jogoComecou = false;
    int lane = 0;


    float timer = 0;
    int tempoAttVelocidade = 1;

    // Start is called before the first frame update
    void Start()
    {
        Physics.gravity = new Vector3(0, 0, 0);
        velocidade = 10;

        playerRigidBody = player.GetComponent<Rigidbody>();

        ptsControl = camera.GetComponent<pontosControlador>();
        uiConttrollerScript = camera.GetComponent<uiControler>();

    }

    // Update is called once per frame
    void Update()
    {
        if (jogoComecou == true)
        {
            if (vivo == true)
            {
                movimentoPlayer();
            }
            mudaVel();
            playerEstaVivo();


            playerRigidBody.velocity = new Vector3(0, playerRigidBody.velocity.y, velocidade);

            if (playerRigidBody.velocity.z != 0)
            {
                camera.transform.position = new Vector3(camera.transform.position.x, camera.transform.position.y, player.transform.position.z - 10);
                chao.transform.position = new Vector3(chao.transform.position.x, chao.transform.position.y, player.transform.position.z + 45);
                parede1.transform.position = new Vector3(parede1.transform.position.x, parede1.transform.position.y, player.transform.position.z + 44);
                parede2.transform.position = new Vector3(parede2.transform.position.x, parede2.transform.position.y, player.transform.position.z + 44);
                paredeFim.transform.position = new Vector3(paredeFim.transform.position.x, paredeFim.transform.position.y, player.transform.position.z + 94);
            }


        }
    }

    private void movimentoPlayer()
    {
        timer += Time.deltaTime;

        if ((Input.GetMouseButtonDown(0) || Input.GetKeyDown("space")))
        {
            if (player.transform.position.y < 6)
            {
                playerRigidBody.velocity = new Vector3(0, 5, 0);
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

    private void playerMorreu(int velocidadePosMorte)
    {
        vivo = false;
        velocidade = velocidadePosMorte;
        timer = 0;
        uiConttrollerScript.mostrarTelaFinal();
    }

    private void playerEstaVivo()
    {
        if (player.transform.position.y > 6)
        {
            player.transform.position = new Vector3(player.transform.position.x, 6, player.transform.position.z);
        }
        else
        if (player.transform.position.y <= -4.5f && vivo == true)
        {
            playerMorreu((int)playerRigidBody.velocity.z);
        }
    }

    private void mudaVel()
    {
        if (timer > tempoAttVelocidade && vivo == true)
        {
            //Debug.Log(velocidade);
            velocidade += 0.1f;
            timer = 0;
        }
        else
        {
            if (vivo == false && velocidade > 0)
            {
                Debug.Log(velocidade);
                velocidade -= velocidade*Time.deltaTime;
                timer = 0;
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        //Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.tag == "Obstaculos")
        {
            playerMorreu(0);
        }

    }

    public void addMoeda()
    {
        //Debug.Log("moeda");
        ptsControl.addMoedas();
    }

    public void addPtsObstaculo()
    {
        ptsControl.addObstaculosPassados();
    }

    private void setUpJogo(bool primeiraVez)
    {

        Physics.gravity = new Vector3(0, -9.81f, 0);
        velocidade = 10;
        vivo = true;
        lane = 0;
        jogoComecou = true;
        tempoAttVelocidade = 1;
        timer = 0;
        ptsControl.resetQtdMoedas();
        ptsControl.attPosInicial();
        ptsControl.resetObstaculosPassados();

        player.transform.position = new Vector3(0, 1.5f, player.transform.position.z);
        camera.transform.position = new Vector3(camera.transform.position.x, camera.transform.position.y, player.transform.position.z - 10);
        playerRigidBody.velocity = new Vector3(0, 0, velocidade);
        playerRigidBody.angularVelocity = new Vector3(0, 0, 0);
        player.transform.rotation = new Quaternion(0, 0, 0,0);

        uiConttrollerScript.iniciarJogoUI();

        if (primeiraVez == false)
        {
            camera.gameObject.GetComponent<spawnerObj>().reiniciarObstaculos();
            camera.gameObject.GetComponent<spawnerObj>().reiniciarMoedas();
        }
            
    }

    public void comecouJogo(Button btn)
    {

        if (player.transform.position.z != 0)
        {
            setUpJogo(false);
        }
        else
            setUpJogo(true);

    }
}
