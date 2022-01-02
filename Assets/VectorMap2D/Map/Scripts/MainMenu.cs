
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject audio;
    // play level - the mode is PlayMode
    public void PlayGame()
    {
        string level = "StartGame";
        SceneManager.LoadScene(level);
    }
    public void QuitGame()
    {
        Debug.Log("Quitting the game");
        Application.Quit();
    }

    // tutorial level - the mode is TutorialMode
    public void PlayTutorial()
    {
        string tut = "Tutorial";
        SceneManager.LoadScene(tut);
    }

    // the home page with the explainations 
    public void TutorialHomePage()
    {
        string tut = "TutorialHomePage";
        SceneManager.LoadScene(tut);
    }

    public void setMusic()
    {
        if (audio.GetComponent<AudioSource>().isPlaying)
            audio.GetComponent<AudioSource>().Pause();
        else
            audio.GetComponent<AudioSource>().Play();
    }

    public void Menu()
    {
        string menu = "HomePage";
        SceneManager.LoadScene(menu);
    }
}
