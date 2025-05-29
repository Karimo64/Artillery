using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Canon : MonoBehaviour
{
    /// <summary>
    /// Variable estática que indica si el cañón está bloqueado para disparar.
    /// </summary>
    public static bool Bloqueado;

    /// <summary>
    /// Clip de sonido que se reproduce al disparar.
    /// </summary>
    public AudioClip clipDisparo;

    /// <summary>
    /// Prefab que representa la bala disparada por el cañón.
    /// </summary>
    [SerializeField] public GameObject BalaPrefab;

    /// <summary>
    /// Prefab de partículas que se instancian al disparar.
    /// </summary>
    public GameObject ParticulasDisparo;

    /// <summary>
    /// Slider que permite al jugador ajustar la fuerza del disparo.
    /// </summary>
    public Slider sliderFuerza;

    /// <summary>
    /// Texto que muestra el número de disparos restantes.
    /// </summary>
    public TextMeshProUGUI textoDisparos;

    /// <summary>
    /// Valor mínimo permitido para la fuerza del disparo.
    /// </summary>
    public float fuerzaMin = 10f;

    /// <summary>
    /// Valor máximo permitido para la fuerza del disparo.
    /// </summary>
    public float fuerzaMax = 50f;

    /// <summary>
    /// Controles definidos para manejar el cañón (entrada del jugador).
    /// </summary>
    public CanonControls canonControls;

    // Miembros privados
    private GameObject SonidoDisparo;
    private AudioSource SourceDisparo;
    private GameObject puntaCanon;
    private float rotacion;
    private InputAction apuntar;
    private InputAction modificarFuerza;
    private InputAction disparar;
    private float fuerzaActual;

    private void Awake()
    {
        canonControls = new CanonControls();
    }

    /// <summary>
    /// Se llama cuando el objeto se activa en la escena. /n 
    /// Aquí se habilitan las acciones del jugador y se conecta el evento de disparo. 
    /// </summary>
    private void OnEnable()
    {
        apuntar = canonControls.Canon.Apuntar;
        modificarFuerza = canonControls.Canon.ModificarFuerza;
        disparar = canonControls.Canon.Disparar;
        apuntar.Enable();
        modificarFuerza.Enable();
        disparar.Enable();
        disparar.performed += Disparar;
    }

    /// <summary>
    /// Se ejecuta cuando el objeto se desactiva. 
    /// Sirve para limpiar las acciones y evitar errores o memoria usada innecesariamente.
    /// </summary>
    private void OnDisable()
    {
        apuntar.Disable();
        modificarFuerza.Disable();
        disparar.Disable();
    }

    private void Start()
    {
        puntaCanon = transform.Find("PuntaCanon").gameObject;
        SonidoDisparo = GameObject.Find("SonidoDisparo");
        SourceDisparo = SonidoDisparo.GetComponent<AudioSource>();

        if (sliderFuerza != null)
        {
            sliderFuerza.minValue = fuerzaMin;
            sliderFuerza.maxValue = fuerzaMax;
            fuerzaActual = sliderFuerza.value;
        }
    }

    private void Update()
    {
        rotacion += apuntar.ReadValue<float>() * AdministradorJuego.SingletonAdministradorJuego.VelocidadRotacion;
        rotacion = Mathf.Clamp(rotacion, 0, 90); // Limitar la rotación entre 0 y 90 grados
        transform.eulerAngles = new Vector3(rotacion, 90, 0.0f);

        float inputFuerza = modificarFuerza.ReadValue<float>();
        if (sliderFuerza != null)
        {
            sliderFuerza.value = Mathf.Clamp(sliderFuerza.value + inputFuerza * Time.deltaTime * 10f, fuerzaMin, fuerzaMax);
            fuerzaActual = sliderFuerza.value;
        }

        if (AdministradorJuego.SingletonAdministradorJuego != null)
        {
            textoDisparos.text = "Disparos: " + AdministradorJuego.SingletonAdministradorJuego.DisparosPorJuego;
        }
    }

    /**
    * La funcion se ejecuta cuando el jugador presiona el botón de disparo. /n
    * Instancia la bala, aplica la fuerza, genera partículas y reproduce el sonido. /n
    * También resta un disparo al contador. /n
    * 
    * _Parametros_ 
    *
    * __context__ : Contexto del input recibido. /n
    */ 
    /// <param name="context">Contexto del input recibido.</param>
    private void Disparar(InputAction.CallbackContext context)
    {
        if (Bloqueado) return;
        if (AdministradorJuego.SingletonAdministradorJuego.DisparosPorJuego <= 0) return;

        GameObject temp = Instantiate(BalaPrefab, puntaCanon.transform.position, transform.rotation);
        Rigidbody tempRB = temp.GetComponent<Rigidbody>();
        SeguirCamara.objetivo = temp;

        Vector3 direccionDisparo = transform.rotation.eulerAngles;
        direccionDisparo.y = 90 - direccionDisparo.x;
        Vector3 direccionParticulas = new Vector3(-90 + direccionDisparo.x, 90, 0);
        GameObject Particulas = Instantiate(ParticulasDisparo, puntaCanon.transform.position, Quaternion.Euler(direccionParticulas), transform);

        tempRB.velocity = direccionDisparo.normalized * fuerzaActual;
        AdministradorJuego.SingletonAdministradorJuego.DisparosPorJuego--;
        Debug.Log("El numero de disparos restantes es: " + AdministradorJuego.SingletonAdministradorJuego.DisparosPorJuego);

        SourceDisparo.Play();
        Bloqueado = true;
    }
}