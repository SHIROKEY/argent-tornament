using UnityEngine;
using System.Collections;

namespace Assets.Scripts
{
    public class Armor : DamageableObject
    {
        public NumberTypeDivider AbsorbtionType = NumberTypeDivider.Constant;
        public float DamageAbsorbationAmount = 0;
        public float AbsorbtionPerLevel = 0;

        public override void TakeDamage(Pointer pointer, Vector2 pos)
        {
            pointer.DecreaseDamage(DamageAbsorbationAmount/(int)AbsorbtionType);
        }
    }
}
