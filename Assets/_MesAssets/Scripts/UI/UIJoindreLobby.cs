using UnityEngine;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;

public class UIJoindreLobby : MonoBehaviour
{
    //Conteneur des boutons
    [SerializeField] private Transform _contentParent = default;
    [SerializeField] private LobbyListElement _lobbyListElementPrefab = default;
    [SerializeField] private float _tempsRafraichissement = 2f;

    private float _timer = 0;  // Variable de temps pour le rafraichissement de la liste

    // Afficher la liste des lobby quand on active le panneau JoindreLobby
    private void OnEnable()
    {
        UpdateLobbyList();
    }

    private void Update()
    {
        // Lance le update à chaque temps de rafraichissement
        if (_timer > _tempsRafraichissement)
        {
            UpdateLobbyList();
            _timer -= _tempsRafraichissement;
        }
        _timer += Time.deltaTime;
    }

    // Méthode qui met à jour la liste des lobbys disponibles 
    public async void UpdateLobbyList()
    {
        // Interroge le service Lobby pour récupérer tous les lobbys
        QueryResponse response = await Lobbies.Instance.QueryLobbiesAsync();

        // On commence par effacer tous les boutons avant de remettre les actuels
        for (int i = 0; i < _contentParent.childCount; i++)
        {
            Destroy(_contentParent.GetChild(i).gameObject);
        }

        // Pour chaque Lobby récupérer dans response
        foreach (var lobby in response.Results)
        {
            // On instancie le prefab à l'intérieur du content et le place dans la variable
            LobbyListElement spawnElement = Instantiate(_lobbyListElementPrefab, _contentParent);
            spawnElement.Initialiser(lobby);
        }
    }
}

