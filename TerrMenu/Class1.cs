#pragma warning disable CS0626 // Method, operator, or accessor is marked external and has no attributes on it

using HarmonyLib;
using System.Reflection;

namespace Terraria
{
  class patch_Main : Main
  {
    protected extern void orig_Initialize();
    protected override void Initialize()
    {
      Console.WriteLine("LMAO");
      var f = this.GetType().GetField("quickSplash", BindingFlags.Instance | BindingFlags.NonPublic);
      f.SetValue(this, true);
      orig_Initialize();
    }
  }
  class Patches
  {
    class QuickSplashSetPatch
    {
      
    }
  }
}