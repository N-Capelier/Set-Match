using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TennisMatch
{
    public class InputExchange : MonoBehaviour
    {
        [Header("GameEvent")]
        private MatchEvents matchEvents;

        [Header("Variable")]
        [SerializeField] private MatchData match;
        [SerializeField] private RallyScorer rally;

        static Stack<Exchange> moveHistory = new Stack<Exchange>();

        private void Awake()
        {
            matchEvents = MatchEvents.Instance;
        }

        public void ExchangeOf(int increment)
        {
            bool aTeamShoot = match.teamA_Turn;
            int pos = rally.value;

            bool isLastSet = match.currentSet >= match.MatchSetNumber;
            int set = match.currentSet;

            bool pointMarked = false;
            bool gameMarked = false;
            bool setMarked = false;

            pos = Mathf.Clamp(pos, -3, 3);
            increment = aTeamShoot? increment : -increment;

            pointMarked = PartyScore.PointWin( pos, increment);
            if (pointMarked)
            {
                if(aTeamShoot)
                {
                    gameMarked = PartyScore.GameWin(match.teamA_Score.point, match.teamB_Score.point);
                
                }
                else
                {
                    gameMarked = PartyScore.GameWin(match.teamB_Score.point, match.teamA_Score.point);
                }
            }
            if (gameMarked)
            {
                if (aTeamShoot)
                {
                    setMarked = PartyScore.SetWin(match.teamA_Score.gamePerSet[set - 1], match.teamB_Score.gamePerSet[set - 1], isLastSet);

                }
                else
                {
                    setMarked = PartyScore.SetWin(match.teamB_Score.gamePerSet[set - 1], match.teamA_Score.gamePerSet[set - 1], isLastSet);
                }
            }

            //Construction du move à save
            Exchange currentMove = new Exchange(aTeamShoot, false, pos, increment, pointMarked, gameMarked, setMarked);

            //Move Storage
            moveHistory.Push(currentMove);

            //UpdateRally
            rally.MovedTo(pos + increment, pointMarked);

            //Event du move
            matchEvents.Exchange(aTeamShoot);
            if (pointMarked)
            {
                matchEvents.PointMarked(aTeamShoot);
            }
            if (gameMarked)
            {
                matchEvents.GameMarked(aTeamShoot);
            }
            if (setMarked)
            {
                matchEvents.SetMarked(aTeamShoot);
            }

            matchEvents.VisualUpdate();
        }

    }
}
