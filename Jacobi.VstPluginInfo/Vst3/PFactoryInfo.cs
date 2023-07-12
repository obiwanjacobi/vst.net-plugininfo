﻿using System.Runtime.InteropServices;

namespace Jacobi.VstPluginInfo.Vst3;

// Note CharSet is not the same as the platform global value.
[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = Platform.StructurePack)]
public struct PFactoryInfo
{
    public static readonly int Size = Marshal.SizeOf<PFactoryInfo>();

    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = Constants.MaxSizeVendor)]
    public String Vendor;

    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = Constants.MaxSizeUrl)]
    public String Url;

    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = Constants.MaxSizeEmail)]
    public String Email;

    [MarshalAs(UnmanagedType.I4)]
    public FactoryFlags Flags;
}

[Flags]
public enum FactoryFlags
{
    /// <summary>Nothing</summary>
    None = 0,

    /// <summary>
    /// The number of exported classes can change each time the Module is loaded. 
    /// If this flag is set, the host does not cache class information. 
    /// This leads to a longer startup time because the host always has to load the Module to get the current class information.
    /// </summary>
    ClassesDiscardable = 1 << 0,

    /// <summary>
    /// Class IDs of components are interpreted as Syncrosoft-License (LICENCE_UID). 
    /// Loaded in a Steinberg host, the module will not be loaded when the license is not valid.
    /// </summary>
    LicenseCheck = 1 << 1,

    /// <summary>Component won't be unloaded until process exit.</summary>
    ComponentNonDiscardable = 1 << 3,

    /// <summary>Components have entirely unicode encoded strings. (True for VST 3 Plug-ins so far)</summary>
    Unicode = 1 << 4
}