using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;
using Steamworks;
using UnityEngine.SceneManagement;

namespace BrettArnett
{
    public class PlayerListItem : MonoBehaviour
    {
        public string playerName;
        public int ConnectionId;
        public int deathNbr;
        public int killNbr;
        public double damageDealt;
        public ulong playerSteamId;
        private bool avatarRetrieved;

        private bool bUIStatsEnabled = false;

        [SerializeField] private Text killTxt;
        [SerializeField] private Text deathTxt;
        [SerializeField] private Text scoreTxt;

        [SerializeField] private Text PlayerNameText;
        [SerializeField] private RawImage playerAvatar;

        protected Callback<AvatarImageLoaded_t> avatarImageLoaded;

        void Start()
        {
            avatarImageLoaded = Callback<AvatarImageLoaded_t>.Create(OnAvatarImageLoaded);
            
        }

        public void SetPlayerListItemValues()
        {
            PlayerNameText.text = playerName;
            
            if (SceneManager.GetActiveScene().name != "Scene_Lobby" && SceneManager.GetActiveScene().name != "Scene_Steamworks")
            {
                EnableUIStats();
                bUIStatsEnabled = true;
            }
            
            if(bUIStatsEnabled) UpdateUIStats();
            
            if (!avatarRetrieved)
                GetPlayerAvatar();
        }

        private void UpdateUIStats()
        {
            killTxt.text = killNbr.ToString();
            deathTxt.text = deathNbr.ToString();
            scoreTxt.text = Convert.ToString(damageDealt);
        }

        private void EnableUIStats()
        {
            killTxt.enabled = true;
            deathTxt.enabled = true;
            scoreTxt.enabled = true;
        }
        
        void GetPlayerAvatar()
        {
            int imageId = SteamFriends.GetLargeFriendAvatar((CSteamID)playerSteamId);

            if (imageId == -1)
            {
                return;
            }

            playerAvatar.texture = GetSteamImageAsTexture(imageId);
        }
        private Texture2D GetSteamImageAsTexture(int iImage)
        {
            Debug.Log("Executing GetSteamImageAsTexture for player: " + this.playerName);
            Texture2D texture = null;

            bool isValid = SteamUtils.GetImageSize(iImage, out uint width, out uint height);
            if (isValid)
            {
                Debug.Log("GetSteamImageAsTexture: Image size is valid?");
                byte[] image = new byte[width * height * 4];

                isValid = SteamUtils.GetImageRGBA(iImage, image, (int)(width * height * 4));

                if (isValid)
                {
                    Debug.Log("GetSteamImageAsTexture: Image size is valid for GetImageRBGA?");
                    texture = new Texture2D((int)width, (int)height, TextureFormat.RGBA32, false, true);
                    texture.LoadRawTextureData(image);
                    texture.Apply();
                }
            }
            avatarRetrieved = true;
            return texture;
        }
        private void OnAvatarImageLoaded(AvatarImageLoaded_t callback)
        {
            if (callback.m_steamID.m_SteamID == playerSteamId)
            {
                playerAvatar.texture = GetSteamImageAsTexture(callback.m_iImage);
            }
            else
            {
                return;
            }
        }
    }
}