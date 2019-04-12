//using UnityEditor;
//using UnityEngine;

//[CustomEditor(typeof(Guid))]

//class GuidInspector : Editor
//{
//    void OnEnable()
//    {
//        Guid guid = (Guid)target;

//        if (guid.guid == System.Guid.Empty)
//        {
//            guid.Generate();
//            EditorUtility.SetDirty(target);
//        }
//    }

//    public override void OnInspectorGUI()
//    {
//        Guid guid = (Guid)target;

//        EditorGUILayout.SelectableLabel(guid.guid.ToString());
//    }
//}

//public class Guid : MonoBehaviour
//{
//    [SerializeField]
//    private string guidAsString;

//    private System.Guid _guid;
//    public System.Guid guid
//    {
//        get
//        {
//            if (_guid == System.Guid.Empty &&
//                 !System.String.IsNullOrEmpty(guidAsString) )
//            {
//                _guid = new System.Guid(guidAsString);
//            }
//            return _guid;
//        }
//    }

//    public void Generate()
//    {
//        _guid = System.Guid.NewGuid();
//        guidAsString = guid.ToString();
//    }
//}