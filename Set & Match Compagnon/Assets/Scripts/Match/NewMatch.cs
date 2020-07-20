using UnityEngine;

namespace TennisMatch
{
    /// <summary>
    /// ARD
    /// </summary>
    public class NewMatch : MonoBehaviour
    {
        private MatchData matchData;

        public void HeadOrTail()
        {
            /// <summary>
            /// Head = 0
            /// Tail = 1
            /// </summary>
             
            if(Random.Range(0,2) == 1)
            {
                //Head Win 

            }
            else
            {
                //Tail Win 

            }
        }

    }
}
