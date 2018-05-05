using UnityEngine;
using Assets.Scripts.Logic;

namespace Assets.Scripts.Abstract
{
    public abstract class DamageableObject : StorableElement
    {
        public abstract void TakeDamage(Pointer pointer, Vector2 point);
    }
}