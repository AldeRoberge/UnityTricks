using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Object = System.Object;

namespace UnityUtility
{
    public static class TMPChangeListener
    {
        private static readonly Dictionary<Object, List<Action<string>>> listeners = new Dictionary<Object, List<Action<string>>>();

        private static bool HasBeenInit;

        public static void OnChange(this TextMeshProUGUI textMeshPro, Action<string> onChange)
        {
            if (!HasBeenInit)
            {
                HasBeenInit = true;
                TMPro_EventManager.TEXT_CHANGED_EVENT.Add(OnTextChanged);
            }

            if (listeners.ContainsKey(textMeshPro))
            {
                listeners[textMeshPro].Add(onChange);
            }
            else
            {
                listeners.Add(textMeshPro, new List<Action<string>> { onChange });
            }
        }

        private static void OnTextChanged(object o)
        {
            Dispatcher.Instance.Invoke(() =>
            {
                if (listeners.ContainsKey(o))
                {
                    listeners[o].ForEach(action => action(o.ToString()));
                }
            });
        }
    }
}