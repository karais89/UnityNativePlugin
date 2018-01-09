namespace KARA
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class UnityNativePlugin : MonoBehaviour
    {
        private static INativePlugin _instance;
        public static INativePlugin Instance
        {
            get
            {
                if (_instance == null)
                {
#if UNITY_EDITOR
                    _instance = new UnityNativePluginEditor();
#elif UNITY_ANDROID
                    _instance = new UnityNativePluginAndroid();
#elif UNITY_IOS
                    _instance = new UnityNativePluginIOS();
#endif
                    GameObject go = null;
                    UnityNativePlugin[] objs = FindObjectsOfType<UnityNativePlugin>();
                    if (objs.Length > 0)
                    {
                        go = objs[0].gameObject;
                        // 나머지 오브젝트는 전부 제거한다.
                        for (int i = 1; i < objs.Length; i++)
                        {
                            Destroy(objs[i].gameObject);
                        }
                    }

                    if (go == null)
                    { 
                        // 유니티 오브젝트 생성 및 init 함수 실행
                        string goName = typeof(UnityNativePlugin).ToString();
                        go = GameObject.Find(goName);
                        if (go == null) 
                            go = new GameObject(goName);
                    
                        go.AddComponent<UnityNativePlugin>();
                        _instance.Init(go.name);
                        DontDestroyOnLoad(go);
                    }
                }
                return _instance;
            }
        }
    }
}
