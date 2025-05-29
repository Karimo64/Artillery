using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Clase que administra el estado general del juego. \n Controla los paneles de victoria/derrota, velocidad del disparo y rotación, y el flujo entre escenas. \n
/// Implementa el patrón Singleton para asegurar una única instancia.
/// </summary>

public class AdministradorJuego : MonoBehaviour
{
    /// <summary>
    /// Instancia única de la clase AdministradorJuego.
    /// </summary>
    public static AdministradorJuego SingletonAdministradorJuego;
     public int hola = 0;

    [SerializeField] private int velocidadBala = 30;
    [SerializeField] private int disparosPorJuego = 3;
    [SerializeField] private float velocidadRotacion = 1;

    /// <summary>
    /// Panel que se muestra al ganar el nivel.
    /// </summary>
    public GameObject PanelGanar;

    /// <summary>
    /// Panel que se muestra al perder el nivel.
    /// </summary>
    public GameObject PanelPerder;

    /// <summary>
    /// Indica si este es el último nivel del juego.
    /// </summary>
    public bool esUltimoNivel = false;

    /// <summary>
    /// Velocidad con la que se lanza la bala.
    /// </summary>
    public int VelocidadBala
    {
        get { return velocidadBala; }
        set { velocidadBala = value; }
    }

    /// <summary>
    /// Cantidad de disparos disponibles por nivel.
    /// </summary>
    public int DisparosPorJuego
    {
        get { return disparosPorJuego; }
        set { disparosPorJuego = value; }
    }

    /// <summary>
    /// Velocidad con la que se rota el cañón al apuntar.
    /// </summary>
    public float VelocidadRotacion
    {
        get { return velocidadRotacion; }
        set { velocidadRotacion = value; }
    }

    private void Awake()
    {
        if (SingletonAdministradorJuego == null)
        {
            SingletonAdministradorJuego = this;
        }
        else
        {
            Debug.LogError("Ya existe una instancia de esta clase");
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (DisparosPorJuego <= 0 && !PanelPerder.activeSelf && !PanelGanar.activeSelf)
        {
            if (GameObject.FindGameObjectsWithTag("Bala").Length == 0)
            {
                PerderJuego();
            }
        }
    }

    /** 
    * Funcion que se encarga de mostrar el panel de victoria y detiene el tiempo del juego.
    */ 
    public void GanarJuego()
    {
        PanelGanar.SetActive(true);
        Time.timeScale = 0f;
    }

    /** <summary>
    * Funcion que se encarga de mostrar el panel de derrota y detiene el tiempo del juego.
    */ 
    public void PerderJuego()
    {
        PanelPerder.SetActive(true);
        Time.timeScale = 0f;
    }

    /** 
    * Funcion para cargar la siguiente escena (nivel) en el orden del build index.
    */ 
    public void SiguienteNivel()
    {
        Time.timeScale = 1f; // Reanuda el tiempo
        int siguienteIndice = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(siguienteIndice);
    }

    /** 
    * Funcion para cargar la escena del menú principal.
    */ 
    public void RegresarAlMenu()
    {
        Time.timeScale = 1f; // Reanuda el tiempo
        SceneManager.LoadScene("MenuPrincipal");
    }

    /** <summary>
    * Funcion para reiniciar el nivel actual.
    */ 
    public void ReintentarNivel()
    {
        Time.timeScale = 1f; // Reanuda el tiempo
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
