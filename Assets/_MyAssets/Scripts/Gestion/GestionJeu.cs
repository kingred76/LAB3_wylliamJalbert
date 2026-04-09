using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GestionJeu : MonoBehaviour
{
    public static GestionJeu Instance;

    // ***** Attributs *****
    public float TempsCumule { get; set; } = 0f;

    private int _pointage = 0;
    public int Pointage => _pointage;

    private List<int> _listeAccrochages = new List<int>();
    public List<int> ListeAccrochages => _listeAccrochages;

    private List<float> _listeTemps = new List<float>();
    public List<float> ListeTemps => _listeTemps;

    private float _startTime;
    public float StartTime => _startTime;

    private float _endTime;
    public float EndTime { get => _endTime; set => _endTime = value; }

    private bool _isPaused = false;

    private int _pointageNiveau;
    public int PointageNiveau { get => _pointageNiveau; set => _pointageNiveau = value; }

    private float _startTimeNiveau;
    public float StartTimeNiveau { get => _startTimeNiveau; set => _startTimeNiveau = value; }

    // ***** Awake *****
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            // Subscriptions globales
            GestionCollision.OnCollisionOccured += GestionCollision_OnCollisionOccured;
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    // ***** Start *****
    private void Start()
    {
        Time.timeScale = 1.0f;
        _pointage = 0;
        _startTime = Time.time;
    }

    // ***** Quand une nouvelle scène est chargée *****
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 🔁 IMPORTANT : rebind au Player de la nouvelle scène

        Player.OnPlayerPaused -= Player_OnPlayerPaused; // évite double subscription
        Player.OnPlayerPaused += Player_OnPlayerPaused;
    }

    // ***** Cleanup *****
    private void OnDestroy()
    {
        GestionCollision.OnCollisionOccured -= GestionCollision_OnCollisionOccured;
        Player.OnPlayerPaused -= Player_OnPlayerPaused;
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // ***** Gestion Pause *****
    private void Player_OnPlayerPaused(object sender, System.EventArgs e)
    {
        if (_isPaused)
        {
            Time.timeScale = 1.0f;
            _isPaused = false;
        }
        else
        {
            Time.timeScale = 0.0f;
            _isPaused = true;
        }
    }

    // ***** Gestion collisions *****
    private void GestionCollision_OnCollisionOccured(object sender, GestionCollision.OnCollisionOccuredEventArgs e)
    {
        _pointage += e.CollisionValue;
    }

    // ***** Gestion des niveaux *****
    public void SetNiveau(float temps)
    {
        if (_listeAccrochages.Count == 0)
        {
            _listeAccrochages.Add(_pointage);
        }
        else
        {
            _listeAccrochages.Add(_pointage - _listeAccrochages.Sum());
        }

        _listeTemps.Add(temps);
    }
}