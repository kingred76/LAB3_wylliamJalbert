using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GestionJeu : MonoBehaviour
{
    public static GestionJeu Instance;
    
    // ***** Attributs *****
    private int _pointage = 0;  // Attribut qui conserve le nombre d'accrochages
    public int Pointage => _pointage; // Accesseur de l'attribut

    private List<int> _listeAccrochages = new List<int>();
    public List<int> ListeAccrochages => _listeAccrochages;

    private List<float> _listeTemps = new List<float>();
    public List<float> ListeTemps => _listeTemps;

    // ***** M�thodes priv�es *****
    private void Awake()
    {
        // Singleton        
        // V�rifie si un gameObject GestionJeu est d�j� pr�sent sur la sc�ne si oui
        // on d�truit celui qui vient d'�tre ajout�. Sinon on le conserve pour le 
        // sc�ne suivante et associe Instance.
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        GestionCollision.OnCollisionOccured += GestionCollision_OnCollisionOccured;

    }

    private void OnDestroy()
    {
        GestionCollision.OnCollisionOccured -= GestionCollision_OnCollisionOccured;
    }



    private float _startTime; 
    public float StartTime => _startTime;

    private void Start()
    {
        _pointage = 0;
        _startTime = Time.time;
    }

    // ***** M�thodes publiques ******

    /*
     * M�thode publique qui permet d'augmenter le pointage de 1
     */
    

    // M�thode qui re�oit les valeurs pour le niveau et l'ajoute dans les listes respectives
    public void SetNiveau(float temps)
    {
        //Si premier niveau on ajoute directement le nombre de collision
        //Sinon on ajoute les collisions - les collisions des niveaux pr�c�dents
        if (_listeAccrochages.Count == 0)
        {
            _listeAccrochages.Add(_pointage);
        }
        else
        {
            ListeAccrochages.Add(_pointage - _listeAccrochages.Sum());
        }
        _listeTemps.Add(temps);
    }

    private void GestionCollision_OnCollisionOccured(object sender, GestionCollision.OnCollisionOccuredEventArgs e)
    {
        _pointage += e.CollisionValue;
    }
}
