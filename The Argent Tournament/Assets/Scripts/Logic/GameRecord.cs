using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Logic
{
    public class GameRecord
    {
        public float Score { get; set; }

        public float StaminaAmount { get; set; }

        public float DamagePerStaminaPoint { get; set; }

        public int EnemyLevel { get; set; }

        public string KillerName { get; set; }
    }
}
