using UnityEngine;

namespace TennisMatch
{
    public class _MatchRally : MonoBehaviour
    {
        [Header("Component")]
        [SerializeField] private _MatchScore score;

        [Header("Variable")]
        [SerializeField, Range(-3, 3)] private int rallyPosition = 0;
        public int Pos
        {
            get { return rallyPosition; }
            //Pour ne pas set le rally en dehors de la range de base
            set { rallyPosition = Mathf.Clamp(value, -3, 3); }
        }

        public void MovedTo(int pos)
        {
            pos = Mathf.Clamp(pos, -3, 3);

            rallyPosition = pos;

            if(Mathf.Abs(rallyPosition) == 3)
            {
                //ResetRally();
                Invoke("ResetRally", 0.2f);
            }
        }
        public void ResetRally()
        {
            rallyPosition = 0;
        }

        public static bool PointWin(int rallyPosition)
        {
            if (Mathf.Abs(rallyPosition) >= 3)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
