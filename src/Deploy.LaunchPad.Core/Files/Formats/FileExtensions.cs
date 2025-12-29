using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.Core.Files.Formats
{
    public enum FileExtensions
    {
        [Display(Name = "None")]
        None,
        [Display(Name = ".astro")]
        astro,
        // Camtasia / Techsmith Audiate .audiate
        [Display(Name = ".audiate")]
        audiate,
        [Display(Name = ".avi")]
        avi,
        [Display(Name = ".bmp")]
        bmp,
        [Display(Name = ".config")]
        config,
        [Display(Name = ".cs")]
        cs,
        [Display(Name = ".csproj")]
        csproj,
        [Display(Name = ".css")]
        css,
        [Display(Name = ".csv")]
        csv,
        [Display(Name = ".doc")]
        doc,
        [Display(Name = ".docx")]
        docx,
        [Display(Name = ".env")]
        env,
        [Display(Name = ".gif")]
        gif,
        [Display(Name = ".gz")]
        gz,
        [Display(Name = ".html")]
        html,
        [Display(Name = ".ini")]
        ini,
        [Display(Name = ".jpeg")]
        jpeg,
        [Display(Name = ".jpg")]
        jpg,   
        [Display(Name = ".js")]
        js,
        [Display(Name = ".json")]
        json,
        [Display(Name = ".ipynb")]
        ipynb,
        [Display(Name = ".md")]
        md,
        [Display(Name = ".mdx")]
        mdx,
        [Display(Name = ".mov")]
        mov,
        [Display(Name = ".mp3")]
        mp3,
        [Display(Name = ".mp4")]
        mp4,
        [Display(Name = ".odb")]
        odb, // Base (Database)
        [Display(Name = ".ods")]
        ods, // Calc (Spreadsheet)
        [Display(Name = ".ots")]
        ots, // Calc (Spreadsheet) template
        [Display(Name = ".odg")]
        odg, // Draw (Vector Graphics)
        [Display(Name = ".odt")]
        odt, // Libre Office Writer (Word Processor) document
        [Display(Name = ".ott")]
        ott, // Libre Office Writer (Word Processor) template format
        [Display(Name = ".odp")]
        odp, // Libre Office presentation format
        [Display(Name = ".otp")]
        otp, // Libre Office presentation template format
        [Display(Name = ".pdf")]
        pdf,
        [Display(Name = ".png")]
        png,
        [Display(Name = ".potx")]
        potx,
        [Display(Name = ".ppt")]
        ppt,
        [Display(Name = ".pptx")]
        pptx,
        [Display(Name = ".ps1")]
        ps1,
        [Display(Name = ".py")]
        py,
        [Display(Name = ".rar")]
        rar,
        [Display(Name = ".rad")]
        rad,
        [Display(Name = ".resx")]
        resx,
        [Display(Name = ".sql")]
        sql,
        [Display(Name = ".svg")]
        svg,
        [Display(Name = ".tar")]
        tar,
        [Display(Name = "tar.gz")]
        targz,
        [Display(Name = ".tiff")]
        tiff,
        [Display(Name = ".toon")]
        toon,
        // Camtasia / Techsmith Video .tscproj
        [Display(Name = ".tscproj")]
        tscproj,
        [Display(Name = ".wav")]
        wav,
        [Display(Name = ".webp")]
        webp,
        [Display(Name = ".wmv")]
        wmv,
        [Display(Name = ".xls")]
        xls,
        [Display(Name = ".xlsx")]
        xlsx,
        [Display(Name = ".xml")]
        xml,
        [Display(Name = ".yaml")]
        yaml,
        [Display(Name = ".zarr")]
        zarr,
        [Display(Name = ".zip")]
        zip
    }
}
