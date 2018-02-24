using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(WeaponConfig)), CanEditMultipleObjects]
public class WeaponConfigEditor : Editor
{

    public SerializedProperty
        weaponType_Prop,
        grenadeObject_Prop,
        grenadeThrowForce_Prop,
        gunDamage_Prop,
        gunRange_Prop,
        gunFireRate_Prop,
        gunKnockbackForce_Prop,
        gunGraphics_Prop;

    void OnEnable()
    {
        // Setup the SerializedProperties
        weaponType_Prop = serializedObject.FindProperty("weaponType");
        grenadeObject_Prop = serializedObject.FindProperty("grenade_Obj");
        grenadeThrowForce_Prop = serializedObject.FindProperty("grenade_throwForce");
        gunDamage_Prop = serializedObject.FindProperty("gun_Damage");
        gunRange_Prop = serializedObject.FindProperty("gun_Range");
        gunFireRate_Prop = serializedObject.FindProperty("gun_FireRate");
        gunKnockbackForce_Prop = serializedObject.FindProperty("gun_KnockbackForce");
        gunGraphics_Prop = serializedObject.FindProperty("gun_Graphics");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(weaponType_Prop);

        WeaponConfig.WeaponType st = (WeaponConfig.WeaponType)weaponType_Prop.enumValueIndex;

        switch (st)
        {
            case WeaponConfig.WeaponType.Grenade:
                EditorGUILayout.ObjectField(grenadeObject_Prop, new GUIContent("grenade_Obj"));
                EditorGUILayout.IntSlider(grenadeThrowForce_Prop, 0, 1000, new GUIContent("grenade_throwForce"));
                break;

            case WeaponConfig.WeaponType.Gun:
                EditorGUILayout.IntSlider(gunDamage_Prop, 0, 1000, new GUIContent("gun_Damage"));
                EditorGUILayout.IntSlider(gunRange_Prop, 0, 1000, new GUIContent("gun_Range"));
                EditorGUILayout.IntSlider(gunFireRate_Prop, 0, 50, new GUIContent("gun_FireRate"));
                EditorGUILayout.IntSlider(gunKnockbackForce_Prop, 0, 1000, new GUIContent("gun_KnockbackForce"));
                EditorGUILayout.ObjectField(gunGraphics_Prop, new GUIContent("gun_Graphics"));
                break;

            case WeaponConfig.WeaponType.C:
                break;
        }

        serializedObject.ApplyModifiedProperties();
    }
}
