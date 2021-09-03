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
    private pontosControlador ptsControl;
    private uiControler uiConttrollerScript;
    private spawnerObj spawnerObjetos;

    private Rigidbody rb;

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
        Physics.gravity = new Vector3(0, 0, 0);
        velocidade = 10;

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

            if (rb.velocity.z != 0)
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
        vivo = false;
        velocidade = velocidadePosMorte;
        timer = 0;
        uiConttrollerScript.mostrarTelaFinal();
        ptsControl.salvarDados();
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
                Debug.Log(velocidade);
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
        jogoComecou = true;
        tempoAttVelocidade = 1;
        timer = 0;
        ptsControl.resetQtdMoedas();

        ptsControl.resetObstaculosPassados();

        player.transform.position = new Vector3(0, 1.5f, player.transform.position.z);
        camera.transform.position = new Vector3(camera.transform.position.x, camera.transform.position.y, player.transform.position.z - 10);
        rb.velocity = new Vector3(0, 0, velocidade);
        rb.angularVelocity = new Vector3(0, 0, 0);
        player.transform.rotation = new Quaternion(0, 0, 0,0);

        uiConttrollerScript.iniciarJogoUI();

        ptsControl.setPosInicial(gameObject.transform.position.z);
        spawnerObjetos.setPosInicialPlayer(ptsControl.getPosInicial());

        if (primeiraVez == false)
        {
            spawnerObjetos.reiniciarObstaculos();
            spawnerObjetos.reiniciarMoedas();
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
