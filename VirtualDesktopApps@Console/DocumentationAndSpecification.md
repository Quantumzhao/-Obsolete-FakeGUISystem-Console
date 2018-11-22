# Documentation

As no one would take it seriously (or to say, no one would take a look at it), I'd rather keep it brief. 

The whole program is divided into several modules: The virtual system `VSystem`, a launcher, programs that run in virtual system `SubProgram`, and an interactive units class library `InteractiveUnits`.

---

## Launcher

It is a class used to launch `VSystem`. 

It includes: 

- ### KeyPressed

    ```C#
    public static ConsoleKeyInfo KeyPressed {get; set; }
    ```

    It stores the information of current keyboard event. 

- ### initiation

    ```C#
    public static void initiation();
    ```

    It initiates the system console before VSystem is activated. 

- ### Main

    ```C#
    public static void Main(string[]);
    ```

    The finite application access point. 

## VSystem

This is the superior module of the system, which manages and distributes the thread resources, tasking/multitasking, rendering and other stuff like communicating with CLR and Windows system. 

It includes:

- ### Width

    ```C#
    public const int Width;
    ```

    It specifies the width of displayer area inside a system console. 

- ### Height

    ```C#
    public const int Height;
    ```

    It specifies the height of display area inside a system console. 

- ### RenderBufferModificationQueue

    ```C#
    public static List<int[]> RenderBufferModificationQueue;
    ```

    It stores the modifications that components made during a complete keyboard event. `VSystem` will retrive the information later and update parts of the screen in order to save computation. 

- ### IsFocused

    ```C#
    public static bool IsFocused {get; set; }
    ```

    This specifies whether the focus pointer is located at `VSystem`. 

- ### Layers

    ```C#
    public static LayerCollection Layers { get; set; }
    ```

    This is a child component of `VSystem`, which abstracts the management of layers. 

- ### SubPrograms

    ```C#
    public static SubProgramCollection SubPrograms { get; set; }
    ```

    This is a child component of `VSystem`, which abstracts the management of subprograms. 

- ### RenderAll

    ```C#
    public static void RenderAll();
    ```

    Updates the whole screen. 

- ### RenderPartially

    ```C#
    public void RenderPartially();
    ```

    Updates part of the screen, which is indicated by `RenderBufferModificationQueue`. 

- ### ParseAndExecute

    ```C#
    public static bool ParseAndExecute(ConsoleKeyInfo keyPressed);
    ```

    This method receives a keyboard event, and resolve this command by parsing the context. The pressed key is send to the highlighted subprogram at the very beginning, and if no actions are raised by subprograms, `VSystem` will try to deal with it at last. 
    
    If the key is not used, return a _false_

- ### GetFocusedSubProgram

    ```C#
    public static SubProgram GetFocusedSubProgram();
    ```

    It retrieves a **reference** of the current focused subprogram. 

- ### Start

    ```C#
    public void Start();
    ```

    > **WARNING:** It is a non-implemented method, and it would probably not be fully implemented because I have already forgotten WHY I HAVE CREATED THIS!

    It might be... eh... used to invoke an instance of `VSystem` from exterior? 

## SubProgramCollection

It abstracts the management of subprograms. 

It includes: 

- ### subPrograms