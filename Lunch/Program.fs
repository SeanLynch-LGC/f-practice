// open System
// open System.Runtime.InteropServices

// type MOUSEINPUT = struct
//     val dx:int
//     val dy:int
//     val mouseData:int
//     val dwFlags:int
//     val time:int
//     val dwExtraInfo:int
//     new(_dx, _dy, _mouseData, _dwFlags, _time, _dwExtraInfo) = {dx=_dx; dy=_dy; mouseData=_mouseData; dwFlags=_dwFlags; time=_time; dwExtraInfo=_dwExtraInfo}
// end

// type INPUT = struct
//     //need to escape traditional INPUT identifier here since "type" is a reserved word
//     //though could use any other identifier name if so desired
//     val ``type``:int //0 is mouse
//     val mi:MOUSEINPUT
//     new(_type, _mi) = {``type``=_type; mi=_mi}
// end

// let MOUSEEVENTF_RIGHTDOWN = 0x0008
// let MOUSEEVENTF_RIGHTUP = 0x0010

// [<DllImport("user32.dll", SetLastError=true)>]
// extern uint32 SendInput(uint32 nInputs, INPUT* pInputs, int cbSize)

// let mutable inputRightDown = INPUT(0, MOUSEINPUT(0, 0, 0, MOUSEEVENTF_RIGHTDOWN, 0, 0))
// let mutable inputRightUp = INPUT(0, MOUSEINPUT(0, 0, 0, MOUSEEVENTF_RIGHTUP, 0, 0))

// SendInput(uint32 1, &&inputRightDown, Marshal.SizeOf(inputRightDown))
// SendInput(uint32 1, &&inputRightUp, Marshal.SizeOf(inputRightUp))
open System
open System.Threading

type Utility() =
    static let rand = Random()
    static member wait =
        let ms = rand.Next(600000,800000)
        Thread.Sleep ms
        printfn "%A" ms

[<EntryPoint>]
let main args =
    let x  = DateTime.UtcNow
    while true do
        x.ToLongTimeString |> printfn "%A" 
        Utility.wait
        x.ToLongTimeString |> printfn "%A" 
        InteropWithNative.MouseLeftClick()
    0