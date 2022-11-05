using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;
using Steamworks;
using System;

namespace BrettArnett
{
    public class SteamInventoryListItem : MonoBehaviour
    {
        public string itemName;
        public string itemSpecialization;
        public int itemIdNumber; // This is here to reference if you want it. The player won't have any use for knowing the game's ID numbers of Steam items. You can use it to debug.
        public SteamItemInstanceID_t itemInstanceID;
        public int itemStackQuantity;

        public GameObject DeleteButton;

        [SerializeField] private Text ItemNameText;
        [SerializeField] private Text ItemStackQuantityText;

        public void SetSteamInventoryListItemValues()
        {
            ItemNameText.text = itemName;
            ItemStackQuantityText.text = itemStackQuantity.ToString();

            if ((uint)itemInstanceID == 0)
            {
                Destroy(DeleteButton);
            }

            if (itemStackQuantity < 2) // You may wish to disable this, then each item with a stack size of 1 will show that there is "1" item
            {
                ItemStackQuantityText.enabled = false;
            }
        }

        public void SelectSteamInventoryItem()
        {
            if (itemSpecialization == "Hat")
            {
                Debug.Log("Hat selected");
            }
            else if (itemSpecialization == "Other")
            {
                Debug.Log("Other selected");
            }
            else
            {
                Debug.Log("No specialization selected");
            }
        }

        public void DeleteItem() // Note: deletion can be slow, giving delayed results. You may wish to prompt every deletion with a delay/loading screen to be more clear.
        {
            SteamInventory.ConsumeItem(out SteamInventoryResult_t pDeleteResult, itemInstanceID, (UInt32)1);

            itemStackQuantity -= 1;
            ItemStackQuantityText.text = itemStackQuantity.ToString();

            Debug.Log("Deleted 1 item: " + itemName);

            if (itemStackQuantity < 1)
            {
                Destroy(this.gameObject);
            }
        }
    }
}