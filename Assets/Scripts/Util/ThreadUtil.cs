using System;
using System.Threading;

/// <summary>
/// 线程管理 后续加入线程池
/// </summary>
public class ThreadUtil : Singleton<ThreadUtil>
{
    /// <summary>
    /// 主线程
    /// </summary>
    public SynchronizationContext MainThreadSynContext;

    public void Load()
    {
        MainThreadSynContext = SynchronizationContext.Current;
    }

    #region 通知主线程回调
    public void PostMainThreadAction(Action action)
    {
        MainThreadSynContext.Post(new SendOrPostCallback((o) =>
        {
            Action e = (Action)o.GetType().GetProperty("action").GetValue(o);
            if (e != null) e();
        }), new { action = action });
    }
    public void PostMainThreadAction<T>(Action<T> action, T arg1)
    {
        MainThreadSynContext.Post(new SendOrPostCallback((o) =>
        {
            Action<T> e = (Action<T>)o.GetType().GetProperty("action").GetValue(o);
            T t1 = (T)o.GetType().GetProperty("arg1").GetValue(o);
            if (e != null) e(t1);
        }), new { action = action, arg1 = arg1 });
    }
    public void PostMainThreadAction<T1, T2>(Action<T1, T2> action, T1 arg1, T2 arg2)
    {
        MainThreadSynContext.Post(new SendOrPostCallback((o) =>
         {
             Action<T1, T2> e = (Action<T1, T2>)o.GetType().GetProperty("action").GetValue(o);
             T1 t1 = (T1)o.GetType().GetProperty("arg1").GetValue(o);
             T2 t2 = (T2)o.GetType().GetProperty("arg2").GetValue(o);
             if (e != null) e(t1, t2);
         }), new { action = action, arg1 = arg1, arg2 = arg2 });
    }
    public void PostMainThreadAction<T1, T2, T3>(Action<T1, T2, T3> action, T1 arg1, T2 arg2, T3 arg3)
    {
        MainThreadSynContext.Post(new SendOrPostCallback((o) =>
         {
             Action<T1, T2, T3> e = (Action<T1, T2, T3>)o.GetType().GetProperty("action").GetValue(o);
             T1 t1 = (T1)o.GetType().GetProperty("arg1").GetValue(o);
             T2 t2 = (T2)o.GetType().GetProperty("arg2").GetValue(o);
             T3 t3 = (T3)o.GetType().GetProperty("arg3").GetValue(o);
             if (e != null) e(t1, t2, t3);
         }), new { action = action, arg1 = arg1, arg2 = arg2, arg3 = arg3 });
    }
    public void PostMainThreadAction<T1, T2, T3, T4>(Action<T1, T2, T3, T4> action, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
    {
        MainThreadSynContext.Post(new SendOrPostCallback((o) =>
         {
             Action<T1, T2, T3, T4> e = (Action<T1, T2, T3, T4>)o.GetType().GetProperty("action").GetValue(o);
             T1 t1 = (T1)o.GetType().GetProperty("arg1").GetValue(o);
             T2 t2 = (T2)o.GetType().GetProperty("arg2").GetValue(o);
             T3 t3 = (T3)o.GetType().GetProperty("arg3").GetValue(o);
             T4 t4 = (T4)o.GetType().GetProperty("arg4").GetValue(o);
             if (e != null) e(t1, t2, t3, t4);
         }), new { action = action, arg1 = arg1, arg2 = arg2, arg3 = arg3, arg4 = arg4 });
    }
    #endregion


}