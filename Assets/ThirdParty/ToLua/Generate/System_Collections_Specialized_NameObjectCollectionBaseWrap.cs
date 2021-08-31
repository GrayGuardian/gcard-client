﻿//this source code was auto-generated by tolua#, do not modify it
using System;
using LuaInterface;

public class System_Collections_Specialized_NameObjectCollectionBaseWrap
{
	public static void Register(LuaState L)
	{
		L.BeginClass(typeof(System.Collections.Specialized.NameObjectCollectionBase), typeof(System.Object));
		L.RegFunction("GetObjectData", GetObjectData);
		L.RegFunction("OnDeserialization", OnDeserialization);
		L.RegFunction("GetEnumerator", GetEnumerator);
		L.RegFunction("__tostring", ToLua.op_ToString);
		L.RegVar("Count", get_Count, null);
		L.RegVar("Keys", get_Keys, null);
		L.EndClass();
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetObjectData(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 3);
			System.Collections.Specialized.NameObjectCollectionBase obj = (System.Collections.Specialized.NameObjectCollectionBase)ToLua.CheckObject<System.Collections.Specialized.NameObjectCollectionBase>(L, 1);
			System.Runtime.Serialization.SerializationInfo arg0 = (System.Runtime.Serialization.SerializationInfo)ToLua.CheckObject(L, 2, typeof(System.Runtime.Serialization.SerializationInfo));
			System.Runtime.Serialization.StreamingContext arg1 = StackTraits<System.Runtime.Serialization.StreamingContext>.Check(L, 3);
			obj.GetObjectData(arg0, arg1);
			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnDeserialization(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			System.Collections.Specialized.NameObjectCollectionBase obj = (System.Collections.Specialized.NameObjectCollectionBase)ToLua.CheckObject<System.Collections.Specialized.NameObjectCollectionBase>(L, 1);
			object arg0 = ToLua.ToVarObject(L, 2);
			obj.OnDeserialization(arg0);
			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetEnumerator(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			System.Collections.Specialized.NameObjectCollectionBase obj = (System.Collections.Specialized.NameObjectCollectionBase)ToLua.CheckObject<System.Collections.Specialized.NameObjectCollectionBase>(L, 1);
			System.Collections.IEnumerator o = obj.GetEnumerator();
			ToLua.Push(L, o);
			return 1;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Count(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			System.Collections.Specialized.NameObjectCollectionBase obj = (System.Collections.Specialized.NameObjectCollectionBase)o;
			int ret = obj.Count;
			LuaDLL.lua_pushinteger(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index Count on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Keys(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			System.Collections.Specialized.NameObjectCollectionBase obj = (System.Collections.Specialized.NameObjectCollectionBase)o;
			System.Collections.Specialized.NameObjectCollectionBase.KeysCollection ret = obj.Keys;
			ToLua.PushObject(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index Keys on a nil value");
		}
	}
}
