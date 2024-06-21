using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using System;
using Unity.Services.Relay;
using Unity.Services.Core;
using Unity.Services.Authentication;
using Unity.Services.Relay.Models;
using Unity.Netcode.Transports.UTP;
using Unity.Networking.Transport.Relay;
using UnityEngine.UI;
using TMPro;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] private Button clientButton;
    [SerializeField] private Button hostButton;

    [SerializeField] private TMP_InputField joinCodeInputField;
    [SerializeField] private Button joinCodeButton;
    
    // Start is called before the first frame update
    void Start() {
        clientButton.onClick.AddListener(() => {
            joinCodeInputField.gameObject.SetActive(true);
            joinCodeInputField.Select();
            joinCodeInputField.ActivateInputField();

            joinCodeButton.gameObject.SetActive(true);
            clientButton.gameObject.SetActive(false);

            joinCodeButton.onClick.AddListener(async () => {
                await UnityServices.InitializeAsync();
                if (!AuthenticationService.Instance.IsSignedIn){
                    await AuthenticationService.Instance.SignInAnonymouslyAsync();
                }

                var joinAllocation = await RelayService.Instance.JoinAllocationAsync(joinCode: joinCodeInputField.text.ToString());
                NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(new RelayServerData(joinAllocation, "dtls"));
                NetworkManager.Singleton.StartClient();
                joinCodeButton.gameObject.SetActive(false);
                joinCodeInputField.gameObject.SetActive(false);
            });
        });
        hostButton.onClick.AddListener(async () =>
        {
            await UnityServices.InitializeAsync();
            if (!AuthenticationService.Instance.IsSignedIn) {
                await AuthenticationService.Instance.SignInAnonymouslyAsync();
            }
            Allocation allocation = await RelayService.Instance.CreateAllocationAsync(1);
            NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(new RelayServerData(allocation, "dtls"));
            var joinCode = await RelayService.Instance.GetJoinCodeAsync(allocation.AllocationId);
            var code = NetworkManager.Singleton.StartHost() ? joinCode : null;
            clientButton.gameObject.SetActive(false);
            joinCodeButton.gameObject.SetActive(false);
            joinCodeInputField.gameObject.SetActive(false);
            hostButton.gameObject.SetActive(false);
            Debug.Log(code);

        });
    }
}
