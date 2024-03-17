using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Diagnostics;

public class Managers : MonoBehaviour
{

    public static Managers s_instance = null;
    public static Managers Instance { get { return s_instance; } }

    private static ResourceManager s_resourceManager = new ResourceManager();
    private static PoolManager s_poolManager = new PoolManager();
    private static UIManager s_uiManager = new UIManager();
    private static SoundManager s_soundManager = new SoundManager();
    private static GameManager s_gameManager = new GameManager();
    
    public static ResourceManager Resource { get { Init(); return s_resourceManager; }}
    public static PoolManager Pool { get { Init(); return s_poolManager; }}
    public static UIManager UI { get { Init(); return s_uiManager; }}
    public static SoundManager Sound { get { Init(); return s_soundManager; }}
    public static GameManager Game { get { Init(); return s_gameManager; }}
    
    private void Start()
    {
        Init();
    }

    private static void Init()
    {
        if (s_instance == null)
        {
            GameObject go = GameObject.Find("@Managers");
            if (go == null)
                go = new GameObject { name = "@Managers" };

            s_instance = Utils.GetOrAddComponent<Managers>(go);
            DontDestroyOnLoad(go);
            
            s_resourceManager.Init();
            s_soundManager.Init();
            s_poolManager.Init();
        }
    }
}
