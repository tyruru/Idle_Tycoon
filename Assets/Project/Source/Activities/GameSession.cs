using System;
using System.Collections;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    [SerializeField] private float _autoSaveInterval = 30f;

    public static GameSession I { get; private set; }
    public PlayerInventoryPresenter PlayerInventory { get; private set; }
    
    public BuildingGridPresenter BuildingGridPresenter { get; set; }

    public event Action<PlayerInventoryPresenter> OnInventoryChanged;
    
    public BuildingsMediator BuildingsMediator { get; private set; }
    
    private bool _needsSave;
    
    private void Awake()
    {
        if (I != null)
        {
            Destroy(gameObject);
            return;
        }

        I = this;
        DontDestroyOnLoad(gameObject);

        PlayerInventory = JsonPlayerInventorySaver.Load();
        BuildingsMediator = new BuildingsMediator();
        
        StartCoroutine(AutoSaveRoutine());
    }
    
    public void SaveSession()
    {
        JsonPlayerInventorySaver.Save(PlayerInventory);
        JsonBuildingGridSaver.Save(new BuildingsSaveData()
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
