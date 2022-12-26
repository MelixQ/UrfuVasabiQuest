using System;
using Mirror;
using UnityEngine;
using UnityEngine.UI;

public class NetworkHUD : MonoBehaviour
{
    private NetworkManager _networkManager;
    [SerializeField] private GameObject _HUDCanvas;
    [SerializeField] private Button _hostButton;
    [SerializeField] private Button _clientButton;
    [SerializeField] private InputField _ipInputField;

    private void Awake()
    {
        _networkManager = GetComponent<NetworkManager>();
        _clientButton.onClick.AddListener(ClientStart);
        _hostButton.onClick.AddListener(HostStart);
    }
    
    public void HostStart()
    {
        if (!NetworkClient.isConnected && !NetworkServer.active)
        {
            if (!NetworkClient.active)
            {
                _networkManager.StartHost();
                _networkManager.networkAddress = "25.50.145.13";
                _HUDCanvas.SetActive(false);
            }
            else
                _networkManager.StopClient();
        }
    }

    public void ClientStart()
    {
        if (!NetworkClient.isConnected && !NetworkServer.active)
        {
            if (!NetworkClient.active)
            {
                _networkManager.StartClient();
                _networkManager.networkAddress = _ipInputField.text == "" ? "25.50.145.13" : _ipInputField.text;
                _HUDCanvas.SetActive(false);
            }
            else
                _networkManager.StopClient();
        }
        
        if (NetworkClient.isConnected && !NetworkClient.ready)
        {
            NetworkClient.Ready();
            if (NetworkClient.localPlayer == null)
            {
                NetworkClient.AddPlayer();
            }
        }
    }
}
