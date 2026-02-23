using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.Files.Formats
{
    public enum FileExtension
    {
        [Display(Name = "None")]
        None, 
        [Display(Name = "Unknown")]
        Unknown,
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
        [Display(Name = ".pdf")]
        pdf,
        [Display(Name = ".png")]
        png,
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
        [Display(Name = ".zip")]
        zip,
        [Display(Name = ".323")]
        _323,
        [Display(Name = ".3g2")]
        _3g2,
        [Display(Name = ".3gp")]
        _3gp,
        [Display(Name = ".3gp2")]
        _3gp2,
        [Display(Name = ".3gpp")]
        _3gpp,
        [Display(Name = ".7z")]
        _7z,
        [Display(Name = ".aa")]
        aa,
        [Display(Name = ".AAC")]
        AAC,
        [Display(Name = ".aaf")]
        aaf,
        [Display(Name = ".aax")]
        aax,
        [Display(Name = ".ac3")]
        ac3,
        [Display(Name = ".aca")]
        aca,
        [Display(Name = ".accda")]
        accda,
        [Display(Name = ".accdb")]
        accdb,
        [Display(Name = ".accdc")]
        accdc,
        [Display(Name = ".accde")]
        accde,
        [Display(Name = ".accdr")]
        accdr,
        [Display(Name = ".accdt")]
        accdt,
        [Display(Name = ".accdw")]
        accdw,
        [Display(Name = ".accft")]
        accft,
        [Display(Name = ".acx")]
        acx,
        [Display(Name = ".AddIn")]
        AddIn,
        [Display(Name = ".ade")]
        ade,
        [Display(Name = ".adobebridge")]
        adobebridge,
        [Display(Name = ".adp")]
        adp,
        [Display(Name = ".ADT")]
        ADT,
        [Display(Name = ".ADTS")]
        ADTS,
        [Display(Name = ".afm")]
        afm,
        [Display(Name = ".ai")]
        ai,
        [Display(Name = ".aif")]
        aif,
        [Display(Name = ".aifc")]
        aifc,
        [Display(Name = ".aiff")]
        aiff,
        [Display(Name = ".air")]
        air,
        [Display(Name = ".amc")]
        amc,
        [Display(Name = ".anx")]
        anx,
        [Display(Name = ".apk")]
        apk,
        [Display(Name = ".apng")]
        apng,
        [Display(Name = ".application")]
        application,
        [Display(Name = ".art")]
        art,
        [Display(Name = ".asa")]
        asa,
        [Display(Name = ".asax")]
        asax,
        [Display(Name = ".ascx")]
        ascx,
        [Display(Name = ".asd")]
        asd,
        [Display(Name = ".asf")]
        asf,
        [Display(Name = ".ashx")]
        ashx,
        [Display(Name = ".asi")]
        asi,
        [Display(Name = ".asm")]
        asm,
        [Display(Name = ".asmx")]
        asmx,
        [Display(Name = ".aspx")]
        aspx,
        [Display(Name = ".asr")]
        asr,
        [Display(Name = ".asx")]
        asx,
        [Display(Name = ".atom")]
        atom,
        [Display(Name = ".au")]
        au,
        [Display(Name = ".avci")]
        avci,
        [Display(Name = ".avcs")]
        avcs,
        [Display(Name = ".avif")]
        avif,
        [Display(Name = ".avifs")]
        avifs,
        [Display(Name = ".axa")]
        axa,
        [Display(Name = ".axs")]
        axs,
        [Display(Name = ".axv")]
        axv,
        [Display(Name = ".bas")]
        bas,
        [Display(Name = ".bcpio")]
        bcpio,
        [Display(Name = ".bin")]
        bin,
        [Display(Name = ".c")]
        c,
        [Display(Name = ".cab")]
        cab,
        [Display(Name = ".caf")]
        caf,
        [Display(Name = ".calx")]
        calx,
        [Display(Name = ".cat")]
        cat,
        [Display(Name = ".cc")]
        cc,
        [Display(Name = ".cd")]
        cd,
        [Display(Name = ".cdda")]
        cdda,
        [Display(Name = ".cdf")]
        cdf,
        [Display(Name = ".cer")]
        cer,
        [Display(Name = ".cfg")]
        cfg,
        [Display(Name = ".chm")]
        chm,
        [Display(Name = ".class")]
        _class,
        [Display(Name = ".clp")]
        clp,
        [Display(Name = ".cmd")]
        cmd,
        [Display(Name = ".cmx")]
        cmx,
        [Display(Name = ".cnf")]
        cnf,
        [Display(Name = ".cod")]
        cod,
        [Display(Name = ".contact")]
        contact,
        [Display(Name = ".coverage")]
        coverage,
        [Display(Name = ".cpio")]
        cpio,
        [Display(Name = ".cpp")]
        cpp,
        [Display(Name = ".crd")]
        crd,
        [Display(Name = ".crl")]
        crl,
        [Display(Name = ".crt")]
        crt,
        [Display(Name = ".csh")]
        csh,
        [Display(Name = ".csproj")]
        csproj,
        [Display(Name = ".cur")]
        cur,
        [Display(Name = ".czx")]
        czx,
        [Display(Name = ".cxx")]
        cxx,
        [Display(Name = ".dat")]
        dat,
        [Display(Name = ".datasource")]
        datasource,
        [Display(Name = ".dbproj")]
        dbproj,
        [Display(Name = ".dcr")]
        dcr,
        [Display(Name = ".def")]
        def,
        [Display(Name = ".deploy")]
        deploy,
        [Display(Name = ".der")]
        der,
        [Display(Name = ".dgml")]
        dgml,
        [Display(Name = ".dib")]
        dib,
        [Display(Name = ".dif")]
        dif,
        [Display(Name = ".dir")]
        dir,
        [Display(Name = ".disco")]
        disco,
        [Display(Name = ".divx")]
        divx,
        [Display(Name = ".dll")]
        dll,
        [Display(Name = ".dll.config")]
        dll_config,
        [Display(Name = ".dlm")]
        dlm,
        [Display(Name = ".dot")]
        dot,
        [Display(Name = ".dotm")]
        dotm,
        [Display(Name = ".dotx")]
        dotx,
        [Display(Name = ".dsp")]
        dsp,
        [Display(Name = ".dsw")]
        dsw,
        [Display(Name = ".dtd")]
        dtd,
        [Display(Name = ".dtsConfig")]
        dtsConfig,
        [Display(Name = ".dv")]
        dv,
        [Display(Name = ".dvi")]
        dvi,
        [Display(Name = ".dwf")]
        dwf,
        [Display(Name = ".dwg")]
        dwg,
        [Display(Name = ".dwp")]
        dwp,
        [Display(Name = ".dxf")]
        dxf,
        [Display(Name = ".dxr")]
        dxr,
        [Display(Name = ".eml")]
        eml,
        [Display(Name = ".emf")]
        emf,
        [Display(Name = ".emz")]
        emz,
        [Display(Name = ".eot")]
        eot,
        [Display(Name = ".eps")]
        eps,
        [Display(Name = ".es")]
        es,
        [Display(Name = ".etl")]
        etl,
        [Display(Name = ".etx")]
        etx,
        [Display(Name = ".evy")]
        evy,
        [Display(Name = ".exe")]
        exe,
        [Display(Name = ".exe.config")]
        exe_config,
        [Display(Name = ".f4v")]
        f4v,
        [Display(Name = ".fdf")]
        fdf,
        [Display(Name = ".fif")]
        fif,
        [Display(Name = ".filters")]
        filters,
        [Display(Name = ".fla")]
        fla,
        [Display(Name = ".flac")]
        flac,
        [Display(Name = ".flr")]
        flr,
        [Display(Name = ".flv")]
        flv,
        [Display(Name = ".fsscript")]
        fsscript,
        [Display(Name = ".fsx")]
        fsx,
        [Display(Name = ".generictest")]
        generictest,
        [Display(Name = ".gpx")]
        gpx,
        [Display(Name = ".group")]
        group,
        [Display(Name = ".gsm")]
        gsm,
        [Display(Name = ".gtar")]
        gtar,
        [Display(Name = ".h")]
        h,
        [Display(Name = ".hdf")]
        hdf,
        [Display(Name = ".hdml")]
        hdml,
        [Display(Name = ".heic")]
        heic,
        [Display(Name = ".heics")]
        heics,
        [Display(Name = ".heif")]
        heif,
        [Display(Name = ".heifs")]
        heifs,
        [Display(Name = ".hhc")]
        hhc,
        [Display(Name = ".hhk")]
        hhk,
        [Display(Name = ".hhp")]
        hhp,
        [Display(Name = ".hlp")]
        hlp,
        [Display(Name = ".hpp")]
        hpp,
        [Display(Name = ".hqx")]
        hqx,
        [Display(Name = ".hta")]
        hta,
        [Display(Name = ".htc")]
        htc,
        [Display(Name = ".htt")]
        htt,
        [Display(Name = ".hxa")]
        hxa,
        [Display(Name = ".hxc")]
        hxc,
        [Display(Name = ".hxd")]
        hxd,
        [Display(Name = ".hxe")]
        hxe,
        [Display(Name = ".hxf")]
        hxf,
        [Display(Name = ".hxh")]
        hxh,
        [Display(Name = ".hxi")]
        hxi,
        [Display(Name = ".hxk")]
        hxk,
        [Display(Name = ".hxq")]
        hxq,
        [Display(Name = ".hxr")]
        hxr,
        [Display(Name = ".hxs")]
        hxs,
        [Display(Name = ".hxt")]
        hxt,
        [Display(Name = ".hxv")]
        hxv,
        [Display(Name = ".hxw")]
        hxw,
        [Display(Name = ".hxx")]
        hxx,
        [Display(Name = ".i")]
        i,
        [Display(Name = ".ical")]
        ical,
        [Display(Name = ".icalendar")]
        icalendar,
        [Display(Name = ".ico")]
        ico,
        [Display(Name = ".ics")]
        ics,
        [Display(Name = ".idl")]
        idl,
        [Display(Name = ".ief")]
        ief,
        [Display(Name = ".ifb")]
        ifb,
        [Display(Name = ".iii")]
        iii,
        [Display(Name = ".inc")]
        inc,
        [Display(Name = ".inf")]
        inf,
        [Display(Name = ".inl")]
        inl,
        [Display(Name = ".ins")]
        ins,
        [Display(Name = ".ipa")]
        ipa,
        [Display(Name = ".ipg")]
        ipg,
        [Display(Name = ".ipproj")]
        ipproj,
        [Display(Name = ".ipsw")]
        ipsw,
        [Display(Name = ".ipynb")]
        ipynb,
        [Display(Name = ".iqy")]
        iqy,
        [Display(Name = ".isp")]
        isp,
        [Display(Name = ".isma")]
        isma,
        [Display(Name = ".ismv")]
        ismv,
        [Display(Name = ".ite")]
        ite,
        [Display(Name = ".itlp")]
        itlp,
        [Display(Name = ".itms")]
        itms,
        [Display(Name = ".itpc")]
        itpc,
        [Display(Name = ".IVF")]
        IVF,
        [Display(Name = ".jar")]
        jar,
        [Display(Name = ".java")]
        java,
        [Display(Name = ".jck")]
        jck,
        [Display(Name = ".jcz")]
        jcz,
        [Display(Name = ".jfif")]
        jfif,
        [Display(Name = ".jnlp")]
        jnlp,
        [Display(Name = ".jpb")]
        jpb,
        [Display(Name = ".jpe")]
        jpe,
        [Display(Name = ".jsx")]
        jsx,
        [Display(Name = ".jsxbin")]
        jsxbin,
        [Display(Name = ".kml")]
        kml,
        [Display(Name = ".kmz")]
        kmz,
        [Display(Name = ".ktx")]
        ktx,
        [Display(Name = ".latex")]
        latex,
        [Display(Name = ".library-ms")]
        library_ms,
        [Display(Name = ".lit")]
        lit,
        [Display(Name = ".loadtest")]
        loadtest,
        [Display(Name = ".lpk")]
        lpk,
        [Display(Name = ".lsf")]
        lsf,
        [Display(Name = ".lst")]
        lst,
        [Display(Name = ".lsx")]
        lsx,
        [Display(Name = ".lzh")]
        lzh,
        [Display(Name = ".m13")]
        m13,
        [Display(Name = ".m14")]
        m14,
        [Display(Name = ".m1v")]
        m1v,
        [Display(Name = ".m2t")]
        m2t,
        [Display(Name = ".m2ts")]
        m2ts,
        [Display(Name = ".m2v")]
        m2v,
        [Display(Name = ".m3u")]
        m3u,
        [Display(Name = ".m3u8")]
        m3u8,
        [Display(Name = ".m4a")]
        m4a,
        [Display(Name = ".m4b")]
        m4b,
        [Display(Name = ".m4p")]
        m4p,
        [Display(Name = ".m4r")]
        m4r,
        [Display(Name = ".m4v")]
        m4v,
        [Display(Name = ".mac")]
        mac,
        [Display(Name = ".mak")]
        mak,
        [Display(Name = ".man")]
        man,
        [Display(Name = ".manifest")]
        manifest,
        [Display(Name = ".map")]
        map,
        [Display(Name = ".master")]
        master,
        [Display(Name = ".mbox")]
        mbox,
        [Display(Name = ".md")]
        md,
        [Display(Name = ".mda")]
        mda,
        [Display(Name = ".mdb")]
        mdb,
        [Display(Name = ".mde")]
        mde,
        [Display(Name = ".mdp")]
        mdp,
        [Display(Name = ".mdx")]
        mdx, 
        [Display(Name = ".me")]
        me,
        [Display(Name = ".mfp")]
        mfp,
        [Display(Name = ".mht")]
        mht,
        [Display(Name = ".mhtml")]
        mhtml,
        [Display(Name = ".mid")]
        mid,
        [Display(Name = ".midi")]
        midi,
        [Display(Name = ".mix")]
        mix,
        [Display(Name = ".mk")]
        mk,
        [Display(Name = ".mk3d")]
        mk3d,
        [Display(Name = ".mka")]
        mka,
        [Display(Name = ".mkv")]
        mkv,
        [Display(Name = ".mmf")]
        mmf,
        [Display(Name = ".mno")]
        mno,
        [Display(Name = ".mny")]
        mny,
        [Display(Name = ".mod")]
        mod,
        [Display(Name = ".mov")]
        mov,
        [Display(Name = ".movie")]
        movie,
        [Display(Name = ".mp2")]
        mp2,
        [Display(Name = ".mp2v")]
        mp2v,
        [Display(Name = ".mp4v")]
        mp4v,
        [Display(Name = ".mpa")]
        mpa,
        [Display(Name = ".mpe")]
        mpe,
        [Display(Name = ".mpeg")]
        mpeg,
        [Display(Name = ".mpf")]
        mpf,
        [Display(Name = ".mpg")]
        mpg,
        [Display(Name = ".mpp")]
        mpp,
        [Display(Name = ".mpv2")]
        mpv2,
        [Display(Name = ".mqv")]
        mqv,
        [Display(Name = ".mp3")]
        mp3,
        [Display(Name = ".mp4")]
        mp4,
        [Display(Name = ".nsc")]
        nsc,
        [Display(Name = ".nws")]
        nws,
        [Display(Name = ".ocx")]
        ocx,
        [Display(Name = ".oda")]
        oda,
        [Display(Name = ".odb")]
        odb, // Base (Database)
        [Display(Name = ".odc")]
        odc,
        [Display(Name = ".odf")]
        odf,
        [Display(Name = ".odg")]
        odg, // Draw (Vector Graphics)
        [Display(Name = ".odh")]
        odh,
        [Display(Name = ".odi")]
        odi,
        [Display(Name = ".odl")]
        odl,
        [Display(Name = ".odm")]
        odm,
        [Display(Name = ".ods")]
        ods, // Calc (Spreadsheet)
        [Display(Name = ".odt")]
        odt, // Libre Office Writer (Word Processor) document
        [Display(Name = ".odp")]
        odp, // Libre Office presentation format
        [Display(Name = ".oga")]
        oga,
        [Display(Name = ".ogg")]
        ogg,
        [Display(Name = ".ogv")]
        ogv,
        [Display(Name = ".ogx")]
        ogx,
        [Display(Name = ".one")]
        one,
        [Display(Name = ".onea")]
        onea,
        [Display(Name = ".onepkg")]
        onepkg,
        [Display(Name = ".onetmp")]
        onetmp,
        [Display(Name = ".onetoc")]
        onetoc,
        [Display(Name = ".onetoc2")]
        onetoc2,
        [Display(Name = ".opus")]
        opus,
        [Display(Name = ".orderedtest")]
        orderedtest,
        [Display(Name = ".osdx")]
        osdx,
        [Display(Name = ".otf")]
        otf,
        [Display(Name = ".otg")]
        otg,
        [Display(Name = ".oth")]
        oth,
        [Display(Name = ".otp")] // Libre Office presentation template format
        otp,
        [Display(Name = ".ots")] // Calc (Spreadsheet) templateodg,
        ots, 
        [Display(Name = ".ott")] // Libre Office Writer (Word Processor) template format
        ott,
        [Display(Name = ".oxps")] 
        oxps,
        [Display(Name = ".oxt")]
        oxt,
        [Display(Name = ".p10")]
        p10,
        [Display(Name = ".p12")]
        p12,
        [Display(Name = ".p7b")]
        p7b,
        [Display(Name = ".p7c")]
        p7c,
        [Display(Name = ".p7m")]
        p7m,
        [Display(Name = ".p7r")]
        p7r,
        [Display(Name = ".p7s")]
        p7s,
        [Display(Name = ".pbm")]
        pbm,
        [Display(Name = ".pcast")]
        pcast,
        [Display(Name = ".pct")]
        pct,
        [Display(Name = ".pcx")]
        pcx,
        [Display(Name = ".pcz")]
        pcz,
        [Display(Name = ".pfb")]
        pfb,
        [Display(Name = ".pfm")]
        pfm,
        [Display(Name = ".pfx")]
        pfx,
        [Display(Name = ".pgm")]
        pgm,
        [Display(Name = ".pic")]
        pic,
        [Display(Name = ".pict")]
        pict,
        [Display(Name = ".pkgdef")]
        pkgdef,
        [Display(Name = ".pkgundef")]
        pkgundef,
        [Display(Name = ".pko")]
        pko,
        [Display(Name = ".pls")]
        pls,
        [Display(Name = ".pma")]
        pma,
        [Display(Name = ".pmc")]
        pmc,
        [Display(Name = ".pml")]
        pml,
        [Display(Name = ".pmr")]
        pmr,
        [Display(Name = ".pmw")]
        pmw,
        [Display(Name = ".pnm")]
        pnm,
        [Display(Name = ".pnt")]
        pnt,
        [Display(Name = ".pntg")]
        pntg,
        [Display(Name = ".pnz")]
        pnz,
        [Display(Name = ".pot")]
        pot,
        [Display(Name = ".potm")]
        potm,
        [Display(Name = ".potx")]
        potx,
        [Display(Name = ".ppa")]
        ppa,
        [Display(Name = ".ppam")]
        ppam,
        [Display(Name = ".ppm")]
        ppm,
        [Display(Name = ".pps")]
        pps,
        [Display(Name = ".ppsm")]
        ppsm,
        [Display(Name = ".ppsx")]
        ppsx,
        [Display(Name = ".pptm")]
        pptm,
        [Display(Name = ".prf")]
        prf,
        [Display(Name = ".prm")]
        prm,
        [Display(Name = ".prx")]
        prx,
        [Display(Name = ".ps")]
        ps,
        [Display(Name = ".psc1")]
        psc1,
        [Display(Name = ".psd")]
        psd,
        [Display(Name = ".psess")]
        psess,
        [Display(Name = ".psm")]
        psm,
        [Display(Name = ".psp")]
        psp,
        [Display(Name = ".pst")]
        pst,
        [Display(Name = ".pub")]
        pub,
        [Display(Name = ".pwz")]
        pwz,
        [Display(Name = ".qht")]
        qht,
        [Display(Name = ".qhtm")]
        qhtm,
        [Display(Name = ".qt")]
        qt,
        [Display(Name = ".qti")]
        qti,
        [Display(Name = ".qtif")]
        qtif,
        [Display(Name = ".qtl")]
        qtl,
        [Display(Name = ".qxd")]
        qxd,
        [Display(Name = ".ra")]
        ra,
        [Display(Name = ".ram")]
        ram,
        [Display(Name = ".ras")]
        ras,
        [Display(Name = ".rat")]
        rat,
        [Display(Name = ".rc")]
        rc,
        [Display(Name = ".rc2")]
        rc2,
        [Display(Name = ".rct")]
        rct,
        [Display(Name = ".rdlc")]
        rdlc,
        [Display(Name = ".reg")]
        reg,
        [Display(Name = ".rf")]
        rf,
        [Display(Name = ".rgb")]
        rgb,
        [Display(Name = ".rgs")]
        rgs,
        [Display(Name = ".rm")]
        rm,
        [Display(Name = ".rmi")]
        rmi,
        [Display(Name = ".rmp")]
        rmp,
        [Display(Name = ".rmvb")]
        rmvb,
        [Display(Name = ".roff")]
        roff,
        [Display(Name = ".rpm")]
        rpm,
        [Display(Name = ".rqy")]
        rqy,
        [Display(Name = ".rtf")]
        rtf,
        [Display(Name = ".rtx")]
        rtx,
        [Display(Name = ".rvt")]
        rvt,
        [Display(Name = ".ruleset")]
        ruleset,
        [Display(Name = ".s")]
        s,
        [Display(Name = ".safariextz")]
        safariextz,
        [Display(Name = ".scd")]
        scd,
        [Display(Name = ".scr")]
        scr,
        [Display(Name = ".sct")]
        sct,
        [Display(Name = ".sd2")]
        sd2,
        [Display(Name = ".sdp")]
        sdp,
        [Display(Name = ".sea")]
        sea,
        [Display(Name = ".searchConnector-ms")]
        searchConnector_ms,
        [Display(Name = ".setpay")]
        setpay,
        [Display(Name = ".setreg")]
        setreg,
        [Display(Name = ".settings")]
        settings,
        [Display(Name = ".sgimb")]
        sgimb,
        [Display(Name = ".sgml")]
        sgml,
        [Display(Name = ".sh")]
        sh,
        [Display(Name = ".shar")]
        shar,
        [Display(Name = ".shtml")]
        shtml,
        [Display(Name = ".sit")]
        sit,
        [Display(Name = ".sitemap")]
        sitemap,
        [Display(Name = ".skin")]
        skin,
        [Display(Name = ".skp")]
        skp,
        [Display(Name = ".sldm")]
        sldm,
        [Display(Name = ".sldx")]
        sldx,
        [Display(Name = ".slk")]
        slk,
        [Display(Name = ".sln")]
        sln,
        [Display(Name = ".slupkg-ms")]
        slupkg_ms,
        [Display(Name = ".smd")]
        smd,
        [Display(Name = ".smi")]
        smi,
        [Display(Name = ".smx")]
        smx,
        [Display(Name = ".smz")]
        smz,
        [Display(Name = ".snd")]
        snd,
        [Display(Name = ".snippet")]
        snippet,
        [Display(Name = ".snp")]
        snp,
        [Display(Name = ".sol")]
        sol,
        [Display(Name = ".sor")]
        sor,
        [Display(Name = ".spc")]
        spc,
        [Display(Name = ".spl")]
        spl,
        [Display(Name = ".spx")]
        spx,
        [Display(Name = ".src")]
        src,
        [Display(Name = ".srf")]
        srf,
        [Display(Name = ".SSISDeploymentManifest")]
        SSISDeploymentManifest,
        [Display(Name = ".ssm")]
        ssm,
        [Display(Name = ".sst")]
        sst,
        [Display(Name = ".stl")]
        stl,
        [Display(Name = ".sv4cpio")]
        sv4cpio,
        [Display(Name = ".sv4crc")]
        sv4crc,
        [Display(Name = ".svc")]
        svc,
        [Display(Name = ".swf")]
        swf,
        [Display(Name = ".step")]
        step,
        [Display(Name = ".stp")]
        stp,
        [Display(Name = ".t")]
        t,
        [Display(Name = ".tar")]
        tar,
        [Display(Name = ".tar.gz")]
        targz,
        [Display(Name = ".tcl")]
        tcl,
        // Camtasia / Techsmith Video .tscproj
        [Display(Name = ".tscproj")]
        tscproj,
        [Display(Name = ".testrunconfig")]
        testrunconfig,
        [Display(Name = ".testsettings")]
        testsettings,
        [Display(Name = ".tex")]
        tex,
        [Display(Name = ".texi")]
        texi,
        [Display(Name = ".texinfo")]
        texinfo,
        [Display(Name = ".tgz")]
        tgz,
        [Display(Name = ".thmx")]
        thmx,
        [Display(Name = ".thn")]
        thn,
        [Display(Name = ".tif")]
        tif, 
        [Display(Name = ".tiff")]
        tiff,
        [Display(Name = ".toon")]
        toon,
        [Display(Name = ".tlh")]
        tlh,
        [Display(Name = ".tli")]
        tli,
        [Display(Name = ".toc")]
        toc,
        [Display(Name = ".tr")]
        tr,
        [Display(Name = ".trm")]
        trm,
        [Display(Name = ".trx")]
        trx,
        [Display(Name = ".ts")]
        ts,
        [Display(Name = ".tsv")]
        tsv,
        [Display(Name = ".ttf")]
        ttf,
        [Display(Name = ".tts")]
        tts,
        [Display(Name = ".txt")]
        txt,
        [Display(Name = ".u32")]
        u32,
        [Display(Name = ".uls")]
        uls,
        [Display(Name = ".user")]
        user,
        [Display(Name = ".ustar")]
        ustar,
        [Display(Name = ".vb")]
        vb,
        [Display(Name = ".vbdproj")]
        vbdproj,
        [Display(Name = ".vbk")]
        vbk,
        [Display(Name = ".vbproj")]
        vbproj,
        [Display(Name = ".vbs")]
        vbs,
        [Display(Name = ".vcf")]
        vcf,
        [Display(Name = ".vcproj")]
        vcproj,
        [Display(Name = ".vcs")]
        vcs,
        [Display(Name = ".vcxproj")]
        vcxproj,
        [Display(Name = ".vddproj")]
        vddproj,
        [Display(Name = ".vdp")]
        vdp,
        [Display(Name = ".vdproj")]
        vdproj,
        [Display(Name = ".vdx")]
        vdx,
        [Display(Name = ".vml")]
        vml,
        [Display(Name = ".vscontent")]
        vscontent,
        [Display(Name = ".vsct")]
        vsct,
        [Display(Name = ".vsd")]
        vsd,
        [Display(Name = ".vsi")]
        vsi,
        [Display(Name = ".vsix")]
        vsix,
        [Display(Name = ".vsixlangpack")]
        vsixlangpack,
        [Display(Name = ".vsixmanifest")]
        vsixmanifest,
        [Display(Name = ".vsmdi")]
        vsmdi,
        [Display(Name = ".vspscc")]
        vspscc,
        [Display(Name = ".vss")]
        vss,
        [Display(Name = ".vsscc")]
        vsscc,
        [Display(Name = ".vssettings")]
        vssettings,
        [Display(Name = ".vssscc")]
        vssscc,
        [Display(Name = ".vst")]
        vst,
        [Display(Name = ".vstemplate")]
        vstemplate,
        [Display(Name = ".vsto")]
        vsto,
        [Display(Name = ".vsw")]
        vsw,
        [Display(Name = ".vsx")]
        vsx,
        [Display(Name = ".vtt")]
        vtt,
        [Display(Name = ".vtx")]
        vtx,
        [Display(Name = ".wasm")]
        wasm,
        [Display(Name = ".wave")]
        wave,
        [Display(Name = ".wax")]
        wax,
        [Display(Name = ".wbk")]
        wbk,
        [Display(Name = ".wbmp")]
        wbmp,
        [Display(Name = ".wcm")]
        wcm,
        [Display(Name = ".wdb")]
        wdb,
        [Display(Name = ".wdp")]
        wdp,
        [Display(Name = ".webarchive")]
        webarchive,
        [Display(Name = ".webm")]
        webm,
        [Display(Name = ".webtest")]
        webtest,
        [Display(Name = ".wiq")]
        wiq,
        [Display(Name = ".wiz")]
        wiz,
        [Display(Name = ".wks")]
        wks,
        [Display(Name = ".WLMP")]
        WLMP,
        [Display(Name = ".wlpginstall")]
        wlpginstall,
        [Display(Name = ".wlpginstall3")]
        wlpginstall3,
        [Display(Name = ".wm")]
        wm,
        [Display(Name = ".wma")]
        wma,
        [Display(Name = ".wmd")]
        wmd,
        [Display(Name = ".wmf")]
        wmf,
        [Display(Name = ".wml")]
        wml,
        [Display(Name = ".wmlc")]
        wmlc,
        [Display(Name = ".wmls")]
        wmls,
        [Display(Name = ".wmlsc")]
        wmlsc,
        [Display(Name = ".wmp")]
        wmp,
        [Display(Name = ".wmx")]
        wmx,
        [Display(Name = ".wmz")]
        wmz,
        [Display(Name = ".woff")]
        woff,
        [Display(Name = ".woff2")]
        woff2,
        [Display(Name = ".wpl")]
        wpl,
        [Display(Name = ".wps")]
        wps,
        [Display(Name = ".wri")]
        wri,
        [Display(Name = ".wrl")]
        wrl,
        [Display(Name = ".wrz")]
        wrz,
        [Display(Name = ".wsc")]
        wsc,
        [Display(Name = ".wsdl")]
        wsdl,
        [Display(Name = ".wvx")]
        wvx,
        [Display(Name = ".x")]
        x,
        [Display(Name = ".xaf")]
        xaf,
        [Display(Name = ".xaml")]
        xaml,
        [Display(Name = ".xap")]
        xap,
        [Display(Name = ".xbap")]
        xbap,
        [Display(Name = ".xbm")]
        xbm,
        [Display(Name = ".xdr")]
        xdr,
        [Display(Name = ".xht")]
        xht,
        [Display(Name = ".xhtml")]
        xhtml,
        [Display(Name = ".xla")]
        xla,
        [Display(Name = ".xlam")]
        xlam,
        [Display(Name = ".xlc")]
        xlc,
        [Display(Name = ".xld")]
        xld,
        [Display(Name = ".xlk")]
        xlk,
        [Display(Name = ".xll")]
        xll,
        [Display(Name = ".xlm")]
        xlm,
        [Display(Name = ".xlt")]
        xlt,
        [Display(Name = ".xltm")]
        xltm,
        [Display(Name = ".xltx")]
        xltx,
        [Display(Name = ".xlw")]
        xlw,
        [Display(Name = ".xlsb")]
        xlsb,
        [Display(Name = ".xlsm")]
        xlsm,
        [Display(Name = ".xmp")]
        xmp,
        [Display(Name = ".xmta")]
        xmta,
        [Display(Name = ".xof")]
        xof,
        [Display(Name = ".XOML")]
        XOML,
        [Display(Name = ".xpm")]
        xpm,
        [Display(Name = ".xps")]
        xps,
        [Display(Name = ".xrm-ms")]
        xrm_ms,
        [Display(Name = ".xsc")]
        xsc,
        [Display(Name = ".xsd")]
        xsd,
        [Display(Name = ".xsf")]
        xsf,
        [Display(Name = ".xsl")]
        xsl,
        [Display(Name = ".xslt")]
        xslt,
        [Display(Name = ".xsn")]
        xsn,
        [Display(Name = ".xss")]
        xss,
        [Display(Name = ".xspf")]
        xspf,
        [Display(Name = ".xtp")]
        xtp,
        [Display(Name = ".xwd")]
        xwd,
        [Display(Name = ".yaml")]
        yaml,
        [Display(Name = ".z")]
        z,
        [Display(Name = ".zarr")]
        zarr
    }
}
