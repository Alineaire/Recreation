using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[System.Serializable]
public class RecreationScene
{
    public string sceneToLoad;
    public string title;
    public GameObject visualizerPrefab;
    public AudioSource clipToPlayOnSelected;
}

public class SelectSceneManager : MonoBehaviour {

    public Text titleText;
    public RecreationScene[] playbleScenes;
    public GameObject globalCommandManagerPrefab;
    int nextScene = 0;

    GlobalCommandManager globalCommandManager;

    private void Awake()
    {
        // singleton
        if(globalCommandManager == null)
        {
            GlobalCommandManager tempG = GameObject.FindObjectOfType<GlobalCommandManager>();

            if (tempG == null)
            {
                GameObject g = Instantiate(globalCommandManagerPrefab) as GameObject;
                globalCommandManager = g.GetComponent<GlobalCommandManager>();
            }
        }
    }

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
