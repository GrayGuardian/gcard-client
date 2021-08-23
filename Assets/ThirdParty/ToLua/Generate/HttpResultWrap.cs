﻿//this source code was auto-generated by tolua#, do not modify it
using System;
using LuaInterface;

public class HttpResultWrap
{
	public static void Register(LuaState L)
	{
		L.BeginClass(typeof(HttpResult), typeof(System.Object));
		L.RegFunction("New", _CreateHttpResult);
		L.RegFunction("__tostring", ToLua.op_ToString);
		L.RegVar("code", get_code, set_code);
		L.RegVar("bytes", get_bytes, set_bytes);
		L.RegVar("response", get_response, set_response);
		L.EndClass();
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateHttpResult(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 0)
			{
				HttpResult obj = new HttpResult();
				ToLua.PushObject(L, obj);
				return 1;
			}
			else
			{
				return LuaDLL.luaL_throw(L, "invalid arguments to ctor method: HttpResult.New");
			}
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_code(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			HttpResult obj = (HttpResult)o;
			int ret = obj.code;
			LuaDLL.lua_pushinteger(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index code on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_bytes(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			HttpResult obj = (HttpResult)o;
			byte[] ret = obj.bytes;
			ToLua.Push(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index bytes on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_response(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			HttpResult obj = (HttpResult)o;
			System.Net.HttpWebResponse ret = obj.response;
			ToLua.PushObject(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index response on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_code(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			HttpResult obj = (HttpResult)o;
			int arg0 = (int)LuaDLL.luaL_checknumber(L, 2);
			obj.code = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index code on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_bytes(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			HttpResult obj = (HttpResult)o;
			byte[] arg0 = ToLua.CheckByteBuffer(L, 2);
			obj.bytes = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index bytes on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_response(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			HttpResult obj = (HttpResult)o;
			System.Net.HttpWebResponse arg0 = (System.Net.HttpWebResponse)ToLua.CheckObject<System.Net.HttpWebResponse>(L, 2);
			obj.response = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index response on a nil value");
		}
	}
}

