using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ShopSkinHairUI : ShopSkinUI<SkinHatSO>
{
    [SerializeField] private Button btn;

    private void Start()
    {
        btn.onClick.AddListener(ResetButton);
    }

    private void ResetButton()
    {
        BuySkinButton.Ins.SetShopSkinTag(ShopSkinTag.hair);
        BuySkinButton.Ins.SetSkinHairSO(skinSO);

        shopItemID = skinSO.ID;

        bool isUnlocked = Pref.GetBool(PrefConst.SKINHAIR_PEFIX + shopItemID);

        if (isUnlocked)
        {
            if (shopItemID == Pref.CurHairId) this.ShopSkinItemAction("UnEqqip",frame,ShopManager.Ins.imageButtonUnEquip);
            else this.ShopSkinItemAction("Select",frame, ShopManager.Ins.imageButtonSelect);
        }
        else
        {
            this.ShopSkinItemAction(skinSO.price.ToString(),frame, ShopManager.Ins.imageButtonBuy);
        }
    }


    public override void SetInfoItem(int currentIndex)
    {
        hud.sprite = SOManager.Ins.skinHairS0[currentIndex].hud;

        skinSO = SOManager.Ins.skinHairS0[currentIndex];

    }


}
