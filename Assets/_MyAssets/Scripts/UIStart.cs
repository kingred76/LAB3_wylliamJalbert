using UnityEngine;
using UnityEngine.SceneManagement;

public class UIStart : MonoBehaviour
{
     public void OnStartClick()
    {
        SceneManager.LoadScene(1);

    }

    public void OnInstructionsClick()
    {

        //faire apparaitre les instructions
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
