using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIEnd : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _TxtTotalTime;
    [SerializeField] private TextMeshProUGUI _TxtTotalCollisions;
    [SerializeField] private TextMeshProUGUI _TxtFinalTime;
    [SerializeField] private Button _restartButton;

    private void Awake()
    {
        UIGame uiGaem = FindAnyObjectByType<UIGame>();
        if(uiGaem != null)
        {
            Destroy(uiGaem.gameObject);
        }
    }

    private void Start()
    {
        _TxtTotalCollisions.text = $"Collisions: {GestionJeu.Instance.Pointage}";
        _TxtTotalTime.text = $"Temps Total: {GestionJeu.Instance.EndTime:f2}";
        float final = GestionJeu.Instance.Pointage + GestionJeu.Instance.EndTime;
        _TxtFinalTime.text = $"Temps Final: {final:f2}";

        EventSystem.current.SetSelectedGameObject(_restartButton.gameObject);

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
}
