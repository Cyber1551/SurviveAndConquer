using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInventoryItem
{
    string name { get; }

    Sprite Image { get; }

}
public class InventoryEventArgs : EventArgs {

	public InventoryEventArgs(IInventoryItem _item)
    {
        item = _item;
    }
    public IInventoryItem item;
}
