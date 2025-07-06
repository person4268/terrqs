#pragma warning disable CS0626 // Method, operator, or accessor is marked external and has no attributes on it

using System.Reflection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Terraria
{
  class patch_Main : Main
  {
    protected extern void orig_Initialize();
    protected override void Initialize()
    {
      Console.WriteLine("initialize was called");
      // private bool quickSplash;
      var qs = this.GetType().GetField("quickSplash", BindingFlags.Instance | BindingFlags.NonPublic);
      qs.SetValue(this, true);
      // private int splashCounter;
      var sc = this.GetType().GetField("splashCounter", BindingFlags.Instance | BindingFlags.NonPublic);
      sc.SetValue(this, 125);

      orig_Initialize();
    }

    protected extern void orig_DrawSplash(GameTime gameTime);
    bool drewOnce = false;
    bool init1Ran = false;
    bool init2Ran = false;
    Texture2D? loader;
    protected new void DrawSplash(GameTime gameTime)
    {
      if (loader == null)
      {
        try
        {
          loader = this.Content.Load<Texture2D>("loader");
        }
        catch
        {
          loader = null;
          Console.WriteLine("couldnt load loader image");
        }
      }

      GraphicsDevice.Clear(Color.DarkGoldenrod);
      spriteBatch.Begin();
      GetType().GetMethod("DrawSplash_LoadingFlower", BindingFlags.NonPublic | BindingFlags.Instance).Invoke(this, new object[] { Color.White });
      if (loader != null)
      {
        Vector2 position = new Vector2(screenWidth / 2, screenHeight / 4);
        spriteBatch.Draw(loader, position, null, Color.White, 0, loader.Size() / 2, 1, SpriteEffects.None, 0);
      }
      spriteBatch.End();
      Draw(gameTime);

      if (!drewOnce) // lets get some stuff on early before we have to wait for shit to load
      {
        drewOnce = true;
        return;
      }

      GetType().GetMethod("TickLoadProcess", BindingFlags.NonPublic | BindingFlags.Instance).Invoke(this, null);

      int pendingAssets = Assets.PendingAssets;
      bool musicLoaded = (bool)GetType().GetField("_musicLoaded", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(this);
      bool artLoaded = (bool)GetType().GetField("_artLoaded", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(this);
      bool loadedEverything = Program.LoadedEverything;
      bool done = pendingAssets == 0 && musicLoaded && artLoaded && loadedEverything;
      Console.WriteLine("Pending assets: " + pendingAssets + ", musicLoaded: " + musicLoaded + ", artLoaded:" + artLoaded + ", loadedEverything: " + loadedEverything);

      if (!init1Ran && loadedEverything)
      {
        GetType().GetMethod("Initialize_AlmostEverything", BindingFlags.NonPublic | BindingFlags.Instance).Invoke(this, null);
        init1Ran = true;
      }
      if (!done) return;

      if (!init2Ran)
      {
        GetType().GetMethod("PostContentLoadInitialize", BindingFlags.NonPublic | BindingFlags.Static).Invoke(null, null);
        init2Ran = true;
      }

      showSplash = false;
      skipMenu = true;
      fadeCounter = 0;
    }

  }
}