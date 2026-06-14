using Reloaded.Hooks.ReloadedII.Interfaces;
using Reloaded.Mod.Interfaces;
using BlackHairHaru.Template;
using BlackHairHaru.Configuration;
using CriFs.V2.Hook.Interfaces;
using SPD.File.Emulator.Interfaces;
using P5R.CostumeFramework.Interfaces;
using CriExtensions;

namespace BlackHairHaru
{
/// <summary>
/// Your mod logic goes here.
/// </summary>
public class Mod : ModBase // <= Do not Remove.
{
    /// <summary>
    /// Provides access to the mod loader API.
    /// </summary>
    private readonly IModLoader _modLoader;

    /// <summary>
    /// Provides access to the Reloaded.Hooks API.
    /// </summary>
    /// <remarks>This is null if you remove dependency on Reloaded.SharedLib.Hooks in your mod.</remarks>
    private readonly IReloadedHooks? _hooks;

    /// <summary>
    /// Provides access to the Reloaded logger.
    /// </summary>
    private readonly ILogger _logger;

    /// <summary>
    /// Entry point into the mod, instance that created this class.
    /// </summary>
    private readonly IMod _owner;

    /// <summary>
    /// Provides access to this mod's configuration.
    /// </summary>
    private Config _configuration;

    /// <summary>
    /// The configuration of the currently executing mod.
    /// </summary>
    private readonly IModConfig _modConfig;

    public Mod(ModContext context)
    {
        _modLoader = context.ModLoader;
        _hooks = context.Hooks;
        _logger = context.Logger;
        _owner = context.Owner;
        _configuration = context.Configuration;
        _modConfig = context.ModConfig;

        string modDir = _modLoader.GetDirectoryForModId(_modConfig.ModId);
        string modId = _modConfig.ModId;

        // Initialize file emulator controllers
        var criFsCtl = _modLoader.GetController<ICriFsRedirectorApi>();
        var costumeCtl = _modLoader.GetController<ICostumeApi>();
        var spdEmuCtl = _modLoader.GetController<ISpdEmulator>();

        if (criFsCtl == null || !criFsCtl.TryGetTarget(out var criFsApi)) { _logger.WriteLine("CRI FS missing → cpk and binds broken.", System.Drawing.Color.Red); return; }
        if (costumeCtl == null || !costumeCtl.TryGetTarget(out var costumeApi)) { _logger.WriteLine("Costume API missing → Costumes broken.", System.Drawing.Color.Red); return; }
        if (spdEmuCtl == null || !spdEmuCtl.TryGetTarget(out var spdEmu)) { _logger.WriteLine("SPD Emu missing → SPD merges broken.", System.Drawing.Color.Red); return; }

        // For more information about this template, please see
        // https://reloaded-project.github.io/Reloaded-II/ModTemplate/

        // If you want to implement e.g. unload support in your mod,
        // and some other neat features, override the methods in ModBase.

        // TODO: Implement some mod logic

            // Summer Uniform
            if (_configuration.SummerUniformBHHValue != Config.SummerUniformBHH.Default)
                {
                    string SummerUniformBHHFolder = "";
                    if (_configuration.SummerUniformBHHValue == Config.SummerUniformBHH.ClassicSummerUnif)
                        {
                            SummerUniformBHHFolder = "ClassicSummerUniform";
                        }
                    BindAllFilesIn(Path.Combine("OptionalModFiles", "SummerUniform", SummerUniformBHHFolder), modDir, criFsApi, modId);
                }

            // Uniform Uniform
            if (_configuration.WinterUniformBHHValue != Config.WinterUniformBHH.Default)
                {
                    string WinterUniformBHHFolder = "";
                    if (_configuration.WinterUniformBHHValue == Config.WinterUniformBHH.ClassicWinterUnif)
                        {
                            WinterUniformBHHFolder = "ClassicWinterUniform";
                        }
                    BindAllFilesIn(Path.Combine("OptionalModFiles", "WinterUniform", WinterUniformBHHFolder), modDir, criFsApi, modId);
                }

            // EPIC colorful party panel
            if (_configuration.ColorPartyPanelBHH)
                spdEmu.AddDirectory(Path.Combine(modDir, "OptionalModFiles", "EPICPartyPanel", "SPD"));

            // Protagonist Compatibility patches
            if (_configuration.CompatibilityPatchBHHValue != Config.CompatibilityPatchBHH.Default)
                {
                    string CompatibilityPatchBHHFolder = "";
                    if (_configuration.CompatibilityPatchBHHValue == Config.CompatibilityPatchBHH.KAPPatch)
                        {
                            CompatibilityPatchBHHFolder = "KasumiAsProtag";
                        }
                    else if (_configuration.CompatibilityPatchBHHValue == Config.CompatibilityPatchBHH.HAPPatch)
                        {
                            CompatibilityPatchBHHFolder = "HaruAsProtag";
                        }
                    else if (_configuration.CompatibilityPatchBHHValue == Config.CompatibilityPatchBHH.HAPBlackPatch)
                        {
                            CompatibilityPatchBHHFolder = "HaruAsProtagBlackHair";
                        }
                    BindAllFilesIn(Path.Combine("OptionalModFiles", "CompatibilityPatches", CompatibilityPatchBHHFolder), modDir, criFsApi, modId);
                }

    }

    private static void BindAllFilesIn(string subPathRelativeToModDir, string modDir, ICriFsRedirectorApi criFsApi, string modId)
    {
        var absoluteFolder = Path.Combine(modDir, subPathRelativeToModDir);
        if (!Directory.Exists(absoluteFolder)) return;
        foreach (var file in Directory.EnumerateFiles(absoluteFolder, "*", SearchOption.AllDirectories))
            criFsApi.AddBind(file, Path.GetRelativePath(absoluteFolder, file).Replace(Path.DirectorySeparatorChar, '/'), modId);
    }

    #region Standard Overrides
    public override void ConfigurationUpdated(Config configuration)
    {
        // Apply settings from configuration.
        // ... your code here.
        _configuration = configuration;
        _logger.WriteLine($"[{_modConfig.ModId}] Config Updated: Applying");
    }
    #endregion

    #region For Exports, Serialization etc.
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public Mod() { }
#pragma warning restore CS8618
    #endregion
}

}