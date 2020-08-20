using UnityEngine;

namespace TennisMatch
{
    /// <summary>
    /// ARD
    /// </summary>
    public class _MatchScore : MonoBehaviour
    {
        [Header("GameMeta")]
        private MatchEvents matchEvents;
        [SerializeField] private MatchData match;
        
        private void Awake()
        {
            matchEvents = MatchEvents.Instance;

            Initialisation(match.score.MatchSetNumber);
        }
        private void OnEnable()
        {
            matchEvents.onMatchStart += Initialise;
        }
        private void OnDisable()
        {
            matchEvents.onMatchStart -= Initialise;
        }
       
        private void Initialise()
        {
            Initialisation(match.score.MatchSetNumber);
        }
        public void Initialisation(int SetNumber)
        {
            SetNumber = Mathf.Clamp(SetNumber, 1, 3);

            match.score = new Score();

            match.score.MatchSetNumber = SetNumber;
            match.score.actualSet = 0;

            for (int i = 0; i < SetNumber; i++)
            {
                match.score.Sets.Add(new Set());
            }

            match.score.Sets[0].Games.Add(new Game(0, 0));
        }

        //Static fonction
        /// <summary> ARD script
        /// <list type="explication">
        /// <item> 0 = 0   </item>
        /// <item> 1 = 15  </item>
        /// <item> 2 = 30  </item>
        /// <item> 3 = 40  </item>
        /// <item> 4 ou plus = 40 ou 40A  </item>
        /// </list> </summary>
        public static bool GameWin(int playingTeamPoints, int advTeamPoints)
        {
            //Si on dépasse 40
            if (playingTeamPoints > 3)
            {
                //Si l'adversaire a l'avantage ou qu'on est à égalité
                if (playingTeamPoints <= advTeamPoints)
                {
                    return false;
                }
                else
                //on est devant le joueur adv
                {
                    //Si l'adversaire n'a pas atteind 40
                    if (advTeamPoints < 3)
                    {
                        return true;
                    }
                    else
                    //Est ce qu'on a deux point d'avance ?
                    if (playingTeamPoints >= advTeamPoints + 2)
                    {
                        return true;
                    }
                    //On vient de prendre l'avantage mais pas le point
                    else
                    {
                        return false;
                    }
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

        //Main Method
        public void MarkedPoint(bool aTeamIsPlaying)
        {
            int currentSet = match.score.actualSet;
            int currentGame = match.score.Sets[currentSet].actualGame;

            //ajout du point
            AddPoint(aTeamIsPlaying, currentSet, currentGame);
            matchEvents.PointMarked();

            int aTeamPoints = match.score.Sets[currentSet].Games[currentGame].aTeamPoint;
            int bTeamPoints = match.score.Sets[currentSet].Games[currentGame].bTeamPoint;

            int winingPointTeam = aTeamIsPlaying ? aTeamPoints : bTeamPoints;
            int losingPointTeam = aTeamIsPlaying ? bTeamPoints : aTeamPoints;

            if (GameWin(winingPointTeam, losingPointTeam))
            {
                matchEvents.GameMarked();
                CloseGame(aTeamIsPlaying, currentGame);

                bool lastGame = match.score.actualSet == match.score.MatchSetNumber;

                int aTeamGamesMarked = match.score.Sets[currentSet].aTeamGames;
                int bTeamGamesMarked = match.score.Sets[currentSet].bTeamGames;

                int winingGameTeam = aTeamIsPlaying ? aTeamGamesMarked : bTeamGamesMarked;
                int losingGameTeam = aTeamIsPlaying ? bTeamGamesMarked : aTeamGamesMarked;

                if (SetWin(winingGameTeam, losingGameTeam, lastGame))
                {
                    matchEvents.SetMarked();
                    CloseSet(aTeamIsPlaying, currentSet);

                    if (lastGame)
                    {
                        matchEvents.MatchClose();
                        EndMatch(); 
                    }
                    else
                    { 
                        NextSet(); 
                    }
                }
                else
                { NextGame(); }
            }
        }
        public void RemovePoint(bool aTeamUndo)
        {
            int currentSet = match.score.actualSet;
            int currentGame = match.score.Sets[currentSet].actualGame;

            if (CanRemovePoint(aTeamUndo, currentGame, currentSet))
            {
                RemovePoint(aTeamUndo, currentSet, currentGame);
            }
            else
            if(CanRewingGame(aTeamUndo))
            {
                RewindGame(aTeamUndo, currentGame);
                currentGame = match.score.Sets[currentSet].actualGame;
                RemovePoint(aTeamUndo, currentSet, currentGame);
            }
            else
            if (CanRewindSet(aTeamUndo))
            {
                RewindSet(aTeamUndo, currentSet);
                currentSet = match.score.actualSet;
                RewindGame(aTeamUndo, currentGame);
                currentGame = match.score.Sets[currentSet].actualGame;
                RemovePoint(aTeamUndo, currentSet, currentGame);
            }
            else
            {
                Debug.Log("you're at the begining of the match or at the state save of a load match");
            }
        }

        //Match
        private void EndMatch()
        {
            if (match.score.aTeamSets > match.score.bTeamSets)
            {
                match.teamA_WinMatch = true;
            }
            else
            {
                match.teamA_WinMatch = false;
            }

            match.matchEnd = true;

        }

        //Set
        private bool CanRewindSet(bool aTeamAction)
        {
            if (aTeamAction)
            {
                return match.score.aTeamSets > 0;
            }
            else
            {
                return match.score.bTeamSets > 0;
            }
        }
        private void RewindSet(bool aTeamAction, int currentSet)
        {
            if (aTeamAction)
            {
                match.score.aTeamSets--;
            }
            else
            {
                match.score.bTeamSets--;
            }

            match.score.Sets[currentSet].setEnd = false;
            match.score.actualSet--;
        }
        private void CloseSet(bool aTeamAction, int currentSet)
        {
            if (aTeamAction)
            {
                match.score.Sets[currentSet].aTeamWinSet = true;
                match.score.aTeamSets++;
            }
            else
            {
                match.score.Sets[currentSet].aTeamWinSet = false;
                match.score.bTeamSets++;
            }

            match.score.Sets[currentSet].setEnd = true;
        }
        private void NextSet()
        {
            match.score.actualSet++;
            match.score.Sets[match.score.actualSet].Games.Add(new Game());
        }
        //Game
        private bool CanRewingGame(bool aTeamAction)
        {
            if (aTeamAction)
            {
                return match.score.Sets[match.score.actualSet].aTeamGames > 0;
            }
            else
            {
                return match.score.Sets[match.score.actualSet].bTeamGames > 0;
            }
        }
        private void RewindGame(bool aTeamAction, int currentGame)
        {
            if (aTeamAction)
            {
                match.score.Sets[match.score.actualSet].aTeamGames--;
            }
            else
            {
                match.score.Sets[match.score.actualSet].bTeamGames--;
            }

            match.score.Sets[match.score.actualSet].Games[currentGame].gameEnd = false;
            match.score.Sets[match.score.actualSet].actualGame--;
        }
        private void CloseGame(bool aTeamAction, int currentGame)
        {
            if (aTeamAction)
            {
                match.score.Sets[match.score.actualSet].Games[currentGame].aTeamWinGame = true;
                match.score.Sets[match.score.actualSet].aTeamGames++;
            }
            else
            {
                match.score.Sets[match.score.actualSet].Games[currentGame].aTeamWinGame = false;
                match.score.Sets[match.score.actualSet].bTeamGames++;
            }

            match.score.Sets[match.score.actualSet].Games[currentGame].gameEnd = true;
        }
        private void NextGame()
        {
            match.score.Sets[match.score.actualSet].Games.Add(new Game());
            match.score.Sets[match.score.actualSet].actualGame++;
        }
        //Point
        private bool CanRemovePoint(bool aTeamAction, int currentGame, int currentSet)
        {
            if (aTeamAction)
            {
                return match.score.Sets[currentSet].Games[currentGame].aTeamPoint > 0;
            }
            else
            {
                return match.score.Sets[currentSet].Games[currentGame].bTeamPoint > 0;
            }
        }
        private void RemovePoint(bool aTeamAction, int currentSet, int currentGame)
        {
            if (aTeamAction)
            {
                match.score.Sets[currentSet].Games[currentGame].aTeamPoint--;
            }
            else
            {
                match.score.Sets[currentSet].Games[currentGame].bTeamPoint--;
            }
        }
        private void AddPoint(bool aTeamAction, int currentSet, int currentGame)
        {
            if (aTeamAction)
            {
                match.score.Sets[currentSet].Games[currentGame].aTeamPoint++;
            }
            else
            {
                match.score.Sets[currentSet].Games[currentGame].bTeamPoint++;
            }
        }
    }
}