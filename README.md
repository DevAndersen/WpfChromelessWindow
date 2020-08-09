# WpfChromelessWindow

When working with WPF windows that use custom window decoration (by changing the window's WindowChrome), the window expand beyond the screen's boundaries when maximized.

This project aims to provide a solution to this problem, by providing a custom window type, `ChromelessWindow`, which takes care of this issue. It does so by calculating the offset, and adjust the useable window area's location and size accordingly using hidden borders.

Note:

- This project has not been tested on high-DPI displays. While DPI-scaling is taken into consideration, the project has only been tested on 1080p monitors with different Windows scaling values.
- While this project is made for .NET Core 3.1, the concept behind the solution also works for .NET Framework 4.8.

## Implementation

For examples of implementation, please see the `DevAndersen.WpfChromelessWindow.Demo` project found in the solution.

Running this project will start out with a simple selection window, where you can choose which of two demos you want to see:

- Minimalist: Contains the bare minimum, plus a 1-pixel wide red border, to show that the window doesn't leak outside of the current screen's boundaries.
- Decorated: Demonstrates simple implementation of basic window controls, such as a window header with icon/title/minimize-/maximize-/close buttons.

## Known issues

A shortcoming of this project that is worth mentioning is that the window does not resize to fit the screen when all of the following conditions are met:

- A `ChromelessWindow` is moved from one screen to another using the ([Windows Key] + [Shift] + [Left/Right arrow]) keyboard shortcut.
- The screen it is moved from has the same resolution as the screen is moved to.
- The Windows taskbar is only visible on one of these two screens.

I was unable to find a way to neatly detect when the aforementioned keyboard shortcut is used, when both screen resolutions are identical. Any information how to resolve this issue would be very welcome.
