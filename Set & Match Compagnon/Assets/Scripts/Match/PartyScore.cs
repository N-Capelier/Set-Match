using System;
using UnityEngine;


namespace TennisMatch
{
    /// <summary>
    /// ARD
    /// </summary>
    public class PartyScore : MonoBehaviour
    {
        [Header("GameEvent")]
        private MatchEvents matchEvents;

        [Header("Component")]
        [SerializeField] private RallyScorer rally;
        [SerializeField] private HeadOrTail headOrTail;

        [Header("Variable")]
        [SerializeField] private MatchData match;

        private void Awake() => matchEvents = MatchEvents.Instance;

        private void OnEnable()
        {
            matchEvents.onMatchStart += ResetTeamsPoints;
            matchEvents.onPointMarked += OnPointMarked;
            matchEvents.onGameMarked += OnGameMarked;
            matchEvents.onSetMarked += OnSetMarked;
        }
        private void OnDisable()
        {
            matchEvents.onMatchStart -= ResetTeamsPoints;
            matchEvents.onPointMarked -= OnPointMarked;
            matchEvents.onGameMarked -= OnGameMarked;
            matchEvents.onSetMarked -= OnSetMarked;

        }
       
        private void ResetTeamsPoints()
        {
            match.teamA_Score.point = 0;
            match.teamB_Score.point = 0;
        }

        private void OnPointMarked()
        {
            //Si l'équipe A a joué
            if (match.teamA_Turn)
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
                        matchEvents.GameMarked();
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

                        matchEvents.GameMarked();
                    }
                }
                //Si on a pas encore atteind 40
                else
                {
                    match.teamA_Score.point++;
                }

            }
            //Sinon l'équipe B a joué
            else
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
                        matchEvents.GameMarked();
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

                        matchEvents.GameMarked();
                    }
                }
                //Si on a pas encore atteind 40
                else
                {
                    match.teamB_Score.point++;
                }
            }
        }

        private void OnGameMarked()
        {
            //Si l'équipe A a joué
            if (match.teamA_Turn)
            {
                match.teamA_Score.gamePerSet[match.currentSet - 1]++;

                int teamA_SetScore = match.teamA_Score.gamePerSet[match.currentSet - 1];
                int teamB_SetScore = match.teamB_Score.gamePerSet[match.currentSet - 1];

                //Si on a atteind ou dépassé la limite du set
                if (match.teamA_Score.gamePerSet[match.currentSet - 1] >= 3)
                {
                    //Si on est sur le dernier set et 
                    if (match.currentSet >= match.MatchSetNumber)
                    {
                        //Si on a deux point d'écart avec l'adversaire
                        if (teamA_SetScore > teamB_SetScore + 1)
                        {
                            matchEvents.SetMarked();
                        }
                    }
                    else
                    {
                        matchEvents.SetMarked();
                    }
                }
            }
            //Sinon l'équipe B a joué
            else
            {
                match.teamB_Score.gamePerSet[match.currentSet - 1]++;

                int teamA_SetScore = match.teamA_Score.gamePerSet[match.currentSet - 1];
                int teamB_SetScore = match.teamB_Score.gamePerSet[match.currentSet - 1];

                //Si on a atteind ou dépassé la limite du set
                if (teamB_SetScore >= 3)
                {
                    //Si on est sur le dernier set
                    if (match.currentSet >= match.MatchSetNumber)
                    {
                        //Si on a deux point d'écart avec l'adversaire
                        if (teamB_SetScore > teamA_SetScore + 1)
                        {
                            matchEvents.SetMarked();
                        }
                    }
                    else
                    {
                        matchEvents.SetMarked();
                    }
                }
            }

            ResetTeamsPoints();
        }

        private void OnSetMarked()
        {
            //Si on a atteint le dernier set
            if(match.currentSet >= match.MatchSetNumber)
            {
                matchEvents.MatchEnd();
                Debug.Log("EndOfTheMatch");
            }
            else
            {
                match.currentSet++;
                Debug.Log("Set");
            }
        }

    }
}
