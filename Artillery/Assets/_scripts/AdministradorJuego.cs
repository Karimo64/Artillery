using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdministradorJuego : MonoBehaviour
{
    public static AdministradorJuego SingletonAdministradorJuego;
    [SerializeField] private int velocidadBala = 30;
    [SerializeField] private int disparosPorJuego = 10;
    [SerializeField] private float velocidadRotacion = 1;


    public int VelocidadBala
    {
        get { return velocidadBala; }
        set { velocidadBala = value; }
    }

    public int DisparosPorJuego
    {
        get { return disparosPorJuego; }
        set { disparosPorJuego = value; }
    }

    public float VelocidadRotacion
    {
        get { return velocidadRotacion; }
        set { velocidadRotacion = value; }
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
