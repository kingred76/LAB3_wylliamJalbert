using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class UIGame : MonoBehaviour
{
    public static UIGame Instance;

    [SerializeField] private TextMeshProUGUI _TxtTime;
    [SerializeField] private TextMeshProUGUI _TxtCollisions;
    [SerializeField] private GameObject _pausePanel;
    [SerializeField] private GameObject _button;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Debug.LogError("2 fois");
            Destroy(gameObject);
        }

    }
    private void Start()
    {
        GestionCollision.OnCollisionOccured += GestionCollision_OnCollisionOccured;
        Player.OnPlayerPaused += Player_OnPlayerPaused;
    }

    private void Player_OnPlayerPaused(object sender, System.EventArgs e)
    {
        _pausePanel.SetActive(!_pausePanel.activeSelf);
        EventSystem.current.SetSelectedGameObject(_button.gameObject);
    }

    private void OnDestroy()
    {
        GestionCollision.OnCollisionOccured -= GestionCollision_OnCollisionOccured;
        Player.OnPlayerPaused -= Player_OnPlayerPaused;
    }
    private void Update()
    {
        float ElapsedTime = GestionJeu.Instance.TempsCumule; // temps des niveaux précédents

        Player player = FindAnyObjectByType<Player>();
        if (player != null && player.GetABouger())
        {
            ElapsedTime += Time.time - player.GetTempsDepart(); // temps courant du niveau
        }


        _TxtTime.text = $"Temps: {ElapsedTime:f2}"; 
    }

    private void GestionCollision_OnCollisionOccured(object sender, GestionCollision.OnCollisionOccuredEventArgs e)
    {
        _TxtCollisions.text = $"Collisions : {GestionJeu.Instance.Pointage}";
    }

    public void OnQuitClick()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();//quitter l'exec en cours
#endif
    }

    public void OnRestartClick()
    {
        SceneManager.LoadScene(0);
    }

    public void OnContinueClick()
    {
        Player.TriggerOnPlayerPause(this);
    }
}
