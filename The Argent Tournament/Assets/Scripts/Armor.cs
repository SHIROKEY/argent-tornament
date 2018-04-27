using UnityEngine;
using System.Collections;

namespace Assets.Scripts
{
    public class Armor : DamageableObject
    {
        public float DamageAbsorbationAmount;

        public override void OnDamageTaken(float amount)
        {

        }

        public override void TakeDamage(Pointer pointer, Vector2 pos)
        {
            pointer.DecreaseDamage(DamageAbsorbationAmount);
        }
    }
}
