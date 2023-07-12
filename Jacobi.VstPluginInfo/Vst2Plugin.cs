using System.Runtime.InteropServices;

namespace Jacobi.VstPluginInfo;

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
internal unsafe delegate Vst2Plugin* Vst2PluginMain(IntPtr hostCmdHandler);
[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
internal unsafe delegate IntPtr Vst2HostCommand(Vst2Plugin* plugin, Int16 hostCommand, Int32 index, IntPtr value, IntPtr ptr, float opt);
[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
internal unsafe delegate IntPtr Vst2PluginCommand(Vst2Plugin* plugin, Vst2PluginCommands command, Int32 index, IntPtr value, IntPtr ptr, float opt);

internal unsafe struct Vst2Plugin
{
    public Int32 VstP;
    public IntPtr command;
    public IntPtr process;
    public IntPtr parameterSet;
    public IntPtr parameterGet;

    public Int32 programCount;
    public Int32 parameterCount;
    public Int32 inputCount;
    public Int32 outputCount;

    public Vst2PluginFlags flags;

    public IntPtr reserved1;
    public IntPtr reserved2;

    public Int32 startupDelay;
    public Int32 realQualities;
    public Int32 offQualities;
    public float ioRatio;

    public void* obj;
    public void* user;

    public Int32 id;
    public Int32 version;

    public IntPtr replace;
    public IntPtr replaceDouble;

    //Byte[56] filler;
}

/// <summary>
/// Plugin Flags indicating its supported features.
/// </summary>
public enum Vst2PluginFlags
{
    /// <summary>The plugin has its own parameter editor.</summary>
    HasEditor = 1 << 0,
    /// <summary>Legacy.</summary>
    HasClip = 1 << 1,
    /// <summary>Legacy.</summary>
    HasVu = 1 << 2,
    /// <summary>Legacy.</summary>
    CanMono = 1 << 3,
    /// <summary>The plugin supports the new way of passing samples to the host.</summary>
    CanReplace = 1 << 4,
    /// <summary>The plugin supports programs.</summary>
    Programs = 1 << 5,
    /// <summary>The plugin is a synth instrument.</summary>
    IsSynth = 1 << 8,
    /// <summary>The plugin does not generate sound when the sequencer is stopped.</summary>
    NoSoundInStop = 1 << 9,
    /// <summary>Legacy.</summary>
    ExtIsAsync = 1 << 10,
    /// <summary>Legacy.</summary>
    ExtHasBuffer = 1 << 11,
    /// <summary>The plugin supports double precision (64 bits) audio samples.</summary>
    CanReplaceDouble = 1 << 12
}


internal enum Vst2PluginCommands
{
    Open,
    Close,
    ProgramSet,
    ProgramGet,
    ProgramSetName,
    ProgramGetName,
    ParameterGetLabel,
    ParameterGetDisplay,
    ParameterGetName,
    VuGet,
    SampleRateSet,
    BlockSizeSet,
    OnOff,
    EditorGetRectangle,
    EditorOpen,
    EditorClose,
    EditorDraw,
    EditorMouse,
    EditorKey,
    EditorIdle,
    EditorTop,
    EditorSleep,
    Identify,
    ChunkGet,
    ChunkSet,
    ProcessEvents,
    ParameterCanBeAutomated,
    ParameterFromString,
    ProgramGetCategoriesCount,
    ProgramGetNameByIndex,
    ProgramCopy,
    ConnectInput,
    ConnectOutput,
    GetInputProperties,
    GetOutputProperties,
    PluginGetCategory,
    GetCurrentPosition,
    GetDestinationBuffer,
    OfflineNotify,
    OfflinePrepare,
    OfflineRun,
    ProcessVariableIo,
    SetSpeakerArrangement,
    SetBlockSizeAndSampleRate,
    SetBypass,
    PluginGetName,
    GetErrorText,
    VendorGetString,
    ProductGetString,
    VendorGetVersion,
    VendorSpecific,
    CanDo,
    GetTailSizeInSamples,
    Idle,
    GetIcon,
    SetViewPosition,
    ParameterGetProperties,
    KeysRequired,
    GetVstVersion,
    EditorKeyDown,
    EditorKeyUp,
    SetKnobMode,
    MidiProgramGetName,
    MidiProgramGetCurrent,
    MidiProgramGetCategory,
    MidiProgramsChanged,
    MidiKeyGetName,
    BeginSetProgram,
    EndSetProgram,
    GetSpeakerArrangement,
    GetNextPlugin,
    ProcessStart,
    ProcessStop,
    SetTotalFramesToProcess,
    SetPanLaw,
    BeginLoadBank,
    BeginLoadProgram,
    SetProcessPrecision,
    MidiGetInputChannelCount,
    MidiGetOutputChannelCount,
};
