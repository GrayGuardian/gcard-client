
using System.Net.Mime;
using System;
using System.Text;
using System.Reflection;
using UnityEditor;
using UnityEditor.Callbacks;
using System.Text.RegularExpressions;
using UnityEngine;

/// <summary>
/// Lua控制台日志重定向
/// </summary>
public class LuaConsoleRedirect
{
    private static int s_InstanceID = AssetDatabase.LoadAssetAtPath<MonoScript>("Assets/ThirdParty/XLua/Gen/UnityEngine_DebugWrap.cs").GetInstanceID(); // Lua文件打印起始堆栈
    private static Type consoleWindowType
    {
        get
        {
            if (_consoleWindowType == null)
            {
                _consoleWindowType = typeof(EditorWindow).Assembly.GetType("UnityEditor.ConsoleWindow");
            }
            return _consoleWindowType;
        }
    }
    private static Type _consoleWindowType;
    private static object consoleWindowInstance
    {
        get
        {
            if (_consoleWindowInstance == null)
            {
                var fieldInfo = consoleWindowType.GetField("ms_ConsoleWindow", BindingFlags.Static | BindingFlags.NonPublic);
                _consoleWindowInstance = fieldInfo.GetValue(null);
            }
            return _consoleWindowInstance;
        }
    }
    private static object _consoleWindowInstance;

    private static string consoleText
    {
        get
        {
            var fieldInfo = consoleWindowType.GetField("m_ActiveText", BindingFlags.Instance | BindingFlags.NonPublic);
            return fieldInfo.GetValue(consoleWindowInstance).ToString();
        }
    }

    [OnOpenAssetAttribute(0)]
    public static bool OnOpenAsset(int instanceID, int line)
    {
        if (!EditorWindow.focusedWindow.titleContent.text.Equals("Console")) // 只对控制台的开启进行重定向
            return false;
        if (instanceID != s_InstanceID)   // 只对Lua打印的日志重定向 注意lua层是通过CS.UnityEngine.Debug打印
            return false;
        // 获取控制台信息
        string text = consoleText;
        // 匹配Lua文件信息
        Regex reg = new Regex(@"<color=#BE81F7FF>\[(\S+):(\d+)\]</color>");   //根据日志打印规则改动
        Match match = reg.Match(text);
        if (match.Groups.Count != 3)
        {
            return false;
        }
        var luaFileName = match.Groups[1].Value;
        var luaFileLine = int.Parse(match.Groups[2].Value);
        var luaFileInfo = FileUtil.Instance.GetChildFile(GameConst.LUA_FILE_ROOT, luaFileName);
        if (luaFileInfo == null)
        {
            return false;
        }
        var assetPath = luaFileInfo.FullName.Replace("\\", "/").Replace(Application.dataPath, "Assets");

        AssetDatabase.OpenAsset(AssetDatabase.LoadAssetAtPath<UnityEngine.Object>(assetPath), luaFileLine);
        return true;
    }
}
