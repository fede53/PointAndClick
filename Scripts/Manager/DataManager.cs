using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance { get; private set; }
    public Inventory Inventory { get { return inventory; } }

    [SerializeField] Inventory inventory;

    public event System.Action OnSave = delegate { };
    public event System.Action OnLoad = delegate { };

    public string PrevSceneName { get; private set; }
    public LevelManager LevelManager { get; private set; }

    private int saveDataId = 0;
    //private List<SaveData> saveDatas = new List<SaveData>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        LevelManager = GetComponentInChildren<LevelManager>();
    }

    public void SetPrevScene(string name)
    {
        PrevSceneName = name;
    }
}