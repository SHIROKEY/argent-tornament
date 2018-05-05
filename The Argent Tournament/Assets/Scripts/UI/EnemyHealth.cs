using Assets.Scripts.Abstract;

namespace Assets.Scripts.UI
{
    public class EnemyHealth : Bar
    {
        private Name _enemyName;

        public void Awake()
        {
            _enemyName = GetComponentInChildren<Name>();
            InitializeIndication();
        }

        public void Refresh(float maxAmount, string newName)
        {
            this.MaxAmount = maxAmount;
            _enemyName.SetName(newName);
            Increase(MaxAmount);
        }

        public bool IsOutOfHP(float amount)
        {
            var remainingHP = Decrease(amount);
            if (remainingHP <= 0)
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