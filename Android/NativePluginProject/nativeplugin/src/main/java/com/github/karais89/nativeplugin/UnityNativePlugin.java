package com.github.karais89.nativeplugin;

import android.util.Log;
import com.unity3d.player.UnityPlayer;

/**
 * Created by jh on 2018. 1. 7..
 */

public class UnityNativePlugin {

    // 싱글톤 인스턴스를 초기화하고 가져온다.
    private static UnityNativePlugin instance = null;
    public static synchronized UnityNativePlugin getInstance() {
        if (instance == null) {
            instance = new UnityNativePlugin();
        }
        return instance;
    }

    // Unity에 보낼 오브젝트 이름 저장
    private String unityGameObjName;
    private boolean isDebug = false;
    private String debugTag = "Unity";

    // 네이티브 플러그인 사용시 반드시 처음에 먼저 호출해 줘야 된다.
    public void init(String unityGameObjectName) {
        this.unityGameObjName = unityGameObjectName;
        LogMessage("void init(String unityGameObjectName) : " + unityGameObjectName);
    }

    // 디버그 설정시 모든 api 호출시 호출 메서드가 출력 된다.
    public void setIsDebug(boolean isDebug) {
        this.isDebug = isDebug;
        LogMessage("void setIsDebug(boolean isDebug) : " + isDebug);
    }

    public void setDebugTag(String debugTag) {
        this.debugTag = debugTag;
        LogMessage("void setDebugTag(String debugTag) : " + debugTag);
    }

    private void LogMessage(String msg)
    {
        if (!isDebug) {
            return;
        }

        Log.d(debugTag, msg);
    }

    // unity <-> android 양방향 통신 테스트 코드
    public void testAndroidFunc(String strFromUnity) {
        sendMessage("SetAndroidLog", strFromUnity + "Hello World!");
    }

    // android unity 리턴 값 테스트 코드
    public int testJavaFuncReturnInt() {
        return 777;
    }

    // android unity 인자 및 리턴 테스트 코드
    public String testJavaFuncReturnStr(int number) {
        return "HelloWorld : " + number;
    }

    // sendMessage 관련 함수
    private void sendMessage(String methodName, String message) {
        if (methodName == "") {
            return;
        }

        // if null our app is not running
        if (UnityPlayer.currentActivity == null) {
            return;
        }

        UnityPlayer.UnitySendMessage(unityGameObjName, methodName, message);
    }
}
