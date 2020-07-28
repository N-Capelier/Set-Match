using Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Android;


namespace TennisMatch
{
    public class MatchMaking : MonoBehaviour
    {
        public PlayerID playerID;
        List<PlayerID> playersNear = new List<PlayerID>();

        public Text text;
        public Text console;

        bool matchMakingInitialized = false;

        int i = 0;

        private void Start()
        {
            playerID = new PlayerID();
            StartCoroutine(StartLocationService());

            if (!Permission.HasUserAuthorizedPermission(Permission.CoarseLocation))
            {
                Permission.RequestUserPermission(Permission.CoarseLocation);
            }

        }

        private void Update()
        {
            if (!matchMakingInitialized)
            {
                if (!Permission.HasUserAuthorizedPermission(Permission.CoarseLocation))
                {
                    //Display message explaining why we need the location with yes/no button.
                    //if yes, initialize match making
                    //if not, go back to main menu
                }
                else
                {
                    matchMakingInitialized = true;
                    StartCoroutine(StartLocationService());
                }
            }
            else
            {
                if (Input.location.status == LocationServiceStatus.Running)
                {
                    playerID.location = new Vector2(
                    Input.location.lastData.latitude,
                    Input.location.lastData.longitude);
                    i++;
                    console.text = i.ToString();
                }
                text.text = "Lat : " + playerID.location.x + " || Lon: " + playerID.location.y;
            }

        }

        IEnumerator StartLocationService()
        {
            if (!Input.location.isEnabledByUser)
            {
                print("GPS not enabled");
                console.text = "GPS not enabled" + Input.location.status.ToString();
                yield return new WaitForSeconds(0.1f);
                StartCoroutine(StartLocationService());
                yield break;
            }

            Input.location.Start();
            int maxWait = 20;
            console.text = "Waiting for initalization " + Input.location.status.ToString();
            while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
            {
                yield return new WaitForSeconds(1f);
                maxWait--;
            }

            if (maxWait <= 0)
            {
                print("Connection timed out");
                console.text = "Connection timed out " + Input.location.status.ToString();
                yield break;
            }

            if (Input.location.status == LocationServiceStatus.Failed)
            {
                print("Unable to determin device location");
                console.text = "Unable to determin device location " + Input.location.status.ToString();
                yield break;
            }

            console.text = "Location initialized " + Input.location.status.ToString();

            playerID.location = new Vector2(
                Input.location.lastData.latitude,
                Input.location.lastData.longitude);

            yield break;
        }

        public void GetOtherPlayersLocation()
        {
            //
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