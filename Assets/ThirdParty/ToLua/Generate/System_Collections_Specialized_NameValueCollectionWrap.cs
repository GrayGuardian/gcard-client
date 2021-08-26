﻿//this source code was auto-generated by tolua#, do not modify it
using System;
using LuaInterface;

public class System_Collections_Specialized_NameValueCollectionWrap
{
	public static void Register(LuaState L)
	{
		L.BeginClass(typeof(System.Collections.Specialized.NameValueCollection), typeof(System.Collections.Specialized.NameObjectCollectionBase));
		L.RegFunction("Add", Add);
		L.RegFunction("Clear", Clear);
		L.RegFunction("CopyTo", CopyTo);
		L.RegFunction("HasKeys", HasKeys);
		L.RegFunction("Get", Get);
		L.RegFunction("GetValues", GetValues);
		L.RegFunction("Set", Set);
		L.RegFunction("Remove", Remove);
		L.RegFunction("get_Item", get_Item);
		L.RegFunction("set_Item", set_Item);
		L.RegFunction("GetKey", GetKey);
		L.RegFunction("New", _CreateSystem_Collections_Specialized_NameValueCollection);
		L.RegVar("this", _this, null);
		L.RegFunction("__tostring", ToLua.op_ToString);
		L.RegVar("AllKeys", get_AllKeys, null);
		L.EndClass();
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateSystem_Collections_Specialized_NameValueCollection(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 0)
			{
				System.Collections.Specialized.NameValueCollection obj = new System.Collections.Specialized.NameValueCollection();
				ToLua.PushObject(L, obj);
				return 1;
			}
			else if (count == 1 && TypeChecker.CheckTypes<int>(L, 1))
			{
				int arg0 = (int)LuaDLL.lua_tonumber(L, 1);
				System.Collections.Specialized.NameValueCollection obj = new System.Collections.Specialized.NameValueCollection(arg0);
				ToLua.PushObject(L, obj);
				return 1;
			}
			else if (count == 1 && TypeChecker.CheckTypes<System.Collections.Specialized.NameValueCollection>(L, 1))
			{
				System.Collections.Specialized.NameValueCollection arg0 = (System.Collections.Specialized.NameValueCollection)ToLua.ToObject(L, 1);
				System.Collections.Specialized.NameValueCollection obj = new System.Collections.Specialized.NameValueCollection(arg0);
				ToLua.PushObject(L, obj);
				return 1;
			}
			else if (count == 1 && TypeChecker.CheckTypes<System.Collections.IEqualityComparer>(L, 1))
			{
				System.Collections.IEqualityComparer arg0 = (System.Collections.IEqualityComparer)ToLua.ToObject(L, 1);
				System.Collections.Specialized.NameValueCollection obj = new System.Collections.Specialized.NameValueCollection(arg0);
				ToLua.PushObject(L, obj);
				return 1;
			}
			else if (count == 2 && TypeChecker.CheckTypes<System.Collections.IEqualityComparer>(L, 2))
			{
				int arg0 = (int)LuaDLL.luaL_checknumber(L, 1);
				System.Collections.IEqualityComparer arg1 = (System.Collections.IEqualityComparer)ToLua.ToObject(L, 2);
				System.Collections.Specialized.NameValueCollection obj = new System.Collections.Specialized.NameValueCollection(arg0, arg1);
				ToLua.PushObject(L, obj);
				return 1;
			}
			else if (count == 2 && TypeChecker.CheckTypes<System.Collections.Specialized.NameValueCollection>(L, 2))
			{
				int arg0 = (int)LuaDLL.luaL_checknumber(L, 1);
				System.Collections.Specialized.NameValueCollection arg1 = (System.Collections.Specialized.NameValueCollection)ToLua.ToObject(L, 2);
				System.Collections.Specialized.NameValueCollection obj = new System.Collections.Specialized.NameValueCollection(arg0, arg1);
				ToLua.PushObject(L, obj);
				return 1;
			}
			else
			{
				return LuaDLL.luaL_throw(L, "invalid arguments to ctor method: System.Collections.Specialized.NameValueCollection.New");
			}
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _get_this(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 2 && TypeChecker.CheckTypes<int>(L, 2))
			{
				System.Collections.Specialized.NameValueCollection obj = (System.Collections.Specialized.NameValueCollection)ToLua.CheckObject<System.Collections.Specialized.NameValueCollection>(L, 1);
				int arg0 = (int)LuaDLL.lua_tonumber(L, 2);
				string o = obj[arg0];
				LuaDLL.lua_pushstring(L, o);
				return 1;
			}
			else if (count == 2 && TypeChecker.CheckTypes<string>(L, 2))
			{
				System.Collections.Specialized.NameValueCollection obj = (System.Collections.Specialized.NameValueCollection)ToLua.CheckObject<System.Collections.Specialized.NameValueCollection>(L, 1);
				string arg0 = ToLua.ToString(L, 2);
				string o = obj[arg0];
				LuaDLL.lua_pushstring(L, o);
				return 1;
			}
			else
			{
				return LuaDLL.luaL_throw(L, "invalid arguments to operator method: System.Collections.Specialized.NameValueCollection.this");
			}
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _set_this(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 3);
			System.Collections.Specialized.NameValueCollection obj = (System.Collections.Specialized.NameValueCollection)ToLua.CheckObject<System.Collections.Specialized.NameValueCollection>(L, 1);
			string arg0 = ToLua.CheckString(L, 2);
			string arg1 = ToLua.CheckString(L, 3);
			obj[arg0] = arg1;
			return 0;

		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _this(IntPtr L)
	{
		try
		{
			LuaDLL.lua_pushvalue(L, 1);
			LuaDLL.tolua_bindthis(L, _get_this, _set_this);
			return 1;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Add(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 2)
			{
				System.Collections.Specialized.NameValueCollection obj = (System.Collections.Specialized.NameValueCollection)ToLua.CheckObject<System.Collections.Specialized.NameValueCollection>(L, 1);
				System.Collections.Specialized.NameValueCollection arg0 = (System.Collections.Specialized.NameValueCollection)ToLua.CheckObject<System.Collections.Specialized.NameValueCollection>(L, 2);
				obj.Add(arg0);
				return 0;
			}
			else if (count == 3)
			{
				System.Collections.Specialized.NameValueCollection obj = (System.Collections.Specialized.NameValueCollection)ToLua.CheckObject<System.Collections.Specialized.NameValueCollection>(L, 1);
				string arg0 = ToLua.CheckString(L, 2);
				string arg1 = ToLua.CheckString(L, 3);
				obj.Add(arg0, arg1);
				return 0;
			}
			else
			{
				return LuaDLL.luaL_throw(L, "invalid arguments to method: System.Collections.Specialized.NameValueCollection.Add");
			}
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Clear(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			System.Collections.Specialized.NameValueCollection obj = (System.Collections.Specialized.NameValueCollection)ToLua.CheckObject<System.Collections.Specialized.NameValueCollection>(L, 1);
			obj.Clear();
			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int CopyTo(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 3);
			System.Collections.Specialized.NameValueCollection obj = (System.Collections.Specialized.NameValueCollection)ToLua.CheckObject<System.Collections.Specialized.NameValueCollection>(L, 1);
			System.Array arg0 = (System.Array)ToLua.CheckObject<System.Array>(L, 2);
			int arg1 = (int)LuaDLL.luaL_checknumber(L, 3);
			obj.CopyTo(arg0, arg1);
			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int HasKeys(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			System.Collections.Specialized.NameValueCollection obj = (System.Collections.Specialized.NameValueCollection)ToLua.CheckObject<System.Collections.Specialized.NameValueCollection>(L, 1);
			bool o = obj.HasKeys();
			LuaDLL.lua_pushboolean(L, o);
			return 1;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Get(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 2 && TypeChecker.CheckTypes<int>(L, 2))
			{
				System.Collections.Specialized.NameValueCollection obj = (System.Collections.Specialized.NameValueCollection)ToLua.CheckObject<System.Collections.Specialized.NameValueCollection>(L, 1);
				int arg0 = (int)LuaDLL.lua_tonumber(L, 2);
				string o = obj.Get(arg0);
				LuaDLL.lua_pushstring(L, o);
				return 1;
			}
			else if (count == 2 && TypeChecker.CheckTypes<string>(L, 2))
			{
				System.Collections.Specialized.NameValueCollection obj = (System.Collections.Specialized.NameValueCollection)ToLua.CheckObject<System.Collections.Specialized.NameValueCollection>(L, 1);
				string arg0 = ToLua.ToString(L, 2);
				string o = obj.Get(arg0);
				LuaDLL.lua_pushstring(L, o);
				return 1;
			}
			else
			{
				return LuaDLL.luaL_throw(L, "invalid arguments to method: System.Collections.Specialized.NameValueCollection.Get");
			}
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetValues(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 2 && TypeChecker.CheckTypes<int>(L, 2))
			{
				System.Collections.Specialized.NameValueCollection obj = (System.Collections.Specialized.NameValueCollection)ToLua.CheckObject<System.Collections.Specialized.NameValueCollection>(L, 1);
				int arg0 = (int)LuaDLL.lua_tonumber(L, 2);
				string[] o = obj.GetValues(arg0);
				ToLua.Push(L, o);
				return 1;
			}
			else if (count == 2 && TypeChecker.CheckTypes<string>(L, 2))
			{
				System.Collections.Specialized.NameValueCollection obj = (System.Collections.Specialized.NameValueCollection)ToLua.CheckObject<System.Collections.Specialized.NameValueCollection>(L, 1);
				string arg0 = ToLua.ToString(L, 2);
				string[] o = obj.GetValues(arg0);
				ToLua.Push(L, o);
				return 1;
			}
			else
			{
				return LuaDLL.luaL_throw(L, "invalid arguments to method: System.Collections.Specialized.NameValueCollection.GetValues");
			}
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Set(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 3);
			System.Collections.Specialized.NameValueCollection obj = (System.Collections.Specialized.NameValueCollection)ToLua.CheckObject<System.Collections.Specialized.NameValueCollection>(L, 1);
			string arg0 = ToLua.CheckString(L, 2);
			string arg1 = ToLua.CheckString(L, 3);
			obj.Set(arg0, arg1);
			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Remove(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			System.Collections.Specialized.NameValueCollection obj = (System.Collections.Specialized.NameValueCollection)ToLua.CheckObject<System.Collections.Specialized.NameValueCollection>(L, 1);
			string arg0 = ToLua.CheckString(L, 2);
			obj.Remove(arg0);
			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Item(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 2 && TypeChecker.CheckTypes<int>(L, 2))
			{
				System.Collections.Specialized.NameValueCollection obj = (System.Collections.Specialized.NameValueCollection)ToLua.CheckObject<System.Collections.Specialized.NameValueCollection>(L, 1);
				int arg0 = (int)LuaDLL.lua_tonumber(L, 2);
				string o = obj[arg0];
				LuaDLL.lua_pushstring(L, o);
				return 1;
			}
			else if (count == 2 && TypeChecker.CheckTypes<string>(L, 2))
			{
				System.Collections.Specialized.NameValueCollection obj = (System.Collections.Specialized.NameValueCollection)ToLua.CheckObject<System.Collections.Specialized.NameValueCollection>(L, 1);
				string arg0 = ToLua.ToString(L, 2);
				string o = obj[arg0];
				LuaDLL.lua_pushstring(L, o);
				return 1;
			}
			else
			{
				return LuaDLL.luaL_throw(L, "invalid arguments to method: System.Collections.Specialized.NameValueCollection.get_Item");
			}
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_Item(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 3);
			System.Collections.Specialized.NameValueCollection obj = (System.Collections.Specialized.NameValueCollection)ToLua.CheckObject<System.Collections.Specialized.NameValueCollection>(L, 1);
			string arg0 = ToLua.CheckString(L, 2);
			string arg1 = ToLua.CheckString(L, 3);
			obj[arg0] = arg1;
			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetKey(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			System.Collections.Specialized.NameValueCollection obj = (System.Collections.Specialized.NameValueCollection)ToLua.CheckObject<System.Collections.Specialized.NameValueCollection>(L, 1);
			int arg0 = (int)LuaDLL.luaL_checknumber(L, 2);
			string o = obj.GetKey(arg0);
			LuaDLL.lua_pushstring(L, o);
			return 1;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_AllKeys(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			System.Collections.Specialized.NameValueCollection obj = (System.Collections.Specialized.NameValueCollection)o;
			string[] ret = obj.AllKeys;
			ToLua.Push(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index AllKeys on a nil value");
		}
	}
}

