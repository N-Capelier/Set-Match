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

        public static event Action onExchange;
        public static event Action onUndo;

        public static event Action onFault;
        public static event Action onNet;

        public static event Action onPointMarked;
        public static event Action onGameMarked;
        public static event Action onSetMarked;

        public static event Action onVisualUpdate;

        public static event Action onMatchStart;
        public static event Action onMatchStoped;
        public static event Action onMatchEnd;
        public static event Action onMatchClose;

        #endregion

        #region Events Call

        public static void Exchange() => onExchange?.Invoke();
        public static void Undo() => onUndo?.Invoke();

        public static void Faute() => onFault?.Invoke();
        public static void Filet() => onNet?.Invoke();

        public static void PointMarked() => onPointMarked?.Invoke();
        public static void GameMarked() => onGameMarked?.Invoke();
        public static void SetMarked() => onSetMarked?.Invoke();

        public static void VisualUpdate() => onVisualUpdate?.Invoke();

        public static void MatchStart() => onMatchStart?.Invoke();
        public static void MatchStoped() => onMatchStoped?.Invoke();
        public static void MatchEnd() => onMatchEnd?.Invoke();
        public static void MatchClose() => onMatchClose?.Invoke();

        #endregion
    }
}
