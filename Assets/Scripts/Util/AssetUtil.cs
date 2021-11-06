using System.Threading;
using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEditor;
using UnityEngine;

public enum AssetLoadType
{
    AssetBundle = 1,
    Resources = 2,
    EditorAssetBundle = 3,
}

public class AssetLoadInfo
{
    public string Key;
    public string Name;
    public UnityEngine.Object Asset;
    public AssetLoadType Type;
    public int Index = 1;   //使用计次
}

public class AssetUtil : Singleton<AssetUtil>
{
    // 已解密完毕的AB包
    private Dictionary<string, byte[]> _bundleBytesMap = new Dictionary<string, byte[]>();
    // 已加载的AB包
    private Dictionary<string, AssetBundle> _bundleMap = new Dictionary<string, AssetBundle>();
    // 已加载的资源信息 用于记录引用卸载AB包
    private Dictionary<string, List<AssetLoadInfo>> _loadInfoMap = new Dictionary<string, List<AssetLoadInfo>>();
    // 正在加载的AB包数组
    private List<string> _loadingBundleList = new List<string>();
    // 资源依赖文件Json对象
    private JObject _relyJObject;
    // 资源版本文件 主要用于查找资源文件名
    private AssetVersion _vModel;
    /// <summary>
    /// 清理缓存
    /// </summary>
    public void Reset()
    {
        _bundleBytesMap = new Dictionary<string, byte[]>();
        _relyJObject = null;
        _vModel = null;
    }
    /// <summary>
    /// 查找资源文件字节集
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public byte[] GetAssetFileBytes(string name)
    {
        // 从本地资源文件读取
        string filePath = Path.Combine(GameConst.ASSET_ROOT, name);
        if (File.Exists(filePath))
        {
            return FileUtil.Instance.ReadBytes(filePath);
        }
        // 从StreamingAssetsPath调用
        WWW www = new WWW(Path.Combine(Application.streamingAssetsPath, name));
        while (!www.isDone) { }
        if (www.bytes.Length > 0)
        {
            return www.bytes;
        }

        // 通过版本文件查找携带md5的名字 递归回调
        string fileName = Path.GetFileNameWithoutExtension(name);
        if (_vModel == null)
        {
            string json = EncryptUtil.Instance.AesDecryptString(System.Text.Encoding.UTF8.GetString(GetAssetFileBytes("Version")));
            _vModel = JsonConvert.DeserializeObject<AssetVersion>(json);
        }
        foreach (var asset in _vModel.Assets)
        {
            if (asset.name.IndexOf(fileName) != -1)
            {
                return GetAssetFileBytes(asset.fileName);
            }
        }

        return new byte[] { };
    }

    /// <summary>
    /// 获取资源依赖关系
    /// </summary>
    /// <param name="key"></param>
    /// <param name="assetName"></param>
    /// <returns></returns>
    public string[] getRelyBundleKeys(string key, string assetName = null)
    {
        _relyJObject = _relyJObject ?? JObject.Parse(EncryptUtil.Instance.AesDecryptString(System.Text.Encoding.UTF8.GetString(GetAssetFileBytes("AssetBundleRely"))));
        List<string> bundleNameList = new List<string> { key };
        JToken jToken;
        if (_relyJObject.TryGetValue(key, out jToken))
        {
            if (assetName != null)
            {
                // 获取固定资源关联AB包信息
                JToken jArray = jToken[assetName];
                if (jArray != null)
                {
                    foreach (var ab in jArray.Values<string>())
                    {
                        bundleNameList.Add(ab);
                    }
                }
            }
            else
            {
                // 获取所有资源关联AB包信息
                foreach (var item in jToken.Children<JToken>())
                {
                    foreach (var ab in item.ToObject<JProperty>().Value.ToObject<JArray>().Values<string>())
                    {
                        if (bundleNameList.IndexOf(ab) == -1)
                        {
                            bundleNameList.Add(ab);
                        }
                    }
                }
            }
        }
        return bundleNameList.ToArray();
    }

