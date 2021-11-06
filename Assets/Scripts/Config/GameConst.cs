

using System;
using System.IO;
using System.Threading;
using UnityEngine;

public enum ENV_TYPE
{
    // 正式环境
    MASTER = 0,
    // 开发环境
    DEV = 1,

}
public class GameConst
{
    /// <summary>
    /// 运行环境
    /// </summary>
#if UNITY_EDITOR
    public static ENV_TYPE PRO_ENV = ENV_TYPE.DEV;
#else
    public static ENV_TYPE PRO_ENV = ENV_TYPE.MASTER;
#endif

    /// <summary>
    /// Resources文件夹
    /// </summary>
    /// <returns></returns>
    public static string RESOURCES = Path.Combine(Application.dataPath, "./Resources");
    /// <summary>
    /// 资源存放根目录
    /// </summary>
    /// <returns></returns>
    public static string ASSET_ROOT = Path.Combine(Application.persistentDataPath, "./Asset");
    /// <summary>
    /// 打包根目录
    /// </summary>
    /// <returns></returns>
    public static string BUILD_ROOT = Path.Combine(Application.dataPath, "../Build");
    /// <summary>
    /// AB包根目录
    /// </summary>
    /// <returns></returns>
    public static string ASSETBUNDLES_ROOT = Path.Combine(Application.dataPath, "../AssetBundles");
    /// <summary>
    /// Lua文件所在根目录
    /// </summary>
    /// <returns></returns>
    public static string LUA_FILE_ROOT = Path.Combine(Application.dataPath, "./AssetBundles", "./lua");
    /// <summary>
    /// 云端下载资源根目录
    /// </summary>
    /// <returns></returns>
    public static string WEB_DOWNLOAD_ROOT = Path.Combine(Application.dataPath, "../../gcard-server/web-server/public/Download");
}