using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Utils;

namespace Mechanics.ColorMixing
{
    public class ColorMixingButton : MonoBehaviour
    {

        [SerializeField]
        private ColorHelper.AcceptableColor _colorName;

        private LinkedList<(EventTriggerType, System.Action<GameObject>)> _events = new LinkedList<(EventTriggerType, System.Action<GameObject>)>();

        private EventTrigger _eventTrigger;

        public void Construct(EventTrigger eventTrigger, ColorMixingManager colorMixing)
        {
            _eventTrigger = eventTrigger;
            _events.Clear();
            _events.AddLast((EventTriggerType.PointerClick, colorMixing.PointerClick));
            _events.AddLast((EventTriggerType.BeginDrag, colorMixing.BeginDrag));
            _events.AddLast((EventTriggerType.PointerEnter, colorMixing.PointerEnter));
            _events.AddLast((EventTriggerType.EndDrag, colorMixing.EndDrag));
        }

        void Start()
        {
            Color color = ColorHelper.EnumToColor(_colorName);
            color.a = 0;
            ColorHelper.SetUIColor(gameObject, color);

            foreach ((EventTriggerType type, System.Action<GameObject> func) in _events)
            {
                EventTrigger.Entry entry = new EventTrigger.Entry();
                entry.eventID = type;
                entry.callback.AddListener((data) => func(gameObject));
                _eventTrigger.triggers.Add(entry);
            }
        }

        private void OnDestroy()
        {
            _eventTrigger.triggers.Clear();
        }

    }
}