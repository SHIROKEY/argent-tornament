using UnityEngine;
using Assets.Scripts.Abstract;
using Assets.Scripts.Management;

namespace Assets.Scripts.Logic
{
    public class Armor : DamageableObject
    {
        private ElementManager _elementManager;

        public float AbsorbtionPercent = 0;

        private void Start()
        {
            LinkToGameLogic(FindObjectOfType<GameLogicManager>());
        }

        public override void TakeDamage(Pointer pointer, Vector2 point)
        {
            var decreased = (int)Mathf.Round(AbsorbtionPercent * pointer.GetDamage() / 100);
            if (decreased>0)
            {
                pointer.DecreaseDamage(decreased);
                GameLogicManager.CreateFloatingText("blocked", point - new Vector2(300, 0), new Color(0, 1, 0));
            }
        }
    }
}
