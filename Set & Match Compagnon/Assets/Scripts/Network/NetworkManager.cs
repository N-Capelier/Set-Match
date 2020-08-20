using Sfs2X;
using Sfs2X.Core;
using Sfs2X.Logging;
using Sfs2X.Requests;
using Sfs2X.Util;
using System;
using UnityEngine;

/// <summary>
/// NCO
/// </summary>
public class NetworkManager : Singleton<NetworkManager>
{
    #region Variables

    public SmartFox sfs;

    public static bool isConnectedToServer = false;
    public static bool isLoggedIn = false;
    public static bool isInMatch = false;

    #endregion

    #region Unity Methods

    private void Awake()
    {
        //Avoid multiple instances of the Network Manager object
        CreateSingleton(true);
    }

    private void Start()
    {
        //Connect to the server on Start
        if (sfs == null || !sfs.IsConnected)
        {
            Connect();
        }
    }

    void Update()
    {
        //Queue server's events
        if (sfs != null)
        {
            sfs.ProcessEvents();
        }

#if UNITY_EDITOR
        //Debug : Disconnect player
        if (Input.GetKeyDown(KeyCode.Space))
        {
            {
                Disconnect();
            }
        }
#endif

    }

    #endregion

    #region Connection Methods

    void Connect()
    {
        // sfs2x init
        sfs = new SmartFox();

        // sfs2x listeners
        sfs.AddEventListener(SFSEvent.CONNECTION, OnConnection);
        sfs.AddEventListener(SFSEvent.CONNECTION_LOST, OnConnectionLost);
        sfs.AddEventListener(SFSEvent.CONFIG_LOAD_SUCCESS, OnConfigLoadSuccess);
        sfs.AddEventListener(SFSEvent.CONFIG_LOAD_FAILURE, OnConfigLoadFailure);

        sfs.AddEventListener(SFSEvent.LOGIN, OnLogin);
        sfs.AddEventListener(SFSEvent.LOGIN_ERROR, OnLoginError);

        sfs.AddEventListener(SFSEvent.ROOM_JOIN, OnJoinRoom);
        sfs.AddEventListener(SFSEvent.ROOM_JOIN_ERROR, OnJoinRoomError);

        sfs.AddLogListener(LogLevel.ERROR, OnErrorMessage);
        sfs.AddLogListener(LogLevel.WARN, OnWarnMessage);
        sfs.AddLogListener(LogLevel.INFO, OnInfoMessage);

        //Connection parameters
        ConfigData cfg = new ConfigData();
        cfg.Host = "127.0.0.1";
        //cfg.Host = SFS2XExamples.Panel.Settings.ipAddress;
        cfg.Port = Convert.ToInt32("9933");
        //cfg.Port = Convert.ToInt32(SFS2XExamples.Panel.Settings.port.ToString());
        cfg.Zone = "Main";
        cfg.Debug = true;

        //sfs2x connection
        sfs.Connect(cfg);
    }

    void Disconnect()
    {
        isConnectedToServer = false;
        sfs.Disconnect();
    }

    #endregion



    #region Server action events

    void OnConnection(BaseEvent evt)
    {
        if ((bool)evt.Params["success"])
        {
            isConnectedToServer = true;
            print("Connection successful");
        }
        else
        {
            print("Connection failed : " + (string)evt.Params["error"]);
        }
    }

    void OnConnectionLost(BaseEvent evt)
    {
        print("Connection lost : " + evt.Params["reason"]);
    }

    void OnConfigLoadSuccess(BaseEvent evt)
    {
        print("Config loaded with settings : " + sfs.Config.Host + ":" + sfs.Config.Port);
    }

    void OnConfigLoadFailure(BaseEvent evt)
    {
        print("Config not loaded due to a failure");
    }

    #endregion

    #region Server login events

    void OnLogin(BaseEvent evt)
    {
        print("Login success");
    }

    void OnLoginError(BaseEvent evt)
    {
        print("Login error : " + evt.Params["errorMessage"]);
    }

    #endregion

    #region Server room events

    void OnJoinRoom(BaseEvent evt)
    {
        print("Joined room successfully");
    }

    void OnJoinRoomError(BaseEvent evt)
    {
        print("Join failed : " + evt.Params["error"]);
    }

    #endregion

    #region Server logs events

    void OnErrorMessage(BaseEvent evt)
    {
        print("ERROR : " + (string)evt.Params["message"]);
    }

    void OnWarnMessage(BaseEvent evt)
    {
        print("WARN : " + (string)evt.Params["message"]);
    }

    void OnInfoMessage(BaseEvent evt)
    {
        print("INFO : " + (string)evt.Params["message"]);
    }

    #endregion
}
