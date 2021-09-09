using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class birdMovimento : MonoBehaviour
{
    public GameObject player;

    public GameObject chao;
    public GameObject paredeFim;

    public GameObject camera;
    private pontosControlador ptsControl;
    private uiControler uiConttrollerScript;
    private spawnerObj spawnerObjetos;

    private Rigidbody rb;

    //SpeedBoost Variables
    private float oldSpeed;
    private float boostTimer;
    private bool boosting;

    //Extra Life
    private bool extraLife;

    public float velocidade;
    bool vivo = false;
    public bool jogoComecou = false;

    private Button btn_play = null;
    
    private Vector3 verticalTargetPosition; 
    private int currentLane = 1;

    float timer = 0;
    int tempoAttVelocidade = 1;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        // playerCollider = gameObject.GetComponent<Collider>().Enabled;
        Physics.gravity = new Vector3(0, 0, 0);

        velocidade = 10;
        oldSpeed = velocidade;
        boostTimer = 0;
        boosting = false;

        extraLife = false;

        ptsControl = camera.GetComponent<pontosControlador>();
        uiConttrollerScript = camera.GetComponent<uiControler>();
        spawnerObjetos = camera.gameObject.GetComponent<spawnerObj>();

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

            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, velocidade);

            if(boosting) {
                boostTimer += Time.deltaTime;
                if(boostTimer >= 5){
                    velocidade = (float)(oldSpeed + velocidade / (oldSpeed * 0.8));
                    boosting = false;
                    boostTimer = 0;
                }
            }

            if (rb.velocity.z != 0)
            {
                camera.transform.position = new Vector3(camera.transform.position.x, camera.transform.position.y, player.transform.position.z - 10);
                chao.transform.position = new Vector3(chao.transform.position.x, chao.transform.position.y, player.transform.position.z + 45);
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
                rb.velocity = new Vector3(0, 7, 0);
            }
        }

        if (Input.GetKeyDown("a"))
        {
            HandleMove(-1);
        }

        if (Input.GetKeyDown("d"))
        {
            HandleMove(1);
        }

        Vector3 targetPosition = new Vector3(verticalTargetPosition.x, transform.position.y, transform.position.z);
		transform.position = Vector3.MoveTowards(transform.position, targetPosition, velocidade * 10 * Time.deltaTime);
    }

    private void HandleMove(int direction){
        int targetLane = currentLane + direction;
		if (targetLane < 0 || targetLane > 2)
			return;
		currentLane = targetLane;
		verticalTargetPosition = new Vector3((currentLane - 1)*5, 0, 0);
    }

    private void playerMorreu(int velocidadePosMorte)
    {
        if (extraLife == false)
        {
            vivo = false;
            ptsControl.setPlayerVivo(false);
            velocidade = velocidadePosMorte;
            timer = 0;
            uiConttrollerScript.mostrarTelaFinal();
            ptsControl.salvarDados();
        } else if (extraLife == true) {
            uiConttrollerScript.imgExtraLifeOff();
            extraLife = false;
            resetPlayer(10);
        }
    }

    private void playerEstaVivo()
    {
        if (player.transform.position.y > 6)
        {
            player.transform.position = new Vector3(player.transform.position.x, 6, player.transform.position.z);
        }
        else
        if (player.transform.position.y <= -4.45f && vivo == true)
        {
            playerMorreu((int)rb.velocity.z);
        }
    }

    private void mudaVel()
    {
        if (timer > tempoAttVelocidade && vivo == true)
        {
            velocidade += 0.1f;
            timer = 0;
        }
        else
        {
            if (vivo == false && velocidade > 0)
            {
                velocidade -= velocidade*Time.deltaTime;
                timer = 0;
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Obstaculos" && vivo == true)
        {
            playerMorreu(0);
        }
    }

    public void resetPlayer(int pos) {
        player.transform.position = new Vector3(0, 1.5f, player.transform.position.z - pos);
        camera.transform.position = new Vector3(camera.transform.position.x, camera.transform.position.y, player.transform.position.z - 10);
        rb.velocity = new Vector3(0, 0, velocidade);
        rb.angularVelocity = new Vector3(0, 0, 0);
        player.transform.rotation = new Quaternion(0, 0, 0,0);

        uiConttrollerScript.iniciarJogoUI();
    }

    public void addExtraLife () {
        if(extraLife == false) {
            extraLife = true;
            uiConttrollerScript.imgExtraLifeOn();
        }
    }

    public void addSpeedBoost () {
        if(boosting == false){
            oldSpeed = velocidade;
            velocidade = velocidade*0.8f;
            boosting = true;
        } else if (boosting) {
            boostTimer = 0;
        }
    }

    public void addMoeda()
    {
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
        ptsControl.setPlayerVivo(true);
        jogoComecou = true;
        tempoAttVelocidade = 1;
        timer = 0;
        ptsControl.resetQtdMoedas();

        ptsControl.resetObstaculosPassados();
        camera.gameObject.GetComponent<spawnerObj>().reiniciarSpeedBoosts();
        camera.gameObject.GetComponent<spawnerObj>().reiniciarExtraLifes();
        
        resetPlayer(0);

        ptsControl.setPosInicial(gameObject.transform.position.z);
        spawnerObjetos.setPosInicialPlayer(ptsControl.getPosInicial());

        if (primeiraVez == false)
        {
            camera.gameObject.GetComponent<spawnerObj>().reiniciarSpawns();
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
