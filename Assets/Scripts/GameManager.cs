using Unity.Netcode;
using UnityEngine;
using DilmerGames.Core.Singletons;
using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

    public class GameManager : MonoBehaviour


    {

        [SerializeField]
        private Button startHostButton;

        [SerializeField]
        private Button startClientButton;

        [SerializeField]
        private TMP_InputField joinCodeInput;

        private bool hasServerStarted;

        private void Awake()
        {
            Cursor.visible = true;
        }
        void OnGUI()
        {
            GUILayout.BeginArea(new Rect(10, 10, 300, 300));
            if (!NetworkManager.Singleton.IsClient && !NetworkManager.Singleton.IsServer)
            {
                StartButtons();
            }
            else
            {
                StatusLabels();

                // SubmitNewPosition();
            }

            GUILayout.EndArea();
        }

        async void StartButtons()
        {
            if (GUILayout.Button("Host"))
            {
                if (RelayManager.Instance.IsRelayEnabled)
                    await RelayManager.Instance.SetupRelay();

                if (NetworkManager.Singleton.StartHost())
                    Debug.Log("test");
                else
                    Debug.Log("test");
            }

            if (GUILayout.Button("Client"))
            {
                if (RelayManager.Instance.IsRelayEnabled && !string.IsNullOrEmpty(joinCodeInput.text))
                    await RelayManager.Instance.JoinRelay(joinCodeInput.text);

                if (NetworkManager.Singleton.StartClient())
                    Debug.Log("test");
                else
                    Debug.Log("test");
            }

            if (GUILayout.Button("Server")) NetworkManager.Singleton.StartServer();
        }

        static void StatusLabels()
        {
            var mode = NetworkManager.Singleton.IsHost ?
                "Host" : NetworkManager.Singleton.IsServer ? "Server" : "Client";

            GUILayout.Label("Transport: " +
                            NetworkManager.Singleton.NetworkConfig.NetworkTransport.GetType().Name);
            GUILayout.Label("Mode: " + mode);
        }

        // static void SubmitNewPosition()
        // {
        //     if (GUILayout.Button(NetworkManager.Singleton.IsServer ? "Move" : "Request Position Change"))
        //     {
        //         var playerObject = NetworkManager.Singleton.SpawnManager.GetLocalPlayerObject();
        //         var player = playerObject.GetComponent<PlayerMovement>();
        //         player.Move();
        //     }
        // }
    }
