using UnityEditor;

[InitializeOnLoad]
public class LayerInitializer
{
    private const int _userLayerStartIndex = 8;

    static LayerInitializer()
    {

        CreateLayer();
    }


    static void CreateLayer()
    {
        SerializedObject tagManager = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset")[0]);
        SerializedProperty layers = tagManager.FindProperty("layers");
        for (int i = layers.arraySize-1; i >= _userLayerStartIndex; i--)
        {
            SerializedProperty layerSP = layers.GetArrayElementAtIndex(i);
            if (layerSP.stringValue == DragNDropConfig.DragNDropLayerName)
            {
                return;
            }

        }
        for (int j = layers.arraySize-1; j >= _userLayerStartIndex; j--)
        {
            SerializedProperty layerSP = layers.GetArrayElementAtIndex(j);
            if (layerSP.stringValue == "")
            {
                layerSP.stringValue = DragNDropConfig.DragNDropLayerName;
                tagManager.ApplyModifiedProperties();
                return;
            }
        }
    }
}
