using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TennisMatch
{
    public class ExchangeInvoker : MonoBehaviour
    {
        [Header("GameEvent")]
        private MatchEvents matchEvents;

        [Header("Variable")]
        static Queue<ExchangeCommand> exchangeBuffer;

        private void Awake()
        {
            matchEvents = MatchEvents.Instance;
            exchangeBuffer = new Queue<ExchangeCommand>();
        }

        public static void AddExchange(ExchangeCommand exchange)
        {
            exchangeBuffer.Enqueue(exchange);
        }

        void Update()
        {
            if(exchangeBuffer.Count > 0)
            {
                ExchangeCommand exchange = exchangeBuffer.Dequeue();
                
                exchange.Execute();
                
                matchEvents.Exchange(exchange.info.aTeamShoot);

            }
        }
    }
}
