using UnityEngine;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;
using System.Collections.Generic;

public class LobbyManager : MonoBehaviour
{
    public static LobbyManager Instance; // Création du Singleton

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

    // Méthode publique qui crée un lobby sur Unity services
    public async void CreateLobby(LobbyData lobbyData)
    {
        // Il est possible d'ajouter des options au lobby
        CreateLobbyOptions lobbyOptions = new CreateLobbyOptions();
        lobbyOptions.IsPrivate = false;  // EN placant true un code de connexion serait requis
        // Dans les options on peut aussi transmettre des Datas au Lobby
        lobbyOptions.Data = new Dictionary<string, DataObject>();  //ici je crée une data de type dictionnaire

        // Je peux me servir de ces data envoyé au lobby pour lui transmettre mon joinCode de mon Relay !
        string joinCode = await RelayManager.Instance.CreateRelayGame(lobbyData.maxPlayer);

        // Je créer un DataObject public qui contient le joincode et l'ajoute au dictionnaire
        DataObject dataObject = new DataObject(DataObject.VisibilityOptions.Public, joinCode);
        lobbyOptions.Data.Add("JoinCodeKey", dataObject);

        // Je créer le Lobby sur le Relay
        await Lobbies.Instance.CreateLobbyAsync(lobbyData.lobbyName, lobbyData.maxPlayer,lobbyOptions);
    }

    // Méthode publique pour la partie rapide qui rejoint le premier Lobby
    public async void PartieRapideLobby()
    {
        // Récupère le premier lobby dans la variable lobby
        Lobby lobby = await Lobbies.Instance.QuickJoinLobbyAsync();
        // Va chercher le Joincode du lobby récupéré
        string relayJoinCode = lobby.Data["JoinCodeKey"].Value;
        Debug.Log(relayJoinCode);
        // Rejoint le lobby avec le joinCode récupéré
        RelayManager.Instance.JoinRelayGame(relayJoinCode);
    }

    // Méthode publique pour rejoindre un Lobby précis
    public async void RejoindreLobby(string lobbyID)
    {
        // Récupère le lobby avec le ID reçu en paramètre
        Lobby lobby = await Lobbies.Instance.JoinLobbyByIdAsync(lobbyID);
        // Va chercher le Joincode du lobby récupéré
        string relayJoinCode = lobby.Data["JoinCodeKey"].Value;
        // Rejoint le lobby avec le joinCode récupéré
        RelayManager.Instance.JoinRelayGame(relayJoinCode);
    }
}

