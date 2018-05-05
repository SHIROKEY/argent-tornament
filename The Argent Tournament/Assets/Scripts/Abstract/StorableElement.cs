using Assets.Scripts.Management;
using UnityEngine;

namespace Assets.Scripts.Abstract
{
    public abstract class StorableElement: MonoBehaviour
    {

        public GameLogicManager GameLogicManager { get; private set; }

        public void LinkToGameLogic(GameLogicManager gameLogicManager)
        {
            GameLogicManager = gameLogicManager;
        }
    }
}
