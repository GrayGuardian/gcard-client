
using System.IO;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class UIEditor
{
    /// <summary>
    /// UI 代码所在目录
    /// </summary>
    static string UI_SCRIPT_DIR = Path.Combine(GameConst.LUA_FILE_ROOT, "./ui");
    /// <summary>
    /// UI AB包名
    /// </summary>
    static string UI_ASSETBUNDLE_NAME = "ui";
    /// <summary>
    /// UIControl Tag
    /// </summary>
    static string UICONTROL_TAG = "UIControl";

    [MenuItem("Assets/Generate UI Script", true)]
    static bool ValidateGenerateUIScript()
    {
        return GetSelectUIPrefabs().Length > 0;
    }
    [MenuItem("Assets/Generate UI Script")]
    static void GenerateUIScript()
    {
        foreach (var uiPrefab in GetSelectUIPrefabs())
        {
            Dictionary<Transform, string> dic = new Dictionary<Transform, string>();
            TryGetChildNodeInfo(uiPrefab.transform, ref dic);
            List<string> uiControlCodeList = new List<string>();
            List<string> uiControlNameList = new List<string>();
            foreach (var item in dic)
            {
                if (item.Key.CompareTag(UICONTROL_TAG))
                {
                    if (uiControlNameList.IndexOf(item.Key.name) != -1)
                    {
                        UnityEngine.Debug.LogWarningFormat("[{0}]存在重复UIControl[{1}] >> {0}/{2}", uiPrefab.name, item.Key.name, item.Value);
                    }
                    uiControlNameList.Add(item.Key.name);
                    uiControlCodeList.Add(string.Format("\t{0}", CodeTemplate.GenerateCode("code_template_uiControl", item.Key.name, item.Value)));
                }
            }
            string uiControlCodeText = string.Join("\n", uiControlCodeList);

            string filePath = new FileInfo(string.Format("{0}/{1}.lua.txt", UI_SCRIPT_DIR, uiPrefab.name)).FullName;
            string codeContent = "";
            string defaultContent = CodeTemplate.GenerateCode("code_template_ui_default", uiPrefab.name.ToUpper());
            if (File.Exists(filePath))
            {
                codeContent = File.ReadAllText(filePath);
            }
            codeContent = CodeTemplate.GenerateEditorCode(codeContent, "code_template_ui", new string[] { defaultContent }, uiPrefab.name.ToUpper(), uiPrefab.name, uiControlCodeText);
            File.WriteAllText(filePath, codeContent);
            Debug.LogFormat("[{0}]UI代码生成成功 >>> {1}", uiPrefab.name, filePath);
        }
    }

    static GameObject[] GetSelectUIPrefabs()
    {
        List<GameObject> goList = new List<GameObject>();
        string[] guids = Selection.assetGUIDs;
        for (int i = 0; i < guids.Length; i++)
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(guids[i]);//通过GUID获取路径
            GameObject uiPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(assetPath);
            if (uiPrefab != null && AssetDatabase.GetImplicitAssetBundleName(assetPath) == UI_ASSETBUNDLE_NAME)
            {
                goList.Add(uiPrefab);
            }
        }
        return goList.ToArray();
    }
    /// <summary>
    /// 遍历获得所有子物体信息
    /// </summary>
    static void TryGetChildNodeInfo(Transform node, ref Dictionary<Transform, string> infoDic, string root = "")
    {
        for (int i = 0; i < node.childCount; i++)
        {
            var child = node.GetChild(i);
            var path = root + (root == "" ? "" : "/") + child.name;
            infoDic.Add(child, path);
            if (child.childCount > 0)
            {
                TryGetChildNodeInfo(child, ref infoDic, path);
            }
        }

    }
}
