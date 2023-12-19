using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DirectShowLib;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Drawing.Imaging;

namespace WindowsFormsApp5
{

    public partial class Form3 : Form
    {
        #region 옵션 프로퍼티 DLL 임포트
        [DllImport("oleaut32.dll")]
        public static extern int OleCreatePropertyFrame(
            IntPtr hwndOwner,
            int x,
            int y,
            [MarshalAs(UnmanagedType.LPWStr)] string lpszCaption,
            int cObjects,
            [MarshalAs(UnmanagedType.Interface, ArraySubType = UnmanagedType.IUnknown)]
            ref object ppUnk,
            int cPages,
            IntPtr lpPageClsID,
            int lcid,
            int dwReserved,
            IntPtr lpvReserved);
        #endregion

        // 그래프와 컨트롤 선언
        public IGraphBuilder pGraphBuilder = null;
        public IMediaControl pMediaControl = null;
        public IVideoWindow pVideoWindow = null;
        public IMediaPosition pMediaPosition = null;

        // 장치에 필요한 필터 선언
        public IBaseFilter theDevice = null;
        public IBaseFilter AudDevice = null;
        public IBaseFilter theCompressor = null;

        // 캡처 그래프, 커스텀 필터 그래프를 구축하는 메서드 제공
        public ICaptureGraphBuilder2 pCaptureGraphBuilder2 = null;

        // 녹화 기술에 필요한 필터 선언
        public IBaseFilter mux;
        public IFileSinkFilter sink;

        // 캡쳐에 필요한 필터 선언
        public IBaseFilter pVideoRenderer = null;
        public IBaseFilter pSampleGrabberFilter = null;
        public ISampleGrabber pSampleGrabber = null;
        public int Video_Width, Video_Height;

        public Form3()
        {
            InitializeComponent();
        }

        private void initInterfaces()
        {
            pGraphBuilder = new FilterGraph() as IGraphBuilder; // 새로운 필터그래프
            pMediaControl = pGraphBuilder as IMediaControl; // // 필터그래프에 컨트롤 붙이기
            pVideoWindow = pGraphBuilder as IVideoWindow; // 필터그래프에 윈도우 붙이기
            pMediaPosition = pGraphBuilder as IMediaPosition; // 필터그래프에 포지션 붙이기

            //pCaptureGraphBuilder2  를 생성합니다
            pCaptureGraphBuilder2 = new CaptureGraphBuilder2() as ICaptureGraphBuilder2;

            pSampleGrabber = new SampleGrabber() as ISampleGrabber;
            pSampleGrabberFilter = (IBaseFilter)pSampleGrabber;
            pVideoRenderer = (IBaseFilter)new VideoRenderer();

        }

        // 녹화버튼을 눌렀을 때 필터 그래프 초기화
        private void setupGraph(string filename)
        {
            try
            {
                AMMediaType am_media_type = new AMMediaType();
                //캡쳐그래프를  pGraphBuilder에 붙여 줍니다
                pCaptureGraphBuilder2.SetFiltergraph(pGraphBuilder);
                //입력 소스를 pGraphBuilder에 붙입니다
                
                // SampleGrabber를 접속하는 포맷을 지정, 여기가 중요부분입니다.
                // 여기서 지정한 방법에 의해 SampleGrabber의 포맷을
                // 결정할 수 있습니다. 이 샘플과 같이 지정을 하면
                // 화면 출력 직전에 샘플을 얻어 올 수 있습니다.
                am_media_type.majorType = MediaType.Video;
                am_media_type.subType = MediaSubType.RGB24;
                am_media_type.formatType = FormatType.VideoInfo;
                pSampleGrabber.SetMediaType(am_media_type);

                pGraphBuilder.AddFilter(theDevice, "WebCam Source");

                // Graph에SampleGrabber Filter를 추가
                pGraphBuilder.AddFilter(pSampleGrabberFilter, "Sample Grabber");
                
                pGraphBuilder.AddFilter(pVideoRenderer, "Video Render");

                if (filename != string.Empty)
                {
                    //압축코덱을 붙여준다
                    pGraphBuilder.AddFilter(theCompressor, "Compressor filter");
                    IBaseFilter mux;
                    IFileSinkFilter sink;
                    //출력할 파일이름 지정
                    pCaptureGraphBuilder2.SetOutputFileName(MediaSubType.Avi, filename, out mux, out sink);
                    pCaptureGraphBuilder2.RenderStream(PinCategory.Capture, MediaType.Video, theDevice, theCompressor, mux);
                    Marshal.ReleaseComObject(mux);
                    Marshal.ReleaseComObject(sink);
                }

                // 변경 렌더 스트림을 정해 줍니다
                pCaptureGraphBuilder2.RenderStream(PinCategory.Preview, MediaType.Video, theDevice,
                                                               pSampleGrabberFilter, pVideoRenderer);

                initWindow(screen);
                
                // 이 처리는 Graph가 구성된 후에 실행하여야 합니다.
                pSampleGrabber.GetConnectedMediaType(am_media_type);

                VideoInfoHeader pVideoInfoHeader = (VideoInfoHeader)Marshal.PtrToStructure(
                                                    am_media_type.formatPtr, typeof(VideoInfoHeader));
                
                // 영상의 폭과 높이를 표시
                // 샘플을 알기 쉽게 하기 위해서 표시하고 있을 뿐 반드시 필요한것은 아닙니다.
                String str = string.Format("웹캠 폭 높이 = {0} x {1}", pVideoInfoHeader.BmiHeader.Width,
                                                                   pVideoInfoHeader.BmiHeader.Height);
                Video_Width = pVideoInfoHeader.BmiHeader.Width;
                Video_Height = pVideoInfoHeader.BmiHeader.Height;

                // 데이터사이즈를 표시 이것도 설명을 위해서 표시하고 있습니다.
                str += string.Format("       Data Size = {0}", am_media_type.sampleSize);
                txtSizeInfo.Text = str;

                DsUtils.FreeAMMediaType(am_media_type);


                // SetBufferSamples을 실행하지 않으면 버퍼로부터 데이터를 얻을 수 없습니다.
                // 불필요하게 부하를 주고 싶지 않은 경우에는 false로 해 두고 데이터를 얻고 싶을때 true로 바꿀수 있습니다
                pSampleGrabber.SetBufferSamples(true);
            }
            catch (Exception)
            {
                MessageBox.Show("시작할 수 없습니다.", "Error");
            }
        }

