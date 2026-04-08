using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinPartie : MonoBehaviour
{
    // ***** Attributs *****
    private bool _finPartie = false;  // bool�en qui d�termine si la partie est termin�e
    private Player _player;  // attribut qui contient un objet de type Player

    // ***** M�thode priv�es  *****
    
    private void Start()
    {
        _player = FindAnyObjectByType<Player>();  // r�cup�re sur la sc�ne le gameObject de type Player
    }

    /*
     * M�thode qui se produit quand il y a collision avec le gameObject de fin
     */
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && !_finPartie)  // Si la collision est produite avec le joueur et la partie n'est pas termin�e
        {
            _finPartie = true; // met le bool�en � vrai pour indiquer la fin de la partie
            int noScene = SceneManager.GetActiveScene().buildIndex; // R�cup�re l'index de la sc�ne en cours

            float tempsNiveau = Time.time - _player.GetTempsDepart();
            GestionJeu.Instance.TempsCumule += tempsNiveau;


            GestionJeu.Instance.PointageNiveau = GestionJeu.Instance.Pointage;
            GestionJeu.Instance.SetNiveau(Time.time - _player.GetTempsDepart());
            if (noScene == SceneManager.sceneCountInBuildSettings -2)
            {
                GestionJeu.Instance.EndTime = Time.time - GestionJeu.Instance.StartTime;
            }
           
            SceneManager.LoadScene(noScene + 1);

        }
    }
}
