using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;

public class NetworkUI : MonoBehaviour
{
    // Repr�sente les 3 boutons de notre sc�ne
    [SerializeField] private Button hostButton = default;
    [SerializeField] private Button serverButton = default;
    [SerializeField] private Button clientButton = default;

    private void Start()
    {
        // souscrit � l'�v�nement sur le click du bouton Host et d�marrer la session Host
        hostButton.onClick.AddListener(() =>NetworkManager.Singleton.StartHost());

        // souscrit � l'�v�nement sur le click du bouton Host et d�marrer la session Host
        serverButton.onClick.AddListener(() => NetworkManager.Singleton.StartServer());

        // souscrit � l'�v�nement sur le click du bouton Host et d�marrer la session Host
        clientButton.onClick.AddListener(() => NetworkManager.Singleton.StartClient());
    }
}
