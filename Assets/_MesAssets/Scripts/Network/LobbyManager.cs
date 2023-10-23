using UnityEngine;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;
using System.Collections.Generic;

public class LobbyManager : MonoBehaviour
{
    public static LobbyManager Instance; // Cr�ation du Singleton

    // Singleton
    private void Awake()
    {
        Instance = this;
    }

    // Structure complexe qui contient l'information sur un lobby
    public struct LobbyData
    {
        public string lobbyName;
        public int maxPlayer;
    }

    // M�thode publique qui cr�e un lobby sur Unity services
    public async void CreateLobby(LobbyData lobbyData)
    {
        // Il est possible d'ajouter des options au lobby
        CreateLobbyOptions lobbyOptions = new CreateLobbyOptions();
        lobbyOptions.IsPrivate = false;  // EN placant true un code de connexion serait requis
        // Dans les options on peut aussi transmettre des Datas au Lobby
        lobbyOptions.Data = new Dictionary<string, DataObject>();  //ici je cr�e une data de type dictionnaire

        // Je peux me servir de ces data envoy� au lobby pour lui transmettre mon joinCode de mon Relay !
        string joinCode = await RelayManager.Instance.CreateRelayGame(lobbyData.maxPlayer);

        // Je cr�er un DataObject public qui contient le joincode et l'ajoute au dictionnaire
        DataObject dataObject = new DataObject(DataObject.VisibilityOptions.Public, joinCode);
        lobbyOptions.Data.Add("JoinCodeKey", dataObject);

        // Je cr�er le Lobby sur le Relay
        await Lobbies.Instance.CreateLobbyAsync(lobbyData.lobbyName, lobbyData.maxPlayer,lobbyOptions);
    }

    // M�thode publique pour la partie rapide qui rejoint le premier Lobby
    public async void PartieRapideLobby()
    {
        // R�cup�re le premier lobby dans la variable lobby
        Lobby lobby = await Lobbies.Instance.QuickJoinLobbyAsync();
        // Va chercher le Joincode du lobby r�cup�r�
        string relayJoinCode = lobby.Data["JoinCodeKey"].Value;
        Debug.Log(relayJoinCode);
        // Rejoint le lobby avec le joinCode r�cup�r�
        RelayManager.Instance.JoinRelayGame(relayJoinCode);
    }

    // M�thode publique pour rejoindre un Lobby pr�cis
    public async void RejoindreLobby(string lobbyID)
    {
        // R�cup�re le lobby avec le ID re�u en param�tre
        Lobby lobby = await Lobbies.Instance.JoinLobbyByIdAsync(lobbyID);
        // Va chercher le Joincode du lobby r�cup�r�
        string relayJoinCode = lobby.Data["JoinCodeKey"].Value;
        // Rejoint le lobby avec le joinCode r�cup�r�
        RelayManager.Instance.JoinRelayGame(relayJoinCode);
    }
}

