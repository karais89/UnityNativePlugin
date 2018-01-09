namespace KARA
{
    using UnityEngine;
    using System.Collections;

    public class PluginTest : MonoBehaviour
    {
        private static readonly string className = "com.github.karais89.nativeplugin.UnityNativePlugin";
        private AndroidJavaObject _plugin;
        string messge = "init";

        private void Start()
        {
            // create Instance        
            using (AndroidJavaClass anClass = new AndroidJavaClass(className))
            {
                _plugin = anClass.CallStatic<AndroidJavaObject>("getInstance");
                if (_plugin == null)
                {
                    Debug.Log("plugin instance is null");
                    return;
                }
                _plugin.Call("init", this.gameObject.name);
            }
        }

        void OnGUI()
        {
            if (GUI.Button(new Rect(100, 100, 100, 50), "testAndroidFunc"))
            {
                _plugin.Call("testAndroidFunc", "PluginTest");
            }

            if (GUI.Button(new Rect(100, 200, 100, 50), "testJavaFuncReturnInt"))
            {
                messge += ("\n" + _plugin.Call<int>("testJavaFuncReturnInt"));
            }

            if (GUI.Button(new Rect(100, 300, 100, 50), "testJavaFuncReturnStr"))
            {
                messge += ("\n" + _plugin.Call<string>("testJavaFuncReturnStr", 70000));
            }

            if (GUI.Button(new Rect(100, 400, 100, 50), "clipboardCopy"))
            {
                bool success = _plugin.Call<bool>("clipBoardCopy", "클립보드 복사 성공");
                if (success)
                {
                    _plugin.Call("Toast", "클립보드 복사 성공", 1);
                }
            }

            GUI.Label(new Rect(Screen.width / 2 - 350, Screen.height / 2 - 150, 700, 300), messge);
        }

        public void SetAndroidLog(string message)
        {
            messge += ("\n" + message);
        }
    }
}