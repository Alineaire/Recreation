using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "Data", menuName = "Inventory/List", order = 1)]
public class ScriptableSceneObject : ScriptableObject  {

    public string sceneToLoad;
    public string title;
    public GameObject visualizerPrefab;
    public AudioSource clipToPlayOnSelected;
}
