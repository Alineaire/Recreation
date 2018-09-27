using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SelectSceneManager : MonoBehaviour {

    public Text titleText;
    public ScriptableSceneObject[] playbleScenes;
    int nextScene = 0;

    private void Start()
    {
        if(playbleScenes.Length == 0)
        {
            Debug.Log("No Game Scene in List");
            enabled = false;
            return;
        }

        InitUI();
    }

    void InitUI()
    {
        titleText.text = playbleScenes[nextScene].title;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            ChangeSceneSelection(-1);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            ChangeSceneSelection(+1);
        }

        if(Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(playbleScenes[nextScene].sceneToLoad);
        }
    }

    void ChangeSceneSelection(int _offset)
    {
        nextScene += _offset;

        if (nextScene < 0)
            nextScene = playbleScenes.Length-1;

        if (nextScene > playbleScenes.Length-1)
            nextScene = 0;

        InitUI();
    }
}