    /// <summary>
    /// 获得解密AB包bytes，并保存到缓存
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public byte[] DecryptBundleBytes(string key)
    {
        if (_bundleBytesMap.ContainsKey(key))
        {
            return _bundleBytesMap[key];
        }
        byte[] data = EncryptUtil.Instance.AesDecryptBytes(GetAssetFileBytes("AssetBundles/" + key));
        _bundleBytesMap.Add(key, data);
        return data;
    }

    /// <summary>
    /// 异步获得解密AB包bytes，并保存到缓存
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public bool DecryptBundleBytesAsync(string key, Action<byte[]> cb)
    {
        if (_bundleBytesMap.ContainsKey(key))
        {
            cb(_bundleBytesMap[key]);
            return true;
        }
        EncryptUtil.Instance.AesDecryptAsync(GetAssetFileBytes("AssetBundles/" + key), (data) =>
        {
            if (!_bundleBytesMap.ContainsKey(key))
            {
                _bundleBytesMap.Add(key, data);
            }
            cb(data);
        });
        return true;
    }
    /// <summary>
    /// AB包加载完毕
    /// </summary>
    /// <param name="key"></param>
    /// <param name="bundle"></param>
    private void _loadBundleOver(string key, AssetBundle bundle)
    {
        Debug.LogFormat("AssetBundle加载完毕>>>{0}", key);
        _bundleMap.Add(key, bundle);
    }
    /// <summary>
    /// AB包卸载完毕
    /// </summary>
    /// <param name="key"></param>
    private void _unloadBundleOver(string key)
    {
        Debug.LogFormat("AssetBundle卸载完毕>>>{0}", key);
        _bundleMap.Remove(key);
    }
    /// <summary>
    /// 加载AB包
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public AssetBundle LoadBundle(string key)
    {
        if (_bundleMap.ContainsKey(key))
        {
            return _bundleMap[key];
        }
        byte[] data = DecryptBundleBytes(key);
        if (data == null) return null;
        AssetBundle bundle = AssetBundle.LoadFromMemory(data);

        _loadBundleOver(key, bundle);

        return bundle;
    }
    /// <summary>
    /// 异步加载AB包
    /// </summary>
    /// <param name="key"></param>
    /// <param name="cb"></param>
    /// <returns></returns>
    public void LoadBundleAsync(string key, Action<AssetBundle> cb = null)
    {
        MonoUtil.Instance.StartCoroutine(_loadBundleAsync(key, cb));
    }
    System.Collections.IEnumerator _loadBundleAsync(string key, Action<AssetBundle> cb = null)
    {
        if (_bundleMap.ContainsKey(key))
        {
            if (cb != null) cb(_bundleMap[key]);
            yield break;
        }
        byte[] data = null;
        bool flag = DecryptBundleBytesAsync(key, (d) =>
        {
            data = d;
        });
        if (!flag)
        {
            if (cb != null) cb(null);
            yield break;
        }
        yield return new WaitUntil(() => { return data != null; });     // 等待AB包异步解密完毕
        // 为了保证异步解密AB包资源过程，AB包未加载，因此再判断一次
        if (_bundleMap.ContainsKey(key))
        {
            if (cb != null) cb(_bundleMap[key]);
            yield break;
        }
        // 判断AB包是否正在通过异步加载
        if (_loadingBundleList.IndexOf(key) != -1)
        {
            UnityEngine.Debug.Log("AB包正在异步加载");
            yield return new WaitUntil(() => { return _bundleMap.ContainsKey(key); });  // 等待AB包异步加载完毕
            UnityEngine.Debug.Log("AB包异步加载完毕" + _bundleMap[key]);
            if (cb != null) cb(_bundleMap[key]);
            yield break;
        }
        _loadingBundleList.Add(key);
        var assetLoadRequest = AssetBundle.LoadFromMemoryAsync(data);
        yield return assetLoadRequest;
        AssetBundle bundle = assetLoadRequest.assetBundle;
        _loadBundleOver(key, bundle);
        _loadingBundleList.Remove(key);

        if (cb != null) cb(bundle);
    }
    /// <summary>
    /// 卸载AB包
    /// </summary>
    /// <param name="key"></param>
    /// <param name="unloadAllLoadedObjects"></param>
    public void UnloadBundle(string key, bool unloadAllLoadedObjects = true)
    {
        if (!_bundleMap.ContainsKey(key))
        {
            return;
        }
        _bundleMap[key].Unload(unloadAllLoadedObjects);
        _unloadBundleOver(key);
    }
    #region AssetBundle加载资源
    /// <summary>
    /// 从AB包中加载资源
    /// </summary>
    /// <param name="key"></param>
    /// <param name="assetName"></param>
    public UnityEngine.Object LoadAssetFromBundle(Type type, string key, string assetName)
    {
        // 加载AB包 包括依赖AB包
        string[] bundleKeys = getRelyBundleKeys(key, assetName);
        List<AssetBundle> bundles = new List<AssetBundle>();
        for (int i = 0; i < bundleKeys.Length; i++)
        {
            var bundle = LoadBundle(bundleKeys[i]);
            if (bundle != null)
            {
                bundles.Add(bundle);
            }
        }
        if (bundles.Count == 0) return null;
        // 加载资源
        var asset = bundles[0].LoadAsset(assetName, type);
        if (asset != null)
        {
            _loadAssetFromBundleOver(key, asset);
            foreach (var bundle in bundles)
            {
                _addLoadAssetInfo(bundle.name, asset, AssetLoadType.AssetBundle);
            }
        }
        return asset;
    }
    public void LoadAssetFromBundleAsync(Type type, string key, string assetName, Action<UnityEngine.Object> cb)
    {
        MonoUtil.Instance.StartCoroutine(_loadAssetFromBundleAsync(type, key, assetName, cb));
    }
    System.Collections.IEnumerator _loadAssetFromBundleAsync(Type type, string key, string assetName, Action<UnityEngine.Object> cb)
    {
        // 加载AB包 包括依赖AB包
        string[] bundleKeys = getRelyBundleKeys(key, assetName);
        List<AssetBundle> bundles = new List<AssetBundle>();
        int sum = 0;
        for (int i = 0; i < bundleKeys.Length; i++)
        {
            LoadBundleAsync(bundleKeys[i], (bundle) =>
            {
                sum += 1;
                if (bundle != null)
                {
                    bundles.Add(bundle);
                }
            });
        }
        yield return new WaitUntil(() => { return bundleKeys.Length <= sum; });
        if (bundles.Count == 0) yield break;
        var assetRequest = bundles[0].LoadAssetAsync(assetName, type);
        yield return assetRequest;
        var asset = assetRequest.asset;
        if (asset != null)
        {
            _loadAssetFromBundleOver(key, asset);
            foreach (var bundle in bundles)
            {
                _addLoadAssetInfo(bundle.name, asset, AssetLoadType.AssetBundle);
            }
        }
        cb(asset);
    }
    public UnityEngine.Object[] LoadAllAssetFromBundle(Type type, string key)
    {
        List<UnityEngine.Object> assets = new List<UnityEngine.Object>();
        var bundle = LoadBundle(key);
        var assetNames = bundle.GetAllAssetNames();
        foreach (var assetName in assetNames)
        {
            assets.Add(LoadAssetFromBundle(type, key, assetName));
        }
        return assets.ToArray();
    }
    public void LoadAllAssetFromBundleAsync(Type type, string key, Action<UnityEngine.Object[]> cb)
    {
        List<UnityEngine.Object> assets = new List<UnityEngine.Object>();
        LoadBundleAsync(key, (bundle) =>
        {
            int sum = 0;
            var assetNames = bundle.GetAllAssetNames();
            foreach (var assetName in assetNames)
            {
                LoadAssetFromBundleAsync(type, key, assetName, (asset) =>
                {
                    sum += 1;
                    assets.Add(asset);
                    if (assets.Count >= sum)
                    {
                        cb(assets.ToArray());
                    }
                });
            }
        });
    }
    private void _loadAssetFromBundleOver(string key, UnityEngine.Object asset)
    {
        Debug.LogFormat("从AssetBundle加载资源 - key：【{0}】 assetName：【{1}】", key, asset.name);
    }
    #endregion
#if UNITY_EDITOR
    #region 模拟AssetBundle加载资源
    /// <summary>
    /// 开发环境时使用的模拟AB包加载资源
    /// </summary>
    /// <param name="type"></param>
    /// <param name="key"></param>
    /// <param name="assetName"></param>
    /// <returns></returns>
    public UnityEngine.Object LoadAssetFromEditorBundle(Type type, string key, string assetName)
    {
        var paths = AssetDatabase.GetAssetPathsFromAssetBundleAndAssetName(key, assetName);
        foreach (var path in paths)
        {
            var asset = AssetDatabase.LoadAssetAtPath(path, type);
            if (asset != null)
            {
                _loadAssetFromEditorBundleOver(key, asset);
                return asset;
            }
        }
        return null;
    }
    // 事实上AssetDatabase并没有异步加载方式，这里凑格式多写一个，本质上还是异步加载
    public void LoadAssetFromEditorBundleAsync(Type type, string key, string assetName, Action<UnityEngine.Object> cb)
    {
        cb(LoadAssetFromEditorBundle(type, key, assetName));
    }
    public UnityEngine.Object[] LoadAllAssetFromEditorBundle(Type type, string key)
    {
        List<UnityEngine.Object> assets = new List<UnityEngine.Object>();
        var paths = AssetDatabase.GetAssetPathsFromAssetBundle(key);
        foreach (var path in paths)
        {
            var asset = AssetDatabase.LoadAssetAtPath(path, type);
            if (asset != null)
            {
                _loadAssetFromEditorBundleOver(key, asset);

                assets.Add(asset);
            }
        }
        return assets.ToArray();
    }
    // 事实上AssetDatabase并没有异步加载全部方式，这里凑格式多写一个，本质上还是异步加载
    public void LoadAllAssetFromEditorBundleAsync(Type type, string key, Action<UnityEngine.Object[]> cb)
    {
        List<UnityEngine.Object> assets = new List<UnityEngine.Object>();
        var paths = AssetDatabase.GetAssetPathsFromAssetBundle(key);
        foreach (var path in paths)
        {
            var asset = AssetDatabase.LoadAssetAtPath(path, type);
            if (asset != null)
            {
                _loadAssetFromEditorBundleOver(key, asset);

                assets.Add(asset);
            }
        }
        cb(assets.ToArray());
    }
    private void _loadAssetFromEditorBundleOver(string key, UnityEngine.Object asset)
    {
        Debug.LogFormat("从模拟AssetBundle加载资源 - key：【{0}】 assetName：【{1}】", key, asset.name);

        _addLoadAssetInfo(key, asset, AssetLoadType.EditorAssetBundle);
    }
    #endregion
#endif

