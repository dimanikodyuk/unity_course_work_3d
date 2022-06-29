using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WizardItem : MonoBehaviour
{
    [SerializeField] private TMP_Text _titleText;
    [SerializeField] private TMP_Text _descriptionText;
    [SerializeField] private TMP_Text _priceText;
    [SerializeField] private Image _iconImage;
    [SerializeField] private Button _buyButton;
    [SerializeField] private bool _isBuying;


    private WizardWeaponStoreData _wizardStoreData;
    public WizardWeaponStoreData Data => _wizardStoreData;

    private System.Action<WizardWeaponStoreData> _onStoreItemClickCallback;

    public void InitData(WizardWeaponStoreData storeData)
    {
        _wizardStoreData = storeData;
        _titleText.text = storeData.Name;
        _descriptionText.text = storeData.Description;
        _priceText.text = string.Format(_priceText.text, storeData.Price);
        _iconImage.sprite = storeData.Icon;
        _isBuying = storeData.Buying;
    }

    public void InitCallback(System.Action<WizardWeaponStoreData> onStoreItemClickCallback)
    {
        _onStoreItemClickCallback = onStoreItemClickCallback;
    }

    void Start()
    {
        _buyButton.onClick.AddListener(OnBuyButtonClickHandler);
    }

    void Update()
    {
        //if (_isBuying == true)
        //{
        //    _buyButton.interactable = false;
        //}
    }

    private void OnBuyButtonClickHandler()
    {
        Debug.Log($"[OnBuyButtonClickHandler] buy {_wizardStoreData.Name} for ${_wizardStoreData.Price}");
        _onStoreItemClickCallback?.Invoke(_wizardStoreData);
        _isBuying = true;
        StoreSave.PrepateGameStorageData(_wizardStoreData.Index, _wizardStoreData.Name, _isBuying);
        Debug.Log($"INDEX: {_wizardStoreData.Index}");

        if (_wizardStoreData.Index == 0)
        {
            WeaponController.woodStaff = false;
            WeaponController.sorcerersStaff = true;
            WeaponController.goldStaff = false;
        }
        else if(_wizardStoreData.Index == 1)
        {
            WeaponController.woodStaff = false;
            WeaponController.sorcerersStaff = false;
            WeaponController.goldStaff = true;
        }
    }
}
