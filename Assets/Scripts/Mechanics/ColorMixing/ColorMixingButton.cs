using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using ColorUtils;

namespace Mechanics.ColorMixing
{
    public class ColorMixingButton : MonoBehaviour
    {

        [SerializeField]
        private ColorHelper.AcceptableColor _colorName;

        private LinkedList<(EventTriggerType, System.Action<GameObject>)> _events = new LinkedList<(EventTriggerType, System.Action<GameObject>)>();

        private ColorMixingManager _colorMixing;

        public void Construct(ColorMixingManager colorMixing)
        {
            _colorMixing = colorMixing;
            _events.Clear();
            _events.AddLast((EventTriggerType.PointerClick, _colorMixing.PointerClick));
            _events.AddLast((EventTriggerType.BeginDrag, _colorMixing.BeginDrag));
            _events.AddLast((EventTriggerType.PointerEnter, _colorMixing.PointerEnter));
            _events.AddLast((EventTriggerType.EndDrag, _colorMixing.EndDrag));
        }

        void Start()
        {
            Color color = ColorHelper.EnumToColor(_colorName);
            color.a = 0;
            ColorHelper.SetUIColor(gameObject, color);

            EventTrigger trigger = GetComponent<EventTrigger>();
            foreach ((EventTriggerType type, System.Action<GameObject> func) in _events)
            {
                EventTrigger.Entry entry = new EventTrigger.Entry();
                entry.eventID = type;
                entry.callback.AddListener((data) => func(gameObject));
                trigger.triggers.Add(entry);
            }
        }

    }
}