    #region 本地资源Resources加载
    /// <summary>
    /// 从本地资源Resources加载
    /// </summary>
    /// <param name="type"></param>
    /// <param name="key"></param>
    /// <param name="assetName"></param>
    /// <returns></returns>
    public UnityEngine.Object LoadAssetFromResources(Type type, string key, string assetName)
    {
        string path = key + "/" + assetName;
        var asset = Resources.Load(path, type);
        if (asset != null)
        {
            _loadAssetFromResourcesOver(key, asset);
        }
        return asset;
    }
    public void LoadAssetFromResourcesAsync(Type type, string key, string assetName, Action<UnityEngine.Object> cb)
    {
        string path = key + "/" + assetName;
        MonoUtil.Instance.StartCoroutine(_loadAssetFromResourcesAsync(type, path, (asset) =>
        {
            if (asset != null)
            {
                _loadAssetFromResourcesOver(key, asset);
            }
            cb(asset);
        }));
    }
    System.Collections.IEnumerator _loadAssetFromResourcesAsync(Type type, string path, Action<UnityEngine.Object> cb)
    {
        ResourceRequest request = Resources.LoadAsync(path, type);
        yield return request;
        var asset = request.asset;
        cb(asset);
    }
    public UnityEngine.Object[] LoadAllAssetFromResources(Type type, string key)
    {
        string path = key;
        var assets = Resources.LoadAll(path, type);
        foreach (var asset in assets)
        {
            _loadAssetFromResourcesOver(key, asset);
        }
        return assets;
    }
    // 事实上Resources并没有异步加载全部方式，这里凑格式多写一个，本质上还是异步加载
    public void LoadAllAssetFromResourcesAsync(Type type, string key, Action<UnityEngine.Object[]> cb)
    {
        cb(LoadAllAssetFromResources(type, key));
    }
    private void _loadAssetFromResourcesOver(string key, UnityEngine.Object asset)
    {
        Debug.LogFormat("从Resources加载资源 - key：【{0}】 assetName：【{1}】", key, asset.name);
        _addLoadAssetInfo(key, asset, AssetLoadType.Resources);
    }
    #endregion


