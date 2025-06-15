using UnityEngine;
using UnityEditor;
using UnityEditor.Build;

namespace CompanyName.LevelEditorService.Editor
{
    /// <summary>
    /// Editor menu items for Level Editor functionality
    /// </summary>
    public static class LevelEditorMenuItems
    {
        private const string LEVEL_EDITOR_DEFINE = "LEVEL_EDITOR_ON";
        private const string MENU_PATH_ENABLE = "LevelEditor/Enable";
        private const string MENU_PATH_DISABLE = "LevelEditor/Disable";
        private const string LEVEL_EDITOR_SCENE_PATH = "Assets/Scenes/LevelEditorScene.unity";

        [MenuItem(MENU_PATH_ENABLE, true)]
        static bool EnableLevelEditorValidator()
        {
            // Only show this menu item when LEVEL_EDITOR_ON is not defined
            return !IsLevelEditorEnabled();
        }

        [MenuItem(MENU_PATH_ENABLE)]
        static void EnableLevelEditor()
        {
            bool defineAdded = AddScriptingDefine();
            bool sceneEnabled = EnableSceneInBuildSettings();

            if (defineAdded || sceneEnabled)
            {
                Debug.Log("Level Editor has been enabled successfully!");
                // Force recompilation when scripting defines change
                if (defineAdded)
                {
                    AssetDatabase.Refresh();
                }
            }
            else
            {
                Debug.Log("Level Editor was already enabled");
            }
        }

        [MenuItem(MENU_PATH_DISABLE, true)]
        static bool DisableLevelEditorValidator()
        {
            // Only show this menu item when LEVEL_EDITOR_ON is defined
            return IsLevelEditorEnabled();
        }

        [MenuItem(MENU_PATH_DISABLE)]
        static void DisableLevelEditor()
        {
            bool defineRemoved = RemoveScriptingDefine();
            bool sceneDisabled = DisableSceneInBuildSettings();

            if (defineRemoved || sceneDisabled)
            {
                Debug.Log("Level Editor has been disabled successfully!");
                // Force recompilation when scripting defines change
                if (defineRemoved)
                {
                    AssetDatabase.Refresh();
                }
            }
            else
            {
                Debug.Log("Level Editor was already disabled");
            }
        }

        /// <summary>
        /// Adds the LEVEL_EDITOR_ON scripting define symbol
        /// </summary>
        /// <returns>True if the define was added, false if it was already present</returns>
        private static bool AddScriptingDefine()
        {
            BuildTarget buildTarget = EditorUserBuildSettings.activeBuildTarget;
            BuildTargetGroup targetGroup = BuildPipeline.GetBuildTargetGroup(buildTarget);
            var namedBuildTarget = NamedBuildTarget.FromBuildTargetGroup(targetGroup);

            string definesString = PlayerSettings.GetScriptingDefineSymbols(namedBuildTarget);

            if (!definesString.Contains(LEVEL_EDITOR_DEFINE))
            {
                if (string.IsNullOrEmpty(definesString))
                {
                    definesString = LEVEL_EDITOR_DEFINE;
                }
                else
                {
                    definesString += ";" + LEVEL_EDITOR_DEFINE;
                }

                PlayerSettings.SetScriptingDefineSymbols(namedBuildTarget, definesString);
                Debug.Log($"Added {LEVEL_EDITOR_DEFINE} scripting define symbol for {namedBuildTarget}");
                return true;
            }

            return false;
        }

        /// <summary>
        /// Removes the LEVEL_EDITOR_ON scripting define symbol
        /// </summary>
        /// <returns>True if the define was removed, false if it wasn't present</returns>
        private static bool RemoveScriptingDefine()
        {
            BuildTarget buildTarget = EditorUserBuildSettings.activeBuildTarget;
            BuildTargetGroup targetGroup = BuildPipeline.GetBuildTargetGroup(buildTarget);
            var namedBuildTarget = NamedBuildTarget.FromBuildTargetGroup(targetGroup);

            string definesString = PlayerSettings.GetScriptingDefineSymbols(namedBuildTarget);

            if (definesString.Contains(LEVEL_EDITOR_DEFINE))
            {
                var defines = definesString.Split(';');
                var newDefines = System.Array.FindAll(defines, define => define.Trim() != LEVEL_EDITOR_DEFINE);
                string newDefinesString = string.Join(";", newDefines);

                PlayerSettings.SetScriptingDefineSymbols(namedBuildTarget, newDefinesString);
                Debug.Log($"Removed {LEVEL_EDITOR_DEFINE} scripting define symbol for {namedBuildTarget}");
                return true;
            }

            return false;
        }

        /// <summary>
        /// Enables the LevelEditorScene in build settings
        /// </summary>
        /// <returns>True if the scene was enabled, false if it was already enabled or not found</returns>
        private static bool EnableSceneInBuildSettings()
        {
            var buildScenes = EditorBuildSettings.scenes;
            
            for (int i = 0; i < buildScenes.Length; i++)
            {
                if (buildScenes[i].path == LEVEL_EDITOR_SCENE_PATH)
                {
                    if (!buildScenes[i].enabled)
                    {
                        buildScenes[i].enabled = true;
                        EditorBuildSettings.scenes = buildScenes;
                        Debug.Log($"Enabled {LEVEL_EDITOR_SCENE_PATH} in build settings");
                        return true;
                    }
                    return false; // Already enabled
                }
            }

            // Scene not found in build settings, add it
            var newScene = new EditorBuildSettingsScene(LEVEL_EDITOR_SCENE_PATH, true);
            var newBuildScenes = new EditorBuildSettingsScene[buildScenes.Length + 1];
            System.Array.Copy(buildScenes, newBuildScenes, buildScenes.Length);
            newBuildScenes[buildScenes.Length] = newScene;
            EditorBuildSettings.scenes = newBuildScenes;
            Debug.Log($"Added and enabled {LEVEL_EDITOR_SCENE_PATH} in build settings");
            return true;
        }

        /// <summary>
        /// Disables the LevelEditorScene in build settings
        /// </summary>
        /// <returns>True if the scene was disabled, false if it was already disabled or not found</returns>
        private static bool DisableSceneInBuildSettings()
        {
            var buildScenes = EditorBuildSettings.scenes;
            
            for (int i = 0; i < buildScenes.Length; i++)
            {
                if (buildScenes[i].path == LEVEL_EDITOR_SCENE_PATH)
                {
                    if (buildScenes[i].enabled)
                    {
                        buildScenes[i].enabled = false;
                        EditorBuildSettings.scenes = buildScenes;
                        Debug.Log($"Disabled {LEVEL_EDITOR_SCENE_PATH} in build settings");
                        return true;
                    }
                    return false; // Already disabled
                }
            }

            Debug.LogWarning($"{LEVEL_EDITOR_SCENE_PATH} not found in build settings");
            return false;
        }

        /// <summary>
        /// Checks if the Level Editor is currently enabled via scripting define symbols
        /// </summary>
        /// <returns>True if LEVEL_EDITOR_ON define is present</returns>
        private static bool IsLevelEditorEnabled()
        {
            BuildTarget buildTarget = EditorUserBuildSettings.activeBuildTarget;
            BuildTargetGroup targetGroup = BuildPipeline.GetBuildTargetGroup(buildTarget);
            var namedBuildTarget = NamedBuildTarget.FromBuildTargetGroup(targetGroup);
            string definesString = PlayerSettings.GetScriptingDefineSymbols(namedBuildTarget);
            
            return definesString.Contains(LEVEL_EDITOR_DEFINE);
        }
    }
} 