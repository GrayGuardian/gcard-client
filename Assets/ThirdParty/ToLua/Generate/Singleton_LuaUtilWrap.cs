﻿//this source code was auto-generated by tolua#, do not modify it
using System;
using LuaInterface;

public class Singleton_LuaUtilWrap
{
	public static void Register(LuaState L)
	{
		L.BeginClass(typeof(Singleton<LuaUtil>), typeof(System.Object), "Singleton_LuaUtil");
		L.RegFunction("New", _CreateSingleton_LuaUtil);
		L.RegFunction("__tostring", ToLua.op_ToString);
		L.RegVar("Instance", get_Instance, null);
		L.EndClass();
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateSingleton_LuaUtil(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 0)
			{
				Singleton<LuaUtil> obj = new Singleton<LuaUtil>();
				ToLua.PushObject(L, obj);
				return 1;
			}
			else
			{
				return LuaDLL.luaL_throw(L, "invalid arguments to ctor method: Singleton<LuaUtil>.New");
			}
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Instance(IntPtr L)
	{
		try
		{
			ToLua.PushObject(L, Singleton<LuaUtil>.Instance);
			return 1;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}
}

