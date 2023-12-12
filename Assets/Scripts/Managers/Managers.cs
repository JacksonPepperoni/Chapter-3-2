using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Managers : MonoBehaviour
{
    private static bool initialized;

    static Managers s_instance; // 유일성이 보장된다
    public static Managers Instance
    {
        get
        {
            if (!initialized)
            {
                initialized = true;

                GameObject go = GameObject.Find("@Managers");

                if (go == null)
                {
                    go = new GameObject { name = "@Managers" };
                    go.AddComponent<Managers>();
                    DontDestroyOnLoad(go);
                    s_instance = go.GetComponent<Managers>();
                }
            }
            return s_instance;
        }
    }
    private readonly SceneManagerEx _scene = new();
    private readonly InputManager _input = new();
    private readonly PoolManager _pool = new();
    private readonly ResourceManager _resource = new();
    //  private readonly ObjectManager _objects = new();
    private readonly UIManager _ui = new();


    public static InputManager Input => Instance?._input;
    public static PoolManager Pool => Instance?._pool;
    public static ResourceManager Resource => Instance?._resource;
    //   public static ObjectManager Object => Instance?._objects;
    public static UIManager UI => Instance?._ui;

    public static SceneManagerEx Scene => Instance?._scene;

    public static void Clear()
    {
        Pool.Clear();
        //   Object.Clear();
    }
}
