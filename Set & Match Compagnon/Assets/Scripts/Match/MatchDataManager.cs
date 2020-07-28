using System.Text.RegularExpressions;
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
        private MatchEvents matchEvents;

        private void Awake()
        {
            //Reset du matchData ou erreur s'il n'est pas là
            if(currentMatch == null)
            {
                Debug.LogError("MatchData non attribué");
            }
            else
            {
                currentMatch.Reboot();
            }

            //Prends l'instance de MatchEvents
            matchEvents = MatchEvents.Instance;
        }

        private void OnEnable()
        {
            matchEvents.onUndo += OnUndo;

            matchEvents.onExchange += OnExchange;
            matchEvents.onPointMarked += OnPointMarked;
            matchEvents.onGameMarked += OnGameMarked;
            matchEvents.onSetMarked += OnSetMarked;

            matchEvents.onMatchStoped += OnMatchStoped;
            matchEvents.onMatchEnd += OnMatchEnd;
            matchEvents.onMatchClose += OnMatchEnd;
        }
        private void OnDisable()
        {
            matchEvents.onUndo -= OnUndo;

            matchEvents.onExchange -= OnExchange;
            matchEvents.onPointMarked -= OnPointMarked;
            matchEvents.onGameMarked -= OnGameMarked;
            matchEvents.onSetMarked -= OnMatchClose;

            matchEvents.onMatchStoped -= OnMatchStoped;
            matchEvents.onMatchEnd -= OnMatchEnd;
            matchEvents.onMatchClose -= OnMatchClose;
        }

        private void OnUndo()
        {
            currentMatch.turnCount--;
        }

        private void OnExchange()
        {
            currentMatch.turnCount++;
        }
        private void OnPointMarked()
        {
            currentMatch.pointCount++;
        }
        private void OnGameMarked()
        {
            currentMatch.gameCount++;
        }
        private void OnSetMarked()
        {
            currentMatch.setCount++;
        }

        private void OnMatchStoped()
        {
            ConvertToMatchUnfinished();
            matchEvents.MatchClose();
        }
        private void OnMatchEnd()
        {
            ConvertToMatchComplete();
        }
        private void OnMatchClose()
        {
            currentMatch.Reboot();
        }


        private void ConvertToMatchUnfinished()
        {

        }
        private void ConvertToMatchComplete()
        {

        }

    }
}
