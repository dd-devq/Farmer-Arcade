using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinUI : BaseUI
{
    public TextMeshProUGUI MessageText;
    protected override void OnShow(UIParam param = null)
    {
        base.OnShow(param);
        string message = (string)param.Data;
        MessageText.SetText(message);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainUI");
    }
}
