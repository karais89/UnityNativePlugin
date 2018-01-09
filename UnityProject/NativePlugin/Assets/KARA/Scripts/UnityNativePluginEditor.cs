namespace KARA
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class UnityNativePluginEditor : INativePlugin
    {
        public void Init(string unityObjName)
        {
            // empty
        }

        public bool ClipBoardCopy(string msg)
        {
            GUIUtility.systemCopyBuffer = msg;
            return true;
        }
    }
}