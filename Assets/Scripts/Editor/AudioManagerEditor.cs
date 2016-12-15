using UnityEngine;
using System.Collections;
using UnityEditor;

public class AudioManagerEditor : MonoBehaviour {

    [CustomEditor(typeof(AudioManager))]
    public class AudioManagerInspector : Editor
    {

        public override void OnInspectorGUI()
        {
            AudioManager Target = (AudioManager)target;
            // Insert additional settings here

            DrawDefaultInspector();
        }
    }


}
