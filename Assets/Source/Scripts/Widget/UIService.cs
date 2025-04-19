using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Source.Scripts.Widget;
using UnityEditor;

public class UIService : SerializedMonoBehaviour, IUIService
{
    [SerializeField, DictionaryDrawerSettings(KeyLabel = "Widget Type", ValueLabel = "Asset"), 
     InfoBox("Select types implementing IWidget for keys.", InfoMessageType.Info)]
    private Dictionary<Type, AssetReference> widgets;

    [SerializeField] private Transform widgetsContainer;

    private Dictionary<Type, IWidget> _instancedWidgets = new Dictionary<Type, IWidget>();

    public async UniTask<TWidget> GetWidget<TWidget>() where TWidget : IWidget
    {
        if (_instancedWidgets.TryGetValue(typeof(TWidget), out var instancedWidget))
        {
            return (TWidget)instancedWidget;
        }

        if (widgets.TryGetValue(typeof(TWidget), out var assetReference))
        {
            var widgetInstance = await CreateWidgetAsync<TWidget>(assetReference);
            if (widgetInstance != null)
            {
                _instancedWidgets[typeof(TWidget)] = widgetInstance;
                return widgetInstance;
            }
        }

        Debug.LogError($"Failed to get or create widget of type {typeof(TWidget).Name}.");
        return default;
    }

    private async UniTask<TWidget> CreateWidgetAsync<TWidget>(AssetReference assetReference) where TWidget : IWidget
    {
        try
        {
            var handle = Addressables.LoadAssetAsync<GameObject>(assetReference);
            var prefab = await handle.Task.AsUniTask();
            var instance = Instantiate(prefab, widgetsContainer);
            var widget = instance.GetComponent<TWidget>();

            if (widget == null)
            {
                Debug.LogError($"Widget component of type {typeof(TWidget).Name} not found on instantiated prefab.");
                Addressables.Release(handle);
                Destroy(instance);
                return default;
            }

            Addressables.Release(handle);
            return widget;
        }
        catch (Exception ex)
        {
            Debug.LogError($"Failed to create widget of type {typeof(TWidget).Name}: {ex.Message}");
            return default;
        }
    }
    
    #if UNITY_EDITOR

    [Button]
    private async void ValidateWidgets()
    {
        Dictionary<Type, AssetReference> newWidgets = new Dictionary<Type, AssetReference>();

        foreach (var widget in widgets)
        {
            var handle = Addressables.LoadAssetAsync<GameObject>(widget.Value);
            var prefab = await handle.Task.AsUniTask();

            if (prefab.TryGetComponent(typeof(IWidget), out var component))
            {
                newWidgets[component.GetType()] = widget.Value;

                Debug.Log(
                    $"{prefab.name} with key {widget.Key} has component {component.GetType()}");
            }

            Addressables.Release(handle);
        }

        widgets = newWidgets;
    }

    #endif
}