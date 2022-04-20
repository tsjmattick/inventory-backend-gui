using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolTipSystem : MonoBehaviour
{
    private static ToolTipSystem _current;

    public Tooltip tooltip;
    private void Awake()
    {
        _current = this;
    }

    public static void Show(string content, string header = "")
    {
        _current.tooltip.SetText(content, header);
        _current.tooltip.gameObject.SetActive(true);
    }

    public static void Hide()
    {
        _current.tooltip.gameObject.SetActive(false);
    }
    
}
