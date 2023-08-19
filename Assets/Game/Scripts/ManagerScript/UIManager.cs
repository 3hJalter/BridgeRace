///
/// Create by linh soi - Abi Game studio
/// mentor Minh tito - CTO Abi Game studio
/// 
/// Manage list UI canvas for easy to use
/// Member nen inherit UI canvas
/// 
/// Update: 09-10-2020 
///             manage UI with Generic
///         09-10-2021 
///             Open, Close UI with Typeof(T)
///         28/11/2022
///             Close All UI
///             Close delay time
///

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class UIManager : Singleton<UIManager>
{
    //canvas container, it should be a canvas - root
    //canvas chua dung cac canvas con, nen la mot canvas - root de chua cac canvas nay
    public Transform canvasParentTf;

    //dict for UI active
    //dict luu cac ui dang dung
    private readonly Dictionary<Type, UICanvas> _uiCanvas = new();

    //dict for quick query UI prefab
    //dict dung de lu thong tin prefab canvas truy cap cho nhanh
    private readonly Dictionary<Type, UICanvas> _uiCanvasPrefab = new();

    //list from resource
    //list load ui resource
    private UICanvas[] _uiResources;

    #region Canvas

    //open UI
    //mo UI canvas
    public T OpenUI<T>() where T : UICanvas
    {
        UICanvas canvas = GetUI<T>();

        canvas.Setup();
        canvas.Open();

        return (T)canvas;
    }

    //close UI directly
    //dong UI canvas ngay lap tuc
    public void CloseUI<T>() where T : UICanvas
    {
        if (IsOpened<T>()) GetUI<T>().CloseDirectly();
    }

    //close UI with delay time
    //dong ui canvas sau delay time
    public void CloseUI<T>(float delayTime) where T : UICanvas
    {
        if (IsOpened<T>()) GetUI<T>().Close(delayTime);
    }

    //check UI is Opened
    //kiem tra UI dang duoc mo len hay khong
    public bool IsOpened<T>() where T : UICanvas
    {
        return IsLoaded<T>() && _uiCanvas[typeof(T)].gameObject.activeInHierarchy;
    }

    //check UI is loaded
    //kiem tra UI da duoc khoi tao hay chua
    public bool IsLoaded<T>() where T : UICanvas
    {
        Type type = typeof(T);
        return _uiCanvas.ContainsKey(type) && _uiCanvas[type] != null;
    }

    //Get component UI 
    //lay component cua UI hien tai
    public T GetUI<T>() where T : UICanvas
    {
        if (!IsLoaded<T>())
        {
            UICanvas canvas = Instantiate(GetUIPrefab<T>(), canvasParentTf);
            _uiCanvas[typeof(T)] = canvas;
        }

        return _uiCanvas[typeof(T)] as T;
    }

    //Close all UI
    //dong tat ca UI ngay lap tuc -> tranh truong hop dang mo UI nao dong ma bi chen 2 UI cung mot luc
    public void CloseAll()
    {
        foreach (var item in _uiCanvas.Where(item => item.Value != null && item.Value.gameObject.activeInHierarchy))
            item.Value.CloseDirectly();
    }

    //Get prefab from resource
    //lay prefab tu Resources/UI 
    private T GetUIPrefab<T>() where T : UICanvas
    {
        if (_uiCanvasPrefab.ContainsKey(typeof(T))) return _uiCanvasPrefab[typeof(T)] as T;
        _uiResources ??= Resources.LoadAll<UICanvas>("UI/");

        foreach (UICanvas t in _uiResources)
            if (t is T)
            {
                _uiCanvasPrefab[typeof(T)] = t;
                break;
            }

        return _uiCanvasPrefab[typeof(T)] as T;
    }

    #endregion

    #region Back Button

    private readonly Dictionary<UICanvas, UnityAction> BackActionEvents = new();
    private readonly List<UICanvas> backCanvas = new();

    private UICanvas BackTopUI
    {
        get
        {
            UICanvas canvas = null;
            if (backCanvas.Count > 0) canvas = backCanvas[backCanvas.Count - 1];

            return canvas;
        }
    }


    private void LateUpdate()
    {
        if (Input.GetKey(KeyCode.Escape) && BackTopUI != null) BackActionEvents[BackTopUI]?.Invoke();
    }

    public void PushBackAction(UICanvas canvas, UnityAction action)
    {
        if (!BackActionEvents.ContainsKey(canvas)) BackActionEvents.Add(canvas, action);
    }

    public void AddBackUI(UICanvas canvas)
    {
        if (!backCanvas.Contains(canvas)) backCanvas.Add(canvas);
    }

    public void RemoveBackUI(UICanvas canvas)
    {
        backCanvas.Remove(canvas);
    }

    /// <summary>
    ///     CLear backey when comeback index UI canvas
    /// </summary>
    public void ClearBackKey()
    {
        backCanvas.Clear();
    }

    #endregion
}