using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class UI : MonoBehaviour
{
    public bool clicked;
    public FadeController fader;

    // Start is called before the first frame update
    void Start()
    {
        fader.FadeOut(1.0f);
    }
    // Update is called once per frame

    public void OnStartButton(string scene)
    {
        clicked = true;
        Time.timeScale = 1.0f;
        fader.FadeIn(1.0f, () =>
        {
            SceneManager.LoadScene(scene);
        });
    }
}