        // 시작버튼을 눌렀을 때 호출되는 메서드
        private void setupGraph()
        {
            try
            {
                //캡쳐그래프를  pGraphBuilder에 붙여 줍니다
                pCaptureGraphBuilder2.SetFiltergraph(pGraphBuilder);
                //입력 소스를 pGraphBuilder에 붙입니다
                pGraphBuilder.AddFilter(theDevice, "WebCam Source");
                // 렌더 스트림을 정해준다
                pCaptureGraphBuilder2.RenderStream(PinCategory.Preview, MediaType.Video, theDevice, null, null); // 소스 필터의 출력 핀을 옵션으로 압축 필터를 경유해 렌더링 필터에 접속
                                                                                                                 // 출력창 메소드 호출
                initWindow(screen);

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
                if (pCaptureGraphBuilder2 != null)
                {
                    Marshal.ReleaseComObject(pCaptureGraphBuilder2);
                }
                if (pSampleGrabber != null)
                {
                    Marshal.ReleaseComObject(pSampleGrabber);
                }
                if (pSampleGrabberFilter != null)
                {
                    Marshal.ReleaseComObject(pSampleGrabberFilter);
                }
                if (pVideoRenderer != null)
                {
                    Marshal.ReleaseComObject(pVideoRenderer);
                }
                if (pMediaPosition != null)
                {
                    Marshal.ReleaseComObject(pMediaPosition);
                }
                if (pMediaPosition != null)
                {
                    Marshal.ReleaseComObject(pMediaPosition);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }

        // 출력창 메소드
        private void initWindow(Control winPanel)
        {
            pVideoWindow = (IVideoWindow)pGraphBuilder;
            // 패널에서 재생하기
            pVideoWindow.put_Owner(winPanel.Handle);
            pVideoWindow.put_WindowStyle(WindowStyle.Child | WindowStyle.ClipSiblings);

            Rectangle rect = winPanel.ClientRectangle;
            pVideoWindow.SetWindowPosition(0, 0, rect.Right, rect.Bottom);
        }


        #region 콤보박스설정이 바뀌면
        private void cbTheDevice_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            //COM objects 해제
            if (theDevice != null)
            {
                Marshal.ReleaseComObject(theDevice);
                theDevice = null;
            }
            // 선택된 video 입력 장치로 필터 생성
            try
            {
                string devicepath = cbTheDevice.SelectedItem.ToString();
                theDevice = CreateFilter(FilterCategory.VideoInputDevice, devicepath);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

        }

        private void cbTheCompressor_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            //COM objects 해제
            if (theCompressor != null)
            {
                Marshal.ReleaseComObject(theCompressor);
                theCompressor = null;
            }
            //선택된 video 압축코덱 필터 생성
            try
            {
                string devicepath = cbTheCompressor.SelectedItem.ToString();
                theCompressor = CreateFilter(FilterCategory.VideoCompressorCategory, devicepath);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private void cbTheAudDevice_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            //COM 객체 해제
            if (AudDevice != null)
            {
                Marshal.ReleaseComObject(AudDevice);
                AudDevice = null;
            }
            //선택된 오디오 입력장치로 필터 생성
            try
            {
                string devicepath = cbTheAudDevice.SelectedItem.ToString();
                AudDevice = CreateFilter(FilterCategory.AudioInputDevice, devicepath);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        #endregion


        /// <summary>
        /// 전달 받은 category 와 friendlyname으로 필터를 생성하여 리턴
        /// </summary>
        /// <param name="category">filter의 카테고리</param>
        /// <param name="friendlyname">필터의 Friendly name </param>
        /// <returns>장치를 위한 IBaseFilter</returns>
        private IBaseFilter CreateFilter(Guid category, string friendlyname)
        {
            object source = null;
            try
            {
                Guid iid = typeof(IBaseFilter).GUID;
                foreach (DsDevice device in DsDevice.GetDevicesOfCat(category))
                {
                    if (device.Name.CompareTo(friendlyname) == 0)
                    {
                        device.Mon.BindToObject(null, null, ref iid, out source);
                        break;
                    }
                }
                return (IBaseFilter)source;
            }

            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine("에러 : {0}", ex);
            }
            return null;

        }

        #region 장치별 옵션 설정하기

        /// <summary>
        /// 장치별 환경설정을 실행시켜서 세팅
        /// </summary>
        /// <param name="dev">property page를 표시하기 위한 필터</param>
        private void displayPropertyPage(IBaseFilter dev)
        {
            // 필터에 맞는 ISpecifyPropertyPages 구하기
            ISpecifyPropertyPages pProp = dev as ISpecifyPropertyPages;
            int hr = 0;

            if (pProp == null)
            {
                // 필터에 ISpecifyPropertyPages가 없을 경우,대신에 IAMVfwCompressDialogs 표시를 시도
                IAMVfwCompressDialogs compressDialog = dev as IAMVfwCompressDialogs;
                if (compressDialog != null) // pProp(=dev=theDevice)이 있다면 옵션창을 띄움
                {

                    hr = compressDialog.ShowDialog(VfwCompressDialogs.Config, IntPtr.Zero);
                    DsError.ThrowExceptionForHR(hr);
                }
                return;
            }

            // FilterInfo 구조체로부터 필터 이름 얻기
            FilterInfo filterInfo;
            hr = dev.QueryFilterInfo(out filterInfo);
            DsError.ThrowExceptionForHR(hr);

            // property bag에서 propertypages 얻기
            DsCAUUID caGUID;
            hr = pProp.GetPages(out caGUID);
            DsError.ThrowExceptionForHR(hr);

            // 출력핀에서 property pages 체크
            IPin pPin = DsFindPin.ByDirection(dev, PinDirection.Output, 0);
            ISpecifyPropertyPages pProp2 = pPin as ISpecifyPropertyPages;
            if (pProp2 != null)
            {
                DsCAUUID caGUID2;
                hr = pProp2.GetPages(out caGUID2);
                DsError.ThrowExceptionForHR(hr);

                if (caGUID2.cElems > 0)
                {
                    int soGuid = Marshal.SizeOf(typeof(Guid));

                    //GUIDs 저장을 위한 새로운 버퍼 생성
                    IntPtr p1 = Marshal.AllocCoTaskMem((caGUID.cElems + caGUID2.cElems) * soGuid);

                    // 필터로 부터 pages 복사
                    for (int x = 0; x < caGUID.cElems * soGuid; x++)
                    {
                        Marshal.WriteByte(p1, x, Marshal.ReadByte(caGUID.pElems, x));
                    }

                    // pages를 핀으로부터 추가
                    for (int x = 0; x < caGUID2.cElems * soGuid; x++)
                    {
                        Marshal.WriteByte(p1, x + (caGUID.cElems * soGuid), Marshal.ReadByte(caGUID2.pElems, x));
                    }

                    // 사용한 메모리 해제
                    Marshal.FreeCoTaskMem(caGUID.pElems);
                    Marshal.FreeCoTaskMem(caGUID2.pElems);

                    // Reset caGUID to include both
                    caGUID.pElems = p1;
                    caGUID.cElems += caGUID2.cElems;
                }
            }

            // OlePropertyFrame 생성후 출력
            object oDevice = (object)dev;
            hr = OleCreatePropertyFrame(this.Handle, 0, 0, filterInfo.achName, 1, ref oDevice, caGUID.cElems, caGUID.pElems, 0, 0, IntPtr.Zero);
            DsError.ThrowExceptionForHR(hr);

            // COM objects 해제
            Marshal.FreeCoTaskMem(caGUID.pElems);
            Marshal.ReleaseComObject(pProp);
            if (filterInfo.pGraph != null)
            {
                Marshal.ReleaseComObject(filterInfo.pGraph);
            }
        }
        #endregion

        private void btnTheDeviceOp_Click(object sender, EventArgs e)
        {
            //영상입력장치 옵션설정
            displayPropertyPage(theDevice);
        }

        private void btnTheCompressorOp_Click(object sender, EventArgs e)
        {
            //압축코덱옵션설정
            displayPropertyPage(theCompressor);
        }

        private void btnTheAudDeviceOp_Click(object sender, EventArgs e)
        {
            //오디오 입력장치 옵션설정
            displayPropertyPage(AudDevice);
        }

        // 시작 버튼
        private void btnWebCamPlay_Click(object sender, EventArgs e)
        {
            if (pGraphBuilder != null)
            {
                closeInterfaces();
            }
            initInterfaces();
            setupGraph(string.Empty);
            pMediaControl.Run();
        }

        // 종료 버튼
        private void btnWebCamStop_Click(object sender, EventArgs e)
        {
            closeInterfaces(); // 그래프 초기화
        }

        // 녹화시작 버튼
        private void btnRecordingPlay_Click(object sender, EventArgs e)
        {
            if (pGraphBuilder != null)
            {
                closeInterfaces();
            }
            initInterfaces();
            setupGraph("test.avi");
            pMediaControl.Run();
        }

        // 녹화종료 버튼
        private void btnRecordingStop_Click(object sender, EventArgs e)
        {
            if (pGraphBuilder != null)
            {
                closeInterfaces();
            }

            try
            {
                Marshal.ReleaseComObject(mux);
                Marshal.ReleaseComObject(sink);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            
        }

        private void button6_Click(object sender, EventArgs e)
        {
            pCapture();
        }

        private void pCapture()
        {

            int bufSize = 0;
            IntPtr imgData;

            pSampleGrabber.GetCurrentBuffer(ref bufSize, IntPtr.Zero);

            if (bufSize < 1)
            {
                //textBox1.Text = "버퍼사이즈를 구하는데 실패했습니다";
                return;
            }
            imgData = Marshal.AllocCoTaskMem(bufSize);

            pSampleGrabber.GetCurrentBuffer(ref bufSize, imgData);

            //비트맵으로 저장하기
            saveToJpg(imgData, bufSize, Video_Height, Video_Width);

            Marshal.FreeCoTaskMem(imgData);

        }

        private void saveToJpg(IntPtr Source, int Size, int height, int width)
        {
            //15강에서는 stride 가 4에서 3으로 바뀌었습니다
            int stride = -3 * width;
            // long a = (long)Source; 
            IntPtr Scan0 = (IntPtr)(((long)Source) + (Size - (3 * width))); // overflowException 떠서 Source를 int에서 long형으로 형변환
            //픽셀포맷역시 15강에서는 14강과 틀립니다 IBasicVideo.GetCurrentImage와 데이타 형식이 틀리기 때문입니다
            Bitmap img = new Bitmap(width, height, stride, PixelFormat.Format24bppRgb, Scan0);
            img.Save("testForm3.jpg", ImageFormat.Jpeg);

            img.Dispose();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            // 설치된 카메라 등의 입력장치 읽어오기
            foreach (DsDevice ds in DsDevice.GetDevicesOfCat(FilterCategory.VideoInputDevice))
            {
                cbTheDevice.Items.Add(ds.Name);
            }

            if (cbTheDevice.Items.Count > 0)
            {
                cbTheDevice.SelectedIndex = 0;
            }

            // 설치된 비디오 압축 코덱 읽어오기
            foreach (DsDevice ds in DsDevice.GetDevicesOfCat(FilterCategory.VideoCompressorCategory))
            {
                cbTheCompressor.Items.Add(ds.Name);
            }

            if (cbTheCompressor.Items.Count > 0)
            {
                cbTheCompressor.SelectedIndex = 0;
            }

            // 설치된 사운드카드 장치 읽어오기
            foreach (DsDevice ds in DsDevice.GetDevicesOfCat(FilterCategory.AudioInputDevice))
            {
                cbTheAudDevice.Items.Add(ds.Name);
            }

            if (cbTheAudDevice.Items.Count > 0)
            {
                cbTheAudDevice.SelectedIndex = 0;
            }
        }
    }
}
