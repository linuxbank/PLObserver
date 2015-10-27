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
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PLObserver
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            this.timer1.Enabled = true; 
            this.timer1.Interval = 2000;    
            this.timer1.Tick += new System.EventHandler(timer_tick);
        }

        private void timer_tick(object sender, EventArgs e)
        {
            int processPMTF = calculator.ProcessPM();
            IntPtr hTPanelTop = calculator.HandleSearch();  // hTPanelTop 은 처방조제창이 떳을 때 확인 가능한 핸들임
            int processPLOCRTF = calculator.ProcessPLOCR();

          //  MessageBox.Show(hTPanelTop.ToString());
            
            if (processPMTF == 1 && hTPanelTop != IntPtr.Zero && processPLOCRTF == 0)     // 처방조제 프로세스 있고, 처방조제창 핸들 있고, PLOCR 은 열려있지 않으면 PLOCR 을 실행시킨다.
            {
                System.Diagnostics.Process.Start(@"C:\Program Files\PLOCR\PLOCR.exe");                
            }             
            else  if (hTPanelTop == IntPtr.Zero) // 처방조제창 핸들이 없어지면 PLOCR 종료
            {
                Process[] myProcesses = Process.GetProcessesByName("PLOCR");    // 처방조제창을 나가면 PLOCR 을 종료시키고
                foreach (Process myProcess in myProcesses)
                {
                    myProcess.Kill();
                }
            }            
        }                     

        private void Form1_Load(object sender, EventArgs e)
        {
            this.ShowInTaskbar = false;     // 작업표시줄에 안보이게                        
        }

        private void killAll(object sender, EventArgs e)
        {
            Process[] myProcesses2 = Process.GetProcessesByName("PLOCR");    // PLOCR 을 종료시키고
            foreach (Process myProcess in myProcesses2)
            {
                myProcess.Kill();
            }

            Process[] myProcesses = Process.GetProcessesByName("PLObserver");       // 옵저버와 PLOCR 모두 종료시킨다.            
            foreach (Process myProcess in myProcesses)
            {
                myProcess.Kill();
            }
        }

        private void killPLOCR(object sender, EventArgs e)
        {
            Process[] myProcesses = Process.GetProcessesByName("PLOCR");       // PLOCR을 종료시킨다.
            foreach (Process myProcess in myProcesses)
            {
                myProcess.Kill();
            }
        }

        private void rerunPLOCR(object sender, EventArgs e)
        {
            Process[] myProcesses = Process.GetProcessesByName("PLOCR");    // PLOCR 을 종료시키고
            foreach (Process myProcess in myProcesses)
            {
                myProcess.Kill();
            }

            System.Diagnostics.Process.Start(@"C:\Program Files\PLOCR\PLOCR.exe");      // PLOCR 재실행
        }

        //private void rerunAll(object sender, EventArgs e)
        //{
        //    Process[] myProcesses2 = Process.GetProcessesByName("PLOCR");    // PLOCR 을 종료시키고
        //    foreach (Process myProcess in myProcesses2)
        //    {
        //        myProcess.Kill();
        //    }

        //    System.Diagnostics.Process.Start(@"C:\Program Files\PLOCR\PLOCR.exe");      // PLOCR 재실행

        //    System.Diagnostics.Process.Start(@"C:\Program Files\PLOCR\PLOCObserver.exe");      // 옵저버 재실행

        //    Process[] myProcesses = Process.GetProcessesByName("PLObserver");       // 옵저버 종료
        //    foreach (Process myProcess in myProcesses)
        //    {
        //        myProcess.Kill();
        //    }                      
        //}
    }
}
