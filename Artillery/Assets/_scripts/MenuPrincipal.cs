using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour
{

    public void Jugar()
    {
        SceneManager.LoadScene("Level1");
    }

    public void Instrucciones()
    {
        SceneManager.LoadScene("Instrucciones");
    }

    public void Menu()
    {
        SceneManager.LoadScene("MenuPrincipal");
    }

    public void Salir()
    {
        Application.Quit();
    }
    
    
}
