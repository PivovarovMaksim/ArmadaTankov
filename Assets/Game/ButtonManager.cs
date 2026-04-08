using UnityEngine;
using UnityEditor;

public class ButtonManager : MonoBehaviour
{
    private SaveSystem saveSystem;

    void Start()
    {
        saveSystem = GameObject.Find("SaveManager").GetComponent<SaveSystem>();
    }

    public void Exit()
    {
        saveSystem.SaveCoins();
        
        Application.Quit();

        #if UNITY_EDITOR

        UnityEditor.EditorApplication.isPlaying = false;
        
        #endif
    }
}
