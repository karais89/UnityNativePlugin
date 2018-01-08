package com.github.karais89.nativeplugin;

import android.app.Activity;
import android.content.Context;
import android.content.Intent;
import android.os.Build;
import android.util.Log;
import android.widget.Toast;

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

    // NativeHelper Method get unityCurrentActivity4
    public static Context getCurrentContext() {
        Context context = UnityPlayer.currentActivity;
        return context;
    }

    // runonUIThread - Thread Safe
    public static void startActivityOnUiThread(final Intent intent) {
        Runnable runnable = new Runnable() {
            @Override
            public void run() {
                getCurrentContext().startActivity(intent);
            }
        };
        executeOnUIThread(runnable);
    }

    // Helper for running a runnable on UI thread
    public static void executeOnUIThread(Runnable runnableThread) {
        // Get current active activity
        Activity currentActivity = (Activity) getCurrentContext();
        if (currentActivity != null) {
            currentActivity.runOnUiThread(runnableThread);
        }
    }

    // 안드로이드 클립 보드 복사
    // https://stackoverflow.com/questions/14189544/copy-with-clipboard-manager-that-supports-old-and-new-android-versions
    public boolean clipBoardCopy(String msg) {
        if (Build.VERSION.SDK_INT >= Build.VERSION_CODES.HONEYCOMB) {
            final android.content.ClipboardManager clipboardManager = (android.content.ClipboardManager) getCurrentContext()
                    .getSystemService(Context.CLIPBOARD_SERVICE);
            final android.content.ClipData clipData = android.content.ClipData
                    .newPlainText("text label", msg);
            clipboardManager.setPrimaryClip(clipData);
            return true;
        } else {
            final android.text.ClipboardManager clipboardManager = (android.text.ClipboardManager) getCurrentContext()
                    .getSystemService(Context.CLIPBOARD_SERVICE);
            clipboardManager.setText(msg);
            return true;
        }
    }

    // 안드로이드 Toast Message
    public void Toast(String msg) {
        Toast(msg, 0);
    }

    public void Toast(String msg, int type) {
        if (type == 0) {
            Toast.makeText(getCurrentContext(), msg, Toast.LENGTH_SHORT).show();
        } else {
            Toast.makeText(getCurrentContext(), msg, Toast.LENGTH_LONG).show();
        }
    }
}
