using UnityEngine;
using System.Collections;

namespace Assets.Scripts
{
    public abstract class DamageableObject : MonoBehaviour
    {
        public abstract void TakeDamage(Pointer pointer, Vector2 point);
    }
}