using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Diagnostics;
using System.Windows.Forms;
using System.Runtime.InteropServices;

using DirectShowLib;
using System.Drawing.Imaging;
using Genghis.Windows.Forms;
using System.IO;

namespace WindowsFormsApp5
{
    public partial class Form1 : Form
    {
        public Form3 f3;
        // 그래프와 컨트롤 선언
        public IGraphBuilder pGraphBuilder = null;
        public IMediaControl pMediaControl = null;
        public IVideoWindow pVideoWindow = null;
        public IMediaPosition pMediaPosition = null;
        public ICaptureGraphBuilder2 pCaptureGraphBuilder2 = null;

        // 캡쳐에 필요한 필터 선언
        public IBaseFilter pSampleGrabberFilter = null;
        public ISampleGrabber pSampleGrabber = null;

        // 오디오 선언
        public IBasicAudio pBasicAudio = null;

        // 비디오의 사이즈, 넓이, 높이 구하기
        IBasicVideo pBasicVideo = null;
        public int Video_Width, Video_Height;

        // 그래프의 현상태를 담는 변수
        enum State { Playing, Paused, Stopped };
        State graphState;

        // 전체 영상 길이를 담기 위한 변수
        double totallen; // totallength

        // 현재 영상 위치를 담기 위한 변수
        double curpos; // currentposition
        
        // 분, 초 단위를 표기
        int minutes, seconds, hour;

        // 실행할 비디오 디렉터리
        string videoDir;

        // 영상 갯수
        int nbFiles;



        // 생성자, 초기화
        public Form1()
        {
            InitializeComponent();
            durationBar.Minimum = 0;
            durationBar.Maximum = 0;
            durationBar.Enabled = false;
            videoDir = "";
            labelNowTime.Text = "";
            labelTotalTime.Text = "";
            // videoFiles.SelectedIndexChanged += new EventHandler(videoListBox); // 리스트박스(videoFiles)에 이벤트 핸들러(videoListBox) 부착 (디자인 속성에서 이벤트처리를 해줬음)
        }

        // 필터 그래프 초기화
        private void initInterfaces()
        {
            try
            {
                pGraphBuilder = new FilterGraph() as IGraphBuilder; // 새로운 필터그래프 == (IGraphBuilder) new FilterGraph();
                pMediaControl = pGraphBuilder as IMediaControl; // 필터그래프에 컨트롤(그래프를 실행, 포즈, 정지 기능) 붙이기 == (IMediaControl)pGraphBuilder;
                pVideoWindow = pGraphBuilder as IVideoWindow; // 필터그래프에 비디오 윈도우(프로퍼티 설정) 붙이기
                pMediaPosition = pGraphBuilder as IMediaPosition; // 필터그래프에 포지션(위치를 시크하는 기능) 붙이기
                pBasicVideo = pGraphBuilder as IBasicVideo;
                pBasicAudio = pGraphBuilder as IBasicAudio;

                //pCaptureGraphBuilder2  를 생성합니다
                pCaptureGraphBuilder2 = new CaptureGraphBuilder2() as ICaptureGraphBuilder2;
                
                pSampleGrabber = new SampleGrabber() as ISampleGrabber;
                pSampleGrabberFilter = (IBaseFilter)pSampleGrabber;
            }
            catch (Exception)
            {
                MessageBox.Show("시작할 수 없습니다.", "Error");
            }
        }

