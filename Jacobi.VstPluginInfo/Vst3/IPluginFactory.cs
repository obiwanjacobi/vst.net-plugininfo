using System.Runtime.InteropServices;

namespace Jacobi.VstPluginInfo.Vst3;

/// <summary>
/// Class factory that any Plug-in defines for creating class instances.
/// - [plug imp]
///
/// From the host's point of view a Plug-in module is a factory which can create 
/// a certain kind of object(s). The interface IPluginFactory provides methods 
/// to get information about the classes exported by the Plug-in and a 
/// mechanism to create instances of these classes (that usually define the IPluginBase interface).
/// </summary>
[ComImport]
[Guid(Interfaces.IPluginFactory)]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public interface IPluginFactory
{
    [PreserveSig]
    [return: MarshalAs(UnmanagedType.Error)]
    int GetFactoryInfo(
        [MarshalAs(UnmanagedType.Struct), In, Out] ref PFactoryInfo info);

    [PreserveSig]
    [return: MarshalAs(UnmanagedType.I4)]
    int CountClasses();

    [PreserveSig]
    [return: MarshalAs(UnmanagedType.Error)]
    int GetClassInfo(
        [MarshalAs(UnmanagedType.I4), In] int index,
        [MarshalAs(UnmanagedType.Struct), In, Out] ref PClassInfo info);

    [PreserveSig]
    [return: MarshalAs(UnmanagedType.Error)]
    int CreateInstance(
        [In] ref Guid classId,
        [In] ref Guid interfaceId,
        [MarshalAs(UnmanagedType.SysInt, IidParameterIndex = 1), In, Out] ref nint instance);
}

[ComImport]
[Guid(Interfaces.IPluginFactory2)]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public interface IPluginFactory2
{

    [PreserveSig]
    [return: MarshalAs(UnmanagedType.Error)]
    int GetFactoryInfo(
        [MarshalAs(UnmanagedType.Struct)] ref PFactoryInfo info);

    [PreserveSig]
    [return: MarshalAs(UnmanagedType.I4)]
    int CountClasses();

    [PreserveSig]
    [return: MarshalAs(UnmanagedType.Error)]
    int GetClassInfo(
        [MarshalAs(UnmanagedType.I4), In] int index,
        [MarshalAs(UnmanagedType.Struct)] ref PClassInfo info);

    [PreserveSig]
    [return: MarshalAs(UnmanagedType.Error)]
    int CreateInstance(
        [In] ref Guid classId,
        [In] ref Guid interfaceId,
        [MarshalAs(UnmanagedType.SysInt, IidParameterIndex = 1), In, Out] ref nint instance);


    //---------------------------------------------------------------------


    [PreserveSig]
    [return: MarshalAs(UnmanagedType.Error)]
    int GetClassInfo2(
        [MarshalAs(UnmanagedType.I4), In] int index,
        [MarshalAs(UnmanagedType.Struct)] ref PClassInfo2 info);

}

[ComImport]
[Guid(Interfaces.IPluginFactory3)]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public interface IPluginFactory3
{
    [PreserveSig]
    [return: MarshalAs(UnmanagedType.Error)]
    int GetFactoryInfo(
        [MarshalAs(UnmanagedType.Struct)] ref PFactoryInfo info);

    [PreserveSig]
    [return: MarshalAs(UnmanagedType.I4)]
    int CountClasses();

    [PreserveSig]
    [return: MarshalAs(UnmanagedType.Error)]
    int GetClassInfo(
        [MarshalAs(UnmanagedType.I4), In] int index,
        [MarshalAs(UnmanagedType.Struct)] ref PClassInfo info);

    [PreserveSig]
    [return: MarshalAs(UnmanagedType.Error)]
    int CreateInstance(
        [In] ref Guid classId,
        [In] ref Guid interfaceId,
        [MarshalAs(UnmanagedType.SysInt, IidParameterIndex = 1), In, Out] ref nint instance);


    //---------------------------------------------------------------------


    [PreserveSig]
    [return: MarshalAs(UnmanagedType.Error)]
    int GetClassInfo2(
        [MarshalAs(UnmanagedType.I4), In] int index,
        [MarshalAs(UnmanagedType.Struct)] ref PClassInfo2 info);


    //---------------------------------------------------------------------


    [PreserveSig]
    [return: MarshalAs(UnmanagedType.Error)]
    int GetClassInfoUnicode(
        [MarshalAs(UnmanagedType.I4), In] int index,
        [MarshalAs(UnmanagedType.Struct)] ref PClassInfoW info);

    [PreserveSig]
    [return: MarshalAs(UnmanagedType.Error)]
    int SetHostContext(
        [MarshalAs(UnmanagedType.IUnknown), In] object context);
}
