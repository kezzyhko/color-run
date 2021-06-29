using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ColorUtils;

public class Properties : MonoBehaviour
{

    public enum Type
    {
        Other,
        Player,
        Free,
        Enemy,
        Obstacle,
    };

    public Type ObjectType;
    public ColorHelper.AcceptableColor ColorName;
    public Renderer[] Renderers;

    public static bool DoesTypeMatch(GameObject obj, Type type)
    {
        if (!obj.TryGetComponent(out Properties props)) return false;
        return props.ObjectType == type;
    }

    public static Renderer[] GetRenderers(GameObject obj)
    {

        if (!obj.TryGetComponent(out Properties props)) return null;
        return props.Renderers;
    }

    private void OnValidate()
    {
        if (Renderers == null || Renderers.Length == 0)
        {
            Renderers = GetComponentsInChildren<Renderer>();
        }

        ChangeColor cc = GetComponent<ChangeColor>();
        if (cc)
        {
            int[] tr = new int[] {1, 2, 4, 5, 3, 6, 7};
            ColorName = (ColorHelper.AcceptableColor) tr[(int)cc.ColorName];
        }
        else
        {
            ColorName = 0;
        }
        UnityEditor.EditorUtility.SetDirty(gameObject);
        UnityEditor.PrefabUtility.RecordPrefabInstancePropertyModifications(gameObject);
    }

    /*void Start()
    {
        Material material = ColorHelper.GetObjectMaterial(gameObject);
        material = Instantiate(material);
        material.color = ColorHelper.EnumToColor(ColorName);
        ColorHelper.SetObjectMaterial(gameObject, material);
    }*/

            }