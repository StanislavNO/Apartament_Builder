using Source.CodeBase.Controllers;
using Source.CodeBase.Gameplay.Character;
using Source.CodeBase.Infrastructure.Services.InputService;
using UnityEngine;
using Zenject;

namespace Source.CodeBase.Infrastructure.Installers
{
    public class SceneInstaller : MonoInstaller
    {
        [SerializeField] private Player _player;

        public override void InstallBindings()
        {
            BindServices();
            BindCharacter();
            BindControllers();
        }

        private void BindControllers()
        {
            Container.BindInterfacesTo<BuildController>().AsSingle();
        }

        private void BindServices()
        {
            Container
                .BindInterfacesTo<StandaloneInput>()
                .AsSingle();
        }

        private void BindCharacter()
        {
            Container
                .BindInterfacesAndSelfTo<Player>()
                .FromInstance(_player)
                .AsSingle();

            Container
                .BindInterfacesTo<MovementController>()
                .AsSingle()
                .NonLazy();
        }
    }
}