using System;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : SingletonMono<UIManager>
{
    private Dictionary<UIIndex, BaseUI> _uiDictionary;
    [SerializeField] private List<BaseUI> listUI = new();

    public UIIndex currentUIIndex;

    public void Awake()
    {
        Application.targetFrameRate = 60;
        _uiDictionary = new Dictionary<UIIndex, BaseUI>();
    }

    private void Start()
    {
        currentUIIndex = UIIndex.None;
        InitUI(null);
    }

    private void InitUI(Action callback)
    {
        foreach (var ui in listUI)
        {
            var goUI = ui.gameObject;
            goUI.transform.SetParent(transform, false);

            var baseUI = goUI.GetComponent<BaseUI>();
            baseUI.OnInit();

            _uiDictionary.Add(baseUI.index, baseUI);

            goUI.SetActive(false);
        }

        callback?.Invoke();

        var isUILoaded = UnityEngine.SceneManagement.SceneManager.GetSceneByName("MainUI").isLoaded;
        if (isUILoaded)
        {
            ShowUI(UIIndex.MainMenu);
        }
        else
        {
            ShowUI(UIIndex.HUD);
        }
    }

    public void ShowUI(UIIndex uiIndex, UIParam param = null, Action callback = null)
    {
        var ui = GetUI(uiIndex);
        currentUIIndex = uiIndex;
        ui.ShowUI(param, callback);
    }

    public void HideUI(BaseUI baseUI, Action callback = null)
    {
        currentUIIndex = UIIndex.None;
        baseUI.HideUI(callback);
    }

    public void HideUI(UIIndex uiIndex, Action callback = null)
    {
        currentUIIndex = UIIndex.None;
        var ui = GetUI(uiIndex);
        ui.HideUI(callback);
    }

    public BaseUI GetUI(UIIndex uiIndex)
    {
        return _uiDictionary[uiIndex];
    }
}
