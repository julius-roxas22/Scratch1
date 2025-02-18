using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace DumbAssStudio
{
    [CustomEditor(typeof(PlayerController))]
    public class PlayerControllerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            //base.OnInspectorGUI(); same thing above the code

            PlayerController playerController = (PlayerController)target;

            if (GUILayout.Button("Setup Ragdoll parts"))
            {
                playerController.setUpRagdoll();
            }
        }
    }
}

