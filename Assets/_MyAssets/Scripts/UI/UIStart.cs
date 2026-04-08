using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIStart : MonoBehaviour
{
    [SerializeField] GameObject _startPanel;
    [SerializeField] GameObject _InstructionsPanel;

    [SerializeField] Button _StartButton;
    [SerializeField] Button _CloseButton;

    private void Awake()
    {
        GestionJeu gestionJeu = FindAnyObjectByType<GestionJeu>();
        if (gestionJeu != null)
        {
            Destroy(gestionJeu.gameObject);
        }

        UIGame uiGaem = FindAnyObjectByType<UIGame>();
        if (uiGaem != null)
        {
            Destroy(uiGaem.gameObject);
        }
    }
    private void Start()
    {
        EventSystem.current.SetSelectedGameObject(_StartButton.gameObject);
    }
    public void OnStartClick()
    {
        SceneManager.LoadScene(1);

    }

    public void OnInstructionsClick()
    {

        _startPanel.SetActive(false);
        _InstructionsPanel.SetActive(true);
        EventSystem.current.SetSelectedGameObject(_CloseButton.gameObject);
    }

    public void OnClosesClick()
    {

        _startPanel.SetActive(true);
        _InstructionsPanel.SetActive(false);
        EventSystem.current.SetSelectedGameObject(_StartButton.gameObject);
    }

    public void OnQuitClick()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying= false;
#else
        Application.Quit();//quitter l'exec en cours
#endif
    }


}
