using UnityEngine;
using UnityEngine.SceneManagement;

public class BackButton : _ButtonEventBase {
    public override void ButtonEvent()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }    
}