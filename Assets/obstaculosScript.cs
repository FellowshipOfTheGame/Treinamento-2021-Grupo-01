using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obstaculosScript : MonoBehaviour
{
    private bool passou;
    private GameObject[] objs;
    private GameObject player;
    private birdMovimento playerScript;

    public enum tipoSaida {nenhum, esquerda, direita, cima, baixo, horizontal, vertical };

    public tipoSaida tipoSaidaObstaculo = tipoSaida.nenhum;

    //objetos que irao sair da tela, maximo de 2
    private Transform object1 = null;
    private Transform object2= null;

    // Start is called before the first frame update
    void Start()
    {
        passou = false;
        objs = GameObject.FindGameObjectsWithTag("Player");
        player = objs[0];
        playerScript = player.GetComponent<birdMovimento>();
        if (tipoSaidaObstaculo == tipoSaida.horizontal || tipoSaidaObstaculo == tipoSaida.vertical)
        {
            object1 = gameObject.transform.GetChild(0);
            object2 = gameObject.transform.GetChild(1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (this.transform.position.z < player.transform.position.z-1.5f)
        {
            passou = true;
        }

        if (passou == true)
        {

            moverObstaculo();

        }
    }

    private void moverObstaculo()
    {
        switch (tipoSaidaObstaculo)
        {
            case tipoSaida.nenhum:
                Destroy(gameObject);
                break;

            case tipoSaida.esquerda://o obstaculo vai sair pela esquerda
                if (this.transform.position.x > -14)
                {
                    gameObject.transform.position = new Vector3(gameObject.transform.position.x - (50 * Time.deltaTime), gameObject.transform.position.y, gameObject.transform.position.z);
                }
                else
                {
                    playerScript.addPtsObstaculo();
                    Destroy(gameObject);
                }
                    
                break;

            case tipoSaida.direita://o obstaculo vai sair pela direita
                if (this.transform.position.x < 14)
                {
                    gameObject.transform.position = new Vector3(gameObject.transform.position.x + (50 * Time.deltaTime), gameObject.transform.position.y, gameObject.transform.position.z);
                }
                else
                {
                    playerScript.addPtsObstaculo();
                    Destroy(gameObject);
                }
                break;
            case tipoSaida.cima://o obstaculo vai sair por cima
                if (this.transform.position.y < 40)
                {
                    gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + (50 * Time.deltaTime), gameObject.transform.position.z);
                }
                else
                {
                    playerScript.addPtsObstaculo();
                    Destroy(gameObject);
                }
                break;
            case tipoSaida.baixo://o obstaculo vai sair por baixo
                if (this.transform.position.y > -7)
                {
                    gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - (50 * Time.deltaTime), gameObject.transform.position.z);
                }
                else
                {
                    playerScript.addPtsObstaculo();
                    Destroy(gameObject);
                }
                break;
            case tipoSaida.horizontal://o obstaculo vai sair abrindo lateralmente
                if (object1.position.x > -14)
                {
                    object1.position = new Vector3(object1.position.x - (50 * Time.deltaTime), object1.position.y, object1.position.z);
                    object2.position = new Vector3(object2.position.x + (50 * Time.deltaTime), object2.position.y, object2.position.z);
                }
                else
                {
                    playerScript.addPtsObstaculo();
                    Destroy(gameObject);
                }
                break;
            case tipoSaida.vertical://o obstaculo vai sair abrindo verticalmente
                if (object1.position.y < -40)
                {
                    object1.position = new Vector3(object1.position.x, object1.position.y + (50 * Time.deltaTime), object1.position.z);
                    object2.position = new Vector3(object2.position.x, object2.position.y - (50 * Time.deltaTime), object2.position.z);
                }
                else
                {
                    playerScript.addPtsObstaculo();
                    Destroy(gameObject);
                }
                break;
        }

        
    }


}
