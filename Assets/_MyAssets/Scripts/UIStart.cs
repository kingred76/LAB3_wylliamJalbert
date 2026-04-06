using UnityEngine;
using UnityEngine.SceneManagement;

public class UIStart : MonoBehaviour
{
    [SerializeField] GameObject _startPanel;
    [SerializeField] GameObject _InstructionsPanel;

    public void OnStartClick()
    {
        SceneManager.LoadScene(1);

    }

    public void OnInstructionsClick()
    {

        _startPanel.SetActive(false);
        _InstructionsPanel.SetActive(true);
    }

    public void OnClosesClick()
    {

        _startPanel.SetActive(true);
        _InstructionsPanel.SetActive(false);
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
