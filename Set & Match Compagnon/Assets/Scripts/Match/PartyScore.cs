using System;
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
        [SerializeField] public string teamA_Name;
        [SerializeField] public string teamB_Name;
        [Space(15)]
        public int teamA_RoundPoint = 0;
        public int teamB_RoundPoint = 0;
        [Space(15)]
        public int[] teamA_PointPerSet = new int[3] { 0, 0, 0 };
        public int[] teamB_PointPerSet = new int[3] { 0, 0, 0 };
        [Space(15)]
        public int TeamA_SetWin = 0;
        public int TeamB_SetWin = 0;
        [Space(15)]
        [Range(0, 3)] public int actualSet = 0;
        [Range(0, 3)] public int partySet = 3;
        [Space(15)]
        public bool TeamA_haveService = true;

        #region Events
        public event Action onTeamA_PointMarked;
        public event Action onTeamB_PointMarked;
        public event Action onTeamA_GameMarked;
        public event Action onTeamB_GameMarked;
        public event Action onSetMarked;
        public event Action onPartyEnd;

        public void TeamA_PointMarked() => onTeamA_PointMarked?.Invoke();
        public void TeamB_PointMarked() => onTeamB_PointMarked?.Invoke();
        public void TeamA_GameMarked() => onTeamA_GameMarked?.Invoke();
        public void TeamB_GameMarked() => onTeamB_GameMarked?.Invoke();
        public void SetMarked() => onSetMarked?.Invoke();
        public void EndGame() => onPartyEnd?.Invoke();
        #endregion

        private void Awake()
        {
            ResetRound();
        }
        private void OnEnable()
        {
            onTeamA_PointMarked += OnTeamA_PointMarked;
            onTeamB_PointMarked += OnTeamB_PointMarked;
            onTeamA_GameMarked += OnTeamA_GameMarked;
            onTeamB_GameMarked += OnTeamB_GameMarked;
            onSetMarked += OnSetMarked;
            onPartyEnd += OnEndGame;
        }
        private void OnDisable()
        {
            onTeamA_PointMarked -= OnTeamA_PointMarked;
            onTeamB_PointMarked -= OnTeamB_PointMarked;
            onTeamA_GameMarked -= OnTeamA_GameMarked;
            onTeamB_GameMarked -= OnTeamB_GameMarked;
            onSetMarked -= OnSetMarked;
            onPartyEnd -= OnEndGame;
        }

        private void ResetRound()
        {
            teamA_RoundPoint = 0;
            teamB_RoundPoint = 0;
        }
        private void ServiceRotate()
        {
            TeamA_haveService = !TeamA_haveService;
        }

        private void OnTeamA_PointMarked()
        {
            //Si on est a 40 est que l'on marque
            if (teamA_RoundPoint + 1 > 3)
            {
                //Team adv à l'avantage
                if (teamB_RoundPoint == 4)
                {
                    //Les deux reviennent à 40
                    teamA_RoundPoint = 3;
                    teamB_RoundPoint = 3;
                }
                else
                //Team marquante a avantage              
                if (teamA_RoundPoint == 4)
                {
                    TeamA_GameMarked();
                }
                else
                //Team adv a 40              
                if (teamB_RoundPoint == 3)
                {
                    //Avantage pris
                    teamA_RoundPoint = 4;
                }
                //Team adv en dessous de 40
                else
                {
                    teamA_RoundPoint = 3;

                    TeamA_GameMarked();
                }
            }
            //Si on a pas encore atteind 40
            else
            {
                teamA_RoundPoint++;
            }
        }
        private void OnTeamB_PointMarked()
        {
            //Si on est a 40 est que l'on marque
            if (teamB_RoundPoint + 1 > 3)
            {
                //Team adv à l'avantage
                if (teamA_RoundPoint == 4)
                {
                    //Les deux reviennent à 40
                    teamA_RoundPoint = 3;
                    teamB_RoundPoint = 3;
                }
                else                
                //Team marquante a avantage              
                if (teamB_RoundPoint == 4)
                {
                    TeamB_GameMarked();
                }
                else
                //Team adv a 40              
                if (teamA_RoundPoint == 3)
                {
                    //Avantage pris
                    teamB_RoundPoint = 4;
                }
                //Team adv en dessous de 40
                else
                {
                    teamB_RoundPoint = 3;

                    TeamB_GameMarked();
                }
            }
            //Si on a pas encore atteind 40
            else
            {
                teamB_RoundPoint++;
            }
        }

        private void OnTeamA_GameMarked()
        {
            teamA_PointPerSet[actualSet]++;

            if (teamA_PointPerSet[actualSet] > 2)
            {
                SetMarked();
            }

            ServiceRotate();
            ResetRound();
        }
        private void OnTeamB_GameMarked()
        {
            teamB_PointPerSet[actualSet]++;
            
            if (teamB_PointPerSet[actualSet] > 2)
            {
                SetMarked();
            }

            ServiceRotate();
            ResetRound();
        }

        private void OnSetMarked()
        {
            if(actualSet +1 < partySet)
            {
                actualSet++;
            }
            else
            {
                EndGame();
            }
        }

        private void OnEndGame()
        {

        }


    }
}
