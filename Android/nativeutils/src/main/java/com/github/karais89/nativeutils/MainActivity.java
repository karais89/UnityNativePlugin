package com.github.karais89.nativeutils;

/**
 * Created by jh on 2018. 1. 7..
 */
import com.unity3d.player.UnityPlayerActivity;

public class MainActivity extends UnityPlayerActivity {

    // Call Static function
    public static int GetStaticInt(int a) {
        return a + a;
    }

    public static String GetStaticString(String str) {
        return "Static GetString() : " + str;
    }

    public int GetInt(int a) {
        return a + a;
    }

    public String GetString(String str) {
        return "GetString() : " + str;
    }
}