    private void _addLoadAssetInfo(string key, UnityEngine.Object asset, AssetLoadType Type)
    {
        var loadInfo = new AssetLoadInfo();
        loadInfo.Key = key;
        loadInfo.Name = asset.name;
        loadInfo.Asset = asset;
        loadInfo.Type = Type;
        loadInfo.Index = 1;

        if (_loadInfoMap.ContainsKey(key))
        {

            AssetLoadInfo info = null;
            var arr = _loadInfoMap[key];
            for (int i = 0; i < arr.Count; i++)
            {
                if (arr[i].Name == asset.name)
                {
                    info = arr[i];
                    break;
                }
            }
            if (info == null)
            {
                arr.Add(loadInfo);
            }
            else
            {
                info.Index++;
            }
        }
        else
        {
            _loadInfoMap.Add(key, new List<AssetLoadInfo>() { loadInfo });
        }
    }

    /// <summary>
    /// 资源加载汇总
    /// </summary>
    /// <param name="type"></param>
    /// <param name="key"></param>
    /// <param name="assetName"></param>
    /// <returns></returns>
    public UnityEngine.Object LoadAsset(Type type, string key, string assetName)
    {
        UnityEngine.Object asset = null;
        if (GameConst.PRO_ENV == ENV_TYPE.MASTER)
        {
            // 正式环境通过AB包加载
            asset = LoadAssetFromBundle(type, key, assetName);
        }
        else
        {
#if UNITY_EDITOR
            // 开发环境模拟AB包本地加载
            asset = LoadAssetFromEditorBundle(type, key, assetName);
#endif
        }
        if (asset == null)
        {
            // AB包方式无法加载，则尝试通过本地Resources加载
            asset = LoadAssetFromResources(type, key, assetName);
        }
        return asset;
    }
    /// <summary>
    /// 资源异步加载汇总
    /// </summary>
    /// <param name="type"></param>
    /// <param name="key"></param>
    /// <param name="assetName"></param>
    /// <param name="cb"></param>
    public void LoadAssetAsync(Type type, string key, string assetName, Action<UnityEngine.Object> cb)
    {
        if (GameConst.PRO_ENV == ENV_TYPE.MASTER)
        {
            LoadAssetFromBundleAsync(type, key, assetName, (asset) =>
            {
                if (asset == null)
                {
                    // AB包方式无法加载，则尝试通过本地Resources加载
                    LoadAssetFromResourcesAsync(type, key, assetName, cb);
                }
                else
                {
                    cb(asset);
                }
            });
        }
        else
        {
#if UNITY_EDITOR
            LoadAssetFromEditorBundleAsync(type, key, assetName, (asset) =>
            {
                if (asset == null)
                {
                    // AB包方式无法加载，则尝试通过本地Resources加载
                    LoadAssetFromResourcesAsync(type, key, assetName, cb);
                }
                else
                {
                    cb(asset);
                }
            });
#endif
        }
    }

