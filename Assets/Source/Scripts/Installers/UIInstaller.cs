using System;
using System.Collections.Generic;
using System.Diagnostics;
using Sirenix.OdinInspector;
using Source.Scripts.Widget;
using UnityEngine;
using Zenject;

namespace Source.Scripts.Installers
{
    public class UIInstaller : MonoInstaller
    {
        [SerializeField] private UIService _uiService;

        public override void InstallBindings()
        {
            Container.Bind<IUIService>().FromInstance(_uiService).AsSingle();
        }
    }
}
