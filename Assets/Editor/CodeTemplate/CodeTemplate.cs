using System.Collections.Generic;
using System.Net.Mime;
using System.IO;
using System;
using UnityEngine;
using System.Text.RegularExpressions;

public class CodeTemplate
{
    /// <summary>
    /// 代码模板所在目录
    /// </summary>
    static string CODE_TEMPLATE_DIR = Path.Combine(Application.dataPath, "./Editor", "./CodeTemplate");
    /// <summary>
    /// 可编辑代码模板内容
    /// </summary>
    static string CODE_TEMPLATE_EDITOR = GetCodeTemplateText("code_template_editor");
    static string GetCodeTemplateText(string name)
    {
        string filePath = string.Format("{0}/{1}.txt", CODE_TEMPLATE_DIR, name);
        if (File.Exists(filePath))
        {
            return File.ReadAllText(filePath);
        }
        else
        {
            UnityEngine.Debug.LogErrorFormat("[{0}]代码模板不存在", name);
        }
        return "";
    }
    public static string GenerateCode(string templateName, params object[] args)
    {
        string codeTemplateText = GetCodeTemplateText(templateName);
        return string.Format(codeTemplateText, args);
    }

    /// <summary>
    /// 生成部分可编辑代码文本
    /// </summary>
    /// <returns></returns>
    public static string GenerateEditorCode(string codeContent, string templateName, string[] defaultCodes = null, params object[] args)
    {
        string result = "";

        string regex = string.Format(CODE_TEMPLATE_EDITOR, @"([\s\S]*?)");
        MatchCollection mc = Regex.Matches(codeContent, regex);

        string[] tempArr = GenerateCode(templateName, args).Split(new string[] { "[EDITOR]" }, StringSplitOptions.None);
        for (int i = 0; i < tempArr.Length; i++)
        {
            result += tempArr[i];
            if (i + 1 < tempArr.Length)
            {
                if (i < mc.Count)
                {
                    result += string.Format(CODE_TEMPLATE_EDITOR.Replace(@"\n", "\n"), mc[i].Groups[1].Value);
                }
                else
                {
                    string defaultCode = "";
                    if (defaultCodes != null)
                    {
                        defaultCode = i < defaultCodes.Length ? defaultCodes[i] : defaultCodes[defaultCodes.Length - 1];
                    }
                    result += string.Format(CODE_TEMPLATE_EDITOR.Replace(@"\n", "\n"), defaultCode);
                }
            }
        }

        return result;
    }
}