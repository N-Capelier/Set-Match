using UnityEngine;


namespace TennisMatch
{

    /// <summary>
    /// ARD ancien score
    /// </summary>
    /*
    [Serializable]
    public struct Score
    {
        public int point;
        public int[] gamePerSet;

        public Score(bool isEmpty)
        {
            point = 0;
            gamePerSet = new int[3] { 0, 0, 0 };
        }
        public Score(int pointValue, int GameWinOnSet1, int GameWinOnSet2, int GameWinOnSet3)
        {
            point = pointValue;
            gamePerSet = new int[3] { GameWinOnSet1, GameWinOnSet2, GameWinOnSet3 };
        }
    }
    */

    /// <summary>
    /// ARD
    /// </summary>
    public class PartyScore : MonoBehaviour
    {
        [Header("GameEvent")]
        private MatchEvents matchEvents;
        /*
        [Header("Component")]
        [SerializeField] private RallyScorer rally;
        [SerializeField] private _MatchHeadOrTail headOrTail;

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

        public static bool PointWin(int pos, int increment)
        {
            if (Mathf.Abs(pos + increment) >= 3)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool GameWin(int playingTeamPoints, int advTeamPoints)
        {
            //0 = 0 
            //1 = 15 
            //2 = 30 
            //3 = 40 
            //4 = 40A 

            //Si on dépasse 40
            if (playingTeamPoints > 3)
            {
                //Si l'adversaire a l'avantage
                if (advTeamPoints >= 4)
                {
                    //retour à 40 des deux cotés
                    return false;
                }
                else
                //Si l'adversaire est a 40
                if (advTeamPoints == 3)
                {
                    //Si on a l'avantage
                    if (playingTeamPoints > 4)
                    {
                        return true;
                    }
                    else
                    //Sinon on prend l'avantage
                    {
                        return false;
                    }
                }
                else
                //Team adv est en dessous de 40
                {
                    return true;
                }
            }
            //on a pas encore atteind 40
            else
            {
                return false;
            }
        }
        public static bool SetWin(int playingTeamGames, int advTeamGames, bool isLastSet)
        {
            //Si on a atteind ou dépassé la limite du set
            if (playingTeamGames >= 3)
            {
                //Si on est sur le dernier set et 
                if (isLastSet)
                {
                    //Si on a deux point d'écart avec l'adversaire
                    if (playingTeamGames > advTeamGames + 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }

        }
        public static bool MatchWin(bool isLastSet)
        {
            //Si on a atteint le dernier set
            if (isLastSet)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void ClampTeamsScore()
        {
            //Si les deux équipe dépassent 40 (donc si il y a 40A/40A)
            if (match.teamA_Score.point > 3 && match.teamB_Score.point > 3)
            {
                match.teamA_Score.point = 3;
                match.teamB_Score.point = 3;
            }
        }
        private void ResetTeamsPoints()
        {
            match.teamA_Score.point = 0;
            match.teamB_Score.point = 0;
        }

        private void OnPointMarked()
        {
            if (match.teamA_Turn)
            {
                match.teamA_Score.point++;

                if (GameWin(match.teamA_Score.point, match.teamB_Score.point))
                {
                    matchEvents.GameMarked();
                    Invoke("ResetTeamsPoints", 1f);
                }
                else
                {
                    ClampTeamsScore();
                }
            }
            else
            {
                match.teamB_Score.point++;

                if (GameWin(match.teamB_Score.point, match.teamA_Score.point))
                {
                    matchEvents.GameMarked();
                    Invoke("ResetTeamsPoints", 0.5f);
                }
                else
                {
                    ClampTeamsScore();
                }
            }
        }
        private void OnGameMarked()
        {
            bool isLastSet = match.currentSet >= match.MatchSetNumber;

            int teamA_SetScore = match.teamA_Score.gamePerSet[match.currentSet - 1];
            int teamB_SetScore = match.teamB_Score.gamePerSet[match.currentSet - 1];

            //+1 pour l'équipe qui prends la Game
            if (match.teamA_Turn)
            {
                match.teamA_Score.gamePerSet[match.currentSet - 1]++;
                teamA_SetScore++;

                if (SetWin(teamA_SetScore, teamB_SetScore, isLastSet))
                {
                    matchEvents.SetMarked();
                }
            }
            else
            {
                match.teamB_Score.gamePerSet[match.currentSet - 1]++;
                teamB_SetScore++;

                if (SetWin(teamB_SetScore, teamA_SetScore, isLastSet))
                {
                    matchEvents.SetMarked();
                }
            }
        }
        private void OnSetMarked()
        {
            bool isLastSet = match.currentSet >= match.MatchSetNumber;

            if (MatchWin(isLastSet))
            {
                matchEvents.MatchEnd();

                Debug.Log("Game End");
            }
            else
            {
                match.currentSet++;
                Debug.Log("SetWin");
            }
        }
        */
    }
}
