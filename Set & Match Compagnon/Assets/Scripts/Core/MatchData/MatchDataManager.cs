using System;
using UnityEngine;

namespace TennisMatch
{
    /// <summary>
    /// ARD
    /// </summary>
    public class MatchDataManager : Singleton<MatchDataManager>
    {
        [Header("MatchData")]
        public MatchData currentMatch;

        [Header("Variable")]
        public bool rebootOnAwake = false;

        private void Awake()
        {
            //Reset du matchData ou erreur s'il n'est pas là
            if (currentMatch == null)
            {
                Debug.LogError("MatchData non attribué");
            }
            else
            if (rebootOnAwake)
            {
                currentMatch.Reboot();
            }
        }


    }
}
