using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreView : MonoBehaviour
{
    [SerializeField] private List<WizardWeaponStoreData> _storeItems = new List<WizardWeaponStoreData>();
    [SerializeField] private WizardItem _storeItemPrefab;
    [SerializeField] private Transform _content;
    [SerializeField] private Button _closeShopButton;
    
    private List<WizardItem> _wizardItems = new List<WizardItem>();
    public static Action OnClosedShop;

    void Start()
    {
        PrepareStoreItems();
        _closeShopButton.onClick.AddListener(ClosedShop);
    }

    private void PrepareStoreItems()
    {
        _wizardItems.Clear();
        foreach(var storeData in _storeItems)
        {
            CreateStoreItem(storeData);
        }

    }

    private void ClosedShop()
    {
        OnClosedShop?.Invoke();
    }

    private void CreateStoreItem(WizardWeaponStoreData wizardStoreData)
    {
        var item = Instantiate(_storeItemPrefab, _content);
        item.InitData(wizardStoreData);
        item.InitCallback(OnBuyStoreItemHandler);
        _wizardItems.Add(item);

        StoreSave.PrepateGameStorageData(_wizardItems.Count - 1, wizardStoreData.Name, wizardStoreData.Buying);
    }

    private void OnBuyStoreItemHandler(WizardWeaponStoreData wizardStoreData)
    {
        Debug.Log($"[StoreView][OnBuyStoreItemHandler] player buy: {wizardStoreData.Name} for {wizardStoreData.Price}");
    }
}


[System.Serializable]
public class WizardWeaponStoreData
{
    [SerializeField] private int _index;
    [SerializeField] private string _name;
    [SerializeField] private string _description;
    [SerializeField] private int _price;
    [SerializeField] private Sprite _icon;
    [SerializeField] private bool _buying;

    public int Index => _index;
    public string Name => _name;
    public string Description => _description;
    public int Price => _price;
    public Sprite Icon => _icon;
    public bool Buying => StoreSave.GetBuyingStatus(_index);
}