using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupController : MonoBehaviour {
     
    private static PopupText popup;
    private static Canvas canvas;

    public static void Initialize(Canvas c)
    {
        canvas = c;
        popup = Resources.Load<PopupText>("PopupSpawn");
    }
    public static void CreateText(string txt)
    {
        if (canvas == null || popup == null) return;

        PopupText instance = Instantiate(popup);
        instance.transform.SetParent(canvas.transform, false);
        
        instance.transform.position += new Vector3(instance.transform.rotation.x, Random.Range(0.0f, 0.3f), instance.transform.rotation.z + Random.Range(-0.5f, 0.5f));
        
        instance.SetText(txt);
    }

}
