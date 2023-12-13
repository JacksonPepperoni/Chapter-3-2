using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BaseScene : MonoBehaviour
{
    public UI_Scene UI { get; protected set; }

    private bool _Initialized = false;

    void Start()
    {
        if (Managers.Resource.Loaded)
        {
            Managers.Data.Initialize();
          //  Managers.Game.Initialize();
            Initialize();
        }
        else
        {
            Managers.Resource.LoadAllAsync<UnityEngine.Object>("PreLoad", (key, count, totalCount) =>
            {
               // Debug.Log($"[GameScene] Load asset {key} ({count}/{totalCount})");
                if (count >= totalCount)
                {
                    Managers.Resource.Loaded = true;
                  Managers.Data.Initialize();
                //    Managers.Game.Initialize();
                    Initialize();
                }
            });
        }
    }

    protected virtual bool Initialize()
    {
        if (_Initialized) return false;

        Managers.Scene.CurrentScene = this;

        Object obj = GameObject.FindObjectOfType<EventSystem>();
        if (obj == null) Managers.Resource.InstantiatePrefab("EventSystem.prefab").name = "@EventSystem";

        _Initialized = true;
        return true;
    }
}