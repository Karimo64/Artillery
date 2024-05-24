using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdministradorJuego : MonoBehaviour
{
    public static AdministradorJuego SingletonAdministradorJuego;
    [SerializeField] private int velocidadBala = 30;
    public int DisparosPorJuego = 10;
    public float VelocidadRotacion = 1;


    public int VelocidadBala
    {
        get { return velocidadBala; }
        set { velocidadBala = value; }
    }
    public void Awake() {
        if (SingletonAdministradorJuego == null)
        {
            SingletonAdministradorJuego = this;
        }
        else 
        {
            Debug.LogError("Ya existe una instancia de esta clase");
        }  
    }
}
