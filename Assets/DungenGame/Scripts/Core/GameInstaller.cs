using MVDEV.DungeonGame.Scripts.Controllers;
using MVDEV.DungeonGame.Scripts.Models;
using MVDEV.DungeonGame.Scripts.PlayerScripts;
using MVDEV.DungeonGame.Scripts.PlayerScripts.Interface;
using MVDEV.DungeonGame.Scripts.Views;
using UnityEngine;
using Zenject;

namespace MVDEV.DungeonGame.Scripts.Core
{
    public class GameInstaller: MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<PlayerModel>().AsSingle();
            Container.Bind<PlayerView>().FromComponentInHierarchy().AsSingle();
            Container.Bind<PlayerController>().FromComponentInHierarchy().AsSingle();
            Container.Bind<PlayerStatsController>().FromComponentInHierarchy().AsSingle();
        }
    }
}