    /// <summary>
    /// 所有资源加载汇总
    /// </summary>
    /// <param name="type"></param>
    /// <param name="key"></param>
    /// <param name="assetName"></param>
    /// <returns></returns>
    public UnityEngine.Object[] LoadAllAsset(Type type, string key)
    {
        UnityEngine.Object[] assets = new UnityEngine.Object[0];
        if (GameConst.PRO_ENV == ENV_TYPE.MASTER)
        {
            // 正式环境通过AB包加载
            assets = LoadAllAssetFromBundle(type, key);
        }
        else
        {
#if UNITY_EDITOR
            // 开发环境模拟AB包本地加载
            assets = LoadAllAssetFromEditorBundle(type, key);
#endif
        }
        if (assets.Length == 0)
        {
            // AB包方式加载无资源，则尝试通过本地Resources加载
            assets = LoadAllAssetFromResources(type, key);
        }
        return assets;
    }
    /// <summary>
    /// 所有资源异步加载汇总
    /// </summary>
    /// <param name="type"></param>
    /// <param name="key"></param>
    /// <param name="assetName"></param>
    /// <param name="cb"></param>
    public void LoadAllAssetAsync(Type type, string key, Action<UnityEngine.Object[]> cb)
    {
        if (GameConst.PRO_ENV == ENV_TYPE.MASTER)
        {
            LoadAllAssetFromBundleAsync(type, key, (assets) =>
            {
                if (assets.Length == 0)
                {
                    // AB包方式加载无资源，则尝试通过本地Resources加载
                    LoadAllAssetFromResourcesAsync(type, key, cb);
                }
                else
                {
                    cb(assets);
                }
            });
        }
        else
        {
#if UNITY_EDITOR
            LoadAllAssetFromEditorBundleAsync(type, key, (assets) =>
            {
                if (assets.Length == 0)
                {
                    // AB包方式加载无资源，则尝试通过本地Resources加载
                    LoadAllAssetFromResourcesAsync(type, key, cb);
                }
                else
                {
                    cb(assets);
                }
            });
#endif
        }
    }

