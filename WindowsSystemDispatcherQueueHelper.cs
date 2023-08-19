using System.Runtime.InteropServices;

namespace AVM;

// API Reference: https://learn.microsoft.com/en-us/windows/win32/api/dispatcherqueue/ns-dispatcherqueue-dispatcherqueueoptions
[StructLayout(LayoutKind.Sequential)]
struct DispatcherQueueOptions
{
  internal int dwSize;
  internal int threadType;
  internal int apartmentType;
}

// This class helps us create a DispatcherQueueController on the current thread which is required by either a MicaControll or DesktopAcrylicController
class WindowsSystemDispatcherQueueHelper
{
  // API Reference: https://learn.microsoft.com/en-us/windows/win32/api/dispatcherqueue/nf-dispatcherqueue-createdispatcherqueuecontroller
  [DllImport("CoreMessaging.dll")]
  private static extern int CreateDispatcherQueueController(
    [In] DispatcherQueueOptions options,
    [In, Out, MarshalAs(UnmanagedType.IUnknown)] ref object? dispatcheerQueueController
  );

  private static object? _dispatcherQueueController;

  public static void EnsureWindowsSystemDispatcherQueueController()
  {
    // Check a DispatcherQueueController has not been created on the current thread
    if (_dispatcherQueueController == null && Windows.System.DispatcherQueue.GetForCurrentThread() == null)
    {
      DispatcherQueueOptions options;
      options.dwSize = Marshal.SizeOf(typeof(DispatcherQueueOptions));
      options.threadType = 2;     // DQTYPE_THREAD_CURRENT
      options.apartmentType = 2;  // DQTAT_COM_STA

      CreateDispatcherQueueController(options, ref _dispatcherQueueController);
    }
  }
}
