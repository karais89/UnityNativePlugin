namespace KARA
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using System.Runtime.InteropServices;

    public class UnityNativePluginIOS : INativePlugin
    {
        [DllImport("__Internal")]
        private static extern void kara_UnityNativePlugin_init(string unityGameObjectName);
        
        [DllImport("__Internal")]
        private static extern bool kara_UnityNativePlugin_clipBoardCopy(string msg);
        
        public void Init(string unityObjName)
        {
            kara_UnityNativePlugin_init(unityObjName);
        }

        public bool ClipBoardCopy(string msg)
        {
            return kara_UnityNativePlugin_clipBoardCopy(msg);
        }
    }
}