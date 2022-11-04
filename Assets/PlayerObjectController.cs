using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Steamworks;

public class PlayerObjectController : NetworkBehaviour
{
   [SyncVar] public int ConnectionID;
   [SyncVar] public int PlayerIdNumber;
   [SyncVar] public ulong PlayerSteamID;
   [SyncVar(hook = nameof(PlayerNameUpdate))] public string PlayerName;

   private CustomNetworkManager manager;

   private CustomNetworkManager Manager
   {
      get
      {
         if (manager != null)
         {
            return manager;
         }

         return manager = CustomNetworkManager.singleton as CustomNetworkManager;
      }
   }

   private void Start()
   {
      DontDestroyOnLoad(this.gameObject);
   }

   public override void OnStartAuthority()
   {
      Debug.Log("OnStartAuthority");
      CmdSetPlayerName(SteamFriends.GetPersonaName().ToString());
      gameObject.name = "LocalGamePlayer";
      LobbyController.Instance.FindLocalPlayer();
      LobbyController.Instance.UpdateLobbyName();
   }

   public override void OnStartClient()
   {
      Debug.Log("OnStartClient");
      Manager.GamePlayers.Add(this);
      LobbyController.Instance.UpdateLobbyName();
      LobbyController.Instance.UpdatePlayerList();
   }

   public override void OnStopClient()
   {
      Manager.GamePlayers.Remove(this);      
      LobbyController.Instance.UpdatePlayerList();
   }

   [Command]
   private void CmdSetPlayerName(string PlayerName)
   {
      this.PlayerNameUpdate(this.PlayerName,PlayerName);
   }
   
   public void PlayerNameUpdate(string OldValue, string NewValue)
   {
      if (isServer)
      {
         this.PlayerName = NewValue;
      }

      if (isClient)
      {
         LobbyController.Instance.UpdatePlayerList();
      }
   }

   public void CanStartGame(string SceneName)
   {
      if (hasAuthority)
      {
         CmdCanStartGame(SceneName);
      }
   }
   
   [Command]
   public void CmdCanStartGame(string SceneName)
   {
      manager.StartGame(SceneName);
   }
}
