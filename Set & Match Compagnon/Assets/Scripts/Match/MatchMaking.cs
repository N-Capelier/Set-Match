using Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TennisMatch
{
    public class MatchMaking : MonoBehaviour
    {
        public PlayerID playerID;
        List<PlayerID> playersNear = new List<PlayerID>();

        private void Start()
        {
            StartCoroutine(StartLocationService());
        }

        IEnumerator StartLocationService()
        {
            if(!Input.location.isEnabledByUser)
            {
                print("GPS not enabled");
                yield break;
            }

            Input.location.Start();
            int maxWait = 20;
            while(Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
            {
                yield return new WaitForSeconds(1f);
                maxWait--;
            }

            if(maxWait <= 0)
            {
                print("Connection timer out");
                yield break;
            }

            if(Input.location.status == LocationServiceStatus.Failed)
            {
                print("Unable to determin device location");
                yield break;
            }

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