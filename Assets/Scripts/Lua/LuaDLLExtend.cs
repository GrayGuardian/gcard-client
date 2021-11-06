using System.Runtime.InteropServices;

namespace XLua.LuaDLL
{
    public partial class Lua
    {
        // [DllImport(LUADLL, CallingConvention = CallingConvention.Cdecl)]
        // public static extern int luaopen_socket_core(System.IntPtr L);//[,,m]

        // [MonoPInvokeCallback(typeof(LuaDLL.lua_CSFunction))]
        // public static int LoadSocketCore(System.IntPtr L)
        // {
        //     return luaopen_socket_core(L);
        // }



        [DllImport(LUADLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern int luaopen_rapidjson(System.IntPtr L);

        [MonoPInvokeCallback(typeof(LuaDLL.lua_CSFunction))]
        public static int LoadRapidJson(System.IntPtr L)
        {
            return luaopen_rapidjson(L);
        }



        [DllImport(LUADLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern int luaopen_pb(System.IntPtr L);

        [MonoPInvokeCallback(typeof(LuaDLL.lua_CSFunction))]
        public static int LoadPB(System.IntPtr L)
        {
            return luaopen_pb(L);
        }



#if UNITY_EDITOR
        [DllImport("emmy_core.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int luaopen_emmy_core(System.IntPtr L);
        [MonoPInvokeCallback(typeof(LuaDLL.lua_CSFunction))]
        public static int LoadEmmyCore(System.IntPtr L)
        {
            return luaopen_emmy_core(L);
        }
#endif
    }
}