using System.ComponentModel;
using BlackHairHaru.Template.Configuration;
using Reloaded.Mod.Interfaces.Structs;
using System.ComponentModel.DataAnnotations;
using CriFs.V2.Hook;
using CriFs.V2.Hook.Interfaces;
using System.Reflection;

namespace BlackHairHaru.Configuration
{
public class Config : Configurable<Config>
{
    /*
        User Properties:
            - Please put all of your configurable properties here.
    
        By default, configuration saves as "Config.json" in mod user config folder.    
        Need more config files/classes? See Configuration.cs
    
        Available Attributes:
        - Category
        - DisplayName
        - Description
        - DefaultValue

        // Technically Supported but not Useful
        - Browsable
        - Localizable

        The `DefaultValue` attribute is used as part of the `Reset` button in Reloaded-Launcher.
    */

    public enum WinterUniformBHH
    {
        [Display(Name = "Default")]
        Default,

        [Display(Name = "Classic Winter Uniform")]
        ClassicWinterUnif,

    }

    public enum SummerUniformBHH
    {
        [Display(Name = "Default")]
        Default,

        [Display(Name = "Classic Summer Uniform")]
        ClassicSummerUnif,

    }

    public enum CompatibilityPatchBHH
    {
        [Display(Name = "Default")]
        Default,

        [Display(Name = "Kasumi As Protagonist")]
        KAPPatch,

        [Display(Name = "Haru As Protagonist")]
        HAPPatch,

        [Display(Name = "Haru As Protagonist (Black Hair)")]
        HAPBlackPatch,

    }        

    [Category("Bustups")]
    [DisplayName("Epic Partypanel In Color")]
    [Description("Colorful bustup in battle. By Zrego.")]
    [DefaultValue(false)]
    [Display(Order = 0)]
    public bool ColorPartyPanelBHH { get; set; } = false;

    [Category("Overworld outfits")]
    [DisplayName("Winter Uniform")]
    [Description("Select your preferred Winter uniform.")]
    [DefaultValue(WinterUniformBHH.Default)]
    [Display(Order = 1)]
    public WinterUniformBHH WinterUniformBHHValue { get; set; }

    [Category("Overworld outfits")]
    [DisplayName("Summer Uniform")]
    [Description("Select your preferred Summer uniform.")]
    [DefaultValue(SummerUniformBHH.Default)]
    [Display(Order = 2)]
    public SummerUniformBHH SummerUniformBHHValue { get; set; }

    [Category("Compatibility patches")]
    [DisplayName("Compatibility Patch for")]
    [Description("Select the protagonist mod you're using.")]
    [DefaultValue(CompatibilityPatchBHH.Default)]
    [Display(Order = 3)]
    public CompatibilityPatchBHH CompatibilityPatchBHHValue { get; set; }

}

/// <summary>
/// Allows you to override certain aspects of the configuration creation process (e.g. create multiple configurations).
/// Override elements in <see cref="ConfiguratorMixinBase"/> for finer control.
/// </summary>
public class ConfiguratorMixin : ConfiguratorMixinBase
{
    // 
}

}