using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class MonoComponent : MonoBehaviour
{
    public event Action AwakeEvent;
    public event Action StartEvent;
    public event Action OnEnableEvent;
    public event Action OnDisableEvent;
    public event Action OnDestroyEvent;
    public event Action UpdateEvent;
    public event Action FixedUpdateEvent;

    public event Action<bool> OnPauseEvent;
    public event Action<bool> OnFocusEvent;

    public event Action<Collider> OnTriggerEnterEvent;
    public event Action<Collider> OnTriggerStayEvent;
    public event Action<Collider> OnTriggerExitEvent;
    public event Action<Collision> OnCollisionEnterEvent;
    public event Action<Collision> OnCollisionStayEvent;
    public event Action<Collision> OnCollisionExitEvent;

    public event Action<Collider2D> OnTriggerEnter2DEvent;
    public event Action<Collider2D> OnTriggerStay2DEvent;
    public event Action<Collider2D> OnTriggerExit2DEvent;
    public event Action<Collision2D> OnCollisionEnter2DEvent;
    public event Action<Collision2D> OnCollisionStay2DEvent;
    public event Action<Collision2D> OnCollisionExit2DEvent;


    #region 生命周期函数
    protected void Awake()
    {
        if (AwakeEvent != null) AwakeEvent();
    }
    protected void Start()
    {
        if (StartEvent != null) StartEvent();
    }
    protected void OnEnable()
    {
        if (OnEnableEvent != null) OnEnableEvent();
    }
    protected void OnDisable()
    {
        if (OnDisableEvent != null) OnDisableEvent();
    }
    protected void OnDestroy()
    {
        if (OnDestroyEvent != null) OnDestroyEvent();
    }
    protected void Update()
    {
        if (UpdateEvent != null) UpdateEvent();
    }
    protected void FixedUpdate()
    {
        if (FixedUpdateEvent != null) FixedUpdateEvent();
    }

    private void OnApplicationPause(bool pauseStatus)
    {
        if (OnPauseEvent != null) OnPauseEvent(pauseStatus);
    }
    private void OnApplicationFocus(bool focusStatus)
    {
        if (OnFocusEvent != null) OnFocusEvent(focusStatus);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (OnTriggerEnterEvent != null) OnTriggerEnterEvent(other);
    }
    private void OnTriggerStay(Collider other)
    {
        if (OnTriggerStayEvent != null) OnTriggerStayEvent(other);
    }
    private void OnTriggerExit(Collider other)
    {
        if (OnTriggerExitEvent != null) OnTriggerExitEvent(other);
    }
    private void OnCollisionEnter(Collision other)
    {
        if (OnCollisionEnterEvent != null) OnCollisionEnterEvent(other);
    }
    private void OnCollisionStay(Collision other)
    {
        if (OnCollisionStayEvent != null) OnCollisionStayEvent(other);
    }
    private void OnCollisionExit(Collision other)
    {
        if (OnCollisionExitEvent != null) OnCollisionExitEvent(other);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (OnTriggerEnter2DEvent != null) OnTriggerEnter2DEvent(other);
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (OnTriggerStay2DEvent != null) OnTriggerStay2DEvent(other);
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (OnTriggerExit2DEvent != null) OnTriggerExit2DEvent(other);
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (OnCollisionEnter2DEvent != null) OnCollisionEnter2DEvent(other);
    }
    private void OnCollisionStay2D(Collision2D other)
    {
        if (OnCollisionStay2DEvent != null) OnCollisionStay2DEvent(other);
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        if (OnCollisionExit2DEvent != null) OnCollisionExit2DEvent(other);
    }
    #endregion

}
