//[License]
//tesseract-ocr - pache License, Version 2.0,  http://code.google.com/p/tesseract-ocr/
//Tesseract.dll - Apache License, Version 2.0,  https://github.com/charlesw/tesseract
//Sarafftwain.dll - GNU Lesser General Public License (LGPL),  https://sarafftwain.codeplex.com/
//SQLite tool box - Microsoft Public License (Ms-PL),  https://visualstudiogallery.msdn.microsoft.com/0e313dfd-be80-4afb-b5e9-6e74d369f7a1
//AForge.dll - GNU Lesser GPL,  http://www.aforgenet.com/ 

//PLOCR(팜스라이프 스캔) - GPL,  http://cafe.daum.net/pharm--poor
//PLObserver(팜스라이프 스캔 옵저버) - GPL,  http://cafe.daum.net/pharm--poor
//DocumentAnalysis(팜스라이프 스캔 환경설정) - GPL,  http://cafe.daum.net/pharm--poor
//PLDB(팜스라이프 스캔 DB 설정) - GPL,  http://cafe.daum.net/pharm--poor 

//http://cafe.daum.net/pharm--poor
//linuxbank@hanmail.net

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace PLObserver
{
    public class Handle
    {
        [DllImport("user32.dll")]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        public static extern IntPtr FindWindowEx(IntPtr parentHandle, IntPtr childAfter, string lclassName, string windowTitle);

        public static IntPtr Findchildchildwindow(IntPtr hParent, int index, string ChildClassName, string ChildChildClassName) // hParent 아래에 ChildClassName 을 가진 핸들을 index 수 만큼 루프를 돌리며 구하고, 구해진 핸들 또 아래에 ChildChildClassName 을 가진 핸들이 있으면, ChildChildClassName 의 부모 윈도우 핸들을 리턴한다.
        {
            if (index == 0)
                return hParent;
            else
            {
                int ct = 0;
                IntPtr hChild = IntPtr.Zero;
                IntPtr hChildChild = IntPtr.Zero;

                do
                {
                    hChild = FindWindowEx(hParent, hChild, ChildClassName, null);  // 주어진 hParent 아래에 주어진 ChildClassName 을 가진 윈도우를 루프를 돌리며 찾는다.
                    //    Console.WriteLine("주어진 핸들의 바로 아래 순환 핸들값:{0:X}", hChild.ToInt32());

                    hChildChild = FindWindowEx(hChild, hChildChild, ChildChildClassName, null);  // 찾은 ChildClassName 을 가진 윈도우 하위에 ChildChildClassName 을 가지고 있는 윈도우를 찾는다.
                    //      Console.WriteLine("아래 아래 순환 핸들값:{0:X}", hChildChild.ToInt32());

                    if (hChildChild != IntPtr.Zero) // ChildChildClassName 을 가지고 있는 원하는 TPanel 을 찾으면 루프를 탈출한다.
                    {
                        break;
                    }

                    if (hChild != IntPtr.Zero)
                    {
                        ++ct;
                    }
                }
                while (ct < index && hChild != IntPtr.Zero);
                {
                    return hChild;       // 찾아낸 원하는 TPanel 핸들을 리턴한다.               
                }
            }
        }
    }
}
