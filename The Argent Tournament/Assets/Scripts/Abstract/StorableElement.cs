using Assets.Scripts.Management;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
