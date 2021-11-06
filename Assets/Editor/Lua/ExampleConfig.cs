using System.Collections.Generic;
using System;
using XLua;
using System.Reflection;
using System.Linq;

public static class ExampleConfig
{

    /// <summary>
    /// LuaCallCSharp 扩展Type
    /// </summary>
    /// <value></value>
    static Type[] LuaCallCSharp_Extension = new Type[]{

    };
    /// <summary>
    /// LuaCallCSharp Unity命名空间
    /// </summary>
    /// <value></value>
    static string[] LuaCallCSharp_UnityNamespaces = new string[] {
        "UnityEngine",
        "UnityEngine.UI",
        "UnityEngine.SceneManagement",
    };
    /// <summary>
    /// LuaCallCSharp 自定义程序集
    /// </summary>
    /// <value></value>
    static string[] LuaCallCSharp_CustomAssemblys = new string[] {
        "Assembly-CSharp",
    };
    /// <summary>
    /// LuaCallCSharp Type名称黑名单
    /// </summary>
    /// <value></value>
    static string[] LuaCallCSharp_TypeNameBlacks = new string[] {
        "HideInInspector", "ExecuteInEditMode",
        "AddComponentMenu", "ContextMenu",
        "RequireComponent", "DisallowMultipleComponent",
        "SerializeField", "AssemblyIsEditorAssembly",
        "Attribute", "Types",
        "UnitySurrogateSelector", "TrackedReference",
        "TypeInferenceRules", "FFTWindow",
        "RPC", "Network", "MasterServer",
        "BitStream", "HostData",
        "ConnectionTesterStatus", "GUI", "EventType",
        "EventModifiers", "FontStyle", "TextAlignment",
        "TextEditor", "TextEditorDblClickSnapping",
        "TextGenerator", "TextClipping", "Gizmos",
        "ADBannerView", "ADInterstitialAd",
        "Android", "Tizen", "jvalue",
        "iPhone", "iOS", "Windows", "CalendarIdentifier",
        "CalendarUnit", "CalendarUnit",
        "ClusterInput", "FullScreenMovieControlMode",
        "FullScreenMovieScalingMode", "Handheld",
        "LocalNotification", "NotificationServices",
        "RemoteNotificationType", "RemoteNotification",
        "SamsungTV", "TextureCompressionQuality",
        "TouchScreenKeyboardType", "TouchScreenKeyboard",
        "MovieTexture", "UnityEngineInternal",
        "Terrain", "Tree", "SplatPrototype",
        "DetailPrototype", "DetailRenderMode",
        "MeshSubsetCombineUtility", "AOT", "Social", "Enumerator",
        "SendMouseEvents", "Cursor", "Flash", "ActionScript",
        "OnRequestRebuild", "Ping",
        "ShaderVariantCollection", "SimpleJson.Reflection",
        "CoroutineTween", "GraphicRebuildTracker",
        "Advertisements", "UnityEditor", "WSA",
        "EventProvider", "Apple",
        "ClusterInput", "Motion",
        "UnityEngine.UI.ReflectionMethodsCache", "NativeLeakDetection",
        "NativeLeakDetectionMode", "WWWAudioExtensions", "UnityEngine.Experimental",
        "ClusterNetwork","GUIStyleState","iPhoneUtils","TerrainData"
    };
    /// <summary>
    /// CSharpCallLua 扩展Type
    /// </summary>
    /// <value></value>
    static Type[] CSharpCallLua_Extension = new Type[]{
        typeof(UnityEngine.Events.UnityAction<float>),
    };
    /// <summary>
    /// CSharpCallLua Type黑名单
    /// </summary>
    /// <value></value>
    static Type[] CSharpCallLua_TypeBlacks = new Type[]{
        typeof(UnityEngine.CanvasRenderer.OnRequestRebuild),
    };
    //黑名单
    [BlackList]
    public static List<List<string>> BlackList = new List<List<string>>()  {
                new List<string>(){"System.Xml.XmlNodeList", "ItemOf"},
                new List<string>(){"UnityEngine.WWW", "movie"},
    #if UNITY_WEBGL
                new List<string>(){"UnityEngine.WWW", "threadPriority"},
    #endif
                new List<string>(){"UnityEngine.Texture2D", "alphaIsTransparency"},
                new List<string>(){"UnityEngine.Security", "GetChainOfTrustValue"},
                new List<string>(){"UnityEngine.CanvasRenderer", "onRequestRebuild"},
                new List<string>(){"UnityEngine.Light", "areaSize"},
                new List<string>(){"UnityEngine.Light", "lightmapBakeType"},
                new List<string>(){"UnityEngine.WWW", "MovieTexture"},
                new List<string>(){"UnityEngine.WWW", "GetMovieTexture"},
                new List<string>(){"UnityEngine.AnimatorOverrideController", "PerformOverrideClipListCleanup"},
    #if !UNITY_WEBPLAYER
                new List<string>(){"UnityEngine.Application", "ExternalEval"},
    #endif
                new List<string>(){"UnityEngine.GameObject", "networkView"}, //4.6.2 not support
                new List<string>(){"UnityEngine.Component", "networkView"},  //4.6.2 not support
                new List<string>(){"System.IO.FileInfo", "GetAccessControl", "System.Security.AccessControl.AccessControlSections"},
                new List<string>(){"System.IO.FileInfo", "SetAccessControl", "System.Security.AccessControl.FileSecurity"},
                new List<string>(){"System.IO.DirectoryInfo", "GetAccessControl", "System.Security.AccessControl.AccessControlSections"},
                new List<string>(){"System.IO.DirectoryInfo", "SetAccessControl", "System.Security.AccessControl.DirectorySecurity"},
                new List<string>(){"System.IO.DirectoryInfo", "CreateSubdirectory", "System.String", "System.Security.AccessControl.DirectorySecurity"},
                new List<string>(){"System.IO.DirectoryInfo", "Create", "System.Security.AccessControl.DirectorySecurity"},
                new List<string>(){"UnityEngine.MonoBehaviour", "runInEditMode"},
                new List<string>(){"UnityEngine.AnimatorControllerParameter","name"},
                new List<string>(){"UnityEngine.AudioSettings","SetSpatializerPluginName"},
                new List<string>(){"UnityEngine.AudioSettings","SetSpatializerPluginName","System.String"},
                new List<string>(){"UnityEngine.AudioSettings","GetSpatializerPluginNames"},
                new List<string>(){"UnityEngine.Caching","SetNoBackupFlag","UnityEngine.CachedAssetBundle"},
                new List<string>(){"UnityEngine.Caching","SetNoBackupFlag","System.String","UnityEngine.Hash128"},
                new List<string>(){"UnityEngine.Caching","ResetNoBackupFlag","UnityEngine.CachedAssetBundle"},
                new List<string>(){"UnityEngine.Caching","ResetNoBackupFlag","System.String","UnityEngine.Hash128"},
                new List<string>(){"UnityEngine.Input","IsJoystickPreconfigured","System.String"},
                new List<string>(){"UnityEngine.DrivenRectTransformTracker","StopRecordingUndo"},
                new List<string>(){"UnityEngine.DrivenRectTransformTracker","StartRecordingUndo"},
                new List<string>(){"UnityEngine.LightProbeGroup","probePositions"},
                new List<string>(){"UnityEngine.LightProbeGroup","dering"},
                new List<string>(){"UnityEngine.MeshRenderer","receiveGI"},
                new List<string>(){"UnityEngine.Light","SetLightDirty"},
                new List<string>(){"UnityEngine.Light","shadowRadius"},
                new List<string>(){"UnityEngine.Light","shadowAngle"},
                new List<string>(){"UnityEngine.QualitySettings","streamingMipmapsRenderersPerFrame"},
                new List<string>(){"UnityEngine.UI.DefaultControls","factory"},
                new List<string>(){"UnityEngine.UI.Graphic","OnRebuildRequested"},
                new List<string>(){"UnityEngine.UI.Text","OnRebuildRequested"},
                new List<string>(){"UnityEngine.Texture","imageContentsHash"},
                new List<string>(){"UnityEngine.ParticleSystemForceField","FindAll"},

                new List<string>(){"AssetUtil","LoadAssetFromEditorBundle","System.Type","System.String","System.String"},
                new List<string>(){"AssetUtil","LoadAssetFromEditorBundleAsync","System.Type","System.String","System.String","System.Action`1[UnityEngine.Object]"},
                new List<string>(){"AssetUtil","LoadAllAssetFromEditorBundle","System.Type","System.String"},
                new List<string>(){"AssetUtil","LoadAllAssetFromEditorBundleAsync","System.Type","System.String","System.Action`1[UnityEngine.Object[]]"},
    };
    [LuaCallCSharp]
    public static List<Type> LuaCallCSharp
    {
        get
        {
            List<Type> result = new List<Type>();
            // Unity
            foreach (var assemblie in AppDomain.CurrentDomain.GetAssemblies())
            {
                if (assemblie.ManifestModule is System.Reflection.Emit.ModuleBuilder)
                {
                    continue;
                }
                foreach (var type in assemblie.GetExportedTypes())
                {
                    if (type.Namespace != null && Array.IndexOf(LuaCallCSharp_UnityNamespaces, type.Namespace) != -1
                    && Array.IndexOf(LuaCallCSharp_TypeNameBlacks, type.Name) == -1 && type.BaseType != typeof(MulticastDelegate)
                    && !type.IsInterface && !type.IsEnum)
                    {
                        result.Add(type);
                    }
                }
            }
            // 自定义
            foreach (var assembly in LuaCallCSharp_CustomAssemblys)
            {
                var assemblie = Assembly.Load(assembly);
                foreach (var type in assemblie.GetExportedTypes())
                {
                    if (type.Namespace == null || !type.Namespace.StartsWith("XLua")
                    && type.BaseType != typeof(MulticastDelegate) && !type.IsInterface && !type.IsEnum)
                    {
                        result.Add(type);
                    }
                }
            }
            // 扩展
            foreach (var type in LuaCallCSharp_Extension)
            {
                if (result.IndexOf(type) == -1)
                {
                    result.Add(type);
                }
            }
            return result;
        }
    }
    [CSharpCallLua]
    public static List<Type> CSharpCallLua
    {
        get
        {
            List<Type> types = new List<Type>();
            var flag = BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.IgnoreCase | BindingFlags.DeclaredOnly;
            foreach (var type in LuaCallCSharp)
            {
                foreach (var field in type.GetFields(flag))
                {
                    if (typeof(Delegate).IsAssignableFrom(field.FieldType))
                    {
                        types.Add(field.FieldType);
                    }
                }
                foreach (var method in type.GetMethods(flag))
                {
                    if (typeof(Delegate).IsAssignableFrom(method.ReturnType))
                    {
                        types.Add(method.ReturnType);
                    }
                    foreach (var param in method.GetParameters())
                    {
                        var paramType = param.ParameterType.IsByRef ? param.ParameterType.GetElementType() : param.ParameterType;
                        if (typeof(Delegate).IsAssignableFrom(paramType))
                        {

                            types.Add(paramType);
                        }
                    }
                }
            }
            List<Type> result = new List<Type>();
            foreach (var type in types)
            {
                if (result.IndexOf(type) == -1 && type.BaseType == typeof(MulticastDelegate) && !hasGenericParameter(type) && !delegateHasEditorRef(type) && Array.IndexOf(CSharpCallLua_TypeBlacks, type) == -1)
                {
                    result.Add(type);
                }
            }
            // 扩展
            foreach (var type in CSharpCallLua_Extension)
            {
                if (result.IndexOf(type) == -1)
                {
                    result.Add(type);
                }
            }
            return result;
        }
    }

