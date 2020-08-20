using Player;
using Sfs2X.Requests;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;

namespace TennisMatch
{
    /// <summary>
    /// NCO
    /// </summary>
    public class MatchMaking : MonoBehaviour
    {
        public PlayerID playerID;
        List<PlayerID> playersNear = new List<PlayerID>();

        bool locationAuthorized = false;

        [SerializeField]
        GameObject opponent;

        //allow debug.log
        [SerializeField] bool allowDebug = false;

        private void Start()
        {
            // get currently connected player ID
            // playerID = new PlayerID();

            StartCoroutine(StartLocationService());

            if (!Permission.HasUserAuthorizedPermission(Permission.CoarseLocation))
            {
                Permission.RequestUserPermission(Permission.CoarseLocation);
            }

        }

        private void Update()
        {

#if UNITY_EDITOR
            // debug
            if(Input.GetKeyDown(KeyCode.A))
            {
                Instantiate(opponent, new Vector3(1f, 1f, -0.1f), Quaternion.identity);
            }
#endif

            if (!locationAuthorized)
            {
                if (!Permission.HasUserAuthorizedPermission(Permission.CoarseLocation))
                {
                    //Display message explaining why we need the location with yes/no button.
                    //if yes, initialize match making
                    //if not, go back to main menu
                }
                else
                {
                    locationAuthorized = true;
                    StartCoroutine(StartLocationService());
                }
            }
            else
            {
                UpdatePlayerPos();
                GetOtherPlayersLocation();
                DisplayOtherPlayersLocation();
            }

        }

        void UpdatePlayerPos()
        {
            if (Input.location.status == LocationServiceStatus.Running)
            {
                playerID.location = new Vector2(
                Input.location.lastData.latitude,
                Input.location.lastData.longitude);
            }
        }

        public void GetOtherPlayersLocation()
        {
            //
        }

        void DisplayOtherPlayersLocation()
        {
            for(int i = 0; i < playersNear.Count; i++)
            {
                if(playersNear[i].location.x.isBetween(playerID.location.x - 0.5f, playerID.location.x + 0.5f, ClusingType.II)
                    && playersNear[i].location.y.isBetween(playerID.location.y - 0.5f, playerID.location.y + 0.5f, ClusingType.II))
                {
                    Instantiate(opponent, new Vector3(playersNear[i].location.x - playerID.location.x, playersNear[i].location.y - playerID.location.y, -0.1f), Quaternion.identity);
                }
            }
        }

        IEnumerator StartLocationService()
        {
            if (!Input.location.isEnabledByUser)
            {
                if(allowDebug)
                    print("GPS not enabled");
                yield return new WaitForSeconds(0.1f);
                StartCoroutine(StartLocationService());
                yield break;
            }

            Input.location.Start();
            int maxWait = 20;
            while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
            {
                yield return new WaitForSeconds(1f);
                maxWait--;
            }

            if (maxWait <= 0)
            {
                if (allowDebug)
                    print("Connection timed out");
                yield break;
            }

            if (Input.location.status == LocationServiceStatus.Failed)
            {
                if (allowDebug)
                    print("Unable to determin device location");
                yield break;
            }

            playerID.location = new Vector2(
                Input.location.lastData.latitude,
                Input.location.lastData.longitude);

            NetworkManager.Instance.sfs.Send(new JoinRoomRequest("Lobby"));

            yield break;
        }

        #region Match Methods

        public void SendChallenge(PlayerID playerID, bool isDoubleMatch)
        {

        }

        public void RecieveChallenge(PlayerID playerID, bool isDoubleMatch)
        {

        }

        #endregion
    }

}