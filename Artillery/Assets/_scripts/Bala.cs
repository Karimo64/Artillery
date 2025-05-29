using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
*Clase que representa una bala en el juego. \n
*Controla la colisión con el suelo y otros objetos, y maneja la explosión de la bala. \n
*/

public class Bala : MonoBehaviour
{
    public GameObject particulasExplosion;

    /**
    * Funcion que se llama al iniciar la colisión con otro objeto. \n
    * Si la bala colisiona con el suelo esta explotara después de 3 segundos. \n
    * Si colisiona con un obstáculo o un objetivo, esta explotara.
    *
    * @param collision : 
    * El objeto con el que colisiona la bala durante el evento OnCollisionEnter._ \n
    */
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Suelo")
        {
            Invoke("Explotar", 3);
        }
        if (collision.gameObject.tag == "Obstaculo" || collision.gameObject.tag == "Objetivo") Explotar();

    }

    public void Explotar()
    {
        GameObject particulas = Instantiate(particulasExplosion, transform.position, Quaternion.identity) as GameObject;
        Canon.Bloqueado = false;
        SeguirCamara.objetivo = null;
        Destroy(this.gameObject);
    }
}
