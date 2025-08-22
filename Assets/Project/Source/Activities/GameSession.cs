using System;
using System.Collections;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    [SerializeField] private float _autoSaveInterval = 30f;
    [SerializeField] private AudioPlayComponent _musicSource;
    [SerializeField] private AudioPlayComponent _sfxSource;

    public AudioPlayComponent MusicSource => _musicSource;
    public AudioPlayComponent SfxSource => _sfxSource;
    public static GameSession I { get; private set; }
    public PlayerInventoryPresenter PlayerInventory { get; private set; }
    
    public BuildingGridPresenter BuildingGridPresenter { get; set; }

    public event Action<PlayerInventoryPresenter> OnInventoryChanged;
    
    public BuildingsMediator BuildingsMediator { get; private set; }
    
    private bool _needsSave;
    private JsonBuildingGridSaver _jsonBuildingGridSaver = new();
    private JsonPlayerInventorySaver _jsonPlayerInventorySaver = new();
    
    private void Awake()
    {
        if (I != null)
        {
            Destroy(gameObject);
            return;
        }

        I = this;
        DontDestroyOnLoad(gameObject);

        PlayerInventory = _jsonPlayerInventorySaver.Load();
        BuildingsMediator = new BuildingsMediator();
        
        // if no coins add some to inventory 
        // need for starting the game
        if(PlayerInventory.FindResourceInInventory("_coin") == null || 
           PlayerInventory.FindResourceInInventory("_coin").Amount <= 0)
        {
            PlayerInventory.AddResource("_coin", 5);
            Debug.Log("Added 5 coins to inventory.");
        }
        
        StartCoroutine(AutoSaveRoutine());
    }

    private void Start()
    {
        _musicSource = GameObject.FindWithTag("Music").GetComponent<AudioPlayComponent>();
        _sfxSource = GameObject.FindWithTag("Sfx").GetComponent<AudioPlayComponent>();
    }

    public void SaveSession()
    {
        _jsonPlayerInventorySaver.Save(PlayerInventory);
        
        if(BuildingGridPresenter != null)
            _jsonBuildingGridSaver.Save(new BuildingsSaveData()
            {
                Buildings = BuildingGridPresenter.PlacedBuildings,
            });
        
        Debug.Log("Session saved.");
    }

    public void NotifyInventoryChanged()
    {
        _needsSave = true;
        OnInventoryChanged?.Invoke(PlayerInventory);
    }
    
    private IEnumerator AutoSaveRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(_autoSaveInterval);
            if (_needsSave)
            {
                SaveSession();
                _needsSave = false;
            }
        }
    }
    
    private void OnApplicationQuit()
    {
        if (_needsSave) 
            SaveSession();
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause && _needsSave)
            SaveSession();
    }
}
