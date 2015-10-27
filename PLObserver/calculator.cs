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
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PLObserver
{
    class calculator
    {       
        public static IntPtr HandleSearch()
        {
            IntPtr hTFrmPrescriptionEdit = Handle.FindWindow("TFrmPrescriptionEdit", "처방조제"); // 처방조제 최상위 윈도우 핸들찾기     
            IntPtr hTPanelTop = Handle.Findchildchildwindow(hTFrmPrescriptionEdit, 10, "TPanel", "TwMaskEdit"); // 처방조제 최상위 바로 아래 TPanel 이 8개 있으므로 10번 정도 돌려보고 찾은 TPanel 중에 하위에 TwMaskEdit 이 있는 TPanel 의 핸들값을 찾는다.      

            return hTPanelTop;
        }


        public static int ProcessPLOCR()        // PLOCR 이 실행중인지 확인해서 실행이면 1 미실행이면 0을 반환한다.
        {
            int processTF = 0;
            Process[] processList = Process.GetProcessesByName("PLOCR");     // PLOCR 프로세스 탐색   
            processList = Process.GetProcessesByName("PLOCR");
     
            if (1 <= processList.Length)
            {
                processTF = 1;               
            }
            else
            {
                processTF = 0;                
            }

            return processTF;   
        }

        public static int ProcessPM()       // PM2000 이 실행중인지 확인해서 실행이면 1 미실행이면 0 을 반환한다.
        {
            int processTF = 0;
            Process[] processList = Process.GetProcessesByName("PM_PharmCharge");     // 처방조제 프로세스 탐색         
            processList = Process.GetProcessesByName("PM_PharmCharge");                                
         
            if (1 <= processList.Length)
            {
                processTF = 1;                
            }
            else
            {
                processTF = 0;
            }            
           
            return processTF;   
        }
    }
}