    static bool hasGenericParameter(Type type)
    {
        if (type.IsGenericTypeDefinition) return true;
        if (type.IsGenericParameter) return true;
        if (type.IsByRef || type.IsArray)
        {
            return hasGenericParameter(type.GetElementType());
        }
        if (type.IsGenericType)
        {
            foreach (var typeArg in type.GetGenericArguments())
            {
                if (hasGenericParameter(typeArg))
                {
                    return true;
                }
            }
        }
        return false;
    }

    static bool typeHasEditorRef(Type type)
    {
        if (type.Namespace != null && (type.Namespace == "UnityEditor" || type.Namespace.StartsWith("UnityEditor.")))
        {
            return true;
        }
        if (type.IsNested)
        {
            return typeHasEditorRef(type.DeclaringType);
        }
        if (type.IsByRef || type.IsArray)
        {
            return typeHasEditorRef(type.GetElementType());
        }
        if (type.IsGenericType)
        {
            foreach (var typeArg in type.GetGenericArguments())
            {
                if (typeArg.IsGenericParameter)
                {
                    continue;
                }
                if (typeHasEditorRef(typeArg))
                {
                    return true;
                }
            }
        }
        return false;
    }
    static bool delegateHasEditorRef(Type delegateType)
    {
        if (typeHasEditorRef(delegateType)) return true;
        var method = delegateType.GetMethod("Invoke");
        if (method == null)
        {
            return false;
        }
        if (typeHasEditorRef(method.ReturnType)) return true;
        return method.GetParameters().Any(pinfo => typeHasEditorRef(pinfo.ParameterType));
    }

}
