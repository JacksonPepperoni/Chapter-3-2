using UnityEngine;

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
    private readonly PoolManager _pool = new();
    private readonly ResourceManager _resource = new();
    //  private readonly ObjectManager _objects = new();
    private readonly UIManager _ui = new();
    private readonly DataManager _data = new();
    private readonly GameManager _game = new();

    public static PoolManager Pool => Instance?._pool;
    public static ResourceManager Resource => Instance?._resource;
    //   public static ObjectManager Object => Instance?._objects;
    public static UIManager UI => Instance?._ui;
    public static GameManager Game => Instance?._game;
    public static DataManager Data => Instance?._data;
    public static SceneManagerEx Scene => Instance?._scene;

    public static void Clear()
    {
        Pool.Clear();
        //   Object.Clear();
    }
}
