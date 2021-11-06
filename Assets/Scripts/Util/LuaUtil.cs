using System.Collections;
using System;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LuaUtil : Singleton<LuaUtil>
{
    public string BytesToString(byte[] bytes)
    {
        return System.Text.Encoding.UTF8.GetString(bytes);
    }
    public bool IsNull(UnityEngine.Object obj)
    {
        return obj == null;
    }
    public void LoadScene(string name, Action<AsyncOperation> cb = null, Action<AsyncOperation, float> loadingFunc = null, bool allowSceneActivation = true, LoadSceneMode mode = LoadSceneMode.Single)
    {
        MonoUtil.Instance.StartCoroutine(_loadScene(name, cb, loadingFunc, allowSceneActivation, mode));
    }
    IEnumerator _loadScene(string name, Action<AsyncOperation> cb = null, Action<AsyncOperation, float> loadingFunc = null, bool allowSceneActivation = true, LoadSceneMode mode = LoadSceneMode.Single)
    {
        yield return null;
        var ao = SceneManager.LoadSceneAsync(name, mode);
        ao.allowSceneActivation = false;
        while (!ao.isDone)
        {
            float progress = Mathf.Clamp01(ao.progress / 0.9f);

            if (loadingFunc != null) loadingFunc(ao, progress);

            if (Mathf.Approximately(progress, 1f))
            {
                ao.allowSceneActivation = allowSceneActivation;
                if (cb != null) cb(ao);
                break;
            }

            yield return null;
        }

    }
    /// <summary>
    /// 获取屏幕分辨率
    /// </summary>
    /// <param name="width"></param>
    /// <param name="height"></param>
    public void GetScreen(out int width, out int height)
    {
#if UNITY_EDITOR
        // Editor GameView分辨率
        System.Type T = System.Type.GetType("UnityEditor.GameView,UnityEditor");
        System.Reflection.MethodInfo GetMainGameView = T.GetMethod("GetMainGameView", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);
        System.Object Res = GetMainGameView.Invoke(null, null);
        var gameView = (UnityEditor.EditorWindow)Res;
        var prop = gameView.GetType().GetProperty("currentGameViewSize", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        var gvsize = prop.GetValue(gameView, new object[0] { });
        var gvSizeType = gvsize.GetType();

        width = (int)gvSizeType.GetProperty("width", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance).GetValue(gvsize, new object[0] { });
        height = (int)gvSizeType.GetProperty("height", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance).GetValue(gvsize, new object[0] { });
#else
        // 设备分辨率
        var Screen = UnityEngine.Screen.currentResolution;
        width = Screen.width;
        height = Screen.height;
#endif

    }

}