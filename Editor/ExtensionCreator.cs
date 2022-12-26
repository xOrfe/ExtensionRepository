#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;


namespace Xo.LiquidFramework
{
    public class ContentExtensionCreator : EditorWindow
    {
        private string _extensionName;
        private string _description;
        private string _domain;
        
        [MenuItem("Xo/Extension Creator")]
        public static void ShowWindow()
        {
            var window = CreateInstance(typeof(ContentExtensionCreator)) as ContentExtensionCreator;
            if (window != null) window.ShowUtility();
        }

        void OnEnable()
        {
            titleContent = new GUIContent("ContentExtension Create Wizard");
        }

        void OnGUI()
        {
            _extensionName = EditorGUILayout.TextField("Extension Name", _extensionName);
            _description = EditorGUILayout.TextField("Description(?)", _description);
            _domain = EditorGUILayout.TextField("Domain", _domain);

            if (GUILayout.Button("CREATE"))
            {
                if (string.IsNullOrEmpty(_extensionName))
                {
                    EditorUtility.DisplayDialog("Empty Task Name", "Please give the new task a name", "OK");
                    return;
                }

                CreateFile(GetExtensionTemplate());


                _extensionName = "";
                GUIUtility.hotControl = 0;
                GUIUtility.keyboardControl = 0;
            }

            GUILayout.Label(GetExtensionTemplate());
        }

        void CreateFile(string template)
        {
            var path = GetUniquePath();

            if (System.IO.File.Exists(path))
            {
                if (!EditorUtility.DisplayDialog("File Exists", "Overwrite file?", "YES", "NO"))
                {
                    return;
                }
            }

            System.IO.File.WriteAllText(path, template);
            AssetDatabase.Refresh();
            Debug.LogWarning("New Extension is placed under: " + path);
        }

        string GetUniquePath()
        {
            var path = "";
            
            if (System.IO.Path.GetExtension(path) != "")
            {
                path = path.Replace(System.IO.Path.GetFileName(AssetDatabase.GetAssetPath(Selection.activeObject)), "");
            }

            return AssetDatabase.GenerateUniqueAssetPath(path + "/" + _extensionName + ".cs");
        }

        string GetExtensionTemplate()
        {
            return
                "   " + "\n\n" +
                "namespace " + ( _domain) + "{\n\n" +
                (!string.IsNullOrEmpty(_description) ? "\t[Description(\"" + _description + "\")]\n" : "") +
                "\tpublic struct " + _extensionName + " : IExtension" +
                "{\n\n" +
                "\t\t//Init.\n" +
                "\t\tpublic void Init(BlockContent blockContent){ }\n" +
                "\n" +
                "\t\t//Execute.\n" +
                "\t\tprivate void Execute(ContentEventArgs e){ }\n" +
                
                "\t}\n" +
                "}";
        }
    }
}

#endif