using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrecnyManager : MonoBehaviour
{
    public static CurrecnyManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddCryptocurrency(float amount)
    {
        float savedCryptoValue = LoadCryptoCurrency();
        savedCryptoValue += amount;
        SaveCryptoCurrency(savedCryptoValue);
    }

    public void SaveCryptoCurrency(float value)
    {
        PlayerPrefs.SetFloat("CryptoCurrency", value);
        PlayerPrefs.Save();
    }

    public float LoadCryptoCurrency()
    {
        if (PlayerPrefs.HasKey("CryptoCurrency"))
        {
            return PlayerPrefs.GetFloat("CryptoCurrency");
        }
        else
        {
            return 0f;
        }
    }
}
