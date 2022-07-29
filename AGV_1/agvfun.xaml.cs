using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading; //타이머 생성자를 사용하기 위한 라이브러리 사용

namespace AGV_1
{
    /// <summary>
    /// agvfun.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class agvfun : UserControl
    {

        double TKS_sum = 0.0; //총 합계
        double agv = 0.0;
        private DispatcherTimer timer;
        private int mCalculationIndex = 0;
        private static double total = 0.0; //static 변수를 줘서 메모리 한곳에 유지
        /// </summary>

        public agvfun()
        {
            InitializeComponent(); // 디자이너 단에 정의된 폼컴포넌트 정의를 호출하는
            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 0, 0, 1000);
            timer.Tick += timer_Tick;
        }
        public event Action<double,double> AGVTimeChangedEvent1;
        public event Action<double,double> AGVTimeChangedEvent2;
        public event Action<double,double> AGVTimeChangedEvent3;
      
        public void timer_Tick(object sender, EventArgs e)  //시간 Tick 메서드 이게 기본작동이라 이걸 바꿔야 한다.
        {
            int R = 101;
            int G = 187;
            int B = 29;       
            var brush = new SolidColorBrush(Color.FromArgb(255, (byte)R, (byte)G, (byte)B));

            this.point_1.Background = Brushes.White;
            this.point_2.Background = Brushes.White;
            this.point_3.Background = Brushes.White;
            this.point_4.Background = Brushes.White;
            this.point_5.Background = Brushes.White;
            this.point_6.Background = Brushes.White;

            if (mCalculationIndex > 6)
            {
                timer.Stop();
                return;
            }
            if (mCalculationIndex == 0)
            {
                this.point_1.Background = brush;
            }
            else if (mCalculationIndex == 1)
            {
                this.point_2.Background = brush;
            }

            if(mCalculationIndex == 2)
            {
              this.point_3.Background = brush;
                if (radi1.IsChecked == true)    //순차적으로 보더의 색상이 변할때 해당하는 텍스트값이 TKS_sum 에 더해지면서 최종값에 반영.
                {
                    TKS_sum += Convert.ToDouble(move1.Text);
                }
                else if (radi2.IsChecked == true)
                {
                    TKS_sum += Convert.ToDouble(move2.Text);
                }
                else if (radi3.IsChecked == true)
                {
                    TKS_sum += Convert.ToDouble(move3.Text);
                }
            }
            ////////////이밑은 이동시간,수집시간,계산하는 코드
            else if (mCalculationIndex == 3)
            {
                this.point_4.Background = brush;
                TKS_sum += Convert.ToDouble(move4.Text);
            }
            else if (mCalculationIndex == 4)
            {
                this.point_5.Background = brush;
                TKS_sum += Convert.ToDouble(move5.Text);
            }
            else if (mCalculationIndex == 5)
            {
                this.point_6.Background = brush;
                TKS_sum += Convert.ToDouble(move6.Text);
            }
            else if(mCalculationIndex == 6)
            {
                agv = Math.Truncate(100 * (TKS_sum / 60)) / 100;
                total += agv;
            }
            mCalculationIndex++;
            agv = Math.Truncate(100 * (TKS_sum / 60)) / 100;
   
            AGVTimeChangedEvent3?.BeginInvoke(agv, total, null,null);
            AGVTimeChangedEvent2?.BeginInvoke(agv, total, null,null);
            AGVTimeChangedEvent1?.BeginInvoke(agv, total,null,null);
            // mCalculationIndex 설비동작당 1~7개의 값이 순차적으로 출력
            //Console.WriteLine(mCalculationIndex);

        }

        public void Count(int count) //여기 안에 클릭당 카운터수를 구해서 time_tick 함수의 매개변수 값으로 넘겨주고 그만큼 for 문을 이용하여 반복하여 해당 agv에 led를 구현    // 호출 할 것 
        {
                Console.WriteLine(count);
     



            //Console.WriteLine(count);

        }
        
        public void SequAgv()
        {
            if (timer.IsEnabled == false)       //timer 가 중단된 이후 설비체크가 되어 있으면서 3개의 라디오중 하나라도 체크가 된다면 계산후. 뭐든값 0으로 최기화 후 timer 실행.
            {
                if (Chk1.IsChecked == true && (bool)Chk1.IsChecked || ((bool)radi1.IsChecked || (bool)radi2.IsChecked || (bool)radi3.IsChecked))
                {

                    TKS_sum = 0;
                    agv = 0;
                    mCalculationIndex = 0;
                    timer.Start();

                }
            }
        }


        //(bool) Chk1.IsChecked && ((bool) radi1.IsChecked || (bool) radi2.IsChecked || (bool) radi3.IsChecked) 김과장님이 쓰신 코드 재확인.

        public void StartCalc() //각 정의한 컨트롤 화면에 텍스트값을 계산하는 함수
        {
            if (timer.IsEnabled == false)       //timer 가 중단된 이후 설비체크가 되어 있으면서 3개의 라디오중 하나라도 체크가 된다면 계산후. 뭐든값 0으로 최기화 후 timer 실행.
            {
                if ((bool)Chk1.IsChecked && ((bool)radi1.IsChecked || (bool)radi2.IsChecked || (bool)radi3.IsChecked)) //한 객체에 해당하는 박스에 체크를 하고 3개의 라디오 버튼이 클릭 되어야 start
                {
                    byte[] buffer = new byte[4] {1,2,3,4};
 

                    TKS_sum = 0;
                    agv = 0;
                    mCalculationIndex = 0;
                    timer.Start();
                   // Console.WriteLine(count);
                }
            }
            else
            {
                MessageBox.Show("이미 실행중입니다.", "Warnning"); //timer 가 실행중일떄 버튼을 누르면 이 경고창이 뜬다.
            }
        }


        public void Mesg()
        {
            if ((Chk1.IsChecked == true && radi1.IsChecked == true && ((String.IsNullOrEmpty(move1.Text)))))
                MessageBox.Show("값을 넣어주세요");      
        }
      
     
        public void Clear() //초기화 버튼 동작메서드
        {
            TKS_sum = 0.0;
            agv = 0.0;
            move1.Text = "11.25";
            move4.Text = "120";
            move5.Text = "62.5";
            move6.Text = "120";
            total = 0.0;
            move2.Clear();
            move3.Clear();
           
            radi1.IsChecked = false;
            radi2.IsChecked = false;
            radi3.IsChecked = false;
            Chk1.IsChecked = false;
            
        }
        public void TotalClear()  //최종값만 메인에서 초기화 하기 위한 메서드 
        {
            total = 0.0;
        }

        public void TimerStop()
        {
            this.point_1.Background = Brushes.White;
            this.point_2.Background = Brushes.White;
            this.point_3.Background = Brushes.White;
            this.point_4.Background = Brushes.White;
            this.point_5.Background = Brushes.White;
            this.point_6.Background = Brushes.White;
            timer.Stop();
        }
        

        public void SetTitle(string title) //agv 사용자 정의 컨트롤 메서드
        {
            label_title.Content = title; //사용자 정의로 만들어진 각 CTS,BTS의 레이블의 매개변수를 받아와 넣어서 출력시키는 코드.
            
        }
      
        private void Button_Click(object sender, EventArgs e)
        {
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e) //TKS 충전대 버튼 
        {
          
        }
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
        }
        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e) //충전대 -> TKS 이동시간 기입 
        {
        }
        private void CheckBox_Checked(object sender, RoutedEventArgs e) //TKS 체크 박스 메서드
        {
        }
    }
}
