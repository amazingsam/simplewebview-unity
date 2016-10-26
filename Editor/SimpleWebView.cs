using UnityEngine;
using UnityEditor;
using System.Reflection;

// A simple WebView EditorWindow for your scripting fun.
// Just throw it in an /Editor/ folder. Be warned, only tested on OSX (El Capitan) with Unity 5.4.2f1
// Use at your own risk, variations of this script might cause the whole editor to crash

public class SimpleWebView : ScriptableObject
{

  // Required to overload and keep it static else the Editor will unparent it.
  static BindingFlags bf = BindingFlags.Static | BindingFlags.Public | BindingFlags.FlattenHierarchy;

  // Fancy menu item ofcourse
  [MenuItem ("Experimental/SimpleWebView")]
  static void Example()
  {
      // We need the WebViewEditorWindowTabs and the DLL for this to work
      var t = Types.GetType ("UnityEditor.Web.WebViewEditorWindow", "UnityEditor.dll");
      var swv = t.GetMethod("Create", bf);
      // Late bound operations cannot be performed on certain types or methods so it needs to generic
      swv = swv.MakeGenericMethod(t);
      // Invoke it, set the title and url. The placing and sizing of the toolbox is not really going as expected,
      // but once docked and the layout is saved, no trouble - the point where I stop tinkering ;)
      // I use this to connect to my local meteor server, but you can use any url - even the https kind.
      swv.Invoke (null, new object[] { "Window title", "http://localhost:3000", 0, 0, 400, 600 });
  }

  // Horrible:
  // Once you quit and restart Unity editor, this lovely message will appear
  // Removed unparented EditorWindow while reading window layout: window #xx,
  // type=UnityEditor.Web.WebViewEditorWindowTabs, instanceID=xxxx
  // UnityEditor.WindowLayout:LoadWindowLayout(String, Boolean)

}
