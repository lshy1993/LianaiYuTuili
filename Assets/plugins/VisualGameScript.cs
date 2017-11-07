using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace GalTool
{
    [CustomEditor(typeof(XZGalScript))]
    public class VisualGameScript : Editor
    {

        public override void OnInspectorGUI()
        {

        }

        public void OnSceneGUI()
        {

        }
    }

    public class XZGalScript : MonoBehaviour { }
}
