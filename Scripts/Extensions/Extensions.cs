﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public static class Extensions
{
    public static bool IsMouseOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }

    public static Item CopyItem(Item item)
    {
        Item newItem = new Item(item.ItemId, item.ItemName, item.ItemDesc, item.ItemSprite, item.AllowMultple);

        return newItem;
    }

    public static void RunActions(Actions[] actions)
    {
        for (int i = 0; i < actions.Length; i++)
        {
            actions[i].Act();
        }
    }
}
