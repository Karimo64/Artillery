using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canon : MonoBehaviour
{
    [SerializeField] private GameObject BalaPrefab;
    private GameObject puntaCanon;
    private float rotacion;
    //int numDisparos = AdministradorJuego.DisparosPorJuego;
    private void Start() {
        puntaCanon = transform.Find("PuntaCanon").gameObject;
    }
    void Update()
    {
        rotacion += Input.GetAxis("Horizontal") * AdministradorJuego.VelocidadRotacion;
        if (rotacion <= 90 && rotacion >= 0)
        {
            transform.eulerAngles = new Vector3(rotacion, 90, 0.0f);
        }

        if (rotacion > 90) rotacion = 90;
        if (rotacion < 0) rotacion = 0;

            if (Input.GetKeyDown(KeyCode.Space) && AdministradorJuego.DisparosPorJuego > 0)
            {
                GameObject temp = Instantiate(BalaPrefab, puntaCanon.transform.position, transform.rotation);
                Rigidbody tempRB = temp.GetComponent<Rigidbody>();
                Vector3 direccionDisparo = transform.rotation.eulerAngles;
                direccionDisparo.y = 90 - direccionDisparo.x;
                tempRB.velocity = direccionDisparo.normalized * AdministradorJuego.VelocidadBala;
                AdministradorJuego.DisparosPorJuego--;
                Debug.Log("El numero de disoparos es: " + AdministradorJuego.DisparosPorJuego);
            }
            
        
    }
}
