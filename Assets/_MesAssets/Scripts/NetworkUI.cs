using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;

public class NetworkUI : MonoBehaviour
{
    // Représente les 3 boutons de notre scène
    [SerializeField] private Button hostButton = default;
    [SerializeField] private Button serverButton = default;
    [SerializeField] private Button clientButton = default;

    private void Start()
    {
        // souscrit à l'évènement sur le click du bouton Host et démarrer la session Host
        hostButton.onClick.AddListener(() =>NetworkManager.Singleton.StartHost());

        // souscrit à l'évènement sur le click du bouton Host et démarrer la session Host
        serverButton.onClick.AddListener(() => NetworkManager.Singleton.StartServer());

        // souscrit à l'évènement sur le click du bouton Host et démarrer la session Host
        clientButton.onClick.AddListener(() => NetworkManager.Singleton.StartClient());
    }
}
