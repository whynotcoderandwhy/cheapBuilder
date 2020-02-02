using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ToggleCanvasActive : MonoBehaviour
{
    [SerializeField]
    protected bool state = false;
    public void ToggleCanvasActiveFunct()
    {
        state = !state;
        gameObject.SetActive(state);
    }

    public void SetActiveFunct(bool on)
    {
        state = on;
        gameObject.SetActive(state);
    }

    void Start()
    {
        ToggleCanvasActiveFunct();
        ToggleCanvasActiveFunct();
    }



    public void QuitButtonHit()
    {
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #else
         Application.Quit();
    #endif
    }
}
