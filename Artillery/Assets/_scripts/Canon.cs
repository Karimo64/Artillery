using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Canon : MonoBehaviour
{
    public static bool Bloqueado;

    public AudioClip clipDisparo;
    private GameObject SonidoDisparo;
    private AudioSource SourceDisparo;

    [SerializeField] private GameObject BalaPrefab;
     public GameObject ParticulasDisparo;
    private GameObject puntaCanon;
    private float rotacion;
    //int numDisparos = AdministradorJuego.DisparosPorJuego;

    public CanonControls canonControls;
    private InputAction apuntar;
    private InputAction modificarFuerza;
    private InputAction disparar;

    public Slider sliderFuerza;
    public TextMeshProUGUI textoDisparos;
    public float fuerzaMin = 10f;
    public float fuerzaMax = 50f;
    private float fuerzaActual;

    private void Awake()
    {
        canonControls = new CanonControls();
    }

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
    void Update()
    {
        rotacion += apuntar.ReadValue<float>() * AdministradorJuego.SingletonAdministradorJuego.VelocidadRotacion;
        rotacion = Mathf.Clamp(rotacion, 0, 90); // Limitar la rotación entre 0 y 90 grados
        // rotacion += Input.GetAxis("Horizontal") * AdministradorJuego.SingletonAdministradorJuego.VelocidadRotacion;
        // if (rotacion <= 90 && rotacion >= 0)
        // {
        //     transform.eulerAngles = new Vector3(rotacion, 90, 0.0f);
        // }

        // if (rotacion > 90) rotacion = 90;
        // if (rotacion < 0) rotacion = 0;
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

    private void Disparar(InputAction.CallbackContext context)
    {
        if (Bloqueado) return; // Si el disparo está bloqueado, no hacemos nada
        if (AdministradorJuego.SingletonAdministradorJuego.DisparosPorJuego <= 0) return; // Si no hay disparos restantes, no hacemos nada

        GameObject temp = Instantiate(BalaPrefab, puntaCanon.transform.position, transform.rotation);

        Rigidbody tempRB = temp.GetComponent<Rigidbody>();
        SeguirCamara.objetivo = temp;
        Vector3 direccionDisparo = transform.rotation.eulerAngles;
        direccionDisparo.y = 90 - direccionDisparo.x;
        Vector3 direccionParticulas = new Vector3(-90 + direccionDisparo.x, 90, 0);
        GameObject Particulas = Instantiate(ParticulasDisparo, puntaCanon.transform.position, Quaternion.Euler(direccionParticulas), transform);
        tempRB.velocity = direccionDisparo.normalized * fuerzaActual; // Actualizamos para que tome valor del slider
        // tempRB.velocity = direccionDisparo.normalized * AdministradorJuego.SingletonAdministradorJuego.VelocidadBala;
        // AdministradorJuego.DisparosPorJuego--;
        AdministradorJuego.SingletonAdministradorJuego.DisparosPorJuego--;
        // Debug.Log("El numero de disoparos es: " + AdministradorJuego.DisparosPorJuego);
        Debug.Log("El numero de disparos restantes es: " + AdministradorJuego.SingletonAdministradorJuego.DisparosPorJuego); // Deja saber el numero de disparos restantes
                                                                                                                    //SourceDisparo.PlayOneShot(clipDisparo); //Este sonido solo se repoducira directamente en el objeto
        SourceDisparo.Play(); //Este sonido se reproducira en el objeto y en el prefab
        Bloqueado = true; //Desbloquea el disparo
    }
}