        // 그래프 닫기(종료)
        private void closeInterfaces()
        {
            try
            {
                if (pMediaControl != null)
                {
                    if (pGraphBuilder != null)
                    {
                        Marshal.ReleaseComObject(pGraphBuilder);
                    }

                    if (pMediaControl != null)
                    {
                        Marshal.ReleaseComObject(pMediaControl);
                    }
                    if (pVideoWindow != null)
                    {
                        Marshal.ReleaseComObject(pVideoWindow);
                    }
                    if (pBasicVideo != null)
                    {
                        Marshal.ReleaseComObject(pBasicVideo);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }

        // 영상 리스트 뽑기
        void fillListBox(FolderNameDialog fnd)
        {
            videoFiles.Items.Clear(); // 기존 리스트 내용 지우기
            nbFiles = 0;
            DirectoryInfo di = new DirectoryInfo(fnd.DirectoryPath);
            FileInfo[] files = di.GetFiles(); // 디렉터리 파일들을 읽어온다
            try // BeginUpdate()가 호출되면 리스트뷰의 UI갱신 작업이 멈춰버리기(응답없음) 때문에 예외처리 "https://bigenergy.tistory.com/213"
            {
                videoFiles.BeginUpdate();
                foreach (FileInfo f in files)
                {
                    if (checkExtension(f))
                    {
                        videoFiles.Items.Add(f.Name); // 리스트박스에 영상 개수만큼 추가
                        nbFiles++;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
            finally
            {
                videoFiles.EndUpdate();
                if (videoFiles.Items.Count > 0)
                {
                    videoFiles.SelectedIndex = 0;
                }
            }
        }

        // 확장자 체킹
        bool checkExtension(FileInfo f)
        {
            if (f.Extension.StartsWith(".mp"))
                return true;
            if (f.Extension.StartsWith(".wm"))
                return true;
            if (f.Extension.StartsWith(".avi"))
                return true;
            if (f.Extension.StartsWith(".asf"))
                return true;
            if (f.Extension.StartsWith(".mov"))
                return true;
            if (f.Extension.StartsWith(".rm"))
                return true;
            if (f.Extension.StartsWith(".ram"))
                return true;
            return false;
        }

        // 그래픽 생성과 셋업
        private void loadFile(string fName)
        {
            try {
                pGraphBuilder.RenderFile(fName, null); // // 첫번째 인자 파일을 렌더링 하는 필터 그래프 생성한다 null은 예약이 끝난 상태

                pVideoWindow.put_Owner(panel1.Handle); // 비디오 윈도우의 부모 윈도우를 지정한다(핸들을 받는다)
                pVideoWindow.put_WindowStyle(WindowStyle.Child | WindowStyle.ClipSiblings); // 비디오 윈도우의 스타일을 자식 윈도우로 변경한다
                pVideoWindow.SetWindowPosition(0, 0, panel1.Width, panel1.Height); // 비디오 윈도우가 재생될 위치를 지정한다
                pVideoWindow.put_MessageDrain(panel1.Handle); // 비디오 윈도우로부터 마우스 메시지와 키보드 메시지를 받아들이는 윈도우를 지정한다
                pVideoWindow.put_Visible(OABool.True); // 비디오 윈도우가 표시되도록 설정한다

                // 비디오 넓이 높이 설정
                lblwidthheight.Visible = true;

                // durationBar 설정
                durationBar.Value = 0;
                durationBar.Minimum = 0;
                durationBar.Visible = true;
                pMediaPosition.get_Duration(out totallen);
                durationBar.Maximum = (int)(totallen * 100.0);
                durationBar.Enabled = true;

                pBasicAudio.put_Balance(0); // 밸런스 설정
                pBasicAudio.put_Volume(0); // 초기 음량 설정

                vSize(); // 비디오 넓이와 높이
                vDuration(); // 비디오 전체 길이(총시간)
                timerStart(); // 타이머 시작

                graphState = State.Playing; // Playing 상태로 변경
                pMediaControl.Run(); // 영상 플레이
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }
        
        // 리스트박스(videoFiles)의 다른 인덱스(영상)를 선택 했을 경우 호출
        void videoListBox(object o, EventArgs e)
        {
            try
            {
                closeInterfaces();
                initInterfaces();
                loadFile(videoDir + videoFiles.Text); // 클릭한 인덱스(영상)를 새로 로드
            }
            catch (Exception ex)
            {
                MessageBox.Show("재생 중 오류가 발생하여 프로그램을 종료합니다."
                     + "\\nMessage: " + ex.Message);
                Application.Exit();
            }
        }

        // 비디오 넓이와 높이
        private void vSize()
        {
            try
            {
                pBasicVideo.get_VideoHeight(out Video_Height);
                pBasicVideo.get_VideoWidth(out Video_Width);
                String str_widthheight = string.Format("비디오 넓이 : {0} / 높이 : {1}", Width, Height);
                lblwidthheight.Text = str_widthheight;
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }

        // 비디오 전체 길이(총시간)
        private void vDuration()
        {
            try
            {
                pMediaPosition.get_Duration(out totallen); // 영상 전체 재생 길이(시간)
                //String str_totallen = string.Format("{0}초", totallen);
                hour = (int)totallen / 3600;
                minutes = (int)(totallen % 3600) / 60;
                seconds = (int)(totallen % 3600) % 60;
                labelTotalTime.Text = $"{hour}시 {minutes}분 {seconds}초";
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }

        private void timerStart()
        {
            timer1.Interval = 10;
            timer1.Enabled = true;
            timer1.Start();
        }

        private void timerStop()
        {
            timer1.Stop();
            labelNowTime.Text = "";
            labelTotalTime.Text = "";
            lblwidthheight.Text = "";
        }


        // 타이머
        private void timer1_Tick(object sender, EventArgs e)
        {
            pMediaPosition.get_CurrentPosition(out curpos); // 스트림의 합계 시간폭 기준으로 현재 위치를 얻어옴
            durationBar.Value = (int)(curpos * 100.0);
            String str = string.Format("{0}초", (int)curpos);
            labelNowTime.Text = str;
                
        }

        

        private void 재생ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (pMediaControl != null && graphState == State.Paused)
                {
                    pMediaControl.Run();
                    graphState = State.Playing;
                }
                if (pMediaControl != null && graphState == State.Stopped)
                {
                    closeInterfaces();
                    initInterfaces();
                    loadFile(videoDir + videoFiles.Text);
                    graphState = State.Playing;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }

        private void 중지ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (pMediaControl != null && graphState != State.Paused)
                {
                    pMediaControl.Pause(); // 영상 일시정지
                    graphState = State.Paused;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }

        private void 종료ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (pMediaControl != null && graphState != State.Stopped)
                {
                    closeInterfaces();
                    timerStop(); // 타이머 종료
                    graphState = State.Stopped;
                    durationBar.Visible = false;
                    lblwidthheight.Visible = false;
                }


            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }

        private void 캡쳐ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pCapture();
            graphState = State.Playing;
        }

        // 파일 열기 버튼
        private void btnSetMedia_Click(object sender, EventArgs e)
        {
            FolderNameDialog fnd = new FolderNameDialog();
            fnd.NoNewFolderButton = true;
            if (fnd.ShowDialog() == DialogResult.OK) // 파일 열기에서의 확인 버튼을 눌렀을 때
            {
                closeInterfaces();
                initInterfaces();
                videoDir = fnd.DirectoryPath + "\\";
                txtPath.Text = videoDir; // txtPath에 경로 표시
                fillListBox(fnd); // 영상 리스트를 뽑기 위해 호출
            }
        }

        private void btnWebCamPlay_Click(object sender, EventArgs e)
        {
            f3 = new Form3();
            f3.Show();
        }

        // 캡쳐 메서드
        private void pCapture()
        {
            try
            {
                // 이미지 캡쳐를 하기 전 Pause 해야한다
                pMediaControl.Pause();

                // 넓이와 높이를 얻는다
                pBasicVideo.get_VideoHeight(out Video_Height);
                pBasicVideo.get_VideoWidth(out Video_Width);
                int bufSize = 0;
                IntPtr imgData;
                int hr;
                hr = pBasicVideo.GetCurrentImage(ref bufSize, IntPtr.Zero);
                DsError.ThrowExceptionForHR(hr);

                if (bufSize < 1)
                {
                    MessageBox.Show("버퍼사이즈를 구하는데 실패했습니다.", "실패");
                    return;
                }
                imgData = Marshal.AllocCoTaskMem(bufSize);
                // 취득할 수 있는 데이터는 DIB형식이다
                pBasicVideo.GetCurrentImage(ref bufSize, imgData);
                // 비트맵으로 저장
                saveToJpg(imgData, bufSize, Video_Height, Video_Width);

                Marshal.FreeCoTaskMem(imgData);

                // 다시 재생 시작
                pMediaControl.Run();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }
        

        // 비트맵 저장 메서드
        private void saveToJpg(IntPtr Source, int Size, int height, int width)
        {
            try
            {
                int stride = -4 * width; // -3은 24bbpArgb, -4는 32bbpArgb
                // long a = (long)Source;
                IntPtr Scan0 = (IntPtr)(((long)Source) + (Size - (4 * width))); // overflowException 떠서 Source를 int에서 long형으로 형변환
                Bitmap img = new Bitmap(width, height, stride, PixelFormat.Format32bppArgb, Scan0);
                img.Save("testForm1.jpg", ImageFormat.Jpeg);

                img.Dispose();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }

        }

        private void durationBar_Scroll(object sender, EventArgs e)
        {
            // 특정 구간으로 이동 후에 재생할 지 멈출 지 정한다.
            //pMediaControl.Pause();
            //graphState = State.Paused;
            pMediaPosition.put_CurrentPosition(durationBar.Value / 100.0);
        }
    }
}
