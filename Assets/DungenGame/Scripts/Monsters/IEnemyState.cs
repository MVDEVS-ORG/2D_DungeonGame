using MVDEV.DungeonGame.Scripts.Monsters;
using System.Collections;
using UnityEngine;

namespace Assets.DungenGame.Scripts.Monsters
{
    public interface IEnemyState
    {
        void Enter(EnemyAI enemy);
        void Update();
        void Exit();
    }
}