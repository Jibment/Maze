using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlQuit : MonoBehaviour
{
    // Start is called before the first frame update
    public void EndGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
