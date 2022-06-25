using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreSave : MonoBehaviour
{
    public static bool GetBuyingStatus(int num)
    {
        var dataRaw = PlayerPrefs.GetString($"StoreData_{num}");
        var gameStorageData = JsonUtility.FromJson<StoreSaveData>(dataRaw);
        return gameStorageData.buyItem;
    }

    public static void PrepateGameStorageData(int index, string itemName, bool isBuying)
    {
        var gameStorageData = GetGameStorageData(index, itemName, isBuying);
        var gameStorageRaw = JsonUtility.ToJson(gameStorageData, true);
        PlayerPrefs.SetString($"StoreData_{index}", gameStorageRaw);
    }

    //public static void SaveData(string json)
    //{
    //    PlayerPrefs.SetString("StorageData", json);
    //}

    public static StoreSaveData GetGameStorageData(int index, string itemName, bool isBuying)
    {
        return new StoreSaveData()
        {
            idx = index,
            name = itemName,
            buyItem = isBuying,
        };
    }
}


public class StoreSaveData
{
    public int idx;
    public string name;
    public bool buyItem;
}