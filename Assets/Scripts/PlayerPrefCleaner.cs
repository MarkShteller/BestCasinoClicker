using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

public class PlayerPrefCleaner : MonoBehaviour {

#if UNITY_EDITOR
    [MenuItem("Player Prefs/Clear PlayerPrefs")]
    static void DeleteMyPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
    }
#endif
}
