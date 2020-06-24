using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public static class Extensions
{
    public static bool IsMouseOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }
}
