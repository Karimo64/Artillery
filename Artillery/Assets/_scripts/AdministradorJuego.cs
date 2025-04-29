using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AdministradorJuego : MonoBehaviour
{
    public static AdministradorJuego SingletonAdministradorJuego;
    // public static int VelocidadBala = 30;
    // public static int DisparosPorJuego = 3; 
    // public static float VelocidadRotacion = 1;
    [SerializeField] private int velocidadBala = 30;
    [SerializeField] private int disparosPorJuego = 3;
    [SerializeField] private float velocidadRotacion = 1;

    //public GameObject CanvasGanar;
    //public GameObject CanvasPerder;
    public GameObject PanelGanar;
    public GameObject PanelPerder;
    public bool esUltimoNivel = false;



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

    public void GanarJuego() 
    {
        PanelGanar.SetActive(true);
        Time.timeScale = 0f;
    }

    public void PerderJuego() 
    {
        PanelPerder.SetActive(true);
        Time.timeScale = 0f;
    }

    public void SiguienteNivel()
    {
        Time.timeScale = 1f; // Reanuda el tiempo
        int siguienteIndice = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(siguienteIndice);
    }

    public void RegresarAlMenu()
    {
        Time.timeScale = 1f; // Reanuda el tiempo
        SceneManager.LoadScene("MenuPrincipal");
    }

    public void ReintentarNivel()
    {
        Time.timeScale = 1f; // Reanuda el tiempo
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
