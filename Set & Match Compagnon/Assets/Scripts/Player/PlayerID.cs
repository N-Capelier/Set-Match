using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player.Encryption;

namespace Player
{
    /// <summary>
    /// NCO
    /// </summary>
    public class PlayerID
    {
        public PlayerStats stats;
        public PlayerData data;
        public readonly string name;
        public readonly string surname;
        public readonly string username;
        public readonly string mail;
        public readonly Password password;

        public Vector2 location;

        public PlayerID(string name, string surname, string username, string mail, Password password)
        {
            stats = new PlayerStats();
            data = new PlayerData();

            this.name = name;
            this.surname = surname;
            this.username = username;
            this.mail = mail;
            this.password = password;
            location = new Vector2();
        }
    }

}