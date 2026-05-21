using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using GameWideSystems.CameraManagement;
using GameWideSystems.InputManager;
using GameWideSystems.LocalizationWrapper;
using GameWideSystems.UIManagement.Screen;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace GameWideSystems.UIManagement
{
    public class UIManager
    {
        internal IScreenHostProvider ScreenHostsProvider { get; private set; }
        internal InputControlFacade InputControlFacade { get; private set; }
        internal Stack<UIScreenBase> GameScreens { get; private set; } = new();
        internal ConcurrentDictionary<Type, UIScreenBase> PreloadedScreens { get; } = new();
        internal ILocalizationManager LocalizationManager { get; private set; }
        internal ICameraManager CameraManager { get; private set; }

        public UIManager(IScreenHostProvider screenHostsProvider, InputControlFacade inputControlFacade, ILocalizationManager localizationManager, ICameraManager cameraManager)
        {
            ScreenHostsProvider = screenHostsProvider;
            InputControlFacade = inputControlFacade;
            LocalizationManager = localizationManager;
            CameraManager = cameraManager;
        }
        
        internal async UniTask<UniTask> OpenScreen(IUIScreenBuilder screenBuilder,
            IScreenParams screenParams,
            bool isSilent,
            ScreenHolder openedScreen,
            CancellationToken cancellationToken)
        {
            if (!PreloadedScreens.Remove(screenBuilder.GetType(), out var screenGO))
            {
                screenGO = await screenBuilder.Build(ScreenHostsProvider.CreationHolder, CameraManager.UICamera, cancellationToken);
                screenGO.RootCanvasGroup.gameObject.SetActive(false);
            }
            
            Transform tileHolder = ScreenHostsProvider.GetHolderFor(screenGO.ScreenHolderType);
            screenGO.transform.SetParent(tileHolder);
            

            UIScreenBase uiScreen = screenGO.RootCanvasGroup.gameObject.GetComponent<UIScreenBase>();
            openedScreen.ScreenBase = uiScreen;
            GameScreens.Push(uiScreen);
            
            Canvas canvas = uiScreen.GetComponent<Canvas>();
            canvas.sortingOrder = GameScreens.Count;
            
            foreach (ILocalizer localizer in screenGO.RootCanvasGroup.GetComponentsInChildren<ILocalizer>(true))
            {
                await localizer.Localize(LocalizationManager, cancellationToken);
            }

            UniTask screenAwaitable = await uiScreen.OnBeforeOpen(screenParams, cancellationToken);
            
            if (isSilent)
            {
                await uiScreen.OnOpenSilently(cancellationToken);
                return screenAwaitable;
            }

            await uiScreen.OnOpen(cancellationToken);

            return screenAwaitable;
        }
        
        internal async UniTask CloseAll(bool isSilent, ScreenType screenType, CancellationToken cancellationToken)
        {
            while (GameScreens.TryPeek(out UIScreenBase screen) && 
                   (screenType == ScreenType.Dialog || screen.ScreenType == ScreenType.Dialog))
            {
                UIScreenBase uiScreen = GameScreens.Pop();
                await CloseScreenAwaitableFacade(uiScreen, isSilent, cancellationToken);
            }
        }

        internal UniTask CloseTop(bool isSilent, CancellationToken cancellationToken)
        {
            if (GameScreens.TryPop(out UIScreenBase screen))
            {
                return CloseScreenAwaitableFacade(screen, isSilent, cancellationToken);
            }
            
            return UniTask.CompletedTask;
        }

        internal UniTask<UniTask> LoadTopScreen(CancellationToken cancellationToken)
        {
            if (GameScreens.TryPeek(out UIScreenBase screen))
            {
                return screen.OnOverlayRemoved(cancellationToken);
            }
            
            return UniTask.FromResult(UniTask.CompletedTask);
        }
        
        internal async UniTask<UniTask> UnloadTopScreen(CancellationToken cancellationToken)
        {
            if (GameScreens.TryPeek(out UIScreenBase screen))
            {
                await screen.OnBecomingOverlaid(cancellationToken);
                return UniTask.CompletedTask;
            }
            
            return UniTask.FromResult(UniTask.CompletedTask);
        }
        
        
        private UniTask CloseScreenAwaitableFacade(
            UIScreenBase uiScreen, 
            bool isClosedSilently, 
            CancellationToken cancellationToken)
        {
            return CloseScreenInternal(uiScreen, isClosedSilently, cancellationToken);
        }
        
        private async UniTask CloseScreenInternal(
            UIScreenBase uiScreen, 
            bool isClosedSilently, 
            CancellationToken cancellationToken)
        {
            if (isClosedSilently)
            {
                await uiScreen.OnCloseSilently(cancellationToken);
            }
            else
            {
                await uiScreen.OnClose(cancellationToken);
            }
            
            uiScreen.OnAfterClose();
            
            if (uiScreen.IsPreloaded)
            {
                PreloadedScreens.TryAdd(uiScreen.ScreenBuilder.GetType(), uiScreen);
                uiScreen.RootCanvasGroup.transform.SetParent(ScreenHostsProvider.ScreenPool);
            }
            else
            {
                uiScreen.Dispose();
                Addressables.ReleaseInstance(uiScreen.RootCanvasGroup.gameObject);
            }
        }
        
#region preload functionality
        
        public async UniTask PreloadScreen<T>(T screenBuilder, CancellationToken cancellationToken) where T : IUIScreenBuilder
        {
            if (PreloadedScreens.ContainsKey(typeof(T)))
            {
                return;
            }

            UIScreenBase uiScreen = await screenBuilder.Build(ScreenHostsProvider.CreationHolder, CameraManager.UICamera, cancellationToken);
            Transform poolHolder = ScreenHostsProvider.GetHolderFor(ScreenHolderType.ScreenPool);
            uiScreen.RootCanvasGroup.transform.SetParent(poolHolder);
            
            uiScreen.IsPreloaded = true;
            
            PreloadedScreens.TryAdd(typeof(T), uiScreen);
        }

        public void UnloadPreloadedScreen<T>() where T : IUIScreenBuilder
        {
            if (!PreloadedScreens.TryRemove(typeof(T), out UIScreenBase screen))
            {
                return;
            }
            
            CleanUpPreloadedScreen(screen);
        }

        public void UnloadAllPreloadedScreens()
        {
            foreach (UIScreenBase item in PreloadedScreens.Values)
            {
                CleanUpPreloadedScreen(item);
            }
            
            PreloadedScreens.Clear();
        }
        

        private void CleanUpPreloadedScreen(UIScreenBase screen)
        {
            if (!screen.IsPreloaded)
            {
                return;
            }
            
            screen.Dispose();
            Addressables.ReleaseInstance(screen.RootCanvasGroup.gameObject);
        }
        
#endregion
        
    }
}