    public void UnloadAsset(UnityEngine.Object asset)
    {
        List<AssetLoadInfo> unLoadList = new List<AssetLoadInfo>();
        foreach (var key in _loadInfoMap.Keys)
        {
            var list = _loadInfoMap[key];
            AssetLoadInfo info = null;
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].Asset == asset)
                {
                    info = list[i];
                    break;
                }
            }
            if (info != null)
            {
                unLoadList.Add(info);
            }

        }

        foreach (var info in unLoadList)
        {
            _unloadAsset(info);
        }

    }

    private void _unloadAsset(AssetLoadInfo info)
    {
        if (info == null) return;
        if (info.Type == AssetLoadType.AssetBundle)
        {
            Debug.LogFormat("从AssetBundle卸载资源 - key：【{0}】 assetName：【{1}】", info.Key, info.Name);

            info.Index--;
            if (info.Index == 0)
            {
                _loadInfoMap[info.Key].Remove(info);
                if (_loadInfoMap[info.Key].Count == 0)
                {
                    UnloadBundle(info.Key);
                }
            }
        }
        else if (info.Type == AssetLoadType.EditorAssetBundle)
        {
            Debug.LogFormat("从模拟AssetBundle卸载资源 - key：【{0}】 assetName：【{1}】", info.Key, info.Name);
            if (info.Asset.GetType() != typeof(UnityEngine.GameObject))
            {
                Resources.UnloadAsset(info.Asset);
            }
            else
            {
                // 预制体暂时无法强行释放，没占多少内存，先不处理
            }
        }
        else if (info.Type == AssetLoadType.Resources)
        {
            Debug.LogFormat("从Resources卸载资源 - key：【{0}】 assetName：【{1}】", info.Key, info.Name);
            if (info.Asset.GetType() != typeof(UnityEngine.GameObject))
            {
                Resources.UnloadAsset(info.Asset);
            }
            else
            {
                // 预制体暂时无法强行释放，没占多少内存，先不处理
            }
        }
        Resources.UnloadUnusedAssets();
    }

}