using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data : MonoBehaviour
{
    public static Data inst;
    [SerializeField] private Settings _settings;
    public static Settings settings{get { return inst._settings; }}

    private void Awake()
    {
        if (Data.inst != null)
        {
            DestroyImmediate(this.gameObject);
            return;
        }
        else
        {
            inst = this;
        }

        DontDestroyOnLoad(this);
    }
}