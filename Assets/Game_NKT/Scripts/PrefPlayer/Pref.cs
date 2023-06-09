﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//chỉ lấy dữ liệu nên ko cần khởi tạo trong game => không cần kế thừa monobehaviour
public class Pref 
{
    public static int CurWeaponId
    {
        set => PlayerPrefs.SetInt(PrefConst.CUR_WEAPON_ID, value);

        get => PlayerPrefs.GetInt(PrefConst.CUR_WEAPON_ID,0);
    }
    
    public static int CurHairId
    {
        set => PlayerPrefs.SetInt(PrefConst.CUR_SKINHAIR_ID, value);

        get => PlayerPrefs.GetInt(PrefConst.CUR_SKINHAIR_ID,-1);
    }
    public static int CurPantId
    {
        set => PlayerPrefs.SetInt(PrefConst.CUR_SKINPANT_ID, value);

        get => PlayerPrefs.GetInt(PrefConst.CUR_SKINPANT_ID,-1);
    }
    public static int CurShieldId
    {
        set => PlayerPrefs.SetInt(PrefConst.CUR_SKINSHIELD_ID, value);

        get => PlayerPrefs.GetInt(PrefConst.CUR_SKINSHIELD_ID, -1);
    }


    public static int Coins
    {
        set => PlayerPrefs.SetInt(PrefConst.COIN_KEY, value);

        get => PlayerPrefs.GetInt(PrefConst.COIN_KEY, 1000);
    }

    public static string NamePlayer
    {
        set => PlayerPrefs.SetString(PrefConst.NAME_PLAYER, value);

        get => PlayerPrefs.GetString(PrefConst.NAME_PLAYER, "???");
    }


    public static void SetBool(string key , bool isOn)
    {
        if (isOn) PlayerPrefs.SetInt(key, 1);

        else PlayerPrefs.SetInt(key, 0);
    }

    public static bool GetBool(string key)
    {
        return PlayerPrefs.GetInt(key) == 1 ? true : false ;
    }
}

