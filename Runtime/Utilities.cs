using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Xo.LiquidFramework
{
    public static class Utilities
    {
        public static string GetActiveFolderPath()
        {
            Type projectWindowUtilType = typeof(ProjectWindowUtil);
            MethodInfo getActiveFolderPath =
                projectWindowUtilType.GetMethod("GetActiveFolderPath", BindingFlags.Static | BindingFlags.NonPublic);
            if (getActiveFolderPath != null)
            {
                object obj = getActiveFolderPath.Invoke(null, new object[0]);
                string pathToCurrentFolder = obj.ToString();
                return pathToCurrentFolder;
            }

            Debug.LogError("Active File Path Is Null");
            return "";
        }
    }
}