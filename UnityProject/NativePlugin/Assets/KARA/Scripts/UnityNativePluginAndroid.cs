namespace KARA
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class UnityNativePluginAndroid : INativePlugin
    {
        private static readonly string className = "com.github.karais89.nativeplugin.UnityNativePlugin";
        private AndroidJavaObject _plugin;

        public UnityNativePluginAndroid()
        {
            // create Instance        
            using (AndroidJavaClass anClass = new AndroidJavaClass(className))
            {
                _plugin = anClass.CallStatic<AndroidJavaObject>("getInstance");
                if (_plugin == null)
                {
                    return;
                }
            }
        }

        public void Init(string unityObjName)
        {
            _plugin.Call("init", unityObjName);
        }

        public bool ClipBoardCopy(string msg)
        {
            return _plugin.Call<bool>("clipBoardCopy", msg);
        }
    }
}