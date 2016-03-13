using System.Diagnostics.CodeAnalysis;
using System.Runtime.Versioning;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.ConstrainedExecution;
using System;
using System.Security.Permissions;
using System.Collections;
using System.IO;
using System.Text;
using System.Security;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using IComDataObject = System.Runtime.InteropServices.ComTypes.IDataObject;

namespace Windows
{
    [SuppressUnmanagedCodeSecurity()]
    public static class NativeCOM
    {

        [ComImport(), Guid("00000122-0000-0000-C000-000000000046"), InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
        public interface IOleDropTarget
        {
            [PreserveSig]
            int OleDragEnter(
                [In, MarshalAs(UnmanagedType.Interface)] 
                object pDataObj,
                [In, MarshalAs(UnmanagedType.U4)] 
                int grfKeyState,
                [In, MarshalAs(UnmanagedType.U8)]
                long pt,
                [In, Out] 
                ref int pdwEffect);

            [PreserveSig]
            int OleDragOver(
                [In, MarshalAs(UnmanagedType.U4)] 
                int grfKeyState,
                [In, MarshalAs(UnmanagedType.U8)]
                long pt,
                [In, Out] 
                ref int pdwEffect);

            [PreserveSig]
            int OleDragLeave();

            [PreserveSig]
            int OleDrop(
                [In, MarshalAs(UnmanagedType.Interface)]
                object pDataObj,
                [In, MarshalAs(UnmanagedType.U4)]
                int grfKeyState,
                [In, MarshalAs(UnmanagedType.U8)] 
                long pt,
                [In, Out] 
                ref int pdwEffect);
        }
        [ComImport(), Guid("00000121-0000-0000-C000-000000000046"), InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
        public interface IOleDropSource
        {

            [PreserveSig]
            int OleQueryContinueDrag(
                int fEscapePressed,
                [In, MarshalAs(UnmanagedType.U4)]
                int grfKeyState);

            [PreserveSig]
            int OleGiveFeedback(
                [In, MarshalAs(UnmanagedType.U4)] 
                int dwEffect);
        }
        [ComImport(), Guid("00000016-0000-0000-C000-000000000046"), InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
        public interface IOleMessageFilter
        {

            [PreserveSig]
            int HandleInComingCall(
                int dwCallType,
                IntPtr hTaskCaller,
                int dwTickCount,
                /* LPINTERFACEINFO */ IntPtr lpInterfaceInfo);

            [PreserveSig]
            int RetryRejectedCall(
                IntPtr hTaskCallee,
                int dwTickCount,
                int dwRejectType);

            [PreserveSig]
            int MessagePending(
                IntPtr hTaskCallee,
                int dwTickCount,
                int dwPendingType);
        }
        [ComImport(), Guid("B196B289-BAB4-101A-B69C-00AA00341D07"), InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
        public interface IOleControlSite
        {

            [PreserveSig]
            int OnControlInfoChanged();

            [PreserveSig]
            int LockInPlaceActive(int fLock);

            [PreserveSig]
            int GetExtendedControl(
                [Out, MarshalAs(UnmanagedType.IDispatch)]
                out object ppDisp);

            [PreserveSig]
            int TransformCoords(
                [In, Out]
                NativeMethods._POINTL pPtlHimetric,
                [In, Out]
                NativeMethods.tagPOINTF pPtfContainer,
                [In, MarshalAs(UnmanagedType.U4)]
                int dwFlags);

            [PreserveSig]
            int TranslateAccelerator(
                [In]
                ref NativeMethods.MSG pMsg,
                [In, MarshalAs(UnmanagedType.U4)]
                int grfModifiers);

            [PreserveSig]
            int OnFocus(int fGotFocus);

            [PreserveSig]
            int ShowPropertyFrame();

        }
        [ComImport(), Guid("00000118-0000-0000-C000-000000000046"), InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
        public interface IOleClientSite
        {
            [PreserveSig]
            int SaveObject();
            [PreserveSig]
            int GetMoniker(
                [In, MarshalAs(UnmanagedType.U4)]
                int dwAssign,
                [In, MarshalAs(UnmanagedType.U4)] 
                int dwWhichMoniker,
                [Out, MarshalAs(UnmanagedType.Interface)] 
                out object moniker);

            [PreserveSig]
            int GetContainer(out IOleContainer container);

            [PreserveSig]
            int ShowObject();

            [PreserveSig]
            int OnShowWindow(int fShow);

            [PreserveSig]
            int RequestNewObjectLayout();
        }
        [ComImport(), Guid("00000119-0000-0000-C000-000000000046"), InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
        public interface IOleInPlaceSite
        {

            IntPtr GetWindow();

            [PreserveSig]
            int ContextSensitiveHelp(int fEnterMode);

            [PreserveSig]
            int CanInPlaceActivate();

            [PreserveSig]
            int OnInPlaceActivate();

            [PreserveSig]
            int OnUIActivate();

            [PreserveSig]
            int GetWindowContext(
                [Out, MarshalAs(UnmanagedType.Interface)]
                out NativeCOM.IOleInPlaceFrame ppFrame,
                [Out, MarshalAs(UnmanagedType.Interface)] 
                out NativeCOM.IOleInPlaceUIWindow ppDoc,
                [Out] 
                NativeMethods.COMRECT lprcPosRect,
                [Out]
                NativeMethods.COMRECT lprcClipRect,
                [In, Out] 
                NativeMethods.tagOIFI lpFrameInfo);

            [PreserveSig]
            int Scroll(
                NativeMethods.tagSIZE scrollExtant);

            [PreserveSig]
            int OnUIDeactivate(
                int fUndoable);

            [PreserveSig]
            int OnInPlaceDeactivate();

            [PreserveSig]
            int DiscardUndoState();

            [PreserveSig]
            int DeactivateAndUndo();

            [PreserveSig]
            int OnPosRectChange(
                [In]
                NativeMethods.COMRECT lprcPosRect);
        }
        [ComImport(), Guid("742B0E01-14E6-101B-914E-00AA00300CAB"), InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
        public interface ISimpleFrameSite
        {

            [PreserveSig]
            int PreMessageFilter(
            IntPtr hwnd,
            [In, MarshalAs(UnmanagedType.U4)] 
                int msg,
            IntPtr wp,
            IntPtr lp,
            [In, Out] 
                ref IntPtr plResult,
            [In, Out, MarshalAs(UnmanagedType.U4)] 
                ref int pdwCookie);

            [PreserveSig]
            int PostMessageFilter(
                IntPtr hwnd,
                [In, MarshalAs(UnmanagedType.U4)]
                int msg,
                IntPtr wp,
                IntPtr lp,
                [In, Out] 
                ref IntPtr plResult,
                [In, MarshalAs(UnmanagedType.U4)] 
                int dwCookie);
        }
        [ComImport(), Guid("40A050A0-3C31-101B-A82E-08002B2B2337"), InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
        public interface IVBGetControl
        {

            [PreserveSig]
            int EnumControls(
                int dwOleContF,
                int dwWhich,
                [Out]
                out IEnumUnknown ppenum);
        }
        [ComImport(), Guid("91733A60-3F4C-101B-A3F6-00AA0034E4E9"), InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
        public interface IGetVBAObject
        {

            [PreserveSig]
            int GetObject(
                 [In]
                ref Guid riid,
                 [Out, MarshalAs(UnmanagedType.LPArray)] 
                IVBFormat[] rval,
                 int dwReserved);
        }
        [ComImport(), Guid("9BFBBC02-EFF1-101A-84ED-00AA00341D07"), InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
        public interface IPropertyNotifySink
        {
            void OnChanged(int dispID);

            [PreserveSig]
            int OnRequestEdit(int dispID);
        }
        [ComImport(), Guid("9849FD60-3768-101B-8D72-AE6164FFE3CF"), InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
        public interface IVBFormat
        {

            [PreserveSig]
            int Format(
                [In] 
                ref object var,
                IntPtr pszFormat,
                IntPtr lpBuffer,
                short cpBuffer,
                int lcid,
                short firstD,
                short firstW,
                [Out, MarshalAs(UnmanagedType.LPArray)]
                short[] result);
        }
        [ComImport(), Guid("00000100-0000-0000-C000-000000000046"), InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
        public interface IEnumUnknown
        {

            [PreserveSig]
            int Next(
                [In, MarshalAs(UnmanagedType.U4)]
                int celt,
                [Out]
                IntPtr rgelt,
                IntPtr pceltFetched);

            [PreserveSig]
            int Skip(
            [In, MarshalAs(UnmanagedType.U4)]
                int celt);

            void Reset();

            void Clone(
                [Out]
                out IEnumUnknown ppenum);
        }
        [ComImport(), Guid("0000011B-0000-0000-C000-000000000046"), InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
        public interface IOleContainer
        {

            [PreserveSig]
            int ParseDisplayName(
                [In, MarshalAs(UnmanagedType.Interface)]
                object pbc,
                [In, MarshalAs(UnmanagedType.BStr)]
                string pszDisplayName,
                [Out, MarshalAs(UnmanagedType.LPArray)]
                int[] pchEaten,
                [Out, MarshalAs(UnmanagedType.LPArray)]
                object[] ppmkOut);

            [PreserveSig]
            int EnumObjects(
                [In, MarshalAs(UnmanagedType.U4)]
                int grfFlags,
                [Out]
                out IEnumUnknown ppenum);

            [PreserveSig]
            int LockContainer(
                bool fLock);
        }
        [ComImport(), Guid("00000116-0000-0000-C000-000000000046"), InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
        public interface IOleInPlaceFrame
        {
            IntPtr GetWindow();

            [PreserveSig]
            int ContextSensitiveHelp(int fEnterMode);

            [PreserveSig]
            int GetBorder(
                [Out]
                NativeMethods.COMRECT lprectBorder);

            [PreserveSig]
            int RequestBorderSpace(
                [In]
                NativeMethods.COMRECT pborderwidths);

            [PreserveSig]
            int SetBorderSpace(
                [In] 
                NativeMethods.COMRECT pborderwidths);

            [PreserveSig]
            int SetActiveObject(
                [In, MarshalAs(UnmanagedType.Interface)] 
                NativeCOM.IOleInPlaceActiveObject pActiveObject,
                [In, MarshalAs(UnmanagedType.LPWStr)]
                string pszObjName);

            [PreserveSig]
            int InsertMenus(
                [In] 
                IntPtr hmenuShared,
                [In, Out] 
                NativeMethods.tagOleMenuGroupWidths lpMenuWidths);

            [PreserveSig]
            int SetMenu(
                [In]
                IntPtr hmenuShared,
                [In] 
                IntPtr holemenu,
                [In] 
                IntPtr hwndActiveObject);

            [PreserveSig]
            int RemoveMenus(
                [In]
                IntPtr hmenuShared);

            [PreserveSig]
            int SetStatusText(
                [In, MarshalAs(UnmanagedType.LPWStr)]
                string pszStatusText);

            [PreserveSig]
            int EnableModeless(
                bool fEnable);

            [PreserveSig]
            int TranslateAccelerator(
            [In]
                ref NativeMethods.MSG lpmsg,
            [In, MarshalAs(UnmanagedType.U2)]
                short wID);
        }
        // Used to control the webbrowser appearance and provide DTE to script via window.external 
        [ComVisible(true), ComImport(), Guid("BD3F23C0-D43E-11CF-893B-00AA00BDCE1A"),
        InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
        public interface IDocHostUIHandler
        {

            [return: MarshalAs(UnmanagedType.I4)]
            [PreserveSig]
            int ShowContextMenu(
                [In, MarshalAs(UnmanagedType.U4)] 
                int dwID,
                [In]
                NativeMethods.POINT pt,
                [In, MarshalAs(UnmanagedType.Interface)]
                object pcmdtReserved,
                [In, MarshalAs(UnmanagedType.Interface)]
                object pdispReserved);

            [return: MarshalAs(UnmanagedType.I4)]
            [PreserveSig]
            int GetHostInfo(
                [In, Out] 
                NativeMethods.DOCHOSTUIINFO info);

            [return: MarshalAs(UnmanagedType.I4)]
            [PreserveSig]
            int ShowUI(
                [In, MarshalAs(UnmanagedType.I4)] 
                int dwID,
                [In]
                NativeCOM.IOleInPlaceActiveObject activeObject,
                [In]
                NativeMethods.IOleCommandTarget commandTarget,
                [In]
                NativeCOM.IOleInPlaceFrame frame,
                [In]
                NativeCOM.IOleInPlaceUIWindow doc);

            [return: MarshalAs(UnmanagedType.I4)]
            [PreserveSig]
            int HideUI();

            [return: MarshalAs(UnmanagedType.I4)]
            [PreserveSig]
            int UpdateUI();

            [return: MarshalAs(UnmanagedType.I4)]
            [PreserveSig]
            int EnableModeless(
                [In, MarshalAs(UnmanagedType.Bool)]
                bool fEnable);

            [return: MarshalAs(UnmanagedType.I4)]
            [PreserveSig]
            int OnDocWindowActivate(
                [In, MarshalAs(UnmanagedType.Bool)] 
                bool fActivate);

            [return: MarshalAs(UnmanagedType.I4)]
            [PreserveSig]
            int OnFrameWindowActivate(
                [In, MarshalAs(UnmanagedType.Bool)] 
                bool fActivate);

            [return: MarshalAs(UnmanagedType.I4)]
            [PreserveSig]
            int ResizeBorder(
                [In]
                NativeMethods.COMRECT rect,
                [In]
                NativeCOM.IOleInPlaceUIWindow doc,
                bool fFrameWindow);

            [return: MarshalAs(UnmanagedType.I4)]
            [PreserveSig]
            int TranslateAccelerator(
                [In]
                ref NativeMethods.MSG msg,
                [In]
                ref Guid group,
                [In, MarshalAs(UnmanagedType.I4)]
                int nCmdID);

            [return: MarshalAs(UnmanagedType.I4)]
            [PreserveSig]
            int GetOptionKeyPath(
                [Out, MarshalAs(UnmanagedType.LPArray)]
                String[] pbstrKey,
                [In, MarshalAs(UnmanagedType.U4)] 
                int dw);

            [return: MarshalAs(UnmanagedType.I4)]
            [PreserveSig]
            int GetDropTarget(
                [In, MarshalAs(UnmanagedType.Interface)]
                NativeCOM.IOleDropTarget pDropTarget,
                [Out, MarshalAs(UnmanagedType.Interface)]
                out NativeCOM.IOleDropTarget ppDropTarget);

            [return: MarshalAs(UnmanagedType.I4)]
            [PreserveSig]
            int GetExternal(
                [Out, MarshalAs(UnmanagedType.Interface)] 
                out object ppDispatch);

            [return: MarshalAs(UnmanagedType.I4)]
            [PreserveSig]
            int TranslateUrl(
                [In, MarshalAs(UnmanagedType.U4)] 
                int dwTranslate,
                [In, MarshalAs(UnmanagedType.LPWStr)]
                string strURLIn,
                [Out, MarshalAs(UnmanagedType.LPWStr)]
                out string pstrURLOut);

            [return: MarshalAs(UnmanagedType.I4)]
            [PreserveSig]
            int FilterDataObject(
                IComDataObject pDO,
                out IComDataObject ppDORet);
        }
        [SuppressUnmanagedCodeSecurity, ComImport(), Guid("D30C1661-CDAF-11d0-8A3E-00C04FC9E26E"), TypeLibType(TypeLibTypeFlags.FHidden | TypeLibTypeFlags.FDual | TypeLibTypeFlags.FOleAutomation)]
        public interface IWebBrowser2
        {
            //
            // IWebBrowser members 
            [DispId(100)]
            void GoBack();
            [DispId(101)]
            void GoForward();
            [DispId(102)]
            void GoHome();
            [DispId(103)]
            void GoSearch();
            [DispId(104)]
            void Navigate([In] string Url, [In] ref object flags,
              [In] ref object targetFrameName, [In] ref object postData,
              [In] ref object headers);
            [DispId(-550)]
            void Refresh();
            [DispId(105)]
            void Refresh2([In] ref object level);
            [DispId(106)]
            void Stop();
            [DispId(200)]
            object Application { [return: MarshalAs(UnmanagedType.IDispatch)]get; }
            [DispId(201)]
            object Parent { [return: MarshalAs(UnmanagedType.IDispatch)]get; }
            [DispId(202)]
            object Container { [return: MarshalAs(UnmanagedType.IDispatch)]get; }
            [DispId(203)]
            object Document { [return: MarshalAs(UnmanagedType.IDispatch)]get; }
            [DispId(204)]
            bool TopLevelContainer { get; }
            [DispId(205)]
            string Type { get; }
            [DispId(206)]
            int Left { get; set; }
            [DispId(207)]
            int Top { get; set; }
            [DispId(208)]
            int Width { get; set; }
            [DispId(209)]
            int Height { get; set; }
            [DispId(210)]
            string LocationName { get; }
            [DispId(211)]
            string LocationURL { get; }
            [DispId(212)]
            bool Busy { get; }
            //
            // IWebBrowserApp members 
            [DispId(300)]
            void Quit();
            [DispId(301)]
            void ClientToWindow([Out]out int pcx, [Out]out int pcy);
            [DispId(302)]
            void PutProperty([In] string property, [In] object vtValue);
            [DispId(303)]
            object GetProperty([In] string property);
            [DispId(0)]
            string Name { get; }
            [DispId(-515)]
            int HWND { get; }
            [DispId(400)]
            string FullName { get; }
            [DispId(401)]
            string Path { get; }
            [DispId(402)]
            bool Visible { get; set; }
            [DispId(403)]
            bool StatusBar { get; set; }
            [DispId(404)]
            string StatusText { get; set; }
            [DispId(405)]
            int ToolBar { get; set; }
            [DispId(406)]
            bool MenuBar { get; set; }
            [DispId(407)]
            bool FullScreen { get; set; }
            //
            // IWebBrowser2 members
            [DispId(500)]
            void Navigate2([In] ref object URL, [In] ref object flags,
              [In] ref object targetFrameName, [In] ref object postData,
              [In] ref object headers);
            [DispId(501)]
            NativeMethods.OLECMDF QueryStatusWB([In] NativeMethods.OLECMDID cmdID);
            [DispId(502)]
            void ExecWB([In] NativeMethods.OLECMDID cmdID,
      [In] NativeMethods.OLECMDEXECOPT cmdexecopt,
      ref object pvaIn,
      IntPtr pvaOut);
            [DispId(503)]
            void ShowBrowserBar([In] ref object pvaClsid, [In] ref object pvarShow,
      [In] ref object pvarSize);
            [DispId(-525)]
            WebBrowserReadyState ReadyState { get; }
            [DispId(550)]
            bool Offline { get; set; }
            [DispId(551)]
            bool Silent { get; set; }
            [DispId(552)]
            bool RegisterAsBrowser { get; set; }
            [DispId(553)]
            bool RegisterAsDropTarget { get; set; }
            [DispId(554)]
            bool TheaterMode { get; set; }
            [DispId(555)]
            bool AddressBar { get; set; }
            [DispId(556)]
            bool Resizable { get; set; }
        }
        [ComImport(), Guid("34A715A0-6587-11D0-924A-0020AFC7AC4D"), InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIDispatch), TypeLibType(TypeLibTypeFlags.FHidden)]
        public interface DWebBrowserEvents2
        {
            [DispId(102)]
            void StatusTextChange([In] string text);
            [DispId(108)]
            void ProgressChange([In] int progress, [In] int progressMax);
            [DispId(105)]
            void CommandStateChange([In] long command, [In] bool enable);
            [DispId(106)]
            void DownloadBegin();
            [DispId(104)]
            void DownloadComplete();
            [DispId(113)]
            void TitleChange([In] string text);
            [DispId(112)]
            void PropertyChange([In] string szProperty);
            [DispId(250)]
            void BeforeNavigate2([In, MarshalAs(UnmanagedType.IDispatch)] object pDisp,
                   [In] ref object URL, [In] ref object flags,
                   [In] ref object targetFrameName, [In] ref object postData,
                   [In] ref object headers, [In, Out] ref bool cancel);
            [DispId(251)]
            void NewWindow2([In, Out, MarshalAs(UnmanagedType.IDispatch)] ref object pDisp,
                  [In, Out] ref bool cancel);
            [DispId(252)]
            void NavigateComplete2([In, MarshalAs(UnmanagedType.IDispatch)] object pDisp,
                  [In] ref object URL);
            [DispId(259)]
            void DocumentComplete([In, MarshalAs(UnmanagedType.IDispatch)] object pDisp,
                  [In] ref object URL);
            [DispId(253)]
            void OnQuit();
            [DispId(254)]
            void OnVisible([In] bool visible);
            [DispId(255)]
            void OnToolBar([In] bool toolBar);
            [DispId(256)]
            void OnMenuBar([In] bool menuBar);
            [DispId(257)]
            void OnStatusBar([In] bool statusBar);
            [DispId(258)]
            void OnFullScreen([In] bool fullScreen);
            [DispId(260)]
            void OnTheaterMode([In] bool theaterMode);
            [DispId(262)]
            void WindowSetResizable([In] bool resizable);
            [DispId(264)]
            void WindowSetLeft([In] int left);
            [DispId(265)]
            void WindowSetTop([In] int top);
            [DispId(266)]
            void WindowSetWidth([In] int width);
            [DispId(267)]
            void WindowSetHeight([In] int height);
            [DispId(263)]
            void WindowClosing([In] bool isChildWindow, [In, Out] ref bool cancel);
            [DispId(268)]
            void ClientToHostWindow([In, Out] ref long cx, [In, Out] ref long cy);
            [DispId(269)]
            void SetSecureLockIcon([In] int secureLockIcon);
            [DispId(270)]
            void FileDownload([In, Out] ref bool cancel);
            [DispId(271)]
            void NavigateError([In, MarshalAs(UnmanagedType.IDispatch)] object pDisp,
      [In] ref object URL, [In] ref object frame, [In] ref object statusCode, [In, Out] ref bool cancel);
            [DispId(225)]
            void PrintTemplateInstantiation([In, MarshalAs(UnmanagedType.IDispatch)] object pDisp);
            [DispId(226)]
            void PrintTemplateTeardown([In, MarshalAs(UnmanagedType.IDispatch)] object pDisp);
            [DispId(227)]
            void UpdatePageStatus([In, MarshalAs(UnmanagedType.IDispatch)] object pDisp,
      [In] ref object nPage, [In] ref object fDone);
            [DispId(272)]
            void PrivacyImpactedStateChange([In] bool bImpacted);
        }
        [SuppressUnmanagedCodeSecurity, ComVisible(true), Guid("626FC520-A41E-11cf-A731-00A0C9082637"), InterfaceTypeAttribute(ComInterfaceType.InterfaceIsDual)]
        public interface IHTMLDocument
        {
            [return: MarshalAs(UnmanagedType.IDispatch)]
            object GetScript();
        }
        [SuppressUnmanagedCodeSecurity, ComVisible(true), Guid("332C4425-26CB-11D0-B483-00C04FD90119"),
        InterfaceTypeAttribute(ComInterfaceType.InterfaceIsDual)]
        public interface IHTMLDocument2
        {
            [return: MarshalAs(UnmanagedType.IDispatch)]
            object GetScript();
            IHTMLElementCollection GetAll();
            IHTMLElement GetBody();
            IHTMLElement GetActiveElement();
            IHTMLElementCollection GetImages();
            IHTMLElementCollection GetApplets();
            IHTMLElementCollection GetLinks();
            IHTMLElementCollection GetForms();
            IHTMLElementCollection GetAnchors();
            void SetTitle(string p);
            string GetTitle();
            IHTMLElementCollection GetScripts();
            void SetDesignMode(string p);
            string GetDesignMode();
            [return: MarshalAs(UnmanagedType.Interface)]
            object GetSelection();
            string GetReadyState();
            [return: MarshalAs(UnmanagedType.Interface)]
            object GetFrames();
            IHTMLElementCollection GetEmbeds();
            IHTMLElementCollection GetPlugins();
            void SetAlinkColor(object c);
            object GetAlinkColor();
            void SetBgColor(object c);
            object GetBgColor();
            void SetFgColor(object c);
            object GetFgColor();
            void SetLinkColor(object c);
            object GetLinkColor();
            void SetVlinkColor(object c);
            object GetVlinkColor();
            string GetReferrer();
            [return: MarshalAs(UnmanagedType.Interface)]
            NativeCOM.IHTMLLocation GetLocation();
            string GetLastModified();
            void SetUrl(string p);
            string GetUrl();
            void SetDomain(string p);
            string GetDomain();
            void SetCookie(string p);
            string GetCookie();
            void SetExpando(bool p);
            bool GetExpando();
            void SetCharset(string p);
            string GetCharset();
            void SetDefaultCharset(string p);
            string GetDefaultCharset();
            string GetMimeType();
            string GetFileSize();
            string GetFileCreatedDate();
            string GetFileModifiedDate();
            string GetFileUpdatedDate();
            string GetSecurity();
            string GetProtocol();
            string GetNameProp();
            int Write([In, MarshalAs(UnmanagedType.SafeArray)] object[] psarray);
            int WriteLine([In, MarshalAs(UnmanagedType.SafeArray)] object[] psarray);
            [return: MarshalAs(UnmanagedType.Interface)]
            object Open(string mimeExtension, object name, object features, object replace);
            void Close();
            void Clear();
            bool QueryCommandSupported(string cmdID);
            bool QueryCommandEnabled(string cmdID);
            bool QueryCommandState(string cmdID);
            bool QueryCommandIndeterm(string cmdID);
            string QueryCommandText(
                    string cmdID);
            Object QueryCommandValue(string cmdID);
            bool ExecCommand(string cmdID,
                    bool showUI, Object value);
            bool ExecCommandShowHelp(string cmdID);
            IHTMLElement CreateElement(string eTag);
            void SetOnhelp(Object p);
            Object GetOnhelp();
            void SetOnclick(Object p);
            Object GetOnclick();
            void SetOndblclick(Object p);
            Object GetOndblclick();
            void SetOnkeyup(Object p);
            Object GetOnkeyup();
            void SetOnkeydown(Object p);
            Object GetOnkeydown();
            void SetOnkeypress(Object p);
            Object GetOnkeypress();
            void SetOnmouseup(Object p);
            Object GetOnmouseup();
            void SetOnmousedown(Object p);
            Object GetOnmousedown();
            void SetOnmousemove(Object p);
            Object GetOnmousemove();
            void SetOnmouseout(Object p);
            Object GetOnmouseout();
            void SetOnmouseover(Object p);
            Object GetOnmouseover();
            void SetOnreadystatechange(Object p);
            Object GetOnreadystatechange();
            void SetOnafterupdate(Object p);
            Object GetOnafterupdate();
            void SetOnrowexit(Object p);
            Object GetOnrowexit();
            void SetOnrowenter(Object p);
            Object GetOnrowenter();
            void SetOndragstart(Object p);
            Object GetOndragstart();
            void SetOnselectstart(Object p);
            Object GetOnselectstart();
            IHTMLElement ElementFromPoint(int x, int y);
            [return: MarshalAs(UnmanagedType.Interface)]
            NativeCOM.IHTMLWindow2 GetParentWindow();
            [return: MarshalAs(UnmanagedType.Interface)]
            object GetStyleSheets();
            void SetOnbeforeupdate(Object p);
            Object GetOnbeforeupdate();
            void SetOnerrorupdate(Object p);
            Object GetOnerrorupdate();
            string toString();
            [return: MarshalAs(UnmanagedType.Interface)]
            object CreateStyleSheet(string bstrHref, int lIndex);
        }
        [SuppressUnmanagedCodeSecurity, ComVisible(true), Guid("3050F485-98B5-11CF-BB82-00AA00BDCE0B"), InterfaceTypeAttribute(ComInterfaceType.InterfaceIsDual)]
        public interface IHTMLDocument3
        {
            void ReleaseCapture();
            void Recalc([In] bool fForce);
            object CreateTextNode([In] string text);
            [return: MarshalAs(UnmanagedType.Interface)]
            NativeCOM.IHTMLElement GetDocumentElement();
            string GetUniqueID();
            bool AttachEvent([In] string ev, [In, MarshalAs(UnmanagedType.IDispatch)] object pdisp);
            void DetachEvent([In] string ev, [In, MarshalAs(UnmanagedType.IDispatch)] object pdisp);
            void SetOnrowsdelete([In] Object p);
            Object GetOnrowsdelete();
            void SetOnrowsinserted([In] Object p);
            Object GetOnrowsinserted();
            void SetOncellchange([In] Object p);
            Object GetOncellchange();
            void SetOndatasetchanged([In] Object p);
            Object GetOndatasetchanged();
            void SetOndataavailable([In] Object p);
            Object GetOndataavailable();
            void SetOndatasetcomplete([In] Object p);
            Object GetOndatasetcomplete();
            void SetOnpropertychange([In] Object p);
            Object GetOnpropertychange();
            void SetDir([In] string p);
            string GetDir();
            void SetOncontextmenu([In] Object p);
            Object GetOncontextmenu();
            void SetOnstop([In] Object p);
            Object GetOnstop();
            object CreateDocumentFragment();
            object GetParentDocument();
            void SetEnableDownload([In] bool p);
            bool GetEnableDownload();
            void SetBaseUrl([In] string p);
            string GetBaseUrl();
            [return: MarshalAs(UnmanagedType.IDispatch)]
            object GetChildNodes();
            void SetInheritStyleSheets([In] bool p);
            bool GetInheritStyleSheets();
            void SetOnbeforeeditfocus([In] Object p);
            Object GetOnbeforeeditfocus();
            [return: MarshalAs(UnmanagedType.Interface)]
            NativeCOM.IHTMLElementCollection GetElementsByName([In] string v);
            [return: MarshalAs(UnmanagedType.Interface)]
            NativeCOM.IHTMLElement GetElementById([In] string v);
            [return: MarshalAs(UnmanagedType.Interface)]
            NativeCOM.IHTMLElementCollection GetElementsByTagName([In] string v);
        }
        [SuppressUnmanagedCodeSecurity, ComVisible(true), Guid("3050F69A-98B5-11CF-BB82-00AA00BDCE0B"), InterfaceTypeAttribute(System.Runtime.InteropServices.ComInterfaceType.InterfaceIsDual)]
        public interface IHTMLDocument4
        {
            void Focus();
            bool HasFocus();
            void SetOnselectionchange(object p);
            object GetOnselectionchange();
            object GetNamespaces();
            object createDocumentFromUrl(string bstrUrl, string bstrOptions);
            void SetMedia(string bstrMedia);
            string GetMedia();
            object CreateEventObject([In, Optional] ref object eventObject);
            bool FireEvent(string eventName);
            object CreateRenderStyle(string bstr);
            void SetOncontrolselect(object p);
            object GetOncontrolselect();
            string GetURLUnencoded();
        }
        [ComImport(), Guid("3050f613-98b5-11cf-bb82-00aa00bdce0b"), InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIDispatch), TypeLibType(TypeLibTypeFlags.FHidden)]
        public interface DHTMLDocumentEvents2
        {
            [DispId(-2147418102)]
            bool onhelp(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-600)]
            bool onclick(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-601)]
            bool ondblclick(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-602)]
            void onkeydown(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-604)]
            void onkeyup(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-603)]
            bool onkeypress(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-605)]
            void onmousedown(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-606)]
            void onmousemove(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-607)]
            void onmouseup(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418103)]
            void onmouseout(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418104)]
            void onmouseover(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-609)]
            void onreadystatechange(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418108)]
            bool onbeforeupdate(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418107)]
            void onafterupdate(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418106)]
            bool onrowexit(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418105)]
            void onrowenter(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418101)]
            bool ondragstart(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418100)]
            bool onselectstart(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418099)]
            bool onerrorupdate(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1023)]
            bool oncontextmenu(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1026)]
            bool onstop(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418080)]
            void onrowsdelete(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418079)]
            void onrowsinserted(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418078)]
            void oncellchange(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418093)]
            void onpropertychange(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418098)]
            void ondatasetchanged(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418097)]
            void ondataavailable(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418096)]
            void ondatasetcomplete(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1027)]
            void onbeforeeditfocus(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1037)]
            void onselectionchange(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1036)]
            bool oncontrolselect(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1033)]
            bool onmousewheel(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1048)]
            void onfocusin(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1049)]
            void onfocusout(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1044)]
            void onactivate(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1045)]
            void ondeactivate(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1047)]
            bool onbeforeactivate(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1034)]
            bool onbeforedeactivate(NativeCOM.IHTMLEventObj evtObj);
        }
        [SuppressUnmanagedCodeSecurity, ComVisible(true), Guid("332C4426-26CB-11D0-B483-00C04FD90119"), InterfaceTypeAttribute(ComInterfaceType.InterfaceIsDual)]
        public interface IHTMLFramesCollection2
        {
            object Item(ref object idOrName);
            int GetLength();
        }
        [SuppressUnmanagedCodeSecurity, ComVisible(true), Guid("332C4427-26CB-11D0-B483-00C04FD90119"), InterfaceTypeAttribute(ComInterfaceType.InterfaceIsDual)]
        public interface IHTMLWindow2
        {
            [return: MarshalAs(UnmanagedType.IDispatch)]
            object Item([In] ref object pvarIndex);
            int GetLength();
            [return: MarshalAs(UnmanagedType.Interface)]
            NativeCOM.IHTMLFramesCollection2 GetFrames();
            void SetDefaultStatus([In] string p);
            string GetDefaultStatus();
            void SetStatus([In] string p);
            string GetStatus();
            int SetTimeout([In] string expression, [In] int msec, [In] ref Object language);
            void ClearTimeout([In] int timerID);
            void Alert([In] string message);
            bool Confirm([In] string message);
            [return: MarshalAs(UnmanagedType.Struct)]
            object Prompt([In] string message, [In] string defstr);
            object GetImage();
            [return: MarshalAs(UnmanagedType.Interface)]
            NativeCOM.IHTMLLocation GetLocation();
            [return: MarshalAs(UnmanagedType.Interface)]
            NativeCOM.IOmHistory GetHistory();
            void Close();
            void SetOpener([In] object p);
            [return: MarshalAs(UnmanagedType.IDispatch)]
            object GetOpener();
            [return: MarshalAs(UnmanagedType.Interface)]
            NativeCOM.IOmNavigator GetNavigator();
            void SetName([In] string p);
            string GetName();
            [return: MarshalAs(UnmanagedType.Interface)]
            NativeCOM.IHTMLWindow2 GetParent();
            [return: MarshalAs(UnmanagedType.Interface)]
            NativeCOM.IHTMLWindow2 Open([In] string URL, [In] string name, [In] string features, [In] bool replace);
            object GetSelf();
            object GetTop();
            object GetWindow();
            void Navigate([In] string URL);
            void SetOnfocus([In] Object p);
            Object GetOnfocus();
            void SetOnblur([In] Object p);
            Object GetOnblur();
            void SetOnload([In] Object p);
            Object GetOnload();
            void SetOnbeforeunload(Object p);
            Object GetOnbeforeunload();
            void SetOnunload([In] Object p);
            Object GetOnunload();
            void SetOnhelp(Object p);
            Object GetOnhelp();
            void SetOnerror([In] Object p);
            Object GetOnerror();
            void SetOnresize([In] Object p);
            Object GetOnresize();
            void SetOnscroll([In] Object p);
            Object GetOnscroll();
            [return: MarshalAs(UnmanagedType.Interface)]
            NativeCOM.IHTMLDocument2 GetDocument();
            [return: MarshalAs(UnmanagedType.Interface)]
            NativeCOM.IHTMLEventObj GetEvent();
            object Get_newEnum();
            Object ShowModalDialog([In] string dialog, [In] ref Object varArgIn, [In] ref Object varOptions);
            void ShowHelp([In] string helpURL, [In] Object helpArg, [In] string features);
            [return: MarshalAs(UnmanagedType.Interface)]
            NativeCOM.IHTMLScreen GetScreen();
            object GetOption();
            void Focus();
            bool GetClosed();
            void Blur();
            void Scroll([In] int x, [In] int y);
            object GetClientInformation();
            int SetInterval([In] string expression, [In] int msec, [In] ref Object language);
            void ClearInterval([In] int timerID);
            void SetOffscreenBuffering([In] Object p);
            Object GetOffscreenBuffering();
            [return: MarshalAs(UnmanagedType.Struct)]
            Object ExecScript([In] string code, [In] string language);
            string toString();
            void ScrollBy([In] int x, [In] int y);
            void ScrollTo([In] int x, [In] int y);
            void MoveTo([In] int x, [In] int y);
            void MoveBy([In] int x, [In] int y);
            void ResizeTo([In] int x, [In] int y);
            void ResizeBy([In] int x, [In] int y);
            object GetExternal();
        }
        [SuppressUnmanagedCodeSecurity, ComVisible(true), Guid("3050f4ae-98b5-11cf-bb82-00aa00bdce0b"), InterfaceTypeAttribute(ComInterfaceType.InterfaceIsDual)]
        public interface IHTMLWindow3
        {
            int GetScreenLeft();
            int GetScreenTop();
            bool AttachEvent(string ev, [In, MarshalAs(UnmanagedType.IDispatch)] object pdisp);
            void DetachEvent(string ev, [In, MarshalAs(UnmanagedType.IDispatch)] object pdisp);
            int SetTimeout([In]ref object expression, int msec, [In] ref object language);
            int SetInterval([In]ref object expression, int msec, [In] ref object language);
            void Print();
            void SetBeforePrint(object o);
            object GetBeforePrint();
            void SetAfterPrint(object o);
            object GetAfterPrint();
            object GetClipboardData();
            object ShowModelessDialog(string url, object varArgIn, object options);
        }
        [SuppressUnmanagedCodeSecurity, ComVisible(true), Guid("3050f6cf-98b5-11cf-bb82-00aa00bdce0b"), InterfaceTypeAttribute(ComInterfaceType.InterfaceIsDual)]
        public interface IHTMLWindow4
        {
            [return: MarshalAs(UnmanagedType.IDispatch)]
            object CreatePopup([In] ref object reserved);
            [return: MarshalAs(UnmanagedType.Interface)]
            object frameElement();
        }
        [ComImport(), Guid("3050f625-98b5-11cf-bb82-00aa00bdce0b"), InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIDispatch), TypeLibType(TypeLibTypeFlags.FHidden)]
        public interface DHTMLWindowEvents2
        {
            [DispId(1003)]
            void onload(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1008)]
            void onunload(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418102)]
            bool onhelp(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418111)]
            void onfocus(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418112)]
            void onblur(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1002)]
            bool onerror(string description, string url, int line);
            [DispId(1016)]
            void onresize(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1014)]
            void onscroll(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1017)]
            void onbeforeunload(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1024)]
            void onbeforeprint(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1025)]
            void onafterprint(NativeCOM.IHTMLEventObj evtObj);
        }


        [SuppressUnmanagedCodeSecurity, ComVisible(true), Guid("3050f666-98b5-11cf-bb82-00aa00bdce0b"),
        InterfaceTypeAttribute(ComInterfaceType.InterfaceIsDual)]
        public interface IHTMLPopup
        {
            void show(int x, int y, int w, int h, ref object element);
            void hide();
            [return: MarshalAs(UnmanagedType.Interface)]
            NativeCOM.IHTMLDocument GetDocument();
            bool IsOpen();
        }

        [SuppressUnmanagedCodeSecurity, ComVisible(true), Guid("3050f35c-98b5-11cf-bb82-00aa00bdce0b"),
        InterfaceTypeAttribute(ComInterfaceType.InterfaceIsDual)]
        public interface IHTMLScreen
        {
            int GetColorDepth();
            void SetBufferDepth(int d);
            int GetBufferDepth();
            int GetWidth();
            int GetHeight();
            void SetUpdateInterval(int i);
            int GetUpdateInterval();
            int GetAvailHeight();
            int GetAvailWidth();
            bool GetFontSmoothingEnabled();
        }


        [SuppressUnmanagedCodeSecurity, ComVisible(true), Guid("163BB1E0-6E00-11CF-837A-48DC04C10000"),
        InterfaceTypeAttribute(ComInterfaceType.InterfaceIsDual)]
        public interface IHTMLLocation
        {
            void SetHref([In] string p);
            string GetHref();
            void SetProtocol([In] string p);
            string GetProtocol();
            void SetHost([In] string p);
            string GetHost();
            void SetHostname([In] string p);
            string GetHostname();
            void SetPort([In] string p);
            string GetPort();
            void SetPathname([In] string p);
            string GetPathname();
            void SetSearch([In] string p);
            string GetSearch();
            void SetHash([In] string p);
            string GetHash();
            void Reload([In] bool flag);
            void Replace([In] string bstr);
            void Assign([In] string bstr);
        }

        [SuppressUnmanagedCodeSecurity, ComVisible(true), Guid("FECEAAA2-8405-11CF-8BA1-00AA00476DA6"),
        InterfaceTypeAttribute(ComInterfaceType.InterfaceIsDual)]
        public interface IOmHistory
        {
            short GetLength();
            void Back();
            void Forward();
            void Go([In] ref Object pvargdistance);
        }

        [SuppressUnmanagedCodeSecurity, ComVisible(true), Guid("FECEAAA5-8405-11CF-8BA1-00AA00476DA6"),
        InterfaceTypeAttribute(ComInterfaceType.InterfaceIsDual)]
        public interface IOmNavigator
        {
            string GetAppCodeName();
            string GetAppName();
            string GetAppVersion();
            string GetUserAgent();
            bool JavaEnabled();
            bool TaintEnabled();
            object GetMimeTypes();
            object GetPlugins();
            bool GetCookieEnabled();
            object GetOpsProfile();
            string GetCpuClass();
            string GetSystemLanguage();
            string GetBrowserLanguage();
            string GetUserLanguage();
            string GetPlatform();
            string GetAppMinorVersion();
            int GetConnectionSpeed();
            bool GetOnLine();
            object GetUserProfile();
        }

        [SuppressUnmanagedCodeSecurity, ComVisible(true), Guid("3050F32D-98B5-11CF-BB82-00AA00BDCE0B"),
        InterfaceTypeAttribute(System.Runtime.InteropServices.ComInterfaceType.InterfaceIsDual)]
        public interface IHTMLEventObj
        {
            [return: MarshalAs(UnmanagedType.Interface)]
            NativeCOM.IHTMLElement GetSrcElement();
            bool GetAltKey();
            bool GetCtrlKey();
            bool GetShiftKey();
            void SetReturnValue(object p);
            object GetReturnValue();
            void SetCancelBubble(bool p);
            bool GetCancelBubble();
            [return: MarshalAs(UnmanagedType.Interface)]
            NativeCOM.IHTMLElement GetFromElement();
            [return: MarshalAs(UnmanagedType.Interface)]
            NativeCOM.IHTMLElement GetToElement();
            void SetKeyCode([In] int p);
            int GetKeyCode();
            int GetButton();
            string GetEventType();
            string GetQualifier();
            int GetReason();
            int GetX();
            int GetY();
            int GetClientX();
            int GetClientY();
            int GetOffsetX();
            int GetOffsetY();
            int GetScreenX();
            int GetScreenY();
            object GetSrcFilter();
        }

        [SuppressUnmanagedCodeSecurity, ComVisible(true), Guid("3050f48B-98b5-11cf-bb82-00aa00bdce0b"),
        InterfaceTypeAttribute(System.Runtime.InteropServices.ComInterfaceType.InterfaceIsDual)]
        public interface IHTMLEventObj2
        {
            void SetAttribute(string attributeName, object attributeValue, int lFlags);
            object GetAttribute(string attributeName, int lFlags);
            bool RemoveAttribute(string attributeName, int lFlags);
            void SetPropertyName(string name);
            string GetPropertyName();
            void SetBookmarks(ref object bm);
            object GetBookmarks();
            void SetRecordset(ref object rs);
            object GetRecordset();
            void SetDataFld(string df);
            string GetDataFld();
            void SetBoundElements(ref object be);
            object GetBoundElements();
            void SetRepeat(bool r);
            bool GetRepeat();
            void SetSrcUrn(string urn);
            string GetSrcUrn();
            void SetSrcElement(ref object se);
            object GetSrcElement();
            void SetAltKey(bool alt);
            bool GetAltKey();
            void SetCtrlKey(bool ctrl);
            bool GetCtrlKey();
            void SetShiftKey(bool shift);
            bool GetShiftKey();
            void SetFromElement(ref object element);
            object GetFromElement();
            void SetToElement(ref object element);
            object GetToElement();
            void SetButton(int b);
            int GetButton();
            void SetType(string type);
            string GetType();
            void SetQualifier(string q);
            string GetQualifier();
            void SetReason(int r);
            int GetReason();
            void SetX(int x);
            int GetX();
            void SetY(int y);
            int GetY();
            void SetClientX(int x);
            int GetClientX();
            void SetClientY(int y);
            int GetClientY();
            void SetOffsetX(int x);
            int GetOffsetX();
            void SetOffsetY(int y);
            int GetOffsetY();
            void SetScreenX(int x);
            int GetScreenX();
            void SetScreenY(int y);
            int GetScreenY();
            void SetSrcFilter(ref object filter);
            object GetSrcFilter();
            object GetDataTransfer();
        }

        [SuppressUnmanagedCodeSecurity, ComVisible(true), Guid("3050f814-98b5-11cf-bb82-00aa00bdce0b"),
        InterfaceTypeAttribute(System.Runtime.InteropServices.ComInterfaceType.InterfaceIsDual)]
        public interface IHTMLEventObj4
        {
            int GetWheelDelta();
        };

        [SuppressUnmanagedCodeSecurity, ComVisible(true), Guid("3050F21F-98B5-11CF-BB82-00AA00BDCE0B"),
        InterfaceTypeAttribute(ComInterfaceType.InterfaceIsDual)]
        public interface IHTMLElementCollection
        {
            string toString();
            void SetLength(int p);
            int GetLength();
            [return: MarshalAs(UnmanagedType.Interface)]
            object Get_newEnum();
            [return: MarshalAs(UnmanagedType.IDispatch)]
            object Item(object idOrName, object index);
            [return: MarshalAs(UnmanagedType.Interface)]
            object Tags(object tagName);
        }

        [SuppressUnmanagedCodeSecurity, ComVisible(true), Guid("3050F1FF-98B5-11CF-BB82-00AA00BDCE0B"),
        InterfaceTypeAttribute(ComInterfaceType.InterfaceIsDual)]
        public interface IHTMLElement
        {
            void SetAttribute(string attributeName, object attributeValue, int lFlags);
            object GetAttribute(string attributeName, int lFlags);
            bool RemoveAttribute(string strAttributeName, int lFlags);
            void SetClassName(string p);
            string GetClassName();
            void SetId(string p);
            string GetId();
            string GetTagName();
            IHTMLElement GetParentElement();
            IHTMLStyle GetStyle();
            void SetOnhelp(Object p);
            Object GetOnhelp();
            void SetOnclick(Object p);
            Object GetOnclick();
            void SetOndblclick(Object p);
            Object GetOndblclick();
            void SetOnkeydown(Object p);
            Object GetOnkeydown();
            void SetOnkeyup(Object p);
            Object GetOnkeyup();
            void SetOnkeypress(Object p);
            Object GetOnkeypress();
            void SetOnmouseout(Object p);
            Object GetOnmouseout();
            void SetOnmouseover(Object p);
            Object GetOnmouseover();
            void SetOnmousemove(Object p);
            Object GetOnmousemove();
            void SetOnmousedown(Object p);
            Object GetOnmousedown();
            void SetOnmouseup(Object p);
            Object GetOnmouseup();
            [return: MarshalAs(UnmanagedType.Interface)]
            IHTMLDocument2 GetDocument();
            void SetTitle(string p);
            string GetTitle();
            void SetLanguage(string p);
            string GetLanguage();
            void SetOnselectstart(Object p);
            Object GetOnselectstart();
            void ScrollIntoView(object varargStart);
            bool Contains(IHTMLElement pChild);
            int GetSourceIndex();
            Object GetRecordNumber();
            void SetLang(string p);
            string GetLang();
            int GetOffsetLeft();
            int GetOffsetTop();
            int GetOffsetWidth();
            int GetOffsetHeight();
            [return: MarshalAs(UnmanagedType.Interface)]
            NativeCOM.IHTMLElement GetOffsetParent();
            void SetInnerHTML(string p);
            string GetInnerHTML();
            void SetInnerText(string p);
            string GetInnerText();
            void SetOuterHTML(string p);
            string GetOuterHTML();
            void SetOuterText(string p);
            string GetOuterText();
            void InsertAdjacentHTML(string @where,
                string html);
            void InsertAdjacentText(string @where,
                string text);
            IHTMLElement GetParentTextEdit();
            bool GetIsTextEdit();
            void Click();
            [return: MarshalAs(UnmanagedType.Interface)]
            object GetFilters();
            void SetOndragstart(Object p);
            Object GetOndragstart();
            string toString();
            void SetOnbeforeupdate(Object p);
            Object GetOnbeforeupdate();
            void SetOnafterupdate(Object p);
            Object GetOnafterupdate();
            void SetOnerrorupdate(Object p);
            Object GetOnerrorupdate();
            void SetOnrowexit(Object p);
            Object GetOnrowexit();
            void SetOnrowenter(Object p);
            Object GetOnrowenter();
            void SetOndatasetchanged(Object p);
            Object GetOndatasetchanged();
            void SetOndataavailable(Object p);
            Object GetOndataavailable();
            void SetOndatasetcomplete(Object p);
            Object GetOndatasetcomplete();
            void SetOnfilterchange(Object p);
            Object GetOnfilterchange();
            [return: MarshalAs(UnmanagedType.IDispatch)]
            object GetChildren();
            [return: MarshalAs(UnmanagedType.IDispatch)]
            object GetAll();
        }


        [SuppressUnmanagedCodeSecurity, ComVisible(true), Guid("3050f434-98b5-11cf-bb82-00aa00bdce0b"),
        InterfaceTypeAttribute(ComInterfaceType.InterfaceIsDual)]
        public interface IHTMLElement2
        {
            string ScopeName();
            void SetCapture(bool containerCapture);
            void ReleaseCapture();
            void SetOnLoseCapture(object v);
            object GetOnLoseCapture();
            string GetComponentFromPoint(int x, int y);
            void DoScroll(object component);
            void SetOnScroll(object v);
            object GetOnScroll();
            void SetOnDrag(object v);
            object GetOnDrag();
            void SetOnDragEnd(object v);
            object GetOnDragEnd();
            void SetOnDragEnter(object v);
            object GetOnDragEnter();
            void SetOnDragOver(object v);
            object GetOnDragOver();
            void SetOnDragleave(object v);
            object GetOnDragLeave();
            void SetOnDrop(object v);
            object GetOnDrop();
            void SetOnBeforeCut(object v);
            object GetOnBeforeCut();
            void SetOnCut(object v);
            object GetOnCut();
            void SetOnBeforeCopy(object v);
            object GetOnBeforeCopy();
            void SetOnCopy(object v);
            object GetOnCopy(object p);
            void SetOnBeforePaste(object v);
            object GetOnBeforePaste(object p);
            void SetOnPaste(object v);
            object GetOnPaste(object p);
            object GetCurrentStyle();
            void SetOnPropertyChange(object v);
            object GetOnPropertyChange(object p);
            object GetClientRects();
            object GetBoundingClientRect();
            void SetExpression(string propName, string expression, string language);
            object GetExpression(string propName);
            bool RemoveExpression(string propName);
            void SetTabIndex(int v);
            short GetTabIndex();
            void Focus();
            void SetAccessKey(string v);
            string GetAccessKey();
            void SetOnBlur(object v);
            object GetOnBlur();
            void SetOnFocus(object v);
            object GetOnFocus();
            void SetOnResize(object v);
            object GetOnResize();
            void Blur();
            void AddFilter(object pUnk);
            void RemoveFilter(object pUnk);
            int ClientHeight();
            int ClientWidth();
            int ClientTop();
            int ClientLeft();
            bool AttachEvent(string ev, [In, MarshalAs(UnmanagedType.IDispatch)] object pdisp);
            void DetachEvent(string ev, [In, MarshalAs(UnmanagedType.IDispatch)] object pdisp);
            object ReadyState();
            void SetOnReadyStateChange(object v);
            object GetOnReadyStateChange();
            void SetOnRowsDelete(object v);
            object GetOnRowsDelete();
            void SetOnRowsInserted(object v);
            object GetOnRowsInserted();
            void SetOnCellChange(object v);
            object GetOnCellChange();
            void SetDir(string v);
            string GetDir();
            object CreateControlRange();
            int GetScrollHeight();
            int GetScrollWidth();
            void SetScrollTop(int v);
            int GetScrollTop();
            void SetScrollLeft(int v);
            int GetScrollLeft();
            void ClearAttributes();
            void MergeAttributes(object mergeThis);
            void SetOnContextMenu(object v);
            object GetOnContextMenu();
            [return: MarshalAs(UnmanagedType.Interface)]
            NativeCOM.IHTMLElement
                InsertAdjacentElement(string @where,
                [In, MarshalAs(UnmanagedType.Interface)] NativeCOM.IHTMLElement insertedElement);
            [return: MarshalAs(UnmanagedType.Interface)]
            NativeCOM.IHTMLElement
                applyElement([In, MarshalAs(UnmanagedType.Interface)] NativeCOM.IHTMLElement apply,
                string @where);
            string GetAdjacentText(string @where);
            string ReplaceAdjacentText(string @where, string newText);
            bool CanHaveChildren();
            int AddBehavior(string url, ref object oFactory);
            bool RemoveBehavior(int cookie);
            object GetRuntimeStyle();
            object GetBehaviorUrns();
            void SetTagUrn(string v);
            string GetTagUrn();
            void SetOnBeforeEditFocus(object v);
            object GetOnBeforeEditFocus();
            int GetReadyStateValue();
            [return: MarshalAs(UnmanagedType.Interface)]
            NativeCOM.IHTMLElementCollection GetElementsByTagName(string v);
        }

        [SuppressUnmanagedCodeSecurity, ComVisible(true), Guid("3050f673-98b5-11cf-bb82-00aa00bdce0b"),
        InterfaceTypeAttribute(ComInterfaceType.InterfaceIsDual)]
        public interface IHTMLElement3
        {
            void MergeAttributes(object mergeThis, object pvarFlags);
            bool IsMultiLine();
            bool CanHaveHTML();
            void SetOnLayoutComplete(object v);
            object GetOnLayoutComplete();
            void SetOnPage(object v);
            object GetOnPage();
            void SetInflateBlock(bool v);
            bool GetInflateBlock();
            void SetOnBeforeDeactivate(object v);
            object GetOnBeforeDeactivate();
            void SetActive();
            void SetContentEditable(string v);
            string GetContentEditable();
            bool IsContentEditable();
            void SetHideFocus(bool v);
            bool GetHideFocus();
            void SetDisabled(bool v);
            bool GetDisabled();
            bool IsDisabled();
            void SetOnMove(object v);
            object GetOnMove();
            void SetOnControlSelect(object v);
            object GetOnControlSelect();
            bool FireEvent(string bstrEventName, object pvarEventObject);
            void SetOnResizeStart(object v);
            object GetOnResizeStart();
            void SetOnResizeEnd(object v);
            object GetOnResizeEnd();
            void SetOnMoveStart(object v);
            object GetOnMoveStart();
            void SetOnMoveEnd(object v);
            object GetOnMoveEnd();
            void SetOnMouseEnter(object v);
            object GetOnMouseEnter();
            void SetOnMouseLeave(object v);
            object GetOnMouseLeave();
            void SetOnActivate(object v);
            object GetOnActivate();
            void SetOnDeactivate(object v);
            object GetOnDeactivate();
            bool DragDrop();
            int GlyphMode();
        }

        [SuppressUnmanagedCodeSecurity, ComVisible(true), Guid("3050f5da-98b5-11cf-bb82-00aa00bdce0b"),
        InterfaceTypeAttribute(ComInterfaceType.InterfaceIsDual)]
        public interface IHTMLDOMNode
        {
            long GetNodeType();
            IHTMLDOMNode GetParentNode();
            bool HasChildNodes();
            object GetChildNodes();
            object GetAttributes();
            IHTMLDOMNode InsertBefore(IHTMLDOMNode newChild, object refChild);
            IHTMLDOMNode RemoveChild(IHTMLDOMNode oldChild);
            IHTMLDOMNode ReplaceChild(IHTMLDOMNode newChild, IHTMLDOMNode oldChild);
            IHTMLDOMNode CloneNode(bool fDeep);
            IHTMLDOMNode RemoveNode(bool fDeep);
            IHTMLDOMNode SwapNode(IHTMLDOMNode otherNode);
            IHTMLDOMNode ReplaceNode(IHTMLDOMNode replacement);
            IHTMLDOMNode AppendChild(IHTMLDOMNode newChild);
            string NodeName();
            void SetNodeValue(object v);
            object GetNodeValue();
            IHTMLDOMNode FirstChild();
            IHTMLDOMNode LastChild();
            IHTMLDOMNode PreviousSibling();
            IHTMLDOMNode NextSibling();
        };

        [ComImport(), Guid("3050f60f-98b5-11cf-bb82-00aa00bdce0b"),
        InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIDispatch),
        TypeLibType(TypeLibTypeFlags.FHidden)]
        public interface DHTMLElementEvents2
        {
            [DispId(-2147418102)]
            bool onhelp(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-600)]
            bool onclick(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-601)]
            bool ondblclick(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-603)]
            bool onkeypress(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-602)]
            void onkeydown(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-604)]
            void onkeyup(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418103)]
            void onmouseout(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418104)]
            void onmouseover(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-606)]
            void onmousemove(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-605)]
            void onmousedown(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-607)]
            void onmouseup(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418100)]
            bool onselectstart(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418095)]
            void onfilterchange(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418101)]
            bool ondragstart(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418108)]
            bool onbeforeupdate(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418107)]
            void onafterupdate(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418099)]
            bool onerrorupdate(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418106)]
            bool onrowexit(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418105)]
            void onrowenter(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418098)]
            void ondatasetchanged(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418097)]
            void ondataavailable(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418096)]
            void ondatasetcomplete(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418094)]
            void onlosecapture(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418093)]
            void onpropertychange(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1014)]
            void onscroll(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418111)]
            void onfocus(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418112)]
            void onblur(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1016)]
            void onresize(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418092)]
            bool ondrag(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418091)]
            void ondragend(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418090)]
            bool ondragenter(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418089)]
            bool ondragover(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418088)]
            void ondragleave(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418087)]
            bool ondrop(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418083)]
            bool onbeforecut(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418086)]
            bool oncut(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418082)]
            bool onbeforecopy(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418085)]
            bool oncopy(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418081)]
            bool onbeforepaste(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418084)]
            bool onpaste(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1023)]
            bool oncontextmenu(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418080)]
            void onrowsdelete(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418079)]
            void onrowsinserted(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418078)]
            void oncellchange(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-609)]
            void onreadystatechange(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1030)]
            void onlayoutcomplete(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1031)]
            void onpage(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1042)]
            void onmouseenter(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1043)]
            void onmouseleave(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1044)]
            void onactivate(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1045)]
            void ondeactivate(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1034)]
            bool onbeforedeactivate(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1047)]
            bool onbeforeactivate(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1048)]
            void onfocusin(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1049)]
            void onfocusout(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1035)]
            void onmove(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1036)]
            bool oncontrolselect(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1038)]
            bool onmovestart(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1039)]
            void onmoveend(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1040)]
            bool onresizestart(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1041)]
            void onresizeend(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1033)]
            bool onmousewheel(NativeCOM.IHTMLEventObj evtObj);
        }

        [ComImport(), Guid("3050f610-98b5-11cf-bb82-00aa00bdce0b"),
        InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIDispatch),
        TypeLibType(TypeLibTypeFlags.FHidden)]
        public interface DHTMLAnchorEvents2
        {
            [DispId(-2147418102)]
            bool onhelp(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-600)]
            bool onclick(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-601)]
            bool ondblclick(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-603)]
            bool onkeypress(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-602)]
            void onkeydown(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-604)]
            void onkeyup(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418103)]
            void onmouseout(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418104)]
            void onmouseover(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-606)]
            void onmousemove(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-605)]
            void onmousedown(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-607)]
            void onmouseup(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418100)]
            bool onselectstart(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418095)]
            void onfilterchange(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418101)]
            bool ondragstart(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418108)]
            bool onbeforeupdate(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418107)]
            void onafterupdate(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418099)]
            bool onerrorupdate(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418106)]
            bool onrowexit(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418105)]
            void onrowenter(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418098)]
            void ondatasetchanged(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418097)]
            void ondataavailable(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418096)]
            void ondatasetcomplete(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418094)]
            void onlosecapture(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418093)]
            void onpropertychange(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1014)]
            void onscroll(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418111)]
            void onfocus(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418112)]
            void onblur(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1016)]
            void onresize(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418092)]
            bool ondrag(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418091)]
            void ondragend(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418090)]
            bool ondragenter(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418089)]
            bool ondragover(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418088)]
            void ondragleave(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418087)]
            bool ondrop(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418083)]
            bool onbeforecut(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418086)]
            bool oncut(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418082)]
            bool onbeforecopy(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418085)]
            bool oncopy(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418081)]
            bool onbeforepaste(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418084)]
            bool onpaste(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1023)]
            bool oncontextmenu(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418080)]
            void onrowsdelete(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418079)]
            void onrowsinserted(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418078)]
            void oncellchange(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-609)]
            void onreadystatechange(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1030)]
            void onlayoutcomplete(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1031)]
            void onpage(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1042)]
            void onmouseenter(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1043)]
            void onmouseleave(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1044)]
            void onactivate(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1045)]
            void ondeactivate(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1034)]
            bool onbeforedeactivate(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1047)]
            bool onbeforeactivate(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1048)]
            void onfocusin(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1049)]
            void onfocusout(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1035)]
            void onmove(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1036)]
            bool oncontrolselect(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1038)]
            bool onmovestart(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1039)]
            void onmoveend(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1040)]
            bool onresizestart(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1041)]
            void onresizeend(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1033)]
            bool onmousewheel(NativeCOM.IHTMLEventObj evtObj);
        }

        [ComImport(), Guid("3050f611-98b5-11cf-bb82-00aa00bdce0b"),
        InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIDispatch),
        TypeLibType(TypeLibTypeFlags.FHidden)]
        public interface DHTMLAreaEvents2
        {
            [DispId(-2147418102)]
            bool onhelp(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-600)]
            bool onclick(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-601)]
            bool ondblclick(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-603)]
            bool onkeypress(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-602)]
            void onkeydown(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-604)]
            void onkeyup(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418103)]
            void onmouseout(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418104)]
            void onmouseover(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-606)]
            void onmousemove(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-605)]
            void onmousedown(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-607)]
            void onmouseup(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418100)]
            bool onselectstart(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418095)]
            void onfilterchange(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418101)]
            bool ondragstart(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418108)]
            bool onbeforeupdate(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418107)]
            void onafterupdate(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418099)]
            bool onerrorupdate(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418106)]
            bool onrowexit(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418105)]
            void onrowenter(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418098)]
            void ondatasetchanged(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418097)]
            void ondataavailable(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418096)]
            void ondatasetcomplete(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418094)]
            void onlosecapture(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418093)]
            void onpropertychange(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1014)]
            void onscroll(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418111)]
            void onfocus(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418112)]
            void onblur(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1016)]
            void onresize(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418092)]
            bool ondrag(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418091)]
            void ondragend(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418090)]
            bool ondragenter(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418089)]
            bool ondragover(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418088)]
            void ondragleave(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418087)]
            bool ondrop(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418083)]
            bool onbeforecut(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418086)]
            bool oncut(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418082)]
            bool onbeforecopy(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418085)]
            bool oncopy(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418081)]
            bool onbeforepaste(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418084)]
            bool onpaste(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1023)]
            bool oncontextmenu(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418080)]
            void onrowsdelete(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418079)]
            void onrowsinserted(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418078)]
            void oncellchange(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-609)]
            void onreadystatechange(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1030)]
            void onlayoutcomplete(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1031)]
            void onpage(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1042)]
            void onmouseenter(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1043)]
            void onmouseleave(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1044)]
            void onactivate(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1045)]
            void ondeactivate(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1034)]
            bool onbeforedeactivate(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1047)]
            bool onbeforeactivate(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1048)]
            void onfocusin(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1049)]
            void onfocusout(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1035)]
            void onmove(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1036)]
            bool oncontrolselect(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1038)]
            bool onmovestart(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1039)]
            void onmoveend(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1040)]
            bool onresizestart(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1041)]
            void onresizeend(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1033)]
            bool onmousewheel(NativeCOM.IHTMLEventObj evtObj);
        }

        [ComImport(), Guid("3050f617-98b5-11cf-bb82-00aa00bdce0b"),
        InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIDispatch),
        TypeLibType(TypeLibTypeFlags.FHidden)]
        public interface DHTMLButtonElementEvents2
        {
            [DispId(-2147418102)]
            bool onhelp(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-600)]
            bool onclick(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-601)]
            bool ondblclick(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-603)]
            bool onkeypress(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-602)]
            void onkeydown(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-604)]
            void onkeyup(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418103)]
            void onmouseout(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418104)]
            void onmouseover(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-606)]
            void onmousemove(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-605)]
            void onmousedown(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-607)]
            void onmouseup(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418100)]
            bool onselectstart(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418095)]
            void onfilterchange(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418101)]
            bool ondragstart(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418108)]
            bool onbeforeupdate(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418107)]
            void onafterupdate(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418099)]
            bool onerrorupdate(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418106)]
            bool onrowexit(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418105)]
            void onrowenter(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418098)]
            void ondatasetchanged(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418097)]
            void ondataavailable(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418096)]
            void ondatasetcomplete(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418094)]
            void onlosecapture(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418093)]
            void onpropertychange(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1014)]
            void onscroll(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418111)]
            void onfocus(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418112)]
            void onblur(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1016)]
            void onresize(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418092)]
            bool ondrag(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418091)]
            void ondragend(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418090)]
            bool ondragenter(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418089)]
            bool ondragover(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418088)]
            void ondragleave(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418087)]
            bool ondrop(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418083)]
            bool onbeforecut(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418086)]
            bool oncut(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418082)]
            bool onbeforecopy(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418085)]
            bool oncopy(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418081)]
            bool onbeforepaste(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418084)]
            bool onpaste(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1023)]
            bool oncontextmenu(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418080)]
            void onrowsdelete(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418079)]
            void onrowsinserted(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418078)]
            void oncellchange(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-609)]
            void onreadystatechange(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1030)]
            void onlayoutcomplete(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1031)]
            void onpage(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1042)]
            void onmouseenter(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1043)]
            void onmouseleave(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1044)]
            void onactivate(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1045)]
            void ondeactivate(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1034)]
            bool onbeforedeactivate(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1047)]
            bool onbeforeactivate(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1048)]
            void onfocusin(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1049)]
            void onfocusout(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1035)]
            void onmove(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1036)]
            bool oncontrolselect(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1038)]
            bool onmovestart(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1039)]
            void onmoveend(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1040)]
            bool onresizestart(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1041)]
            void onresizeend(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1033)]
            bool onmousewheel(NativeCOM.IHTMLEventObj evtObj);
        }

        [ComImport(), Guid("3050f612-98b5-11cf-bb82-00aa00bdce0b"),
        InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIDispatch),
        TypeLibType(TypeLibTypeFlags.FHidden)]
        public interface DHTMLControlElementEvents2
        {
            [DispId(-2147418102)]
            bool onhelp(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-600)]
            bool onclick(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-601)]
            bool ondblclick(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-603)]
            bool onkeypress(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-602)]
            void onkeydown(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-604)]
            void onkeyup(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418103)]
            void onmouseout(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418104)]
            void onmouseover(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-606)]
            void onmousemove(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-605)]
            void onmousedown(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-607)]
            void onmouseup(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418100)]
            bool onselectstart(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418095)]
            void onfilterchange(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418101)]
            bool ondragstart(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418108)]
            bool onbeforeupdate(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418107)]
            void onafterupdate(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418099)]
            bool onerrorupdate(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418106)]
            bool onrowexit(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418105)]
            void onrowenter(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418098)]
            void ondatasetchanged(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418097)]
            void ondataavailable(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418096)]
            void ondatasetcomplete(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418094)]
            void onlosecapture(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418093)]
            void onpropertychange(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1014)]
            void onscroll(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418111)]
            void onfocus(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418112)]
            void onblur(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1016)]
            void onresize(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418092)]
            bool ondrag(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418091)]
            void ondragend(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418090)]
            bool ondragenter(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418089)]
            bool ondragover(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418088)]
            void ondragleave(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418087)]
            bool ondrop(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418083)]
            bool onbeforecut(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418086)]
            bool oncut(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418082)]
            bool onbeforecopy(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418085)]
            bool oncopy(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418081)]
            bool onbeforepaste(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418084)]
            bool onpaste(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1023)]
            bool oncontextmenu(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418080)]
            void onrowsdelete(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418079)]
            void onrowsinserted(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418078)]
            void oncellchange(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-609)]
            void onreadystatechange(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1030)]
            void onlayoutcomplete(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1031)]
            void onpage(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1042)]
            void onmouseenter(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1043)]
            void onmouseleave(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1044)]
            void onactivate(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1045)]
            void ondeactivate(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1034)]
            bool onbeforedeactivate(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1047)]
            bool onbeforeactivate(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1048)]
            void onfocusin(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1049)]
            void onfocusout(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1035)]
            void onmove(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1036)]
            bool oncontrolselect(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1038)]
            bool onmovestart(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1039)]
            void onmoveend(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1040)]
            bool onresizestart(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1041)]
            void onresizeend(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1033)]
            bool onmousewheel(NativeCOM.IHTMLEventObj evtObj);
        }

        [ComImport(), Guid("3050f614-98b5-11cf-bb82-00aa00bdce0b"),
        InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIDispatch),
        TypeLibType(TypeLibTypeFlags.FHidden)]
        public interface DHTMLFormElementEvents2
        {
            [DispId(-2147418102)]
            bool onhelp(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-600)]
            bool onclick(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-601)]
            bool ondblclick(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-603)]
            bool onkeypress(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-602)]
            void onkeydown(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-604)]
            void onkeyup(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418103)]
            void onmouseout(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418104)]
            void onmouseover(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-606)]
            void onmousemove(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-605)]
            void onmousedown(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-607)]
            void onmouseup(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418100)]
            bool onselectstart(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418095)]
            void onfilterchange(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418101)]
            bool ondragstart(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418108)]
            bool onbeforeupdate(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418107)]
            void onafterupdate(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418099)]
            bool onerrorupdate(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418106)]
            bool onrowexit(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418105)]
            void onrowenter(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418098)]
            void ondatasetchanged(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418097)]
            void ondataavailable(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418096)]
            void ondatasetcomplete(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418094)]
            void onlosecapture(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418093)]
            void onpropertychange(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1014)]
            void onscroll(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418111)]
            void onfocus(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418112)]
            void onblur(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1016)]
            void onresize(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418092)]
            bool ondrag(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418091)]
            void ondragend(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418090)]
            bool ondragenter(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418089)]
            bool ondragover(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418088)]
            void ondragleave(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418087)]
            bool ondrop(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418083)]
            bool onbeforecut(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418086)]
            bool oncut(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418082)]
            bool onbeforecopy(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418085)]
            bool oncopy(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418081)]
            bool onbeforepaste(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418084)]
            bool onpaste(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1023)]
            bool oncontextmenu(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418080)]
            void onrowsdelete(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418079)]
            void onrowsinserted(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418078)]
            void oncellchange(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-609)]
            void onreadystatechange(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1030)]
            void onlayoutcomplete(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1031)]
            void onpage(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1042)]
            void onmouseenter(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1043)]
            void onmouseleave(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1044)]
            void onactivate(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1045)]
            void ondeactivate(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1034)]
            bool onbeforedeactivate(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1047)]
            bool onbeforeactivate(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1048)]
            void onfocusin(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1049)]
            void onfocusout(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1035)]
            void onmove(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1036)]
            bool oncontrolselect(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1038)]
            bool onmovestart(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1039)]
            void onmoveend(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1040)]
            bool onresizestart(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1041)]
            void onresizeend(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1033)]
            bool onmousewheel(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1007)]
            bool onsubmit(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1015)]
            bool onreset(NativeCOM.IHTMLEventObj evtObj);
        }

        [ComImport(), Guid("3050f7ff-98b5-11cf-bb82-00aa00bdce0b"),
        InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIDispatch),
        TypeLibType(TypeLibTypeFlags.FHidden)]
        public interface DHTMLFrameSiteEvents2
        {
            [DispId(-2147418102)]
            bool onhelp(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-600)]
            bool onclick(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-601)]
            bool ondblclick(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-603)]
            bool onkeypress(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-602)]
            void onkeydown(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-604)]
            void onkeyup(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418103)]
            void onmouseout(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418104)]
            void onmouseover(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-606)]
            void onmousemove(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-605)]
            void onmousedown(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-607)]
            void onmouseup(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418100)]
            bool onselectstart(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418095)]
            void onfilterchange(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418101)]
            bool ondragstart(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418108)]
            bool onbeforeupdate(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418107)]
            void onafterupdate(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418099)]
            bool onerrorupdate(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418106)]
            bool onrowexit(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418105)]
            void onrowenter(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418098)]
            void ondatasetchanged(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418097)]
            void ondataavailable(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418096)]
            void ondatasetcomplete(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418094)]
            void onlosecapture(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418093)]
            void onpropertychange(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1014)]
            void onscroll(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418111)]
            void onfocus(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418112)]
            void onblur(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1016)]
            void onresize(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418092)]
            bool ondrag(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418091)]
            void ondragend(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418090)]
            bool ondragenter(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418089)]
            bool ondragover(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418088)]
            void ondragleave(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418087)]
            bool ondrop(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418083)]
            bool onbeforecut(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418086)]
            bool oncut(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418082)]
            bool onbeforecopy(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418085)]
            bool oncopy(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418081)]
            bool onbeforepaste(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418084)]
            bool onpaste(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1023)]
            bool oncontextmenu(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418080)]
            void onrowsdelete(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418079)]
            void onrowsinserted(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418078)]
            void oncellchange(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-609)]
            void onreadystatechange(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1030)]
            void onlayoutcomplete(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1031)]
            void onpage(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1042)]
            void onmouseenter(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1043)]
            void onmouseleave(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1044)]
            void onactivate(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1045)]
            void ondeactivate(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1034)]
            bool onbeforedeactivate(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1047)]
            bool onbeforeactivate(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1048)]
            void onfocusin(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1049)]
            void onfocusout(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1035)]
            void onmove(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1036)]
            bool oncontrolselect(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1038)]
            bool onmovestart(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1039)]
            void onmoveend(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1040)]
            bool onresizestart(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1041)]
            void onresizeend(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1033)]
            bool onmousewheel(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1003)]
            void onload(NativeCOM.IHTMLEventObj evtObj);
        }

        [ComImport(), Guid("3050f616-98b5-11cf-bb82-00aa00bdce0b"),
        InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIDispatch),
        TypeLibType(TypeLibTypeFlags.FHidden)]
        public interface DHTMLImgEvents2
        {
            [DispId(-2147418102)]
            bool onhelp(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-600)]
            bool onclick(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-601)]
            bool ondblclick(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-603)]
            bool onkeypress(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-602)]
            void onkeydown(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-604)]
            void onkeyup(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418103)]
            void onmouseout(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418104)]
            void onmouseover(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-606)]
            void onmousemove(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-605)]
            void onmousedown(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-607)]
            void onmouseup(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418100)]
            bool onselectstart(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418095)]
            void onfilterchange(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418101)]
            bool ondragstart(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418108)]
            bool onbeforeupdate(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418107)]
            void onafterupdate(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418099)]
            bool onerrorupdate(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418106)]
            bool onrowexit(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418105)]
            void onrowenter(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418098)]
            void ondatasetchanged(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418097)]
            void ondataavailable(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418096)]
            void ondatasetcomplete(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418094)]
            void onlosecapture(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418093)]
            void onpropertychange(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1014)]
            void onscroll(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418111)]
            void onfocus(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418112)]
            void onblur(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1016)]
            void onresize(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418092)]
            bool ondrag(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418091)]
            void ondragend(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418090)]
            bool ondragenter(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418089)]
            bool ondragover(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418088)]
            void ondragleave(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418087)]
            bool ondrop(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418083)]
            bool onbeforecut(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418086)]
            bool oncut(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418082)]
            bool onbeforecopy(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418085)]
            bool oncopy(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418081)]
            bool onbeforepaste(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418084)]
            bool onpaste(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1023)]
            bool oncontextmenu(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418080)]
            void onrowsdelete(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418079)]
            void onrowsinserted(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418078)]
            void oncellchange(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-609)]
            void onreadystatechange(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1030)]
            void onlayoutcomplete(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1031)]
            void onpage(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1042)]
            void onmouseenter(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1043)]
            void onmouseleave(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1044)]
            void onactivate(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1045)]
            void ondeactivate(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1034)]
            bool onbeforedeactivate(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1047)]
            bool onbeforeactivate(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1048)]
            void onfocusin(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1049)]
            void onfocusout(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1035)]
            void onmove(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1036)]
            bool oncontrolselect(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1038)]
            bool onmovestart(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1039)]
            void onmoveend(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1040)]
            bool onresizestart(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1041)]
            void onresizeend(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1033)]
            bool onmousewheel(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1003)]
            void onload(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1002)]
            void onerror(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1000)]
            void onabort(NativeCOM.IHTMLEventObj evtObj);
        }

        [ComImport(), Guid("3050f61a-98b5-11cf-bb82-00aa00bdce0b"),
        InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIDispatch),
        TypeLibType(TypeLibTypeFlags.FHidden)]
        public interface DHTMLInputFileElementEvents2
        {
            [DispId(-2147418102)]
            bool onhelp(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-600)]
            bool onclick(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-601)]
            bool ondblclick(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-603)]
            bool onkeypress(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-602)]
            void onkeydown(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-604)]
            void onkeyup(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418103)]
            void onmouseout(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418104)]
            void onmouseover(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-606)]
            void onmousemove(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-605)]
            void onmousedown(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-607)]
            void onmouseup(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418100)]
            bool onselectstart(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418095)]
            void onfilterchange(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418101)]
            bool ondragstart(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418108)]
            bool onbeforeupdate(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418107)]
            void onafterupdate(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418099)]
            bool onerrorupdate(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418106)]
            bool onrowexit(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418105)]
            void onrowenter(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418098)]
            void ondatasetchanged(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418097)]
            void ondataavailable(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418096)]
            void ondatasetcomplete(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418094)]
            void onlosecapture(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418093)]
            void onpropertychange(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1014)]
            void onscroll(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418111)]
            void onfocus(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418112)]
            void onblur(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1016)]
            void onresize(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418092)]
            bool ondrag(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418091)]
            void ondragend(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418090)]
            bool ondragenter(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418089)]
            bool ondragover(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418088)]
            void ondragleave(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418087)]
            bool ondrop(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418083)]
            bool onbeforecut(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418086)]
            bool oncut(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418082)]
            bool onbeforecopy(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418085)]
            bool oncopy(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418081)]
            bool onbeforepaste(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418084)]
            bool onpaste(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1023)]
            bool oncontextmenu(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418080)]
            void onrowsdelete(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418079)]
            void onrowsinserted(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418078)]
            void oncellchange(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-609)]
            void onreadystatechange(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1030)]
            void onlayoutcomplete(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1031)]
            void onpage(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1042)]
            void onmouseenter(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1043)]
            void onmouseleave(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1044)]
            void onactivate(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1045)]
            void ondeactivate(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1034)]
            bool onbeforedeactivate(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1047)]
            bool onbeforeactivate(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1048)]
            void onfocusin(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1049)]
            void onfocusout(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1035)]
            void onmove(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1036)]
            bool oncontrolselect(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1038)]
            bool onmovestart(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1039)]
            void onmoveend(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1040)]
            bool onresizestart(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1041)]
            void onresizeend(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1033)]
            bool onmousewheel(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147412082)]
            bool onchange(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147412102)]
            void onselect(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1003)]
            void onload(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1002)]
            void onerror(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1000)]
            void onabort(NativeCOM.IHTMLEventObj evtObj);
        }

        [ComImport(), Guid("3050f61b-98b5-11cf-bb82-00aa00bdce0b"),
        InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIDispatch),
        TypeLibType(TypeLibTypeFlags.FHidden)]
        public interface DHTMLInputImageEvents2
        {
            [DispId(-2147418102)]
            bool onhelp(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-600)]
            bool onclick(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-601)]
            bool ondblclick(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-603)]
            bool onkeypress(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-602)]
            void onkeydown(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-604)]
            void onkeyup(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418103)]
            void onmouseout(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418104)]
            void onmouseover(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-606)]
            void onmousemove(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-605)]
            void onmousedown(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-607)]
            void onmouseup(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418100)]
            bool onselectstart(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418095)]
            void onfilterchange(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418101)]
            bool ondragstart(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418108)]
            bool onbeforeupdate(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418107)]
            void onafterupdate(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418099)]
            bool onerrorupdate(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418106)]
            bool onrowexit(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418105)]
            void onrowenter(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418098)]
            void ondatasetchanged(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418097)]
            void ondataavailable(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418096)]
            void ondatasetcomplete(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418094)]
            void onlosecapture(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418093)]
            void onpropertychange(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1014)]
            void onscroll(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418111)]
            void onfocus(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418112)]
            void onblur(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1016)]
            void onresize(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418092)]
            bool ondrag(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418091)]
            void ondragend(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418090)]
            bool ondragenter(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418089)]
            bool ondragover(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418088)]
            void ondragleave(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418087)]
            bool ondrop(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418083)]
            bool onbeforecut(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418086)]
            bool oncut(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418082)]
            bool onbeforecopy(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418085)]
            bool oncopy(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418081)]
            bool onbeforepaste(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418084)]
            bool onpaste(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1023)]
            bool oncontextmenu(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418080)]
            void onrowsdelete(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418079)]
            void onrowsinserted(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418078)]
            void oncellchange(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-609)]
            void onreadystatechange(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1030)]
            void onlayoutcomplete(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1031)]
            void onpage(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1042)]
            void onmouseenter(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1043)]
            void onmouseleave(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1044)]
            void onactivate(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1045)]
            void ondeactivate(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1034)]
            bool onbeforedeactivate(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1047)]
            bool onbeforeactivate(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1048)]
            void onfocusin(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1049)]
            void onfocusout(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1035)]
            void onmove(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1036)]
            bool oncontrolselect(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1038)]
            bool onmovestart(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1039)]
            void onmoveend(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1040)]
            bool onresizestart(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1041)]
            void onresizeend(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1033)]
            bool onmousewheel(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147412080)]
            void onload(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147412083)]
            void onerror(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147412084)]
            void onabort(NativeCOM.IHTMLEventObj evtObj);
        }

        [ComImport(), Guid("3050f618-98b5-11cf-bb82-00aa00bdce0b"),
        InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIDispatch),
        TypeLibType(TypeLibTypeFlags.FHidden)]
        public interface DHTMLInputTextElementEvents2
        {
            [DispId(-2147418102)]
            bool onhelp(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-600)]
            bool onclick(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-601)]
            bool ondblclick(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-603)]
            bool onkeypress(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-602)]
            void onkeydown(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-604)]
            void onkeyup(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418103)]
            void onmouseout(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418104)]
            void onmouseover(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-606)]
            void onmousemove(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-605)]
            void onmousedown(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-607)]
            void onmouseup(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418100)]
            bool onselectstart(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418095)]
            void onfilterchange(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418101)]
            bool ondragstart(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418108)]
            bool onbeforeupdate(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418107)]
            void onafterupdate(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418099)]
            bool onerrorupdate(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418106)]
            bool onrowexit(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418105)]
            void onrowenter(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418098)]
            void ondatasetchanged(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418097)]
            void ondataavailable(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418096)]
            void ondatasetcomplete(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418094)]
            void onlosecapture(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418093)]
            void onpropertychange(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1014)]
            void onscroll(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418111)]
            void onfocus(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418112)]
            void onblur(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1016)]
            void onresize(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418092)]
            bool ondrag(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418091)]
            void ondragend(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418090)]
            bool ondragenter(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418089)]
            bool ondragover(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418088)]
            void ondragleave(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418087)]
            bool ondrop(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418083)]
            bool onbeforecut(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418086)]
            bool oncut(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418082)]
            bool onbeforecopy(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418085)]
            bool oncopy(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418081)]
            bool onbeforepaste(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418084)]
            bool onpaste(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1023)]
            bool oncontextmenu(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418080)]
            void onrowsdelete(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418079)]
            void onrowsinserted(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418078)]
            void oncellchange(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-609)]
            void onreadystatechange(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1030)]
            void onlayoutcomplete(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1031)]
            void onpage(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1042)]
            void onmouseenter(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1043)]
            void onmouseleave(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1044)]
            void onactivate(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1045)]
            void ondeactivate(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1034)]
            bool onbeforedeactivate(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1047)]
            bool onbeforeactivate(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1048)]
            void onfocusin(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1049)]
            void onfocusout(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1035)]
            void onmove(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1036)]
            bool oncontrolselect(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1038)]
            bool onmovestart(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1039)]
            void onmoveend(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1040)]
            bool onresizestart(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1041)]
            void onresizeend(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1033)]
            bool onmousewheel(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1001)]
            bool onchange(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1006)]
            void onselect(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1003)]
            void onload(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1002)]
            void onerror(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1001)]
            void onabort(NativeCOM.IHTMLEventObj evtObj);
        }

        [ComImport(), Guid("3050f61c-98b5-11cf-bb82-00aa00bdce0b"),
        InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIDispatch),
        TypeLibType(TypeLibTypeFlags.FHidden)]
        public interface DHTMLLabelEvents2
        {
            [DispId(-2147418102)]
            bool onhelp(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-600)]
            bool onclick(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-601)]
            bool ondblclick(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-603)]
            bool onkeypress(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-602)]
            void onkeydown(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-604)]
            void onkeyup(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418103)]
            void onmouseout(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418104)]
            void onmouseover(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-606)]
            void onmousemove(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-605)]
            void onmousedown(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-607)]
            void onmouseup(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418100)]
            bool onselectstart(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418095)]
            void onfilterchange(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418101)]
            bool ondragstart(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418108)]
            bool onbeforeupdate(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418107)]
            void onafterupdate(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418099)]
            bool onerrorupdate(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418106)]
            bool onrowexit(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418105)]
            void onrowenter(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418098)]
            void ondatasetchanged(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418097)]
            void ondataavailable(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418096)]
            void ondatasetcomplete(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418094)]
            void onlosecapture(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418093)]
            void onpropertychange(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1014)]
            void onscroll(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418111)]
            void onfocus(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418112)]
            void onblur(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1016)]
            void onresize(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418092)]
            bool ondrag(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418091)]
            void ondragend(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418090)]
            bool ondragenter(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418089)]
            bool ondragover(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418088)]
            void ondragleave(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418087)]
            bool ondrop(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418083)]
            bool onbeforecut(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418086)]
            bool oncut(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418082)]
            bool onbeforecopy(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418085)]
            bool oncopy(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418081)]
            bool onbeforepaste(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418084)]
            bool onpaste(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1023)]
            bool oncontextmenu(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418080)]
            void onrowsdelete(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418079)]
            void onrowsinserted(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418078)]
            void oncellchange(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-609)]
            void onreadystatechange(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1030)]
            void onlayoutcomplete(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1031)]
            void onpage(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1042)]
            void onmouseenter(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1043)]
            void onmouseleave(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1044)]
            void onactivate(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1045)]
            void ondeactivate(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1034)]
            bool onbeforedeactivate(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1047)]
            bool onbeforeactivate(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1048)]
            void onfocusin(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1049)]
            void onfocusout(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1035)]
            void onmove(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1036)]
            bool oncontrolselect(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1038)]
            bool onmovestart(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1039)]
            void onmoveend(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1040)]
            bool onresizestart(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1041)]
            void onresizeend(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1033)]
            bool onmousewheel(NativeCOM.IHTMLEventObj evtObj);
        }

        [ComImport(), Guid("3050f61d-98b5-11cf-bb82-00aa00bdce0b"),
        InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIDispatch),
        TypeLibType(TypeLibTypeFlags.FHidden)]
        public interface DHTMLLinkElementEvents2
        {
            [DispId(-2147418102)]
            bool onhelp(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-600)]
            bool onclick(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-601)]
            bool ondblclick(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-603)]
            bool onkeypress(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-602)]
            void onkeydown(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-604)]
            void onkeyup(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418103)]
            void onmouseout(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418104)]
            void onmouseover(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-606)]
            void onmousemove(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-605)]
            void onmousedown(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-607)]
            void onmouseup(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418100)]
            bool onselectstart(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418095)]
            void onfilterchange(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418101)]
            bool ondragstart(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418108)]
            bool onbeforeupdate(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418107)]
            void onafterupdate(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418099)]
            bool onerrorupdate(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418106)]
            bool onrowexit(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418105)]
            void onrowenter(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418098)]
            void ondatasetchanged(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418097)]
            void ondataavailable(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418096)]
            void ondatasetcomplete(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418094)]
            void onlosecapture(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418093)]
            void onpropertychange(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1014)]
            void onscroll(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418111)]
            void onfocus(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418112)]
            void onblur(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1016)]
            void onresize(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418092)]
            bool ondrag(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418091)]
            void ondragend(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418090)]
            bool ondragenter(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418089)]
            bool ondragover(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418088)]
            void ondragleave(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418087)]
            bool ondrop(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418083)]
            bool onbeforecut(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418086)]
            bool oncut(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418082)]
            bool onbeforecopy(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418085)]
            bool oncopy(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418081)]
            bool onbeforepaste(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418084)]
            bool onpaste(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1023)]
            bool oncontextmenu(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418080)]
            void onrowsdelete(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418079)]
            void onrowsinserted(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418078)]
            void oncellchange(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-609)]
            void onreadystatechange(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1030)]
            void onlayoutcomplete(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1031)]
            void onpage(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1042)]
            void onmouseenter(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1043)]
            void onmouseleave(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1044)]
            void onactivate(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1045)]
            void ondeactivate(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1034)]
            bool onbeforedeactivate(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1047)]
            bool onbeforeactivate(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1048)]
            void onfocusin(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1049)]
            void onfocusout(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1035)]
            void onmove(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1036)]
            bool oncontrolselect(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1038)]
            bool onmovestart(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1039)]
            void onmoveend(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1040)]
            bool onresizestart(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1041)]
            void onresizeend(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1033)]
            bool onmousewheel(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147412080)]
            void onload(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147412083)]
            void onerror(NativeCOM.IHTMLEventObj evtObj);
        }

        [ComImport(), Guid("3050f61e-98b5-11cf-bb82-00aa00bdce0b"),
        InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIDispatch),
        TypeLibType(TypeLibTypeFlags.FHidden)]
        public interface DHTMLMapEvents2
        {
            [DispId(-2147418102)]
            bool onhelp(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-600)]
            bool onclick(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-601)]
            bool ondblclick(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-603)]
            bool onkeypress(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-602)]
            void onkeydown(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-604)]
            void onkeyup(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418103)]
            void onmouseout(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418104)]
            void onmouseover(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-606)]
            void onmousemove(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-605)]
            void onmousedown(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-607)]
            void onmouseup(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418100)]
            bool onselectstart(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418095)]
            void onfilterchange(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418101)]
            bool ondragstart(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418108)]
            bool onbeforeupdate(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418107)]
            void onafterupdate(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418099)]
            bool onerrorupdate(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418106)]
            bool onrowexit(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418105)]
            void onrowenter(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418098)]
            void ondatasetchanged(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418097)]
            void ondataavailable(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418096)]
            void ondatasetcomplete(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418094)]
            void onlosecapture(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418093)]
            void onpropertychange(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1014)]
            void onscroll(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418111)]
            void onfocus(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418112)]
            void onblur(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1016)]
            void onresize(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418092)]
            bool ondrag(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418091)]
            void ondragend(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418090)]
            bool ondragenter(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418089)]
            bool ondragover(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418088)]
            void ondragleave(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418087)]
            bool ondrop(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418083)]
            bool onbeforecut(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418086)]
            bool oncut(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418082)]
            bool onbeforecopy(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418085)]
            bool oncopy(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418081)]
            bool onbeforepaste(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418084)]
            bool onpaste(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1023)]
            bool oncontextmenu(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418080)]
            void onrowsdelete(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418079)]
            void onrowsinserted(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418078)]
            void oncellchange(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-609)]
            void onreadystatechange(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1030)]
            void onlayoutcomplete(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1031)]
            void onpage(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1042)]
            void onmouseenter(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1043)]
            void onmouseleave(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1044)]
            void onactivate(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1045)]
            void ondeactivate(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1034)]
            bool onbeforedeactivate(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1047)]
            bool onbeforeactivate(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1048)]
            void onfocusin(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1049)]
            void onfocusout(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1035)]
            void onmove(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1036)]
            bool oncontrolselect(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1038)]
            bool onmovestart(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1039)]
            void onmoveend(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1040)]
            bool onresizestart(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1041)]
            void onresizeend(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1033)]
            bool onmousewheel(NativeCOM.IHTMLEventObj evtObj);
        }

        [ComImport(), Guid("3050f61f-98b5-11cf-bb82-00aa00bdce0b"),
        InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIDispatch),
        TypeLibType(TypeLibTypeFlags.FHidden)]
        public interface DHTMLMarqueeElementEvents2
        {
            [DispId(-2147418102)]
            bool onhelp(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-600)]
            bool onclick(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-601)]
            bool ondblclick(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-603)]
            bool onkeypress(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-602)]
            void onkeydown(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-604)]
            void onkeyup(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418103)]
            void onmouseout(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418104)]
            void onmouseover(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-606)]
            void onmousemove(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-605)]
            void onmousedown(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-607)]
            void onmouseup(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418100)]
            bool onselectstart(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418095)]
            void onfilterchange(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418101)]
            bool ondragstart(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418108)]
            bool onbeforeupdate(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418107)]
            void onafterupdate(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418099)]
            bool onerrorupdate(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418106)]
            bool onrowexit(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418105)]
            void onrowenter(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418098)]
            void ondatasetchanged(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418097)]
            void ondataavailable(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418096)]
            void ondatasetcomplete(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418094)]
            void onlosecapture(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418093)]
            void onpropertychange(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1014)]
            void onscroll(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418111)]
            void onfocus(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418112)]
            void onblur(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1016)]
            void onresize(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418092)]
            bool ondrag(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418091)]
            void ondragend(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418090)]
            bool ondragenter(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418089)]
            bool ondragover(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418088)]
            void ondragleave(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418087)]
            bool ondrop(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418083)]
            bool onbeforecut(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418086)]
            bool oncut(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418082)]
            bool onbeforecopy(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418085)]
            bool oncopy(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418081)]
            bool onbeforepaste(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418084)]
            bool onpaste(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1023)]
            bool oncontextmenu(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418080)]
            void onrowsdelete(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418079)]
            void onrowsinserted(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418078)]
            void oncellchange(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-609)]
            void onreadystatechange(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1030)]
            void onlayoutcomplete(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1031)]
            void onpage(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1042)]
            void onmouseenter(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1043)]
            void onmouseleave(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1044)]
            void onactivate(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1045)]
            void ondeactivate(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1034)]
            bool onbeforedeactivate(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1047)]
            bool onbeforeactivate(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1048)]
            void onfocusin(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1049)]
            void onfocusout(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1035)]
            void onmove(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1036)]
            bool oncontrolselect(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1038)]
            bool onmovestart(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1039)]
            void onmoveend(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1040)]
            bool onresizestart(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1041)]
            void onresizeend(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1033)]
            bool onmousewheel(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147412092)]
            void onbounce(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147412086)]
            void onfinish(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147412085)]
            void onstart(NativeCOM.IHTMLEventObj evtObj);
        }

        [ComImport(), Guid("3050f619-98b5-11cf-bb82-00aa00bdce0b"),
        InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIDispatch),
        TypeLibType(TypeLibTypeFlags.FHidden)]
        public interface DHTMLOptionButtonElementEvents2
        {
            [DispId(-2147418102)]
            bool onhelp(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-600)]
            bool onclick(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-601)]
            bool ondblclick(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-603)]
            bool onkeypress(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-602)]
            void onkeydown(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-604)]
            void onkeyup(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418103)]
            void onmouseout(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418104)]
            void onmouseover(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-606)]
            void onmousemove(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-605)]
            void onmousedown(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-607)]
            void onmouseup(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418100)]
            bool onselectstart(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418095)]
            void onfilterchange(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418101)]
            bool ondragstart(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418108)]
            bool onbeforeupdate(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418107)]
            void onafterupdate(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418099)]
            bool onerrorupdate(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418106)]
            bool onrowexit(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418105)]
            void onrowenter(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418098)]
            void ondatasetchanged(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418097)]
            void ondataavailable(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418096)]
            void ondatasetcomplete(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418094)]
            void onlosecapture(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418093)]
            void onpropertychange(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1014)]
            void onscroll(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418111)]
            void onfocus(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418112)]
            void onblur(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1016)]
            void onresize(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418092)]
            bool ondrag(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418091)]
            void ondragend(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418090)]
            bool ondragenter(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418089)]
            bool ondragover(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418088)]
            void ondragleave(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418087)]
            bool ondrop(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418083)]
            bool onbeforecut(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418086)]
            bool oncut(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418082)]
            bool onbeforecopy(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418085)]
            bool oncopy(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418081)]
            bool onbeforepaste(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418084)]
            bool onpaste(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1023)]
            bool oncontextmenu(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418080)]
            void onrowsdelete(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418079)]
            void onrowsinserted(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418078)]
            void oncellchange(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-609)]
            void onreadystatechange(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1030)]
            void onlayoutcomplete(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1031)]
            void onpage(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1042)]
            void onmouseenter(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1043)]
            void onmouseleave(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1044)]
            void onactivate(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1045)]
            void ondeactivate(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1034)]
            bool onbeforedeactivate(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1047)]
            bool onbeforeactivate(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1048)]
            void onfocusin(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1049)]
            void onfocusout(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1035)]
            void onmove(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1036)]
            bool oncontrolselect(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1038)]
            bool onmovestart(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1039)]
            void onmoveend(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1040)]
            bool onresizestart(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1041)]
            void onresizeend(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1033)]
            bool onmousewheel(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147412082)]
            bool onchange(NativeCOM.IHTMLEventObj evtObj);
        }

        [ComImport(), Guid("3050f622-98b5-11cf-bb82-00aa00bdce0b"),
        InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIDispatch),
        TypeLibType(TypeLibTypeFlags.FHidden)]
        public interface DHTMLSelectElementEvents2
        {
            [DispId(-2147418102)]
            bool onhelp(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-600)]
            bool onclick(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-601)]
            bool ondblclick(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-603)]
            bool onkeypress(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-602)]
            void onkeydown(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-604)]
            void onkeyup(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418103)]
            void onmouseout(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418104)]
            void onmouseover(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-606)]
            void onmousemove(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-605)]
            void onmousedown(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-607)]
            void onmouseup(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418100)]
            bool onselectstart(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418095)]
            void onfilterchange(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418101)]
            bool ondragstart(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418108)]
            bool onbeforeupdate(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418107)]
            void onafterupdate(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418099)]
            bool onerrorupdate(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418106)]
            bool onrowexit(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418105)]
            void onrowenter(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418098)]
            void ondatasetchanged(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418097)]
            void ondataavailable(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418096)]
            void ondatasetcomplete(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418094)]
            void onlosecapture(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418093)]
            void onpropertychange(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1014)]
            void onscroll(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418111)]
            void onfocus(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418112)]
            void onblur(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1016)]
            void onresize(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418092)]
            bool ondrag(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418091)]
            void ondragend(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418090)]
            bool ondragenter(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418089)]
            bool ondragover(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418088)]
            void ondragleave(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418087)]
            bool ondrop(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418083)]
            bool onbeforecut(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418086)]
            bool oncut(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418082)]
            bool onbeforecopy(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418085)]
            bool oncopy(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418081)]
            bool onbeforepaste(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418084)]
            bool onpaste(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1023)]
            bool oncontextmenu(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418080)]
            void onrowsdelete(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418079)]
            void onrowsinserted(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418078)]
            void oncellchange(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-609)]
            void onreadystatechange(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1030)]
            void onlayoutcomplete(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1031)]
            void onpage(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1042)]
            void onmouseenter(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1043)]
            void onmouseleave(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1044)]
            void onactivate(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1045)]
            void ondeactivate(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1034)]
            bool onbeforedeactivate(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1047)]
            bool onbeforeactivate(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1048)]
            void onfocusin(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1049)]
            void onfocusout(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1035)]
            void onmove(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1036)]
            bool oncontrolselect(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1038)]
            bool onmovestart(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1039)]
            void onmoveend(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1040)]
            bool onresizestart(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1041)]
            void onresizeend(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1033)]
            bool onmousewheel(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147412082)]
            void onchange_void(NativeCOM.IHTMLEventObj evtObj);
        }

        [ComImport(), Guid("3050f615-98b5-11cf-bb82-00aa00bdce0b"),
        InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIDispatch),
        TypeLibType(TypeLibTypeFlags.FHidden)]
        public interface DHTMLStyleElementEvents2
        {
            [DispId(-2147418102)]
            bool onhelp(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-600)]
            bool onclick(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-601)]
            bool ondblclick(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-603)]
            bool onkeypress(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-602)]
            void onkeydown(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-604)]
            void onkeyup(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418103)]
            void onmouseout(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418104)]
            void onmouseover(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-606)]
            void onmousemove(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-605)]
            void onmousedown(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-607)]
            void onmouseup(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418100)]
            bool onselectstart(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418095)]
            void onfilterchange(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418101)]
            bool ondragstart(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418108)]
            bool onbeforeupdate(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418107)]
            void onafterupdate(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418099)]
            bool onerrorupdate(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418106)]
            bool onrowexit(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418105)]
            void onrowenter(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418098)]
            void ondatasetchanged(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418097)]
            void ondataavailable(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418096)]
            void ondatasetcomplete(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418094)]
            void onlosecapture(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418093)]
            void onpropertychange(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1014)]
            void onscroll(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418111)]
            void onfocus(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418112)]
            void onblur(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1016)]
            void onresize(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418092)]
            bool ondrag(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418091)]
            void ondragend(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418090)]
            bool ondragenter(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418089)]
            bool ondragover(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418088)]
            void ondragleave(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418087)]
            bool ondrop(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418083)]
            bool onbeforecut(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418086)]
            bool oncut(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418082)]
            bool onbeforecopy(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418085)]
            bool oncopy(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418081)]
            bool onbeforepaste(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418084)]
            bool onpaste(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1023)]
            bool oncontextmenu(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418080)]
            void onrowsdelete(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418079)]
            void onrowsinserted(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418078)]
            void oncellchange(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-609)]
            void onreadystatechange(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1030)]
            void onlayoutcomplete(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1031)]
            void onpage(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1042)]
            void onmouseenter(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1043)]
            void onmouseleave(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1044)]
            void onactivate(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1045)]
            void ondeactivate(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1034)]
            bool onbeforedeactivate(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1047)]
            bool onbeforeactivate(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1048)]
            void onfocusin(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1049)]
            void onfocusout(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1035)]
            void onmove(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1036)]
            bool oncontrolselect(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1038)]
            bool onmovestart(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1039)]
            void onmoveend(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1040)]
            bool onresizestart(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1041)]
            void onresizeend(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1033)]
            bool onmousewheel(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1003)]
            void onload(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1002)]
            void onerror(NativeCOM.IHTMLEventObj evtObj);
        }

        [ComImport(), Guid("3050f623-98b5-11cf-bb82-00aa00bdce0b"),
        InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIDispatch),
        TypeLibType(TypeLibTypeFlags.FHidden)]
        public interface DHTMLTableEvents2
        {
            [DispId(-2147418102)]
            bool onhelp(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-600)]
            bool onclick(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-601)]
            bool ondblclick(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-603)]
            bool onkeypress(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-602)]
            void onkeydown(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-604)]
            void onkeyup(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418103)]
            void onmouseout(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418104)]
            void onmouseover(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-606)]
            void onmousemove(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-605)]
            void onmousedown(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-607)]
            void onmouseup(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418100)]
            bool onselectstart(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418095)]
            void onfilterchange(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418101)]
            bool ondragstart(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418108)]
            bool onbeforeupdate(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418107)]
            void onafterupdate(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418099)]
            bool onerrorupdate(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418106)]
            bool onrowexit(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418105)]
            void onrowenter(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418098)]
            void ondatasetchanged(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418097)]
            void ondataavailable(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418096)]
            void ondatasetcomplete(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418094)]
            void onlosecapture(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418093)]
            void onpropertychange(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1014)]
            void onscroll(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418111)]
            void onfocus(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418112)]
            void onblur(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1016)]
            void onresize(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418092)]
            bool ondrag(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418091)]
            void ondragend(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418090)]
            bool ondragenter(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418089)]
            bool ondragover(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418088)]
            void ondragleave(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418087)]
            bool ondrop(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418083)]
            bool onbeforecut(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418086)]
            bool oncut(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418082)]
            bool onbeforecopy(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418085)]
            bool oncopy(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418081)]
            bool onbeforepaste(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418084)]
            bool onpaste(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1023)]
            bool oncontextmenu(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418080)]
            void onrowsdelete(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418079)]
            void onrowsinserted(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418078)]
            void oncellchange(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-609)]
            void onreadystatechange(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1030)]
            void onlayoutcomplete(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1031)]
            void onpage(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1042)]
            void onmouseenter(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1043)]
            void onmouseleave(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1044)]
            void onactivate(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1045)]
            void ondeactivate(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1034)]
            bool onbeforedeactivate(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1047)]
            bool onbeforeactivate(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1048)]
            void onfocusin(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1049)]
            void onfocusout(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1035)]
            void onmove(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1036)]
            bool oncontrolselect(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1038)]
            bool onmovestart(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1039)]
            void onmoveend(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1040)]
            bool onresizestart(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1041)]
            void onresizeend(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1033)]
            bool onmousewheel(NativeCOM.IHTMLEventObj evtObj);
        }

        [ComImport(), Guid("3050f624-98b5-11cf-bb82-00aa00bdce0b"),
        InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIDispatch),
        TypeLibType(TypeLibTypeFlags.FHidden)]
        public interface DHTMLTextContainerEvents2
        {
            [DispId(-2147418102)]
            bool onhelp(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-600)]
            bool onclick(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-601)]
            bool ondblclick(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-603)]
            bool onkeypress(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-602)]
            void onkeydown(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-604)]
            void onkeyup(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418103)]
            void onmouseout(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418104)]
            void onmouseover(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-606)]
            void onmousemove(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-605)]
            void onmousedown(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-607)]
            void onmouseup(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418100)]
            bool onselectstart(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418095)]
            void onfilterchange(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418101)]
            bool ondragstart(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418108)]
            bool onbeforeupdate(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418107)]
            void onafterupdate(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418099)]
            bool onerrorupdate(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418106)]
            bool onrowexit(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418105)]
            void onrowenter(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418098)]
            void ondatasetchanged(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418097)]
            void ondataavailable(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418096)]
            void ondatasetcomplete(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418094)]
            void onlosecapture(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418093)]
            void onpropertychange(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1014)]
            void onscroll(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418111)]
            void onfocus(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418112)]
            void onblur(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1016)]
            void onresize(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418092)]
            bool ondrag(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418091)]
            void ondragend(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418090)]
            bool ondragenter(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418089)]
            bool ondragover(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418088)]
            void ondragleave(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418087)]
            bool ondrop(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418083)]
            bool onbeforecut(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418086)]
            bool oncut(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418082)]
            bool onbeforecopy(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418085)]
            bool oncopy(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418081)]
            bool onbeforepaste(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418084)]
            bool onpaste(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1023)]
            bool oncontextmenu(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418080)]
            void onrowsdelete(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418079)]
            void onrowsinserted(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418078)]
            void oncellchange(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-609)]
            void onreadystatechange(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1030)]
            void onlayoutcomplete(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1031)]
            void onpage(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1042)]
            void onmouseenter(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1043)]
            void onmouseleave(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1044)]
            void onactivate(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1045)]
            void ondeactivate(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1034)]
            bool onbeforedeactivate(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1047)]
            bool onbeforeactivate(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1048)]
            void onfocusin(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1049)]
            void onfocusout(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1035)]
            void onmove(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1036)]
            bool oncontrolselect(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1038)]
            bool onmovestart(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1039)]
            void onmoveend(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1040)]
            bool onresizestart(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1041)]
            void onresizeend(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1033)]
            bool onmousewheel(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1001)]
            void onchange_void(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1006)]
            void onselect(NativeCOM.IHTMLEventObj evtObj);
        }

        [ComImport(), Guid("3050f621-98b5-11cf-bb82-00aa00bdce0b"),
        InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIDispatch),
        TypeLibType(TypeLibTypeFlags.FHidden)]
        public interface DHTMLScriptEvents2
        {
            [DispId(-2147418102)]
            bool onhelp(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-600)]
            bool onclick(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-601)]
            bool ondblclick(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-603)]
            bool onkeypress(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-602)]
            void onkeydown(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-604)]
            void onkeyup(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418103)]
            void onmouseout(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418104)]
            void onmouseover(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-606)]
            void onmousemove(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-605)]
            void onmousedown(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-607)]
            void onmouseup(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418100)]
            bool onselectstart(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418095)]
            void onfilterchange(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418101)]
            bool ondragstart(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418108)]
            bool onbeforeupdate(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418107)]
            void onafterupdate(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418099)]
            bool onerrorupdate(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418106)]
            bool onrowexit(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418105)]
            void onrowenter(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418098)]
            void ondatasetchanged(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418097)]
            void ondataavailable(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418096)]
            void ondatasetcomplete(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418094)]
            void onlosecapture(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418093)]
            void onpropertychange(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1014)]
            void onscroll(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418111)]
            void onfocus(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418112)]
            void onblur(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1016)]
            void onresize(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418092)]
            bool ondrag(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418091)]
            void ondragend(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418090)]
            bool ondragenter(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418089)]
            bool ondragover(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418088)]
            void ondragleave(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418087)]
            bool ondrop(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418083)]
            bool onbeforecut(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418086)]
            bool oncut(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418082)]
            bool onbeforecopy(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418085)]
            bool oncopy(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418081)]
            bool onbeforepaste(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418084)]
            bool onpaste(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1023)]
            bool oncontextmenu(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418080)]
            void onrowsdelete(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418079)]
            void onrowsinserted(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-2147418078)]
            void oncellchange(NativeCOM.IHTMLEventObj evtObj);
            [DispId(-609)]
            void onreadystatechange(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1030)]
            void onlayoutcomplete(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1031)]
            void onpage(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1042)]
            void onmouseenter(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1043)]
            void onmouseleave(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1044)]
            void onactivate(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1045)]
            void ondeactivate(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1034)]
            bool onbeforedeactivate(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1047)]
            bool onbeforeactivate(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1048)]
            void onfocusin(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1049)]
            void onfocusout(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1035)]
            void onmove(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1036)]
            bool oncontrolselect(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1038)]
            bool onmovestart(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1039)]
            void onmoveend(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1040)]
            bool onresizestart(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1041)]
            void onresizeend(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1033)]
            bool onmousewheel(NativeCOM.IHTMLEventObj evtObj);
            [DispId(1002)]
            void onerror(NativeCOM.IHTMLEventObj evtObj);
        }

        [SuppressUnmanagedCodeSecurity, ComVisible(true), Guid("3050F25E-98B5-11CF-BB82-00AA00BDCE0B"),
        InterfaceTypeAttribute(ComInterfaceType.InterfaceIsDual)]
        public interface IHTMLStyle
        {
            void SetFontFamily(string p);
            string GetFontFamily();
            void SetFontStyle(string p);
            string GetFontStyle();
            void SetFontObject(string p);
            string GetFontObject();
            void SetFontWeight(string p);
            string GetFontWeight();
            void SetFontSize(object p);
            object GetFontSize();
            void SetFont(string p);
            string GetFont();
            void SetColor(object p);
            object GetColor();
            void SetBackground(string p);
            string GetBackground();
            void SetBackgroundColor(object p);
            object GetBackgroundColor();
            void SetBackgroundImage(string p);
            string GetBackgroundImage();
            void SetBackgroundRepeat(string p);
            string GetBackgroundRepeat();
            void SetBackgroundAttachment(string p);
            string GetBackgroundAttachment();
            void SetBackgroundPosition(string p);
            string GetBackgroundPosition();
            void SetBackgroundPositionX(object p);
            object GetBackgroundPositionX();
            void SetBackgroundPositionY(object p);
            object GetBackgroundPositionY();
            void SetWordSpacing(object p);
            object GetWordSpacing();
            void SetLetterSpacing(object p);
            object GetLetterSpacing();
            void SetTextDecoration(string p);
            string GetTextDecoration();
            void SetTextDecorationNone(bool p);
            bool GetTextDecorationNone();
            void SetTextDecorationUnderline(bool p);
            bool GetTextDecorationUnderline();
            void SetTextDecorationOverline(bool p);
            bool GetTextDecorationOverline();
            void SetTextDecorationLineThrough(bool p);
            bool GetTextDecorationLineThrough();
            void SetTextDecorationBlink(bool p);
            bool GetTextDecorationBlink();
            void SetVerticalAlign(object p);
            object GetVerticalAlign();
            void SetTextTransform(string p);
            string GetTextTransform();
            void SetTextAlign(string p);
            string GetTextAlign();
            void SetTextIndent(object p);
            object GetTextIndent();
            void SetLineHeight(object p);
            object GetLineHeight();
            void SetMarginTop(object p);
            object GetMarginTop();
            void SetMarginRight(object p);
            object GetMarginRight();
            void SetMarginBottom(object p);
            object GetMarginBottom();
            void SetMarginLeft(object p);
            object GetMarginLeft();
            void SetMargin(string p);
            string GetMargin();
            void SetPaddingTop(object p);
            object GetPaddingTop();
            void SetPaddingRight(object p);
            object GetPaddingRight();
            void SetPaddingBottom(object p);
            object GetPaddingBottom();
            void SetPaddingLeft(object p);
            object GetPaddingLeft();
            void SetPadding(string p);
            string GetPadding();
            void SetBorder(string p);
            string GetBorder();
            void SetBorderTop(string p);
            string GetBorderTop();
            void SetBorderRight(string p);
            string GetBorderRight();
            void SetBorderBottom(string p);
            string GetBorderBottom();
            void SetBorderLeft(string p);
            string GetBorderLeft();
            void SetBorderColor(string p);
            string GetBorderColor();
            void SetBorderTopColor(object p);
            object GetBorderTopColor();
            void SetBorderRightColor(object p);
            object GetBorderRightColor();
            void SetBorderBottomColor(object p);
            object GetBorderBottomColor();
            void SetBorderLeftColor(object p);
            object GetBorderLeftColor();
            void SetBorderWidth(string p);
            string GetBorderWidth();
            void SetBorderTopWidth(object p);
            object GetBorderTopWidth();
            void SetBorderRightWidth(object p);
            object GetBorderRightWidth();
            void SetBorderBottomWidth(object p);
            object GetBorderBottomWidth();
            void SetBorderLeftWidth(object p);
            object GetBorderLeftWidth();
            void SetBorderStyle(string p);
            string GetBorderStyle();
            void SetBorderTopStyle(string p);
            string GetBorderTopStyle();
            void SetBorderRightStyle(string p);
            string GetBorderRightStyle();
            void SetBorderBottomStyle(string p);
            string GetBorderBottomStyle();
            void SetBorderLeftStyle(string p);
            string GetBorderLeftStyle();
            void SetWidth(object p);
            object GetWidth();
            void SetHeight(object p);
            object GetHeight();
            void SetStyleFloat(string p);
            string GetStyleFloat();
            void SetClear(string p);
            string GetClear();
            void SetDisplay(string p);
            string GetDisplay();
            void SetVisibility(string p);
            string GetVisibility();
            void SetListStyleType(string p);
            string GetListStyleType();
            void SetListStylePosition(string p);
            string GetListStylePosition();
            void SetListStyleImage(string p);
            string GetListStyleImage();
            void SetListStyle(string p);
            string GetListStyle();
            void SetWhiteSpace(string p);
            string GetWhiteSpace();
            void SetTop(object p);
            object GetTop();
            void SetLeft(object p);
            object GetLeft();
            string GetPosition();
            void SetZIndex(object p);
            object GetZIndex();
            void SetOverflow(string p);
            string GetOverflow();
            void SetPageBreakBefore(string p);
            string GetPageBreakBefore();
            void SetPageBreakAfter(string p);
            string GetPageBreakAfter();
            void SetCssText(string p);
            string GetCssText();
            void SetPixelTop(int p);
            int GetPixelTop();
            void SetPixelLeft(int p);
            int GetPixelLeft();
            void SetPixelWidth(int p);
            int GetPixelWidth();
            void SetPixelHeight(int p);
            int GetPixelHeight();
            void SetPosTop(float p);
            float GetPosTop();
            void SetPosLeft(float p);
            float GetPosLeft();
            void SetPosWidth(float p);
            float GetPosWidth();
            void SetPosHeight(float p);
            float GetPosHeight();
            void SetCursor(string p);
            string GetCursor();
            void SetClip(string p);
            string GetClip();
            void SetFilter(string p);
            string GetFilter();
            void SetAttribute(string strAttributeName, object AttributeValue, int lFlags);
            object GetAttribute(string strAttributeName, int lFlags);
            bool RemoveAttribute(string strAttributeName, int lFlags);
        }

        [ComImport(),
         Guid("39088D7E-B71E-11D1-8F39-00C04FD946D0"),
         InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)
        ]
        public interface IExtender
        {

            int Align { get; set; }

            bool Enabled { get; set; }

            int Height { get; set; }

            int Left { get; set; }

            bool TabStop { get; set; }

            int Top { get; set; }

            bool Visible { get; set; }

            int Width { get; set; }

            string Name { [return: MarshalAs(UnmanagedType.BStr)]get; }

            object Parent { [return: MarshalAs(UnmanagedType.Interface)]get; }

            IntPtr Hwnd { get; }

            object Container { [return: MarshalAs(UnmanagedType.Interface)]get; }

            void Move(
                [In, MarshalAs(UnmanagedType.Interface)]
                object left,
                [In, MarshalAs(UnmanagedType.Interface)]
                object top,
                [In, MarshalAs(UnmanagedType.Interface)]
                object width,
                [In, MarshalAs(UnmanagedType.Interface)] 
                object height);
        }

        [ComImport(),
         Guid("8A701DA0-4FEB-101B-A82E-08002B2B2337"),
         InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)
        ]
        public interface IGetOleObject
        {
            [return: MarshalAs(UnmanagedType.Interface)]
            object GetOleObject(ref Guid riid);
        }

        [
            ComImport(),
            Guid("CB2F6722-AB3A-11d2-9C40-00C04FA30A3E"),
            InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)
        ]
        public interface ICorRuntimeHost
        {

            [PreserveSig()]
            int CreateLogicalThreadState();
            [PreserveSig()]
            int DeleteLogicalThreadState();
            [PreserveSig()]
            int SwitchInLogicalThreadState(
                [In] ref uint pFiberCookie);

            [PreserveSig()]
            int SwitchOutLogicalThreadState(
                out uint FiberCookie);

            [PreserveSig()]
            int LocksHeldByLogicalThread(
                out uint pCount);

            [PreserveSig()]
            int MapFile(
                IntPtr hFile,
                out IntPtr hMapAddress);

            [PreserveSig()]
            int GetConfiguration([MarshalAs(UnmanagedType.IUnknown)] out object pConfiguration);

            [PreserveSig()]
            int Start();

            [PreserveSig()]
            int Stop();

            [PreserveSig()]
            int CreateDomain(string pwzFriendlyName,
                 [MarshalAs(UnmanagedType.IUnknown)] object pIdentityArray, // Optional
                 [MarshalAs(UnmanagedType.IUnknown)] out object pAppDomain);

            [PreserveSig()]
            int GetDefaultDomain([MarshalAs(UnmanagedType.IUnknown)] out object pAppDomain);

            [PreserveSig()]
            int EnumDomains(out IntPtr hEnum);

            [PreserveSig()]
            int NextDomain(IntPtr hEnum,
               [MarshalAs(UnmanagedType.IUnknown)] out object pAppDomain);

            [PreserveSig()]
            int CloseEnum(IntPtr hEnum);

            [PreserveSig()]
            int CreateDomainEx(string pwzFriendlyName, // Optional
                   [MarshalAs(UnmanagedType.IUnknown)] object pSetup,        // Optional 
                   [MarshalAs(UnmanagedType.IUnknown)] object pEvidence,     // Optional
                   [MarshalAs(UnmanagedType.IUnknown)] out object pAppDomain);

            [PreserveSig()]
            int CreateDomainSetup([MarshalAs(UnmanagedType.IUnknown)] out object pAppDomainSetup);

            [PreserveSig()]
            int CreateEvidence([MarshalAs(UnmanagedType.IUnknown)] out object pEvidence);

            [PreserveSig()]
            int UnloadDomain([MarshalAs(UnmanagedType.IUnknown)] object pAppDomain);

            [PreserveSig()]
            int CurrentDomain([MarshalAs(UnmanagedType.IUnknown)] out object pAppDomain);
        }

        [
            ComImport(),
            Guid("CB2F6723-AB3A-11d2-9C40-00C04FA30A3E")
        ]
        public class CorRuntimeHost
        {
        }

        [ComImport(),
        Guid("000C0601-0000-0000-C000-000000000046"),
        InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)
        ]
        public interface IMsoComponentManager
        {

            [PreserveSig]
            int QueryService(
                ref Guid guidService,
                ref Guid iid,
                [MarshalAs(UnmanagedType.Interface)]
            out object ppvObj);

            [PreserveSig]
            bool FDebugMessage(
                IntPtr hInst,
                int msg,
                IntPtr wParam,
                IntPtr lParam);

            [PreserveSig]
            bool FRegisterComponent(
                IMsoComponent component,
                NativeMethods.MSOCRINFOSTRUCT pcrinfo,
                out int dwComponentID);

            [PreserveSig]
            bool FRevokeComponent(int dwComponentID);

            [PreserveSig]
            bool FUpdateComponentRegistration(int dwComponentID, NativeMethods.MSOCRINFOSTRUCT pcrinfo);

            [PreserveSig]
            bool FOnComponentActivate(int dwComponentID);

            [PreserveSig]
            bool FSetTrackingComponent(int dwComponentID, [In, MarshalAs(UnmanagedType.Bool)] bool fTrack);

            [PreserveSig]
            void OnComponentEnterState(int dwComponentID, int uStateID, int uContext, int cpicmExclude,/* IMsoComponentManger** */ int rgpicmExclude, int dwReserved);

            [PreserveSig]
            bool FOnComponentExitState(
                int dwComponentID,
                int uStateID,
                int uContext,
                int cpicmExclude,
                /* IMsoComponentManager** */ int rgpicmExclude);

            [PreserveSig]
            bool FInState(int uStateID,/* PVOID */ IntPtr pvoid);

            [PreserveSig]
            bool FContinueIdle();

            [PreserveSig]
            bool FPushMessageLoop(int dwComponentID, int uReason,/* PVOID */ int pvLoopData);

            [PreserveSig]
            bool FCreateSubComponentManager(
                [MarshalAs(UnmanagedType.Interface)]
            object punkOuter,
                [MarshalAs(UnmanagedType.Interface)]
            object punkServProv,
                ref Guid riid,
                out IntPtr ppvObj);

            [PreserveSig]
            bool FGetParentComponentManager(
                out IMsoComponentManager ppicm);

            [PreserveSig]
            bool FGetActiveComponent(
            int dwgac,
                [Out, MarshalAs(UnmanagedType.LPArray)] 
            IMsoComponent[] ppic,
                NativeMethods.MSOCRINFOSTRUCT pcrinfo,
                int dwReserved);
        }

        [ComImport(),
        Guid("000C0600-0000-0000-C000-000000000046"),
        InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)
        ]
        public interface IMsoComponent
        {

            [PreserveSig]
            bool FDebugMessage(
                IntPtr hInst,
                int msg,
                IntPtr wParam,
                IntPtr lParam);

            [PreserveSig]
            bool FPreTranslateMessage(ref NativeMethods.MSG msg);

            [PreserveSig]
            void OnEnterState(
                int uStateID,
                bool fEnter);

            [PreserveSig]
            void OnAppActivate(
                bool fActive,
                int dwOtherThreadID);

            [PreserveSig]
            void OnLoseActivation();

            [PreserveSig]
            void OnActivationChange(
                IMsoComponent component,
                bool fSameComponent,
                int pcrinfo,
                bool fHostIsActivating,
                int pchostinfo,
                int dwReserved);

            [PreserveSig]
            bool FDoIdle(
                int grfidlef);

            [PreserveSig]
            bool FContinueMessageLoop(
                int uReason,
                int pvLoopData,
                [MarshalAs(UnmanagedType.LPArray)] NativeMethods.MSG[] pMsgPeeked);

            [PreserveSig]
            bool FQueryTerminate(
                bool fPromptUser);

            [PreserveSig]
            void Terminate();

            [PreserveSig]
            IntPtr HwndGetWindow(
                int dwWhich,
                int dwReserved);
        }

        [ComVisible(true), Guid("8CC497C0-A1DF-11ce-8098-00AA0047BE5D"),
        InterfaceTypeAttribute(ComInterfaceType.InterfaceIsDual),
        SuppressUnmanagedCodeSecurity()]
        public interface ITextDocument
        {
            string GetName();
            object GetSelection();
            int GetStoryCount();
            object GetStoryRanges();
            int GetSaved();
            void SetSaved(int value);
            object GetDefaultTabStop();
            void SetDefaultTabStop(object value);
            void New();
            void Open(object pVar, int flags, int codePage);
            void Save(object pVar, int flags, int codePage);
            int Freeze();
            int Unfreeze();
            void BeginEditCollection();
            void EndEditCollection();
            int Undo(int count);
            int Redo(int count);
            [return: MarshalAs(UnmanagedType.Interface)]
            ITextRange Range(int cp1, int cp2);
            [return: MarshalAs(UnmanagedType.Interface)]
            ITextRange RangeFromPoint(int x, int y);
        };

        [ComVisible(true), Guid("8CC497C2-A1DF-11ce-8098-00AA0047BE5D"),
        InterfaceTypeAttribute(ComInterfaceType.InterfaceIsDual),
        SuppressUnmanagedCodeSecurity()]
        public interface ITextRange
        {
            string GetText();
            void SetText(string text);
            object GetChar();
            void SetChar(object ch);
            [return: MarshalAs(UnmanagedType.Interface)]
            ITextRange GetDuplicate();
            [return: MarshalAs(UnmanagedType.Interface)]
            ITextRange GetFormattedText();
            void SetFormattedText([In, MarshalAs(UnmanagedType.Interface)] ITextRange range);
            int GetStart();
            void SetStart(int cpFirst);
            int GetEnd();
            void SetEnd(int cpLim);
            object GetFont();
            void SetFont(object font);
            object GetPara();
            void SetPara(object para);
            int GetStoryLength();
            int GetStoryType();
            void Collapse(int start);
            int Expand(int unit);
            int GetIndex(int unit);
            void SetIndex(int unit, int index, int extend);
            void SetRange(int cpActive, int cpOther);
            int InRange([In, MarshalAs(UnmanagedType.Interface)] ITextRange range);
            int InStory([In, MarshalAs(UnmanagedType.Interface)] ITextRange range);
            int IsEqual([In, MarshalAs(UnmanagedType.Interface)] ITextRange range);
            void Select();
            int StartOf(int unit, int extend);
            int EndOf(int unit, int extend);
            int Move(int unit, int count);
            int MoveStart(int unit, int count);
            int MoveEnd(int unit, int count);
            int MoveWhile(object cset, int count);
            int MoveStartWhile(object cset, int count);
            int MoveEndWhile(object cset, int count);
            int MoveUntil(object cset, int count);
            int MoveStartUntil(object cset, int count);
            int MoveEndUntil(object cset, int count);
            int FindText(string text, int cch, int flags);
            int FindTextStart(string text, int cch, int flags);
            int FindTextEnd(string text, int cch, int flags);
            int Delete(int unit, int count);
            void Cut([Out] out object pVar);
            void Copy([Out] out object pVar);
            void Paste(object pVar, int format);
            int CanPaste(object pVar, int format);
            int CanEdit();
            void ChangeCase(int type);
            void GetPoint(int type, [Out] out int x, [Out] out int y);
            void SetPoint(int x, int y, int type, int extend);
            void ScrollIntoView(int value);
            object GetEmbeddedObject();
        };

        [ComImport, Guid("00020D00-0000-0000-c000-000000000046"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        public interface IRichEditOle
        {
            [return: MarshalAs(UnmanagedType.I4)]
            [PreserveSig]
            int GetClientSite(out IOleClientSite site);
            [return: MarshalAs(UnmanagedType.I4)]
            [PreserveSig]
            int GetObjectCount();
            [return: MarshalAs(UnmanagedType.I4)]
            [PreserveSig]
            int GetLinkCount();
            [return: MarshalAs(UnmanagedType.I4)]
            [PreserveSig]
            int GetObject(int iob, [In, Out] NativeMethods.REOBJECT lpreobject, uint flags);
            [return: MarshalAs(UnmanagedType.I4)]
            [PreserveSig]
            int InsertObject(NativeMethods.REOBJECT lpreobject);
            [return: MarshalAs(UnmanagedType.I4)]
            [PreserveSig]
            int ConvertObject(int iob, Guid rclsidNew, string lpstrUserTypeNew);
            [return: MarshalAs(UnmanagedType.I4)]
            [PreserveSig]
            int ActivateAs(Guid rclsid, Guid rclsidAs);
            [return: MarshalAs(UnmanagedType.I4)]
            [PreserveSig]
            int SetHostNames(string lpstrContainerApp, string lpstrContainerObj);
            [return: MarshalAs(UnmanagedType.I4)]
            [PreserveSig]
            int SetLinkAvailable(int iob, bool fAvailable);
            [return: MarshalAs(UnmanagedType.I4)]
            [PreserveSig]
            int SetDvaspect(int iob, uint dvaspect);
            [return: MarshalAs(UnmanagedType.I4)]
            [PreserveSig]
            int HandsOffStorage(int iob);
            [return: MarshalAs(UnmanagedType.I4)]
            [PreserveSig]
            int SaveCompleted(int iob, IStorage lpstg);
            [return: MarshalAs(UnmanagedType.I4)]
            [PreserveSig]
            int InPlaceDeactivate();
            [return: MarshalAs(UnmanagedType.I4)]
            [PreserveSig]
            int ContextSensitiveHelp(bool fEnterMode);
            [return: MarshalAs(UnmanagedType.I4)]
            [PreserveSig]
            int GetClipboardData([In, Out] ref   NativeMethods.CHARRANGE lpchrg, uint reco, out IDataObject lplpdataobj);
            [return: MarshalAs(UnmanagedType.I4)]
            [PreserveSig]
            int ImportDataObject(IDataObject lpdataobj, int cf, IntPtr hMetaPict);
        }


        [ComImport, InterfaceType(ComInterfaceType.InterfaceIsIUnknown), ComVisible(true), Guid("0000010E-0000-0000-C000-000000000046")]
        public interface IDataObject
        {
            [PreserveSig]
            uint GetData(ref NativeMethods.FORMATETC a, ref NativeMethods.STGMEDIUM b);
            [PreserveSig]
            uint GetDataHere(ref NativeMethods.FORMATETC pFormatetc, out NativeMethods.STGMEDIUM pMedium);
            [PreserveSig]
            uint QueryGetData(ref NativeMethods.FORMATETC pFormatetc);
            [PreserveSig]
            uint GetCanonicalFormatEtc(ref NativeMethods.FORMATETC pformatectIn, out NativeMethods.FORMATETC pformatetcOut);
            [PreserveSig]
            uint SetData(ref NativeMethods.FORMATETC pFormatectIn, ref NativeMethods.STGMEDIUM pmedium, [In, MarshalAs(UnmanagedType.Bool)] bool fRelease);
            [PreserveSig]
            uint EnumFormatEtc(uint dwDirection, IEnumFORMATETC penum);
            [PreserveSig]
            uint DAdvise(ref NativeMethods.FORMATETC pFormatetc, int advf, [In, MarshalAs(UnmanagedType.Interface)] IAdviseSink pAdvSink, out uint pdwConnection);
            [PreserveSig]
            uint DUnadvise(uint dwConnection);
            [PreserveSig]
            uint EnumDAdvise([MarshalAs(UnmanagedType.Interface)] out IEnumSTATDATA ppenumAdvise);
        }

        [ComImport(), Guid("00020D03-0000-0000-C000-000000000046"), InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
        public interface IRichEditOleCallback
        {
            [PreserveSig]
            int GetNewStorage(out IStorage ret);

            [PreserveSig]
            int GetInPlaceContext(IntPtr lplpFrame, IntPtr lplpDoc, IntPtr lpFrameInfo);

            [PreserveSig]
            int ShowContainerUI(int fShow);

            [PreserveSig]
            int QueryInsertObject(ref Guid lpclsid, IntPtr lpstg, int cp);

            [PreserveSig]
            int DeleteObject(IntPtr lpoleobj);

            [PreserveSig]
            int QueryAcceptData(IComDataObject lpdataobj, /* CLIPFORMAT* */ IntPtr lpcfFormat, int reco, int fReally, IntPtr hMetaPict);

            [PreserveSig]
            int ContextSensitiveHelp(int fEnterMode);

            [PreserveSig]
            int GetClipboardData(NativeMethods.CHARRANGE lpchrg, int reco, IntPtr lplpdataobj);

            [PreserveSig]
            int GetDragDropEffect(bool fDrag, int grfKeyState, ref int pdwEffect);

            [PreserveSig]
            int GetContextMenu(short seltype, IntPtr lpoleobj, NativeMethods.CHARRANGE lpchrg, out IntPtr hmenu);
        }

        [ComImport(), Guid("00000115-0000-0000-C000-000000000046"), InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
        public interface IOleInPlaceUIWindow
        {
            IntPtr GetWindow();

            [PreserveSig]
            int ContextSensitiveHelp(

                    int fEnterMode);


            [PreserveSig]
            int GetBorder(
                   [Out]
                      NativeMethods.COMRECT lprectBorder);


            [PreserveSig]
            int RequestBorderSpace(
                   [In]
                      NativeMethods.COMRECT pborderwidths);


            [PreserveSig]
            int SetBorderSpace(
                   [In]
                      NativeMethods.COMRECT pborderwidths);


            void SetActiveObject(
                   [In, MarshalAs(UnmanagedType.Interface)]
                      NativeCOM.IOleInPlaceActiveObject pActiveObject,
                   [In, MarshalAs(UnmanagedType.LPWStr)]
                      string pszObjName);


        }
        [SuppressUnmanagedCodeSecurity, ComImport(),
        Guid("00000117-0000-0000-C000-000000000046"),
        InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
        public interface IOleInPlaceActiveObject
        {

            [PreserveSig]
            int GetWindow(out IntPtr hwnd);


            void ContextSensitiveHelp(

                    int fEnterMode);


            [PreserveSig]
            int TranslateAccelerator(
                   [In] 
                      ref NativeMethods.MSG lpmsg);


            void OnFrameWindowActivate(

                    bool fActivate);


            void OnDocWindowActivate(

                    int fActivate);


            void ResizeBorder(
                   [In]
                      NativeMethods.COMRECT prcBorder,
                   [In]
                      NativeCOM.IOleInPlaceUIWindow pUIWindow,

                    bool fFrameWindow);


            void EnableModeless(

                    int fEnable);


        }
        [ComImport(), Guid("00000114-0000-0000-C000-000000000046"), InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
        public interface IOleWindow
        {

            [PreserveSig]
            int GetWindow([Out]out IntPtr hwnd);


            void ContextSensitiveHelp(

                    int fEnterMode);
        }
        [SuppressUnmanagedCodeSecurity, ComImport(),
        Guid("00000113-0000-0000-C000-000000000046"),
        InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
        public interface IOleInPlaceObject
        {

            [PreserveSig]
            int GetWindow([Out]out IntPtr hwnd);


            void ContextSensitiveHelp(

                    int fEnterMode);


            void InPlaceDeactivate();


            [PreserveSig]
            int UIDeactivate();


            void SetObjectRects(
                   [In]
                      NativeMethods.COMRECT lprcPosRect,
                   [In] 
                      NativeMethods.COMRECT lprcClipRect);


            void ReactivateAndUndo();


        }
        [SuppressUnmanagedCodeSecurity, ComImport(),
        Guid("00000112-0000-0000-C000-000000000046"),
        InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
        public interface IOleObject
        {

            [PreserveSig]
            int SetClientSite(
                   [In, MarshalAs(UnmanagedType.Interface)] 
                      NativeCOM.IOleClientSite pClientSite);


            NativeCOM.IOleClientSite GetClientSite();

            [PreserveSig]
            int SetHostNames(
                   [In, MarshalAs(UnmanagedType.LPWStr)]
                      string szContainerApp,
                   [In, MarshalAs(UnmanagedType.LPWStr)]
                      string szContainerObj);

            [PreserveSig]
            int Close(

                    int dwSaveOption);

            [PreserveSig]
            int SetMoniker(
                   [In, MarshalAs(UnmanagedType.U4)] 
                     int dwWhichMoniker,
                   [In, MarshalAs(UnmanagedType.Interface)]
                     object pmk);

            [PreserveSig]
            int GetMoniker(
                  [In, MarshalAs(UnmanagedType.U4)] 
                     int dwAssign,
                  [In, MarshalAs(UnmanagedType.U4)] 
                     int dwWhichMoniker,
                  [Out, MarshalAs(UnmanagedType.Interface)]
                     out object moniker);

            [PreserveSig]
            int InitFromData(
                   [In, MarshalAs(UnmanagedType.Interface)] 
                     IComDataObject pDataObject,

                    int fCreation,
                   [In, MarshalAs(UnmanagedType.U4)]
                     int dwReserved);

            [PreserveSig]
            int GetClipboardData(
                   [In, MarshalAs(UnmanagedType.U4)] 
                     int dwReserved,
                    out IComDataObject data);

            [PreserveSig]
            int DoVerb(

                    int iVerb,
                   [In]
                     IntPtr lpmsg,
                   [In, MarshalAs(UnmanagedType.Interface)]
                      NativeCOM.IOleClientSite pActiveSite,

                    int lindex,

                    IntPtr hwndParent,
                   [In]
                     NativeMethods.COMRECT lprcPosRect);

            [PreserveSig]
            int EnumVerbs(out NativeCOM.IEnumOLEVERB e);

            [PreserveSig]
            int OleUpdate();

            [PreserveSig]
            int IsUpToDate();

            [PreserveSig]
            int GetUserClassID(
                   [In, Out] 
                      ref Guid pClsid);

            [PreserveSig]
            int GetUserType(
                   [In, MarshalAs(UnmanagedType.U4)]
                     int dwFormOfType,
                   [Out, MarshalAs(UnmanagedType.LPWStr)]
                     out string userType);

            [PreserveSig]
            int SetExtent(
                   [In, MarshalAs(UnmanagedType.U4)]
                     int dwDrawAspect,
                   [In]
                     NativeMethods.tagSIZEL pSizel);

            [PreserveSig]
            int GetExtent(
                   [In, MarshalAs(UnmanagedType.U4)]
                     int dwDrawAspect,
                   [Out]
                     NativeMethods.tagSIZEL pSizel);

            [PreserveSig]
            int Advise(
                    IAdviseSink pAdvSink,
                    out int cookie);

            [PreserveSig]
            int Unadvise(
                   [In, MarshalAs(UnmanagedType.U4)]
                     int dwConnection);

            [PreserveSig]
            int EnumAdvise(out IEnumSTATDATA e);

            [PreserveSig]
            int GetMiscStatus(
                   [In, MarshalAs(UnmanagedType.U4)]
                     int dwAspect,
                    out int misc);

            [PreserveSig]
            int SetColorScheme(
                   [In] 
                      NativeMethods.tagLOGPALETTE pLogpal);
        }

        [ComImport(), Guid("1C2056CC-5EF4-101B-8BC8-00AA003E3B29"), InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
        public interface IOleInPlaceObjectWindowless
        {

            [PreserveSig]
            int SetClientSite(
                   [In, MarshalAs(UnmanagedType.Interface)] 
                      NativeCOM.IOleClientSite pClientSite);

            [PreserveSig]
            int GetClientSite(out NativeCOM.IOleClientSite site);

            [PreserveSig]
            int SetHostNames(
                   [In, MarshalAs(UnmanagedType.LPWStr)] 
                      string szContainerApp,
                   [In, MarshalAs(UnmanagedType.LPWStr)]
                      string szContainerObj);

            [PreserveSig]
            int Close(

                    int dwSaveOption);

            [PreserveSig]
            int SetMoniker(
                   [In, MarshalAs(UnmanagedType.U4)] 
                     int dwWhichMoniker,
                   [In, MarshalAs(UnmanagedType.Interface)]
                     object pmk);

            [PreserveSig]
            int GetMoniker(
                  [In, MarshalAs(UnmanagedType.U4)] 
                     int dwAssign,
                  [In, MarshalAs(UnmanagedType.U4)] 
                     int dwWhichMoniker,
                  [Out, MarshalAs(UnmanagedType.Interface)]
                     out object moniker);

            [PreserveSig]
            int InitFromData(
                   [In, MarshalAs(UnmanagedType.Interface)] 
                     IComDataObject pDataObject,

                    int fCreation,
                   [In, MarshalAs(UnmanagedType.U4)]
                     int dwReserved);

            [PreserveSig]
            int GetClipboardData(
                   [In, MarshalAs(UnmanagedType.U4)] 
                     int dwReserved,
                    out IComDataObject data);

            [PreserveSig]
            int DoVerb(

                    int iVerb,
                   [In] 
                     IntPtr lpmsg,
                   [In, MarshalAs(UnmanagedType.Interface)]
                      NativeCOM.IOleClientSite pActiveSite,

                    int lindex,

                    IntPtr hwndParent,
                   [In]
                     NativeMethods.COMRECT lprcPosRect);

            [PreserveSig]
            int EnumVerbs(out NativeCOM.IEnumOLEVERB e);

            [PreserveSig]
            int OleUpdate();

            [PreserveSig]
            int IsUpToDate();

            [PreserveSig]
            int GetUserClassID(
                   [In, Out]
                      ref Guid pClsid);

            [PreserveSig]
            int GetUserType(
                   [In, MarshalAs(UnmanagedType.U4)] 
                     int dwFormOfType,
                   [Out, MarshalAs(UnmanagedType.LPWStr)]
                     out string userType);

            [PreserveSig]
            int SetExtent(
                   [In, MarshalAs(UnmanagedType.U4)] 
                     int dwDrawAspect,
                   [In] 
                     NativeMethods.tagSIZEL pSizel);

            [PreserveSig]
            int GetExtent(
                   [In, MarshalAs(UnmanagedType.U4)]
                     int dwDrawAspect,
                   [Out] 
                     NativeMethods.tagSIZEL pSizel);

            [PreserveSig]
            int Advise(
                   [In, MarshalAs(UnmanagedType.Interface)] 
                     IAdviseSink pAdvSink,
                    out int cookie);

            [PreserveSig]
            int Unadvise(
                   [In, MarshalAs(UnmanagedType.U4)] 
                     int dwConnection);

            [PreserveSig]
            int EnumAdvise(out IEnumSTATDATA e);

            [PreserveSig]
            int GetMiscStatus(
                   [In, MarshalAs(UnmanagedType.U4)]
                     int dwAspect,
                    out int misc);

            [PreserveSig]
            int SetColorScheme(
                   [In]
                      NativeMethods.tagLOGPALETTE pLogpal);

            [PreserveSig]
            int OnWindowMessage(
               [In, MarshalAs(UnmanagedType.U4)]  int msg,
               [In, MarshalAs(UnmanagedType.U4)]  int wParam,
               [In, MarshalAs(UnmanagedType.U4)]  int lParam,
               [Out, MarshalAs(UnmanagedType.U4)] int plResult);

            [PreserveSig]
            int GetDropTarget(
               [Out, MarshalAs(UnmanagedType.Interface)] object ppDropTarget);

        };


        [SuppressUnmanagedCodeSecurity, ComImport(),
        Guid("B196B288-BAB4-101A-B69C-00AA00341D07"),
        InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
        public interface IOleControl
        {
            [PreserveSig]
            int GetControlInfo(
                   [Out] 
                      NativeMethods.tagCONTROLINFO pCI);

            [PreserveSig]
            int OnMnemonic(
                   [In]
                      ref NativeMethods.MSG pMsg);

            [PreserveSig]
            int OnAmbientPropertyChange(

                    int dispID);

            [PreserveSig]
            int FreezeEvents(

                    int bFreeze);

        }
        [ComImport(), Guid("6D5140C1-7436-11CE-8034-00AA006009FA"), InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
        public interface IOleServiceProvider
        {
            [PreserveSig]
            int QueryService(
                 [In]
                      ref Guid guidService,
                 [In]
                  ref Guid riid,
                 out IntPtr ppvObject);
        }
        [ComImport(), Guid("0000010d-0000-0000-C000-000000000046"), InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
        public interface IViewObject
        {
            [PreserveSig]
            int Draw(
                [In, MarshalAs(UnmanagedType.U4)] 
                int dwDrawAspect,

                int lindex,

                IntPtr pvAspect,
                [In]
                NativeMethods.tagDVTARGETDEVICE ptd,

                IntPtr hdcTargetDev,

                IntPtr hdcDraw,
                [In] 
                NativeMethods.COMRECT lprcBounds,
                [In] 
                NativeMethods.COMRECT lprcWBounds,

                IntPtr pfnContinue,
                [In] 
                int dwContinue);


            [PreserveSig]
            int GetColorSet(
                [In, MarshalAs(UnmanagedType.U4)]
                int dwDrawAspect,

                int lindex,

                IntPtr pvAspect,
                [In] 
                NativeMethods.tagDVTARGETDEVICE ptd,

                IntPtr hicTargetDev,
                [Out]
                NativeMethods.tagLOGPALETTE ppColorSet);

            [PreserveSig]
            int Freeze(
                [In, MarshalAs(UnmanagedType.U4)] 
                int dwDrawAspect,

                int lindex,

                IntPtr pvAspect,
                [Out] 
                IntPtr pdwFreeze);

            [PreserveSig]
            int Unfreeze(
                [In, MarshalAs(UnmanagedType.U4)] 
                int dwFreeze);


            void SetAdvise(
                [In, MarshalAs(UnmanagedType.U4)]
                int aspects,
                [In, MarshalAs(UnmanagedType.U4)] 
                int advf,
                [In, MarshalAs(UnmanagedType.Interface)] 
                IAdviseSink pAdvSink);


            void GetAdvise(
                // These can be NULL if caller doesn't want them
                [In, Out, MarshalAs(UnmanagedType.LPArray)] 
                int[] paspects,
                // These can be NULL if caller doesn't want them
                [In, Out, MarshalAs(UnmanagedType.LPArray)] 
                int[] advf,
                // These can be NULL if caller doesn't want them
                [In, Out, MarshalAs(UnmanagedType.LPArray)]
                IAdviseSink[] pAdvSink);
        }
        [ComImport(), Guid("00000127-0000-0000-C000-000000000046"), InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
        public interface IViewObject2 /* : IViewObject */ {
            void Draw(
                [In, MarshalAs(UnmanagedType.U4)] 
                int dwDrawAspect,

                int lindex,

                IntPtr pvAspect,
                [In] 
                NativeMethods.tagDVTARGETDEVICE ptd,

                IntPtr hdcTargetDev,

                IntPtr hdcDraw,
                [In]
                NativeMethods.COMRECT lprcBounds,
                [In]
                NativeMethods.COMRECT lprcWBounds,

                IntPtr pfnContinue,
                [In] 
                int dwContinue);


            [PreserveSig]
            int GetColorSet(
                [In, MarshalAs(UnmanagedType.U4)] 
                int dwDrawAspect,

                int lindex,

                IntPtr pvAspect,
                [In]
                NativeMethods.tagDVTARGETDEVICE ptd,

                IntPtr hicTargetDev,
                [Out] 
                NativeMethods.tagLOGPALETTE ppColorSet);


            [PreserveSig]
            int Freeze(
                [In, MarshalAs(UnmanagedType.U4)] 
                int dwDrawAspect,

                int lindex,

                IntPtr pvAspect,
                [Out]
                IntPtr pdwFreeze);


            [PreserveSig]
            int Unfreeze(
                [In, MarshalAs(UnmanagedType.U4)] 
                int dwFreeze);


            void SetAdvise(
                [In, MarshalAs(UnmanagedType.U4)]
                int aspects,
                [In, MarshalAs(UnmanagedType.U4)]
                int advf,
                [In, MarshalAs(UnmanagedType.Interface)] 
                IAdviseSink pAdvSink);


            void GetAdvise(
                // These can be NULL if caller doesn't want them
                [In, Out, MarshalAs(UnmanagedType.LPArray)] 
                int[] paspects,
                // These can be NULL if caller doesn't want them 
                [In, Out, MarshalAs(UnmanagedType.LPArray)] 
                int[] advf,
                // These can be NULL if caller doesn't want them 
                [In, Out, MarshalAs(UnmanagedType.LPArray)]
                IAdviseSink[] pAdvSink);


            void GetExtent(
                [In, MarshalAs(UnmanagedType.U4)] 
                int dwDrawAspect,

                int lindex,
                [In]
                NativeMethods.tagDVTARGETDEVICE ptd,
                [Out]
                NativeMethods.tagSIZEL lpsizel);
        }

        [ComImport(), Guid("0000010C-0000-0000-C000-000000000046"), InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
        public interface IPersist
        {

            [System.Security.SuppressUnmanagedCodeSecurityAttribute()]
            void GetClassID(
                           [Out]
                           out Guid pClassID);
        }

        [ComImport(), Guid("37D84F60-42CB-11CE-8135-00AA004BB851"), InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
        public interface IPersistPropertyBag
        {
            void GetClassID(
                [Out]
                out Guid pClassID);


            void InitNew();


            void Load(
                [In, MarshalAs(UnmanagedType.Interface)] 
                IPropertyBag pPropBag,
                [In, MarshalAs(UnmanagedType.Interface)]
                IErrorLog pErrorLog);


            void Save(
                [In, MarshalAs(UnmanagedType.Interface)] 
                IPropertyBag pPropBag,
                [In, MarshalAs(UnmanagedType.Bool)] 
                bool fClearDirty,
                [In, MarshalAs(UnmanagedType.Bool)]
                bool fSaveAllProperties);
        }
        [
            ComImport(),
        Guid("CF51ED10-62FE-11CF-BF86-00A0C9034836"),
        InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)
        ]
        public interface IQuickActivate
        {
            void QuickActivate(
                              [In]
                              NativeCOM.tagQACONTAINER pQaContainer,
                              [Out]
                              NativeCOM.tagQACONTROL pQaControl);


            void SetContentExtent(
                                 [In]
                                 NativeMethods.tagSIZEL pSizel);


            void GetContentExtent(
                                 [Out] 
                                 NativeMethods.tagSIZEL pSizel);

        }

        [ComImport(), Guid("000C060B-0000-0000-C000-000000000046"),
            SuppressMessage("Microsoft.Performance", "CA1812:AvoidUninstantiatedpublicClasses")]
        public class SMsoComponentManager
        {
        }

        [ComImport(), Guid("55272A00-42CB-11CE-8135-00AA004BB851"),
        InterfaceType(ComInterfaceType.InterfaceIsIUnknown)
        ]
        public interface IPropertyBag
        {
            [PreserveSig]
            int Read(
                [In, MarshalAs(UnmanagedType.LPWStr)]
                string pszPropName,
                [In, Out] 
                ref object pVar,
                [In] 
                IErrorLog pErrorLog);

            [PreserveSig]
            int Write(
                [In, MarshalAs(UnmanagedType.LPWStr)]
                string pszPropName,
                [In] 
                ref object pVar);
        }

        [ComImport(), Guid("3127CA40-446E-11CE-8135-00AA004BB851"), InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
        public interface IErrorLog
        {
            void AddError(
                   [In, MarshalAs(UnmanagedType.LPWStr)]
                             string pszPropName_p0,
                   [In, MarshalAs(UnmanagedType.Struct)] 
                              NativeMethods.tagEXCEPINFO pExcepInfo_p1);

        }

        [ComImport(), Guid("00000109-0000-0000-C000-000000000046"), InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
        public interface IPersistStream
        {

            void GetClassID([Out] out Guid pClassId);

            [PreserveSig]
            int IsDirty();


            void Load(
                   [In, MarshalAs(UnmanagedType.Interface)] 
                  NativeCOM.IStream pstm);


            void Save(
                   [In, MarshalAs(UnmanagedType.Interface)] 
                  NativeCOM.IStream pstm,
                   [In, MarshalAs(UnmanagedType.Bool)]
                 bool fClearDirty);


            long GetSizeMax();


        }

        [SuppressUnmanagedCodeSecurity, ComImport(),
        Guid("7FD52380-4E07-101B-AE2D-08002B2EC713"),
        InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
        public interface IPersistStreamInit
        {
            void GetClassID(
                   [Out] 
                  out Guid pClassID);


            [PreserveSig]
            int IsDirty();


            void Load(
                   [In, MarshalAs(UnmanagedType.Interface)] 
                  NativeCOM.IStream pstm);


            void Save(
                   [In, MarshalAs(UnmanagedType.Interface)]
                      IStream pstm,
                   [In, MarshalAs(UnmanagedType.Bool)]
                     bool fClearDirty);


            void GetSizeMax(
                   [Out, MarshalAs(UnmanagedType.LPArray)]
                 long pcbSize);


            void InitNew();


        }

        [SuppressUnmanagedCodeSecurity, ComImport(),
        Guid("B196B286-BAB4-101A-B69C-00AA00341D07"),
        InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
        public interface IConnectionPoint
        {

            [PreserveSig]
            int GetConnectionInterface(out Guid iid);


            [PreserveSig]
            int GetConnectionPointContainer(
                [MarshalAs(UnmanagedType.Interface)]
            ref IConnectionPointContainer pContainer);


            [PreserveSig]
            int Advise(
                   [In, MarshalAs(UnmanagedType.Interface)] 
                  object pUnkSink,
                 ref int cookie);


            [PreserveSig]
            int Unadvise(

                     int cookie);

            [PreserveSig]
            int EnumConnections(out object pEnum);

        }

        [ComImport(), Guid("0000010A-0000-0000-C000-000000000046"), InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
        public interface IPersistStorage
        {
            void GetClassID(
                   [Out] 
                  out Guid pClassID);

            [PreserveSig]
            int IsDirty();

            void InitNew(IStorage pstg);

            [PreserveSig]
            int Load(IStorage pstg);

            void Save(IStorage pStgSave, bool fSameAsLoad);

            void SaveCompleted(IStorage pStgNew);

            void HandsOffStorage();
        }

        [ComImport(), Guid("00020404-0000-0000-C000-000000000046"), InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
        public interface IEnumVariant
        {
            [PreserveSig]
            int Next(
                   [In, MarshalAs(UnmanagedType.U4)] 
                 int celt,
                   [In, Out] 
                 IntPtr rgvar,
                   [Out, MarshalAs(UnmanagedType.LPArray)]
                 int[] pceltFetched);

            void Skip(
                   [In, MarshalAs(UnmanagedType.U4)]
                 int celt);

            void Reset();

            void Clone(
                   [Out, MarshalAs(UnmanagedType.LPArray)] 
                   NativeCOM.IEnumVariant[] ppenum);
        }

        [ComImport(), Guid("00000104-0000-0000-C000-000000000046"), InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
        public interface IEnumOLEVERB
        {


            [PreserveSig]
            int Next(
                   [MarshalAs(UnmanagedType.U4)]
                int celt,
                   [Out]
                NativeMethods.tagOLEVERB rgelt,
                   [Out, MarshalAs(UnmanagedType.LPArray)]
                int[] pceltFetched);

            [PreserveSig]
            int Skip(
                   [In, MarshalAs(UnmanagedType.U4)]
                 int celt);


            void Reset();


            void Clone(
               out IEnumOLEVERB ppenum);
        }

        // INTERFACE: IEnumACString

        // This interface was implemented to return autocomplete strings
        // into the caller's buffer (to reduce the number of memory allocations 
        // A sort index is also returned to control the order of items displayed 
        // by autocomplete.  The sort index should be set to zero if unused
        // The NextItem method increments the current index by one (similar to Next 
        // when one item is requested

        //public interface IEnumString
        //Do not declare IEnumString here -- use IEnumString from interopservices. 
        // even if it looks like it works, if you declaring the marshalling incorrectly, it will barf on appverifier.


        //-------------------------------------------------------------------------
        // IAutoComplete interface 
        // [Member functions]
        // IAutoComplete::Init(hwndEdit, punkACL, pwszRegKeyPath, pwszQuickComplete)
        // This function initializes an AutoComplete object, telling it
        // what control to subclass, and what list of strings to process. 
        // IAutoComplete::Enable(fEnable)
        // This function enables or disables the AutoComplete functionality. 
        //-------------------------------------------------------------------------- 

        [SuppressUnmanagedCodeSecurity, ComImport(), Guid("00bb2762-6a77-11d0-a535-00c04fd7d062"), InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
        public interface IAutoComplete
        {

            int Init(
                    [In] HandleRef hwndEdit,          // hwnd of editbox or editbox deriviative.
                    [In] System.Runtime.InteropServices.ComTypes.IEnumString punkACL,          // Pointer to object containing string to complete from. (IEnumString *) 
                    [In] string pwszRegKeyPath,       // 
                    [In] string pwszQuickComplete
                    );
            void Enable([In] bool fEnable);            // Is it enabled?
        }


        [SuppressUnmanagedCodeSecurity, ComImport(), Guid("EAC04BC0-3791-11d2-BB95-0060977B464C"), InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]

        public interface IAutoComplete2
        {
            int Init(
                    [In] HandleRef hwndEdit,          // hwnd of editbox or editbox deriviative.
                    [In] System.Runtime.InteropServices.ComTypes.IEnumString punkACL,          // Pointer to object containing string to complete from. (IEnumString *)
                    [In] string pwszRegKeyPath,       //
                    [In] string pwszQuickComplete
                    );
            void Enable([In] bool fEnable);            // Is it enabled? 

            int SetOptions([In] int dwFlag);
            void GetOptions([Out] IntPtr pdwFlag);
        }

        [SuppressUnmanagedCodeSecurity, ComImport(), Guid("0000000C-0000-0000-C000-000000000046"), InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
        public interface IStream
        {

            int Read(

                    IntPtr buf,

                    int len);


            int Write(

                    IntPtr buf,

                    int len);

            [return: MarshalAs(UnmanagedType.I8)]
            long Seek(
                   [In, MarshalAs(UnmanagedType.I8)]
                 long dlibMove,

                    int dwOrigin);


            void SetSize(
                   [In, MarshalAs(UnmanagedType.I8)]
                 long libNewSize);

            [return: MarshalAs(UnmanagedType.I8)]
            long CopyTo(
                   [In, MarshalAs(UnmanagedType.Interface)] 
                  NativeCOM.IStream pstm,
                   [In, MarshalAs(UnmanagedType.I8)]
                 long cb,
                   [Out, MarshalAs(UnmanagedType.LPArray)]
                 long[] pcbRead);


            void Commit(

                    int grfCommitFlags);


            void Revert();


            void LockRegion(
                   [In, MarshalAs(UnmanagedType.I8)]
                 long libOffset,
                   [In, MarshalAs(UnmanagedType.I8)] 
                 long cb,

                    int dwLockType);


            void UnlockRegion(
                   [In, MarshalAs(UnmanagedType.I8)]
                 long libOffset,
                   [In, MarshalAs(UnmanagedType.I8)] 
                 long cb,

                    int dwLockType);


            void Stat(
                    [Out]
                 NativeMethods.STATSTG pStatstg,
                    int grfStatFlag);

            [return: MarshalAs(UnmanagedType.Interface)]
            NativeCOM.IStream Clone();
        }

        public abstract class CharBuffer
        {

            public static CharBuffer CreateBuffer(int size)
            {
                if (Marshal.SystemDefaultCharSize == 1)
                {
                    return new AnsiCharBuffer(size);
                }
                return new UnicodeCharBuffer(size);
            }

            public abstract IntPtr AllocCoTaskMem();
            public abstract string GetString();
            public abstract void PutCoTaskMem(IntPtr ptr);
            public abstract void PutString(string s);
        }

        public class AnsiCharBuffer : CharBuffer
        {

            public byte[] buffer;
            public int offset;

            public AnsiCharBuffer(int size)
            {
                buffer = new byte[size];
            }

            public override IntPtr AllocCoTaskMem()
            {
                IntPtr result = Marshal.AllocCoTaskMem(buffer.Length);
                Marshal.Copy(buffer, 0, result, buffer.Length);
                return result;
            }

            public override string GetString()
            {
                int i = offset;
                while (i < buffer.Length && buffer[i] != 0)
                    i++;
                string result = Encoding.Default.GetString(buffer, offset, i - offset);
                if (i < buffer.Length)
                    i++;
                offset = i;
                return result;
            }

            public override void PutCoTaskMem(IntPtr ptr)
            {
                Marshal.Copy(ptr, buffer, 0, buffer.Length);
                offset = 0;
            }

            public override void PutString(string s)
            {
                byte[] bytes = Encoding.Default.GetBytes(s);
                int count = Math.Min(bytes.Length, buffer.Length - offset);
                Array.Copy(bytes, 0, buffer, offset, count);
                offset += count;
                if (offset < buffer.Length) buffer[offset++] = 0;
            }
        }

        public class UnicodeCharBuffer : CharBuffer
        {

            public char[] buffer;
            public int offset;

            public UnicodeCharBuffer(int size)
            {
                buffer = new char[size];
            }

            public override IntPtr AllocCoTaskMem()
            {
                IntPtr result = Marshal.AllocCoTaskMem(buffer.Length * 2);
                Marshal.Copy(buffer, 0, result, buffer.Length);
                return result;
            }

            public override String GetString()
            {
                int i = offset;
                while (i < buffer.Length && buffer[i] != 0) i++;
                string result = new string(buffer, offset, i - offset);
                if (i < buffer.Length) i++;
                offset = i;
                return result;
            }

            public override void PutCoTaskMem(IntPtr ptr)
            {
                Marshal.Copy(ptr, buffer, 0, buffer.Length);
                offset = 0;
            }

            public override void PutString(string s)
            {
                int count = Math.Min(s.Length, buffer.Length - offset);
                s.CopyTo(0, buffer, offset, count);
                offset += count;
                if (offset < buffer.Length) buffer[offset++] = (char)0;
            }
        }

        public class ComStreamFromDataStream : IStream
        {
            protected Stream dataStream;

            // to support seeking ahead of the stream length... 
            private long virtualPosition = -1;


            [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
            public ComStreamFromDataStream(Stream dataStream)
            {
                if (dataStream == null) throw new ArgumentNullException("dataStream");
                this.dataStream = dataStream;
            }

            private void ActualizeVirtualPosition()
            {
                if (virtualPosition == -1) return;

                if (virtualPosition > dataStream.Length)
                    dataStream.SetLength(virtualPosition);

                dataStream.Position = virtualPosition;

                virtualPosition = -1;
            }

            public IStream Clone()
            {
                NotImplemented();
                return null;
            }

            public void Commit(int grfCommitFlags)
            {
                dataStream.Flush();
                // Extend the length of the file if needed.
                ActualizeVirtualPosition();
            }

            public long CopyTo(IStream pstm, long cb, long[] pcbRead)
            {
                int bufsize = 4096; // one page 
                IntPtr buffer = Marshal.AllocHGlobal(bufsize);
                if (buffer == IntPtr.Zero) throw new OutOfMemoryException();
                long written = 0;
                try
                {
                    while (written < cb)
                    {
                        int toRead = bufsize;
                        if (written + toRead > cb) toRead = (int)(cb - written);
                        int read = Read(buffer, toRead);
                        if (read == 0) break;
                        if (pstm.Write(buffer, read) != read)
                        {
                            throw EFail("Wrote an incorrect number of bytes");
                        }
                        written += read;
                    }
                }
                finally
                {
                    Marshal.FreeHGlobal(buffer);
                }
                if (pcbRead != null && pcbRead.Length > 0)
                {
                    pcbRead[0] = written;
                }

                return written;
            }

            public Stream GetDataStream()
            {
                return dataStream;
            }

            public void LockRegion(long libOffset, long cb, int dwLockType)
            {
            }

            protected static ExternalException EFail(string msg)
            {
                ExternalException e = new ExternalException(msg, NativeMethods.E_FAIL);
                throw e;
            }

            protected static void NotImplemented()
            {
                ExternalException e = new ExternalException("UnsafeNativeMethodsNotImplemented", NativeMethods.E_NOTIMPL);
                throw e;
            }

            public int Read(IntPtr buf, /* cpr: int offset,*/  int length)
            {
                //        System.Text.Out.WriteLine("IStream::Read(" + length + ")");
                byte[] buffer = new byte[length];
                int count = Read(buffer, length);
                Marshal.Copy(buffer, 0, buf, count);
                return count;
            }

            public int Read(byte[] buffer, /* cpr: int offset,*/  int length)
            {
                ActualizeVirtualPosition();
                return dataStream.Read(buffer, 0, length);
            }

            public void Revert()
            {
                NotImplemented();
            }

            public long Seek(long offset, int origin)
            {
                // Console.WriteLine("IStream::Seek("+ offset + ", " + origin + ")");
                long pos = virtualPosition;
                if (virtualPosition == -1)
                {
                    pos = dataStream.Position;
                }
                long len = dataStream.Length;
                switch (origin)
                {
                    case NativeMethods.STREAM_SEEK_SET:
                        if (offset <= len)
                        {
                            dataStream.Position = offset;
                            virtualPosition = -1;
                        }
                        else
                        {
                            virtualPosition = offset;
                        }
                        break;
                    case NativeMethods.STREAM_SEEK_END:
                        if (offset <= 0)
                        {
                            dataStream.Position = len + offset;
                            virtualPosition = -1;
                        }
                        else
                        {
                            virtualPosition = len + offset;
                        }
                        break;
                    case NativeMethods.STREAM_SEEK_CUR:
                        if (offset + pos <= len)
                        {
                            dataStream.Position = pos + offset;
                            virtualPosition = -1;
                        }
                        else
                        {
                            virtualPosition = offset + pos;
                        }
                        break;
                }
                if (virtualPosition != -1)
                {
                    return virtualPosition;
                }
                else
                {
                    return dataStream.Position;
                }
            }

            public void SetSize(long value)
            {
                dataStream.SetLength(value);
            }

            public void Stat(NativeMethods.STATSTG pstatstg, int grfStatFlag)
            {
                pstatstg.type = 2; // STGTY_STREAM
                pstatstg.cbSize = dataStream.Length;
                pstatstg.grfLocksSupported = 2; //LOCK_EXCLUSIVE 
            }

            public void UnlockRegion(long libOffset, long cb, int dwLockType)
            {
            }

            public int Write(IntPtr buf, /* cpr: int offset,*/ int length)
            {
                byte[] buffer = new byte[length];
                Marshal.Copy(buf, buffer, 0, length);
                return Write(buffer, length);
            }

            public int Write(byte[] buffer, /* cpr: int offset,*/ int length)
            {
                ActualizeVirtualPosition();
                dataStream.Write(buffer, 0, length);
                return length;
            }
        }
        [ComImport(), Guid("0000000B-0000-0000-C000-000000000046"), InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
        public interface IStorage
        {

            [return: MarshalAs(UnmanagedType.Interface)]
            NativeCOM.IStream CreateStream(
                  [In, MarshalAs(UnmanagedType.BStr)]
                 string pwcsName,
                  [In, MarshalAs(UnmanagedType.U4)]
                 int grfMode,
                  [In, MarshalAs(UnmanagedType.U4)] 
                 int reserved1,
                  [In, MarshalAs(UnmanagedType.U4)] 
                 int reserved2);

            [return: MarshalAs(UnmanagedType.Interface)]
            NativeCOM.IStream OpenStream(
                  [In, MarshalAs(UnmanagedType.BStr)]
                 string pwcsName,

                   IntPtr reserved1,
                  [In, MarshalAs(UnmanagedType.U4)] 
                 int grfMode,
                  [In, MarshalAs(UnmanagedType.U4)]
                 int reserved2);

            [return: MarshalAs(UnmanagedType.Interface)]
            NativeCOM.IStorage CreateStorage(
                  [In, MarshalAs(UnmanagedType.BStr)] 
                 string pwcsName,
                  [In, MarshalAs(UnmanagedType.U4)] 
                 int grfMode,
                  [In, MarshalAs(UnmanagedType.U4)]
                 int reserved1,
                  [In, MarshalAs(UnmanagedType.U4)] 
                 int reserved2);

            [return: MarshalAs(UnmanagedType.Interface)]
            NativeCOM.IStorage OpenStorage(
                  [In, MarshalAs(UnmanagedType.BStr)] 
                 string pwcsName,

                   IntPtr pstgPriority,   // must be null
                  [In, MarshalAs(UnmanagedType.U4)] 
                 int grfMode,

                   IntPtr snbExclude,
                  [In, MarshalAs(UnmanagedType.U4)]
                 int reserved);


            void CopyTo(

                    int ciidExclude,
                   [In, MarshalAs(UnmanagedType.LPArray)] 
                 Guid[] pIIDExclude,

                    IntPtr snbExclude,
                   [In, MarshalAs(UnmanagedType.Interface)]
                 NativeCOM.IStorage stgDest);


            void MoveElementTo(
                   [In, MarshalAs(UnmanagedType.BStr)] 
                 string pwcsName,
                   [In, MarshalAs(UnmanagedType.Interface)]
                 NativeCOM.IStorage stgDest,
                   [In, MarshalAs(UnmanagedType.BStr)]
                 string pwcsNewName,
                   [In, MarshalAs(UnmanagedType.U4)]
                 int grfFlags);


            void Commit(

                    int grfCommitFlags);


            void Revert();


            void EnumElements(
                   [In, MarshalAs(UnmanagedType.U4)] 
                 int reserved1,
                // void * 
                    IntPtr reserved2,
                   [In, MarshalAs(UnmanagedType.U4)]
                 int reserved3,
                   [Out, MarshalAs(UnmanagedType.Interface)] 
                 out object ppVal);                     // IEnumSTATSTG


            void DestroyElement(
                   [In, MarshalAs(UnmanagedType.BStr)] 
                 string pwcsName);


            void RenameElement(
                   [In, MarshalAs(UnmanagedType.BStr)]
                 string pwcsOldName,
                   [In, MarshalAs(UnmanagedType.BStr)] 
                 string pwcsNewName);


            void SetElementTimes(
                   [In, MarshalAs(UnmanagedType.BStr)]
                 string pwcsName,
                   [In]
                 NativeMethods.FILETIME pctime,
                   [In] 
                 NativeMethods.FILETIME patime,
                   [In] 
                 NativeMethods.FILETIME pmtime);


            void SetClass(
                   [In]
                 ref Guid clsid);


            void SetStateBits(

                    int grfStateBits,

                    int grfMask);


            void Stat(
                   [Out]
                 NativeMethods.STATSTG pStatStg,
                    int grfStatFlag);
        }
        [ComImport(), Guid("B196B28F-BAB4-101A-B69C-00AA00341D07"), InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
        public interface IClassFactory2
        {


            void CreateInstance(
                   [In, MarshalAs(UnmanagedType.Interface)] 
                  object unused,
                           [In]
                  ref Guid refiid,
                   [Out, MarshalAs(UnmanagedType.LPArray)] 
                  object[] ppunk);


            void LockServer(

                    int fLock);


            void GetLicInfo(
                   [Out]
                  NativeMethods.tagLICINFO licInfo);


            void RequestLicKey(
                   [In, MarshalAs(UnmanagedType.U4)]
                 int dwReserved,
                   [Out, MarshalAs(UnmanagedType.LPArray)]
                   string[] pBstrKey);


            void CreateInstanceLic(
                   [In, MarshalAs(UnmanagedType.Interface)]
                  object pUnkOuter,
                   [In, MarshalAs(UnmanagedType.Interface)]
                  object pUnkReserved,
                           [In]
                  ref Guid riid,
                   [In, MarshalAs(UnmanagedType.BStr)]
                  string bstrKey,
                   [Out, MarshalAs(UnmanagedType.Interface)] 
                  out object ppVal);
        }
        [SuppressUnmanagedCodeSecurity, ComImport(),
        Guid("B196B284-BAB4-101A-B69C-00AA00341D07"),
        InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
        public interface IConnectionPointContainer
        {

            [return: MarshalAs(UnmanagedType.Interface)]
            object EnumConnectionPoints();

            [PreserveSig]
            int FindConnectionPoint([In] ref Guid guid, [Out, MarshalAs(UnmanagedType.Interface)]out IConnectionPoint ppCP);

        }

        [ComImport(), Guid("B196B285-BAB4-101A-B69C-00AA00341D07"), InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
        public interface IEnumConnectionPoints
        {
            [PreserveSig]
            int Next(int cConnections, out IConnectionPoint pCp, out int pcFetched);

            [PreserveSig]
            int Skip(int cSkip);

            void Reset();

            IEnumConnectionPoints Clone();
        }


        [ComImport(), Guid("00020400-0000-0000-C000-000000000046"), InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
        public interface IDispatch
        {
            int GetTypeInfoCount();

            [return: MarshalAs(UnmanagedType.Interface)]
            ITypeInfo GetTypeInfo(
                   [In, MarshalAs(UnmanagedType.U4)] 
                 int iTInfo,
                   [In, MarshalAs(UnmanagedType.U4)] 
                 int lcid);


            [PreserveSig]
            int GetIDsOfNames(
                   [In] 
                 ref Guid riid,
                   [In, MarshalAs(UnmanagedType.LPArray)]
                 string[] rgszNames,
                   [In, MarshalAs(UnmanagedType.U4)]
                 int cNames,
                   [In, MarshalAs(UnmanagedType.U4)]
                 int lcid,
                   [Out, MarshalAs(UnmanagedType.LPArray)]
                 int[] rgDispId);


            [PreserveSig]
            int Invoke(

                    int dispIdMember,
                   [In] 
                 ref Guid riid,
                   [In, MarshalAs(UnmanagedType.U4)] 
                 int lcid,
                   [In, MarshalAs(UnmanagedType.U4)]
                 int dwFlags,
                   [Out, In]
                  NativeMethods.tagDISPPARAMS pDispParams,
                   [Out, MarshalAs(UnmanagedType.LPArray)]
                  object[] pVarResult,
                   [Out, In]
                  NativeMethods.tagEXCEPINFO pExcepInfo,
                   [Out, MarshalAs(UnmanagedType.LPArray)] 
                  IntPtr[] pArgErr);

        }

        [ComImport(), Guid("00020401-0000-0000-C000-000000000046"), InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
        public interface ITypeInfo
        {
            [PreserveSig]
            int GetTypeAttr(ref IntPtr pTypeAttr);


            [PreserveSig]
            int GetTypeComp(
                    [Out, MarshalAs(UnmanagedType.LPArray)]
                       NativeCOM.ITypeComp[] ppTComp);


            [PreserveSig]
            int GetFuncDesc(
                    [In, MarshalAs(UnmanagedType.U4)]
                     int index, ref IntPtr pFuncDesc);


            [PreserveSig]
            int GetVarDesc(
                   [In, MarshalAs(UnmanagedType.U4)]
                     int index, ref IntPtr pVarDesc);


            [PreserveSig]
            int GetNames(

                    int memid,
                   [Out, MarshalAs(UnmanagedType.LPArray)] 
                      string[] rgBstrNames,
                   [In, MarshalAs(UnmanagedType.U4)] 
                     int cMaxNames,
                   [Out, MarshalAs(UnmanagedType.LPArray)]
                      int[] pcNames);


            [PreserveSig]
            int GetRefTypeOfImplType(
                    [In, MarshalAs(UnmanagedType.U4)]
                     int index,
                    [Out, MarshalAs(UnmanagedType.LPArray)] 
                      int[] pRefType);


            [PreserveSig]
            int GetImplTypeFlags(
                    [In, MarshalAs(UnmanagedType.U4)] 
                     int index,
                    [Out, MarshalAs(UnmanagedType.LPArray)] 
                      int[] pImplTypeFlags);


            [PreserveSig]
            int GetIDsOfNames(IntPtr rgszNames, int cNames, IntPtr pMemId);


            [PreserveSig]
            int Invoke();


            [PreserveSig]
            int GetDocumentation(

                     int memid,
                      ref string pBstrName,
                      ref string pBstrDocString,
                    [Out, MarshalAs(UnmanagedType.LPArray)] 
                      int[] pdwHelpContext,
                    [Out, MarshalAs(UnmanagedType.LPArray)]
                      string[] pBstrHelpFile);


            [PreserveSig]
            int GetDllEntry(

                     int memid,

                      NativeMethods.tagINVOKEKIND invkind,
                    [Out, MarshalAs(UnmanagedType.LPArray)] 
                      string[] pBstrDllName,
                    [Out, MarshalAs(UnmanagedType.LPArray)]
                      string[] pBstrName,
                    [Out, MarshalAs(UnmanagedType.LPArray)] 
                      short[] pwOrdinal);


            [PreserveSig]
            int GetRefTypeInfo(

                    IntPtr hreftype,
                    ref ITypeInfo pTypeInfo);


            [PreserveSig]
            int AddressOfMember();


            [PreserveSig]
            int CreateInstance(
                    [In]
                      ref Guid riid,
                    [Out, MarshalAs(UnmanagedType.LPArray)]
                      object[] ppvObj);


            [PreserveSig]
            int GetMops(

                    int memid,
                   [Out, MarshalAs(UnmanagedType.LPArray)] 
                     string[] pBstrMops);


            [PreserveSig]
            int GetContainingTypeLib(
                    [Out, MarshalAs(UnmanagedType.LPArray)]
                       NativeCOM.ITypeLib[] ppTLib,
                    [Out, MarshalAs(UnmanagedType.LPArray)]
                      int[] pIndex);

            [PreserveSig]
            void ReleaseTypeAttr(IntPtr typeAttr);

            [PreserveSig]
            void ReleaseFuncDesc(IntPtr funcDesc);

            [PreserveSig]
            void ReleaseVarDesc(IntPtr varDesc);

        }
        [ComImport(), Guid("00020403-0000-0000-C000-000000000046"), InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
        public interface ITypeComp
        {
            void RemoteBind(
                   [In, MarshalAs(UnmanagedType.LPWStr)]
                 string szName,
                   [In, MarshalAs(UnmanagedType.U4)]
                 int lHashVal,
                   [In, MarshalAs(UnmanagedType.U2)]
                 short wFlags,
                   [Out, MarshalAs(UnmanagedType.LPArray)] 
                   NativeCOM.ITypeInfo[] ppTInfo,
                   [Out, MarshalAs(UnmanagedType.LPArray)] 
                  NativeMethods.tagDESCKIND[] pDescKind,
                   [Out, MarshalAs(UnmanagedType.LPArray)]
                   NativeMethods.tagFUNCDESC[] ppFuncDesc,
                   [Out, MarshalAs(UnmanagedType.LPArray)] 
                   NativeMethods.tagVARDESC[] ppVarDesc,
                   [Out, MarshalAs(UnmanagedType.LPArray)] 
                   NativeCOM.ITypeComp[] ppTypeComp,
                   [Out, MarshalAs(UnmanagedType.LPArray)]
                  int[] pDummy);


            void RemoteBindType(
                   [In, MarshalAs(UnmanagedType.LPWStr)] 
                 string szName,
                   [In, MarshalAs(UnmanagedType.U4)] 
                 int lHashVal,
                   [Out, MarshalAs(UnmanagedType.LPArray)]
                   NativeCOM.ITypeInfo[] ppTInfo);

        }

        [ComImport(), Guid("00020402-0000-0000-C000-000000000046"), InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
        public interface ITypeLib
        {

            void RemoteGetTypeInfoCount(
                   [Out, MarshalAs(UnmanagedType.LPArray)]
                  int[] pcTInfo);


            void GetTypeInfo(
                   [In, MarshalAs(UnmanagedType.U4)] 
                 int index,
                   [Out, MarshalAs(UnmanagedType.LPArray)] 
                   NativeCOM.ITypeInfo[] ppTInfo);


            void GetTypeInfoType(
                   [In, MarshalAs(UnmanagedType.U4)]
                 int index,
                   [Out, MarshalAs(UnmanagedType.LPArray)] 
                  NativeMethods.tagTYPEKIND[] pTKind);


            void GetTypeInfoOfGuid(
                   [In] 
                  ref Guid guid,
                   [Out, MarshalAs(UnmanagedType.LPArray)]
                   NativeCOM.ITypeInfo[] ppTInfo);


            void RemoteGetLibAttr(
                   IntPtr ppTLibAttr,
                   [Out, MarshalAs(UnmanagedType.LPArray)]
                  int[] pDummy);


            void GetTypeComp(
                   [Out, MarshalAs(UnmanagedType.LPArray)] 
                   NativeCOM.ITypeComp[] ppTComp);


            void RemoteGetDocumentation(

                    int index,
                   [In, MarshalAs(UnmanagedType.U4)]
                 int refPtrFlags,
                   [Out, MarshalAs(UnmanagedType.LPArray)] 
                 string[] pBstrName,
                   [Out, MarshalAs(UnmanagedType.LPArray)] 
                 string[] pBstrDocString,
                   [Out, MarshalAs(UnmanagedType.LPArray)]
                 int[] pdwHelpContext,
                   [Out, MarshalAs(UnmanagedType.LPArray)]
                 string[] pBstrHelpFile);


            void RemoteIsName(
                   [In, MarshalAs(UnmanagedType.LPWStr)] 
                 string szNameBuf,
                   [In, MarshalAs(UnmanagedType.U4)]
                 int lHashVal,
                   [Out, MarshalAs(UnmanagedType.LPArray)]
                 IntPtr[] pfName,
                   [Out, MarshalAs(UnmanagedType.LPArray)]
                 string[] pBstrLibName);


            void RemoteFindName(
                   [In, MarshalAs(UnmanagedType.LPWStr)]
                 string szNameBuf,
                   [In, MarshalAs(UnmanagedType.U4)]
                 int lHashVal,
                   [Out, MarshalAs(UnmanagedType.LPArray)]
                 NativeCOM.ITypeInfo[] ppTInfo,
                   [Out, MarshalAs(UnmanagedType.LPArray)]
                 int[] rgMemId,
                   [In, Out, MarshalAs(UnmanagedType.LPArray)] 
                 short[] pcFound,
                   [Out, MarshalAs(UnmanagedType.LPArray)] 
                 string[] pBstrLibName);


            void LocalReleaseTLibAttr();
        }

        [ComImport(),
         Guid("DF0B3D60-548F-101B-8E65-08002B2BD119"),
         InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
        public interface ISupportErrorInfo
        {

            int InterfaceSupportsErrorInfo(
                    [In] ref Guid riid);


        }

        [ComImport(),
         Guid("1CF2B120-547D-101B-8E65-08002B2BD119"),
         InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
        public interface IErrorInfo
        {

            [System.Security.SuppressUnmanagedCodeSecurityAttribute()]
            [PreserveSig]
            int GetGUID(
                       [Out]
                   out Guid pguid);

            [System.Security.SuppressUnmanagedCodeSecurityAttribute()]
            [PreserveSig]
            int GetSource(
                         [In, Out, MarshalAs(UnmanagedType.BStr)]
                     ref string pBstrSource);

            [System.Security.SuppressUnmanagedCodeSecurityAttribute()]
            [PreserveSig]
            int GetDescription(
                              [In, Out, MarshalAs(UnmanagedType.BStr)]
                          ref string pBstrDescription);

            [System.Security.SuppressUnmanagedCodeSecurityAttribute()]
            [PreserveSig]
            int GetHelpFile(
                           [In, Out, MarshalAs(UnmanagedType.BStr)]
                       ref string pBstrHelpFile);

            [System.Security.SuppressUnmanagedCodeSecurityAttribute()]
            [PreserveSig]
            int GetHelpContext(
                              [In, Out, MarshalAs(UnmanagedType.U4)]
                          ref int pdwHelpContext);

        }

        [StructLayout(LayoutKind.Sequential)]
        public sealed class tagQACONTAINER
        {
            [MarshalAs(UnmanagedType.U4)]
            public int cbSize = Marshal.SizeOf(typeof(tagQACONTAINER));

            public NativeCOM.IOleClientSite pClientSite;

            [MarshalAs(UnmanagedType.Interface)]
            public object pAdviseSink = null;

            public NativeCOM.IPropertyNotifySink pPropertyNotifySink;

            [MarshalAs(UnmanagedType.Interface)]
            public object pUnkEventSink = null;

            [MarshalAs(UnmanagedType.U4)]
            public int dwAmbientFlags;

            [MarshalAs(UnmanagedType.U4)]
            public UInt32 colorFore;

            [MarshalAs(UnmanagedType.U4)]
            public UInt32 colorBack;

            [MarshalAs(UnmanagedType.Interface)]
            public object pFont;

            [MarshalAs(UnmanagedType.Interface)]
            public object pUndoMgr = null;

            [MarshalAs(UnmanagedType.U4)]
            public int dwAppearance;

            public int lcid;

            public IntPtr hpal = IntPtr.Zero;

            [MarshalAs(UnmanagedType.Interface)]
            public object pBindHost = null;

        }

        [StructLayout(LayoutKind.Sequential)/*leftover(noAutoOffset)*/]
        public sealed class tagQACONTROL
        {
            [MarshalAs(UnmanagedType.U4)/*leftover(offset=0, cbSize)*/]
            public int cbSize = Marshal.SizeOf(typeof(tagQACONTROL));

            [MarshalAs(UnmanagedType.U4)/*leftover(offset=4, dwMiscStatus)*/]
            public int dwMiscStatus = 0;

            [MarshalAs(UnmanagedType.U4)/*leftover(offset=8, dwViewStatus)*/]
            public int dwViewStatus = 0;

            [MarshalAs(UnmanagedType.U4)/*leftover(offset=12, dwEventCookie)*/]
            public int dwEventCookie = 0;

            [MarshalAs(UnmanagedType.U4)/*leftover(offset=16, dwPropNotifyCookie)*/]
            public int dwPropNotifyCookie = 0;

            [MarshalAs(UnmanagedType.U4)/*leftover(offset=20, dwPointerActivationPolicy)*/]
            public int dwPointerActivationPolicy = 0;

        }

        [ComImport(), Guid("0000000A-0000-0000-C000-000000000046"), InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
        public interface ILockBytes
        {


            void ReadAt(
                   [In, MarshalAs(UnmanagedType.U8)] 
                 long ulOffset,
                   [Out]
                 IntPtr pv,
                   [In, MarshalAs(UnmanagedType.U4)] 
                 int cb,
                   [Out, MarshalAs(UnmanagedType.LPArray)] 
                 int[] pcbRead);


            void WriteAt(
                   [In, MarshalAs(UnmanagedType.U8)]
                 long ulOffset,

                    IntPtr pv,
                   [In, MarshalAs(UnmanagedType.U4)] 
                 int cb,
                   [Out, MarshalAs(UnmanagedType.LPArray)]
                 int[] pcbWritten);


            void Flush();


            void SetSize(
                   [In, MarshalAs(UnmanagedType.U8)] 
                 long cb);


            void LockRegion(
                   [In, MarshalAs(UnmanagedType.U8)]
                 long libOffset,
                   [In, MarshalAs(UnmanagedType.U8)]
                 long cb,
                   [In, MarshalAs(UnmanagedType.U4)] 
                 int dwLockType);


            void UnlockRegion(
                   [In, MarshalAs(UnmanagedType.U8)]
                 long libOffset,
                   [In, MarshalAs(UnmanagedType.U8)]
                 long cb,
                   [In, MarshalAs(UnmanagedType.U4)] 
                 int dwLockType);


            void Stat(
                   [Out]
                  NativeMethods.STATSTG pstatstg,
                   [In, MarshalAs(UnmanagedType.U4)]
                 int grfStatFlag);

        }

        [StructLayout(LayoutKind.Sequential), SuppressUnmanagedCodeSecurity()]
        public class OFNOTIFY
        {
            // hdr was a by-value NMHDR structure
            public IntPtr hdr_hwndFrom = IntPtr.Zero;
            public IntPtr hdr_idFrom = IntPtr.Zero;
            public int hdr_code = 0;

            public IntPtr lpOFN = IntPtr.Zero;
            public IntPtr pszFile = IntPtr.Zero;
        }

        public static bool IsComObject(object o)
        {
            return Marshal.IsComObject(o);
        }

        public static int ReleaseComObject(object objToRelease)
        {
            return Marshal.ReleaseComObject(objToRelease);
        }

        [ReflectionPermission(SecurityAction.Assert, Unrestricted = true)]
        public static object PtrToStructure(IntPtr lparam, Type cls)
        {
            return Marshal.PtrToStructure(lparam, cls);
        }

        [ReflectionPermission(SecurityAction.Assert, Unrestricted = true)]
        public static void PtrToStructure(IntPtr lparam, object data)
        {
            Marshal.PtrToStructure(lparam, data);
        }

        public static int SizeOf(Type t)
        {
            return Marshal.SizeOf(t);
        }

        public static void ThrowExceptionForHR(int errorCode)
        {
            Marshal.ThrowExceptionForHR(errorCode);
        }


        public delegate int BrowseCallbackProc(
            IntPtr hwnd,
            int msg,
            IntPtr lParam,
            IntPtr lpData);

        [Flags]
        public enum BrowseInfos
        {
            NewDialogStyle = 0x0040,
            HideNewFolderButton = 0x0200
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public class BROWSEINFO
        {
            public IntPtr hwndOwner;
            public IntPtr pidlRoot; //LPCITEMIDLIST pidlRoot; // Root ITEMIDLIST
            public IntPtr pszDisplayName; //LPWSTR pszDisplayName; // Return display name of item selected.
            public string lpszTitle; //LPCWSTR lpszTitle; // text to go in the banner over the tree. 
            public int ulFlags; //UINT ulFlags; // Flags that control the return stuff
            public BrowseCallbackProc lpfn; //BFFCALLBACK lpfn; // Call back pointer 
            [SuppressMessage("Microsoft.Reliability", "CA2006:UseSafeHandleToEncapsulateNativeResources")]
            public IntPtr lParam; //LPARAM lParam; // extra info that's passed back in callbacks
            public int iImage; //int iImage; // output var: where to return the Image index.
        }

        [
        ComImport(),
        Guid("00000002-0000-0000-c000-000000000046"),
        System.Runtime.InteropServices.InterfaceTypeAttribute(System.Runtime.InteropServices.ComInterfaceType.InterfaceIsIUnknown),
        SuppressUnmanagedCodeSecurity()
        ]
        public interface IMalloc
        {
            [PreserveSig]
            IntPtr Alloc(int cb);

            [PreserveSig]
            IntPtr Realloc(IntPtr pv, int cb);

            [PreserveSig]
            void Free(IntPtr pv);

            [PreserveSig]
            int GetSize(IntPtr pv);

            [PreserveSig]
            int DidAlloc(IntPtr pv);

            [PreserveSig]
            void HeapMinimize();
        }

        [
        ComImport,
        Guid("00000126-0000-0000-C000-000000000046"),
        InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)
        ]
        public interface IRunnableObject
        {
            void GetRunningClass(out Guid guid);

            [PreserveSig]
            int Run(IntPtr lpBindContext);
            bool IsRunning();
            void LockRunning(bool fLock, bool fLastUnlockCloses);
            void SetContainedObject(bool fContained);
        }

        [ComVisible(true), ComImport(), Guid("B722BCC7-4E68-101B-A2BC-00AA00404770"), InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
        public interface IOleDocumentSite
        {

            [return: MarshalAs(UnmanagedType.I4)]
            [PreserveSig]
            int ActivateMe(
                 [In, MarshalAs(UnmanagedType.Interface)] 
                    IOleDocumentView pViewToActivate);

        }

        [ComVisible(true), Guid("B722BCC6-4E68-101B-A2BC-00AA00404770"), InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
        public interface IOleDocumentView
        {

            void SetInPlaceSite(
                 [In, MarshalAs(UnmanagedType.Interface)] 
                    IOleInPlaceSite pIPSite);

            [return: MarshalAs(UnmanagedType.Interface)]
            IOleInPlaceSite GetInPlaceSite();

            [return: MarshalAs(UnmanagedType.Interface)]
            object GetDocument();


            void SetRect(
                 [In] 
                    ref NativeMethods.RECT prcView);


            void GetRect(
                 [In, Out]
                    ref NativeMethods.RECT prcView);


            void SetRectComplex(
                 [In]
                    NativeMethods.RECT prcView,
                 [In]
                    NativeMethods.RECT prcHScroll,
                 [In]
                    NativeMethods.RECT prcVScroll,
                 [In] 
                    NativeMethods.RECT prcSizeBox);


            void Show(bool fShow);


            [PreserveSig]
            int UIActivate(bool fUIActivate);


            void Open();

            [PreserveSig]
            int Close(
                 [In, MarshalAs(UnmanagedType.U4)] 
                    int dwReserved);


            void SaveViewState(
                 [In, MarshalAs(UnmanagedType.Interface)] 
                    IStream pstm);


            void ApplyViewState(
                 [In, MarshalAs(UnmanagedType.Interface)]
                    IStream pstm);


            void Clone(
                 [In, MarshalAs(UnmanagedType.Interface)]
                    IOleInPlaceSite pIPSiteNew,
                 [Out, MarshalAs(UnmanagedType.LPArray)]
                    IOleDocumentView[] ppViewNew);


        }

        [
          ComImport,
          Guid("b722bcc5-4e68-101b-a2bc-00aa00404770"),
          InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)
          ]
        public interface IOleDocument
        {

            [PreserveSig]
            int CreateView(IOleInPlaceSite pIPSite,
                                IStream pstm,
                                          int dwReserved,
                                          out IOleDocumentView ppView);

            [PreserveSig]
            int GetDocMiscStatus(
                 out int pdwStatus);

            int EnumViews(
                 out object ppEnum,
                 out IOleDocumentView ppView);
        }

        [
            Guid("0000011e-0000-0000-C000-000000000046"), ComImport, InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)
        ]
        public interface IOleCache
        {
            int Cache(ref FORMATETC pformatetc, int advf);

            void Uncache(int dwConnection);

            object EnumCache(/*[out] IEnumSTATDATA **ppenumSTATDATA*/);

            void InitCache(IComDataObject pDataObject);

            void SetData(ref FORMATETC pformatetc, ref STGMEDIUM pmedium, bool fRelease);
        }





        [ComImport,
         TypeLibType(0x1050),
         Guid("618736E0-3C3D-11CF-810C-00AA00389B71"),
        ]
        public interface IAccessiblepublic
        {
            [return: MarshalAs(UnmanagedType.IDispatch)]
            [DispId(unchecked((int)0xFFFFEC78))]
            [TypeLibFunc(0x0040)]
            object get_accParent();

            [DispId(unchecked((int)0xFFFFEC77))]
            [TypeLibFunc(0x0040)]
            int get_accChildCount();

            [return: MarshalAs(UnmanagedType.IDispatch)]
            [TypeLibFunc(0x0040)]
            [DispId(unchecked((int)0xFFFFEC76))]
            object get_accChild([In][MarshalAs(UnmanagedType.Struct)] object varChild);

            [return: MarshalAs(UnmanagedType.BStr)]
            [DispId(unchecked((int)0xFFFFEC75))]
            [TypeLibFunc(0x0040)]
            string get_accName([In][Optional][MarshalAs(UnmanagedType.Struct)] object varChild);

            [return: MarshalAs(UnmanagedType.BStr)]
            [TypeLibFunc(0x0040)]
            [DispId(unchecked((int)0xFFFFEC74))]
            string get_accValue([In][Optional][MarshalAs(UnmanagedType.Struct)] object varChild);

            [return: MarshalAs(UnmanagedType.BStr)]
            [DispId(unchecked((int)0xFFFFEC73))]
            [TypeLibFunc(0x0040)]
            string get_accDescription([In][Optional][MarshalAs(UnmanagedType.Struct)] object varChild);

            [return: MarshalAs(UnmanagedType.Struct)]
            [DispId(unchecked((int)0xFFFFEC72))]
            [TypeLibFunc(0x0040)]
            object get_accRole([In][Optional][MarshalAs(UnmanagedType.Struct)] object varChild);

            [return: MarshalAs(UnmanagedType.Struct)]
            [TypeLibFunc(0x0040)]
            [DispId(unchecked((int)0xFFFFEC71))]
            object get_accState([In][Optional][MarshalAs(UnmanagedType.Struct)] object varChild);

            [return: MarshalAs(UnmanagedType.BStr)]
            [TypeLibFunc(0x0040)]
            [DispId(unchecked((int)0xFFFFEC70))]
            string get_accHelp([In][Optional][MarshalAs(UnmanagedType.Struct)] object varChild);

            [DispId(unchecked((int)0xFFFFEC6F))]
            [TypeLibFunc(0x0040)]
            int get_accHelpTopic([Out][MarshalAs(UnmanagedType.BStr)] out string pszHelpFile,
                                                        [In][Optional][MarshalAs(UnmanagedType.Struct)] object varChild);

            [return: MarshalAs(UnmanagedType.BStr)]
            [DispId(unchecked((int)0xFFFFEC6E))]
            [TypeLibFunc(0x0040)]
            string get_accKeyboardShortcut([In][Optional][MarshalAs(UnmanagedType.Struct)] object varChild);

            [return: MarshalAs(UnmanagedType.Struct)]
            [DispId(unchecked((int)0xFFFFEC6D))]
            [TypeLibFunc(0x0040)]
            object get_accFocus();

            [return: MarshalAs(UnmanagedType.Struct)]
            [DispId(unchecked((int)0xFFFFEC6C))]
            [TypeLibFunc(0x0040)]
            object get_accSelection();

            [return: MarshalAs(UnmanagedType.BStr)]
            [TypeLibFunc(0x0040)]
            [DispId(unchecked((int)0xFFFFEC6B))]
            string get_accDefaultAction([In][Optional][MarshalAs(UnmanagedType.Struct)] object varChild);

            [DispId(unchecked((int)0xFFFFEC6A))]
            [TypeLibFunc(0x0040)]
            void accSelect([In] int flagsSelect,
                           [In][Optional][MarshalAs(UnmanagedType.Struct)] object varChild);

            [DispId(unchecked((int)0xFFFFEC69))]
            [TypeLibFunc(0x0040)]
            void accLocation([Out] out int pxLeft,
                             [Out] out int pyTop,
                             [Out] out int pcxWidth,
                             [Out] out int pcyHeight,
                             [In][Optional][MarshalAs(UnmanagedType.Struct)] object varChild);

            [return: MarshalAs(UnmanagedType.Struct)]
            [TypeLibFunc(0x0040)]
            [DispId(unchecked((int)0xFFFFEC68))]
            object accNavigate([In] int navDir,
                               [In][Optional][MarshalAs(UnmanagedType.Struct)] object varStart);

            [return: MarshalAs(UnmanagedType.Struct)]
            [TypeLibFunc(0x0040)]
            [DispId(unchecked((int)0xFFFFEC67))]
            object accHitTest([In] int xLeft,
                              [In] int yTop);

            [TypeLibFunc(0x0040)]
            [DispId(unchecked((int)0xFFFFEC66))]
            void accDoDefaultAction([In][Optional][MarshalAs(UnmanagedType.Struct)] object varChild);

            [TypeLibFunc(0x0040)]
            [DispId(unchecked((int)0xFFFFEC75))]
            void set_accName([In][Optional][MarshalAs(UnmanagedType.Struct)] object varChild,
                                 [In][MarshalAs(UnmanagedType.BStr)] string pszName);

            [TypeLibFunc(0x0040)]
            [DispId(unchecked((int)0xFFFFEC74))]
            void set_accValue([In][Optional][MarshalAs(UnmanagedType.Struct)] object varChild,
                              [In][MarshalAs(UnmanagedType.BStr)] string pszValue);
        }

        [
        ComImport(),
        Guid("BEF6E002-A874-101A-8BBA-00AA00300CAB"),
        System.Runtime.InteropServices.InterfaceTypeAttribute(System.Runtime.InteropServices.ComInterfaceType.InterfaceIsIUnknown)]
        public interface IFont
        {

            [return: MarshalAs(UnmanagedType.BStr)]
            string GetName();

            void SetName(
                   [In, MarshalAs(UnmanagedType.BStr)] 
                      string pname);

            [return: MarshalAs(UnmanagedType.U8)]
            long GetSize();

            void SetSize(
                   [In, MarshalAs(UnmanagedType.U8)] 
                     long psize);

            [return: MarshalAs(UnmanagedType.Bool)]
            bool GetBold();

            void SetBold(
                   [In, MarshalAs(UnmanagedType.Bool)]
                     bool pbold);

            [return: MarshalAs(UnmanagedType.Bool)]
            bool GetItalic();

            void SetItalic(
                   [In, MarshalAs(UnmanagedType.Bool)] 
                     bool pitalic);

            [return: MarshalAs(UnmanagedType.Bool)]
            bool GetUnderline();

            void SetUnderline(
                   [In, MarshalAs(UnmanagedType.Bool)] 
                     bool punderline);

            [return: MarshalAs(UnmanagedType.Bool)]
            bool GetStrikethrough();

            void SetStrikethrough(
                   [In, MarshalAs(UnmanagedType.Bool)]
                     bool pstrikethrough);

            [return: MarshalAs(UnmanagedType.I2)]
            short GetWeight();

            void SetWeight(
                   [In, MarshalAs(UnmanagedType.I2)]
                     short pweight);

            [return: MarshalAs(UnmanagedType.I2)]
            short GetCharset();

            void SetCharset(
                   [In, MarshalAs(UnmanagedType.I2)]
                     short pcharset);

            IntPtr GetHFont();

            void Clone(
                      out NativeCOM.IFont ppfont);

            [System.Runtime.InteropServices.PreserveSig]
            int IsEqual(
                   [In, MarshalAs(UnmanagedType.Interface)]
                      NativeCOM.IFont pfontOther);

            void SetRatio(
                    int cyLogical,
                    int cyHimetric);

            void QueryTextMetrics(out IntPtr ptm);

            void AddRefHfont(
                    IntPtr hFont);

            void ReleaseHfont(
                    IntPtr hFont);

            void SetHdc(
                    IntPtr hdc);
        }

        [ComImport(), Guid("7BF80980-BF32-101A-8BBB-00AA00300CAB"), System.Runtime.InteropServices.InterfaceTypeAttribute(System.Runtime.InteropServices.ComInterfaceType.InterfaceIsIUnknown)]
        public interface IPicture
        {
            IntPtr GetHandle();

            IntPtr GetHPal();

            [return: MarshalAs(UnmanagedType.I2)]
            short GetPictureType();

            int GetWidth();

            int GetHeight();

            void Render(
               IntPtr hDC,
               int x,
               int y,
               int cx,
               int cy,
               int xSrc,
               int ySrc,
               int cxSrc,
               int cySrc,
               IntPtr rcBounds
               );

            void SetHPal(
                    IntPtr phpal);

            IntPtr GetCurDC();

            void SelectPicture(
                    IntPtr hdcIn,
                   [Out, MarshalAs(UnmanagedType.LPArray)]
                     IntPtr[] phdcOut,
                   [Out, MarshalAs(UnmanagedType.LPArray)]
                     IntPtr[] phbmpOut);

            [return: MarshalAs(UnmanagedType.Bool)]
            bool GetKeepOriginalFormat();

            void SetKeepOriginalFormat(
                   [In, MarshalAs(UnmanagedType.Bool)]
                     bool pfkeep);

            void PictureChanged();

            [PreserveSig]
            int SaveAsFile(
                   [In, MarshalAs(UnmanagedType.Interface)]
                     NativeCOM.IStream pstm,

                    int fSaveMemCopy,
                   [Out]
                     out int pcbSize);

            int GetAttributes();
        }

        [ComImport(), Guid("7BF80981-BF32-101A-8BBB-00AA00300CAB"), System.Runtime.InteropServices.InterfaceTypeAttribute(System.Runtime.InteropServices.ComInterfaceType.InterfaceIsIDispatch)]
        public interface IPictureDisp
        {
            IntPtr Handle { get; }

            IntPtr HPal { get; }

            short PictureType { get; }

            int Width { get; }

            int Height { get; }

            void Render(
                    IntPtr hdc,
                    int x,
                    int y,
                    int cx,
                    int cy,
                    int xSrc,
                    int ySrc,
                    int cxSrc,
                    int cySrc);
        }

        [SuppressUnmanagedCodeSecurity]
        public class ThemingScope
        {
            private static ACTCTX enableThemingActivationContext;
            private static IntPtr hActCtx;
            private static bool contextCreationSucceeded;

            private static bool IsContextActive()
            {
                IntPtr current = IntPtr.Zero;

                if (contextCreationSucceeded && GetCurrentActCtx(out current))
                {
                    return current == hActCtx;
                }
                return false;
            }

            public static IntPtr Activate()
            {
                IntPtr userCookie = IntPtr.Zero;

                if (UnsafeNativeMethods.UseVisualStyles() && contextCreationSucceeded && OSFeature.Feature.IsPresent(OSFeature.Themes))
                {
                    if (!IsContextActive())
                    {
                        if (!ActivateActCtx(hActCtx, out userCookie))
                        {
                            userCookie = IntPtr.Zero;
                        }
                    }
                }
                return userCookie;
            }

            public static IntPtr Deactivate(IntPtr userCookie)
            {
                if (userCookie != IntPtr.Zero && OSFeature.Feature.IsPresent(OSFeature.Themes))
                {
                    if (DeactivateActCtx(0, userCookie))
                    {
                        userCookie = IntPtr.Zero;
                    }
                }

                return userCookie;
            }

            public static bool CreateActivationContext(string dllPath, int nativeResourceManifestID)
            {
                lock (typeof(ThemingScope))
                {
                    if (!contextCreationSucceeded && OSFeature.Feature.IsPresent(OSFeature.Themes))
                    {

                        enableThemingActivationContext = new ACTCTX();

                        enableThemingActivationContext.cbSize = Marshal.SizeOf(typeof(ACTCTX));
                        enableThemingActivationContext.lpSource = dllPath;
                        enableThemingActivationContext.lpResourceName = (IntPtr)nativeResourceManifestID;
                        enableThemingActivationContext.dwFlags = ACTCTX_FLAG_RESOURCE_NAME_VALID;

                        hActCtx = CreateActCtx(ref enableThemingActivationContext);
                        contextCreationSucceeded = (hActCtx != new IntPtr(-1));
                    }

                    return contextCreationSucceeded;
                }
            }

            [DllImport(ExternDll.Kernel32)]
            private extern static IntPtr CreateActCtx(ref ACTCTX actctx);
            [DllImport(ExternDll.Kernel32)]
            private extern static bool ActivateActCtx(IntPtr hActCtx, out IntPtr lpCookie);
            [DllImport(ExternDll.Kernel32)]
            private extern static bool DeactivateActCtx(int dwFlags, IntPtr lpCookie);
            [DllImport(ExternDll.Kernel32)]
            private extern static bool GetCurrentActCtx(out IntPtr handle);

            private const int ACTCTX_FLAG_ASSEMBLY_DIRECTORY_VALID = 0x004;
            private const int ACTCTX_FLAG_RESOURCE_NAME_VALID = 0x008;

            private struct ACTCTX
            {
                public int cbSize;
                public uint dwFlags;
                public string lpSource;
                public ushort wProcessorArchitecture;
                public ushort wLangId;
                public string lpAssemblyDirectory;
                public IntPtr lpResourceName;
                public string lpApplicationName;
            }
        }

        [StructLayout(LayoutKind.Sequential), SuppressUnmanagedCodeSecurityAttribute()]
        public class PROCESS_INFORMATION
        {
            public IntPtr hProcess = IntPtr.Zero;
            public IntPtr hThread = IntPtr.Zero;
            public int dwProcessId = 0;
            public int dwThreadId = 0;

            ~PROCESS_INFORMATION()
            {
                Close();
            }

            [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
            public void Close()
            {
                if (hProcess != (IntPtr)0 && hProcess != (IntPtr)INVALID_HANDLE_VALUE)
                {
                    CloseHandle(new HandleRef(this, hProcess));
                    hProcess = INVALID_HANDLE_VALUE;
                }

                if (hThread != (IntPtr)0 && hThread != (IntPtr)INVALID_HANDLE_VALUE)
                {
                    CloseHandle(new HandleRef(this, hThread));
                    hThread = INVALID_HANDLE_VALUE;
                }
            }

            private static readonly IntPtr INVALID_HANDLE_VALUE = new IntPtr(-1);

            [DllImport(ExternDll.Kernel32, ExactSpelling = true, CharSet = CharSet.Auto, SetLastError = true)]
            private static extern bool CloseHandle(HandleRef handle);
        }

        [DllImport(ExternDll.Mscorwks, CharSet = CharSet.Unicode, PreserveSig = false, SetLastError = false, BestFitMapping = false, ExactSpelling = true)]
        public static extern void CorLaunchApplication(UInt32 hostType,
            string applicationFullName,
            int manifestPathsCount,
            string[] manifestPaths,
            int activationDataCount,
            string[] activationData,
            PROCESS_INFORMATION processInformation);

        [ComImport, InterfaceType(ComInterfaceType.InterfaceIsIUnknown), Guid("000214E6-0000-0000-C000-000000000046")]
        public interface IShellFolder
        {
            [PreserveSig]
            int ParseDisplayName(
                IntPtr hwnd,
                IntPtr pbc,
                [MarshalAs(UnmanagedType.LPWStr)] 
                string pszDisplayName,
                ref int pchEaten,
                out IntPtr ppidl,
                ref NativeMethods.SFGAOF pdwAttributes);

            [PreserveSig]
            int EnumObjects(
                IntPtr hwnd,
                NativeMethods.SHCONTF grfFlags,
                out IntPtr enumIDList);

            [PreserveSig]
            int BindToObject(
                IntPtr pidl,
                IntPtr pbc,
                ref Guid riid,
                out IntPtr ppv);

            [PreserveSig]
            int BindToStorage(
                IntPtr pidl,
                IntPtr pbc,
                ref Guid riid,
                out IntPtr ppv);

            [PreserveSig]
            int CompareIDs(
                IntPtr lParam,
                IntPtr pidl1,
                IntPtr pidl2);

            [PreserveSig]
            int CreateViewObject(
                IntPtr hwndOwner,
                Guid riid,
                out IntPtr ppv);

            [PreserveSig]
            int GetAttributesOf(
                uint cidl,
                [MarshalAs(UnmanagedType.LPArray)]
                IntPtr[] apidl,
                ref NativeMethods.SFGAOF rgfInOut);

            [PreserveSig]
            int GetUIObjectOf(
                IntPtr hwndOwner,
                int cidl,
                [MarshalAs(UnmanagedType.LPArray)]
                IntPtr[] apidl,
                ref Guid riid,
                IntPtr rgfReserved,
                out IntPtr ppv);

            [PreserveSig()]
            int GetDisplayNameOf(
                IntPtr pidl,
                NativeMethods.SHGNO uFlags,
                ref NativeMethods.STRRET lpName);

            [PreserveSig]
            int SetNameOf(
                IntPtr hwnd,
                IntPtr pidl,
                [MarshalAs(UnmanagedType.LPWStr)] 
                string pszName,
                NativeMethods.SHGNO uFlags,
                out IntPtr ppidlOut);
        }

        [ComImport, InterfaceType(ComInterfaceType.InterfaceIsIUnknown), Guid("000214f2-0000-0000-c000-000000000046")]
        public interface IEnumIDList
        {
            [PreserveSig]
            int Next(int celt, out IntPtr rgelt, out int fetched);
            [PreserveSig]
            int Skip(int celt);
            [PreserveSig]
            int Reset();
            [PreserveSig]
            int Clone(out IEnumIDList ppenum);
        }

        [ComImport, InterfaceType(ComInterfaceType.InterfaceIsIUnknown), Guid("000214e4-0000-0000-c000-000000000046")]
        public interface IContextMenu
        {
            [PreserveSig()]
            Int32 QueryContextMenu(IntPtr hmenu, int iMenu, int idCmdFirst, int idCmdLast, NativeMethods.CMF uFlags);
            [PreserveSig()]
            Int32 InvokeCommand(ref NativeMethods.CMINVOKECOMMANDINFOEX pici);
            [PreserveSig()]
            Int32 GetCommandString(int idCmd, int uFlags, int pwReserved, IntPtr pszName, int cchMax);
        }

        [ComImport, InterfaceType(ComInterfaceType.InterfaceIsIUnknown), Guid("000214f4-0000-0000-c000-000000000046")]
        public interface IContextMenu2
        {
            [PreserveSig()]
            Int32 QueryContextMenu(IntPtr hmenu, int iMenu, int idCmdFirst, int idCmdLast, NativeMethods.CMF uFlags);
            [PreserveSig()]
            Int32 InvokeCommand(ref NativeMethods.CMINVOKECOMMANDINFOEX info);
            [PreserveSig()]
            Int32 GetCommandString(int idcmd, NativeMethods.GCS uflags, int reserved,
                [MarshalAs(UnmanagedType.LPWStr)]
                StringBuilder commandstring,
                int cch);
            [PreserveSig]
            Int32 HandleMenuMsg(int uMsg, IntPtr wParam, IntPtr lParam);
        }

        [ComImport, InterfaceType(ComInterfaceType.InterfaceIsIUnknown), Guid("bcfce0a0-ec17-11d0-8d10-00a0c90f2719")]
        public interface IContextMenu3
        {
            [PreserveSig()]
            Int32 QueryContextMenu(IntPtr hmenu, int iMenu, int idCmdFirst, int idCmdLast, NativeMethods.CMF uFlags);
            [PreserveSig()]
            Int32 InvokeCommand(ref NativeMethods.CMINVOKECOMMANDINFOEX info);
            [PreserveSig()]
            Int32 GetCommandString(
                int idcmd,
                NativeMethods.GCS uflags,
                int reserved,
                [MarshalAs(UnmanagedType.LPWStr)]
                StringBuilder commandstring,
                int cch);
            [PreserveSig]
            Int32 HandleMenuMsg(
                int uMsg,
                IntPtr wParam,
                IntPtr lParam);
            [PreserveSig]
            Int32 HandleMenuMsg2(
                int uMsg,
                IntPtr wParam,
                IntPtr lParam,
                IntPtr plResult);
        }
        [ComImport(), Guid("BEF6E003-A874-101A-8BBA-00AA00300CAB"), InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIDispatch)]
        public interface IFontDisp
        {
            string Name { get; set; }
            long Size { get; set; }
            bool Bold { get; set; }
            bool Italic { get; set; }
            bool Underline { get; set; }
            bool Strikethrough { get; set; }
            short Weight { get; set; }
            short Charset { get; set; }
        }
    }
}
