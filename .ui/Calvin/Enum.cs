//Rev 1.00 - May 22, 2023

namespace Calvin
{
    public enum LogType : short
    {
        No_Log = 0,
        Manual,
        Warmup,
        Evacuation,
        SteamBypass,
        Desorption,
        Repressurization,
        Adsorption,
        Cycle
    }

    public enum State : short
    {
        eIdle = 0,
        eWarmup,
        eEvacuation,
        eSteamBypass,
        eDesorption,
        eRepressurization,
        eAdsorption,
        eReset,
        ePause,
        eSystemError
    }

    public enum MsgButtons : short
    {
        OK = 0,
        YesNo,
        YesNoCancel
    }

    public enum MsgResponse : short
    {
        No = 0,
        Yes,
        Cancel
    }
}


