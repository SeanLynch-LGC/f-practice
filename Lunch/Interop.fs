module InteropWithNative 

open System.Runtime.InteropServices

    [<DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)>]
    extern void mouse_event(System.Int64, System.Int64, System.Int64, System.Int64, System.Int64)

    let MOUSEEVENTF_LEFTDOWN    = 0x02L
    let MOUSEEVENTF_LEFTUP      = 0x04L
    let MOUSEEVENTF_RIGHTDOWN   = 0x08L
    let MOUSEEVENTF_RIGHTUP     = 0x10L

    let MouseLeftClick () = 
        mouse_event(MOUSEEVENTF_LEFTDOWN, 0L, 0L, 0L, 0L)
        mouse_event(MOUSEEVENTF_LEFTUP, 0L, 0L, 0L, 0L)

