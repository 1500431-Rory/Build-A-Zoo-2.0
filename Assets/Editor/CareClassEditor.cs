using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CareClass)), CanEditMultipleObjects]
public class PropertyHolderEditor : Editor
{

    public SerializedProperty
        careType_Prop,
        noAnimalsPerShelter_Prop,
        foodType_Prop;
    
        CareClass.FoodType foodType;

    void OnEnable()
    {
        // Setup the SerializedProperties
        careType_Prop = serializedObject.FindProperty("careType");
        noAnimalsPerShelter_Prop = serializedObject.FindProperty("noAnimalsPerShelter");
        foodType_Prop = serializedObject.FindProperty("foodType");
       
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(careType_Prop);

        CareClass.CareTypes ft = (CareClass.CareTypes)careType_Prop.enumValueIndex;

        switch (ft)
        {
            case CareClass.CareTypes.SHELTER:             
               // EditorGUILayout.FloatField(noAnimalsPerShelter_Prop, new GUIContent("noAnimalsPerShelter"));
                EditorGUILayout.FloatField("Animals per Shelter",CareClass.noAnimalsPerShelter);
                break;

            case CareClass.CareTypes.AID:
                break;

            case CareClass.CareTypes.FOOD:
                // EditorGUILayout.EnumPopup(foodType_Prop, new GUIContent("valForC"));
                //EditorGUILayout.EnumPopup(foodType_Prop);
                //CareClass.FoodType foodType;
                foodType = (CareClass.FoodType)EditorGUILayout.EnumPopup("Primitive to create:", foodType);
                break;

        }


        serializedObject.ApplyModifiedProperties();
    }
}