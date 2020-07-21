using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


namespace TennisMatch
{
    /// <summary>
    /// ARD
    /// </summary>
    public class PartyScore : MonoBehaviour
    {
        [Header("Component")]
        [SerializeField] private RallyScorer rally;
        [SerializeField] private HeadOrTail headOrTail;

        [Header("Variable")]
        public string teamA_Name;
        public string teamB_Name;

        public int[] teamA_PointPerSet;
        public int[] teamB_PointPerSet;

        public bool[] TeamA_SetWin;
        public bool TeamA_haveService = true;

        public int actualSet = 0;
        public int partySet = 3;

        #region Events
        public event Action onTeamA_PointMarked;
        public event Action onTeamB_PointMarked;
        public event Action onTeamA_SetMarked;
        public event Action onTeamB_SetMarked;
        public event Action onPartyEnd;

        public void TeamA_PointMarked() => onTeamA_PointMarked?.Invoke();
        public void TeamB_PointMarked() => onTeamB_PointMarked?.Invoke();
        public void TeamA_SetMarked() => onTeamA_SetMarked?.Invoke();
        public void TeamB_SetMarked() => onTeamB_SetMarked?.Invoke();
        #endregion

        private void Awake()
        {
            ResetScore();
        }
        private void OnEnable()
        {
            onTeamA_PointMarked += OnTeamA_PointMarked;
            onTeamB_PointMarked += OnTeamB_PointMarked;
            onTeamA_SetMarked += OnTeamA_SetMarked;
            onTeamB_SetMarked += OnTeamB_SetMarked;
        }
        private void OnDisable()
        {
            onTeamA_PointMarked -= OnTeamA_PointMarked;
            onTeamB_PointMarked -= OnTeamB_PointMarked;
            onTeamA_SetMarked -= OnTeamA_SetMarked;
            onTeamB_SetMarked -= OnTeamB_SetMarked;
        }

        private void ResetScore()
        {
            teamA_Name = "TeamKarp";
            teamB_Name = "TeamNico";

            for (int i = 0; i < teamA_PointPerSet.Length; i++)
            {
                teamA_PointPerSet[i] = 0;
                teamB_PointPerSet[i] = 0;
            }
        }

        private void OnTeamA_PointMarked()
        {
            TeamMarkedPoint(teamA_PointPerSet, teamB_PointPerSet);
        }

        private void OnTeamB_PointMarked()
        {
            TeamMarkedPoint(teamB_PointPerSet, teamA_PointPerSet);
        }

        private void OnTeamA_SetMarked()
        {

        }

        private void OnTeamB_SetMarked()
        {

        }
    
        private void TeamMarkedPoint(int[] teamHowMarked, int[] teamAdv)
        {
            //Si on est a 40 est que l'on marque
            if (teamHowMarked[actualSet] + 1 > 4)
            {
                //Team adv à l'avantage
                if (teamAdv[actualSet] == 5)
                {
                    //Les deux reviennent à 40
                    teamA_PointPerSet[actualSet] = 4;
                    teamB_PointPerSet[actualSet] = 4;
                }
                else
                //Team adv a 40              
                if (teamAdv[actualSet] == 4)
                {
                    //Avantage pris
                    teamA_PointPerSet[actualSet] = 5;
                }
                //Team adv en dessous de 40
                else
                {
                    teamA_PointPerSet[actualSet] = 5;
                    TeamA_SetMarked();
                }
            }
            //Si on a pas encore atteind 40
            else
            {
                teamA_PointPerSet[actualSet]++;
            }
        }
    }
}
