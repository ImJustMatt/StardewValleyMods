﻿using Harmony;
using Microsoft.Xna.Framework.Graphics;
using StardewModdingAPI;
using System;

namespace TransparentObjects
{
    public class ModEntry : Mod
    {
        
        public static ModConfig config;
        public static IMonitor SMonitor;

        public override void Entry(IModHelper helper)
        {
            config = Helper.ReadConfig<ModConfig>();
            if (!config.EnableMod)
                return;

            //HarmonyInstance.DEBUG = true;


            SMonitor = Monitor;

            
            ObjectPatches.Initialize(Monitor, helper, config);

            var harmony = HarmonyInstance.Create(ModManifest.UniqueID);

            harmony.Patch(
               original: AccessTools.Method(typeof(StardewValley.Object), nameof(StardewValley.Object.draw), new Type[] { typeof(SpriteBatch), typeof(int), typeof(int), typeof(float) }),
               prefix: new HarmonyMethod(typeof(ObjectPatches), nameof(ObjectPatches.Object_draw_Prefix))
            );

        }
     }
}
