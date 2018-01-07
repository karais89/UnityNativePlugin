using UnityEngine;
using System.Collections;
 
public class PluginTest : MonoBehaviour {
 
    string messge = "init";
    AndroidJavaClass myCls;
    AndroidJavaObject myObj;
 
    //// Use this for initialization
    void Start () {
        myCls = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        myObj = myCls.GetStatic<AndroidJavaObject>("currentActivity");       
        Debug.Log("myCls : " + myCls != null ? myCls.ToString() : "");
        Debug.Log("MyObj : " + myObj != null ? myObj.ToString() : "");
    }
    
    void OnGUI()
    {
        if (GUI.Button(new Rect(100, 100, 100, 50), "StaticInt"))
        {  
            messge += ("\n" + myObj.CallStatic<int>("GetStaticInt", 123).ToString());
        }
 
        if (GUI.Button(new Rect(100, 200, 100, 50), "StaticString"))
        {
            messge += ("\n" + myObj.CallStatic<string>("GetStaticString", "StaticString"));
        }
 
        if (GUI.Button(new Rect(100, 300, 100, 50), "Int"))
        {
            messge += ("\n" + myObj.Call<int>("GetInt", 456).ToString());
        }
 
        if (GUI.Button(new Rect(100, 400, 100, 50), "String"))
        {
            messge += ("\n" + myObj.Call<string>("GetString", "String"));
        }
       
        GUI.Label(new Rect(Screen.width / 2 - 350, Screen.height / 2 - 150, 700, 300), messge);
    }
}