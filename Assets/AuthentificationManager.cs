using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.Authentication;
using Unity.Services.Core;

// Pour tester en local avec ParrelSync on ajoute la librairie
#if UNITY_EDITOR
using ParrelSync;
#endif


public class AuthentificationManager : MonoBehaviour
{
    private void Awake()
    {
        Login();
    }

    public async void Login()
    {
        // Variables qui contient les options d'initialisation
        // Ceci va nous permettre de savoir si nous sommes sur un clone en local
        InitializationOptions options = new InitializationOptions();

// Si on teste à partir de l'éditeur Unity
#if UNITY_EDITOR
        if (ClonesManager.IsClone())
        {
            options.SetProfile(ClonesManager.GetArgument());
        }
        else
        {
            options.SetProfile("primary");
        }
#endif

        await UnityServices.InitializeAsync(options);
        await AuthenticationService.Instance.SignInAnonymouslyAsync();
    }
}
