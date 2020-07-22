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
        [SerializeField] private MatchData match;

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
            match.teamA_Score.point = 0;
            match.teamB_Score.point = 0;
        }

        private void OnTeamA_PointMarked()
        {
            //Si on est a 40 est que l'on marque
            if (match.teamA_Score.point + 1 > 3)
            {
                //Team adv à l'avantage
                if (match.teamB_Score.point == 4)
                {
                    //Les deux reviennent à 40
                    match.teamA_Score.point = 3;
                    match.teamB_Score.point = 3;
                }
                else
                //Team marquante a avantage              
                if (match.teamA_Score.point == 4)
                {
                    TeamA_GameMarked();
                }
                else
                //Team adv a 40              
                if (match.teamB_Score.point == 3)
                {
                    //Avantage pris
                    match.teamA_Score.point = 4;
                }
                //Team adv en dessous de 40
                else
                {
                    match.teamA_Score.point = 3;

                    TeamA_GameMarked();
                }
            }
            //Si on a pas encore atteind 40
            else
            {
                match.teamA_Score.point++;
            }
        }
        private void OnTeamB_PointMarked()
        {
            //Si on est a 40 est que l'on marque
            if (match.teamB_Score.point + 1 > 3)
            {
                //Team adv à l'avantage
                if (match.teamA_Score.point == 4)
                {
                    //Les deux reviennent à 40
                    match.teamA_Score.point = 3;
                    match.teamB_Score.point = 3;
                }
                else                
                //Team marquante a avantage              
                if (match.teamB_Score.point == 4)
                {
                    TeamB_GameMarked();
                }
                else
                //Team adv a 40              
                if (match.teamA_Score.point == 3)
                {
                    //Avantage pris
                    match.teamB_Score.point = 4;
                }
                //Team adv en dessous de 40
                else
                {
                    match.teamB_Score.point = 3;

                    TeamB_GameMarked();
                }
            }
            //Si on a pas encore atteind 40
            else
            {
                match.teamB_Score.point++;
            }
        }

        private void OnTeamA_GameMarked()
        {
            match.teamA_Score.gamePerSet[match.currentSet]++;

            if (match.teamA_Score.gamePerSet[match.currentSet] > 2)
            {
                SetMarked();
            }

            ResetRound();
        }
        private void OnTeamB_GameMarked()
        {
            match.teamB_Score.gamePerSet[match.currentSet]++;

            if (match.teamB_Score.gamePerSet[match.currentSet] > 2)
            {
                SetMarked();
            }

            ResetRound();
        }

        private void OnSetMarked()
        {
            if(match.currentSet + 1 < match.MatchSetNumber)
            {
                match.currentSet++;
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
