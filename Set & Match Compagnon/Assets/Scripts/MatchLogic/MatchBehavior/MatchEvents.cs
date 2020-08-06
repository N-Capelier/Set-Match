using System;

namespace TennisMatch
{
    /// <summary>
    /// Gère tout les événements d'un match 
    /// <para> ARD </para>
    /// </summary>
    public class MatchEvents : Singleton<MatchEvents>
    {
        #region Events

        public event Action<bool> onExchange;
        public event Action onUndo;

        public event Action onFault;
        public event Action onNet;

        public event Action<bool> onPointMarked;
        public event Action<bool> onGameMarked;
        public event Action<bool> onSetMarked;

        public event Action onVisualUpdate;

        public event Action onMatchStart;
        public event Action onMatchStoped;
        public event Action<bool> onMatchEnd;
        public event Action onMatchClose;

        #endregion

        #region Events Call

        public void Exchange(bool aTeamAction) => onExchange?.Invoke(aTeamAction);
        public void Undo() => onUndo?.Invoke();

        public void Faute() => onFault?.Invoke();
        public void Filet() => onNet?.Invoke();

        public void PointMarked(bool aTeamAction) => onPointMarked?.Invoke(aTeamAction);
        public void GameMarked(bool aTeamAction) => onGameMarked?.Invoke(aTeamAction);
        public void SetMarked(bool aTeamAction) => onSetMarked?.Invoke(aTeamAction);

        public void VisualUpdate() => onVisualUpdate?.Invoke();

        public void MatchStart() => onMatchStart?.Invoke();
        public void MatchStoped() => onMatchStoped?.Invoke();
        public void MatchEnd(bool aTeamWinner) => onMatchEnd?.Invoke(aTeamWinner);
        public void MatchClose() => onMatchClose?.Invoke();

        #endregion
    }
}
