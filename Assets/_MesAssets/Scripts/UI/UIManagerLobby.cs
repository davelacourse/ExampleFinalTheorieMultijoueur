using UnityEngine;
using UnityEngine.UI;

public class UIManagerLobby : MonoBehaviour
{
    [Header("Panneaux UI")]
    [SerializeField] private GameObject _authentification = default;
    [SerializeField] private GameObject _menuLobby = default;
    [SerializeField] private GameObject _creerLobby = default;
    [SerializeField] private GameObject _rejoindreLobby = default;

    [Header("Boutons")]
    [SerializeField] private Button _partieRapideButton = default;
    [SerializeField] private Button _creerLobbyButton = default;
    [SerializeField] private Button _rejoindreLobbyButton = default;
    [SerializeField] private Button _retourJoindreButton = default;
    [SerializeField] private Button _retourCreerButton = default;

    private void Start()
    {
        ActiverUI(3); // Affiche le panneau d'authentification au départ
        // Appeler l'évènement SignIn de l'authentification
        AuthentificationManager.Instance.SignIn.AddListener(() => ActiverUI(0));

        // Rejoint le premier Lobby Actif
        _partieRapideButton.onClick.AddListener(() => LobbyManager.Instance.PartieRapideLobby());

        _creerLobbyButton.onClick.AddListener(() => ActiverUI(1));
        _rejoindreLobbyButton.onClick.AddListener(() => ActiverUI(2));
        _retourCreerButton.onClick.AddListener(() => ActiverUI(0));
        _retourJoindreButton.onClick.AddListener(() => ActiverUI(0));
    }

    // Permet d'activer le gameObject correspondant à l'index
    // et de déastiver les autres
    public void ActiverUI(int index)
    {
        GameObject[] uiElements = new GameObject[] { _menuLobby, _creerLobby, _rejoindreLobby, _authentification};
        for (int i = 0; i < uiElements.Length; i++)
        {
            uiElements[i].SetActive(i == index);
        }
    }
}

