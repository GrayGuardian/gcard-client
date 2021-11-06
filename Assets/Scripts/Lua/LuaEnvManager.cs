using System;
using System.IO;
using UnityEngine;
using XLua;
public class LuaEnvManager : Singleton<LuaEnvManager>
{
    public LuaEnv LuaEnv;
    public LuaEnvManager()
    {

    }

    public void Load()
    {
        LuaEnv = new LuaEnv();
        // LuaEnv.AddBuildin("socket.core", XLua.LuaDLL.Lua.LoadSocketCore);
        // LuaEnv.AddBuildin("socket", XLua.LuaDLL.Lua.LoadSocketCore);

        LuaEnv.AddBuildin("rapidjson", XLua.LuaDLL.Lua.LoadRapidJson);
        LuaEnv.AddBuildin("pb", XLua.LuaDLL.Lua.LoadPB);

#if UNITY_EDITOR
        LuaEnv.AddBuildin("emmy_core", XLua.LuaDLL.Lua.LoadEmmyCore);
#endif

        LuaEnv.AddLoader(OnLoadLuaFile);
        LuaEnv.DoString(@"require 'main'");

        MonoUtil.Instance.MonoComponent.UpdateEvent += () =>
        {
            LuaEnv.GC();
        };
        MonoUtil.Instance.MonoComponent.OnDestroyEvent += () =>
        {
            LuaEnv.Dispose();
        };
    }
    private byte[] OnLoadLuaFile(ref string filepath)
    {
        TextAsset asset = AssetUtil.Instance.LoadAsset(typeof(TextAsset), "lua", string.Format("{0}.lua", filepath)) as TextAsset;
        if (asset == null)
        {
            UnityEngine.Debug.LogError(string.Format("找不到Lua文件[{0}]", filepath));
            return null;
        }
#if UNITY_EDITOR
        // 这里主要为了调试环境的断点可以打到对应的文件位置，因此修改filepath
        if (GameConst.PRO_ENV == ENV_TYPE.DEV)
        {
            var fileInfo = FileUtil.Instance.GetChildFile(Path.Combine(GameConst.RESOURCES, "AssetBundles/lua"), string.Format("{0}.lua.txt", filepath));
            if (fileInfo != null)
            {
                filepath = fileInfo.FullName;
            }
        }
#endif
        return asset.bytes;
    }
}
