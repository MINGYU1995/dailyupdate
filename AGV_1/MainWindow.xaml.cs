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
//최신화 테스트
namespace AGV_1
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {
        //public static total1 = 0,0
        // public double total = 0,0;
        private static double total = 0.0;
        public MainWindow()
        {
            //사용자 정의 컨트롤 뷰에서 전달받을 데이터
            InitializeComponent();
            MainMethon(); 
        }   
        private void AGVFunc3CalcValueChanged1(double val, double val2) //time 함수를 사용하여 순차적으로 값이 msg3컨텐트에 전달이 되는 것 까지 구현한 함수.
        {
            if (!Dispatcher.CheckAccess())
            {
                Dispatcher.BeginInvoke(new Action<double,double>(AGVFunc3CalcValueChanged1), val,val2);
                return;
            }
            msg4.Content = val2.ToString();
            msg1.Content = val.ToString();            
        }
        private void AGVFunc3CalcValueChanged2(double val,double val2)
        {
            if (!Dispatcher.CheckAccess())
            {
                Dispatcher.BeginInvoke(new Action<double,double>(AGVFunc3CalcValueChanged2), val,val2);
                return;
            }    
            msg4.Content = val2.ToString();
            msg2.Content = val.ToString();
        }
        private void AGVFunc3CalcValueChanged3(double val,double val2)
        {
            if (!Dispatcher.CheckAccess())
            {
                Dispatcher.BeginInvoke(new Action<double,double>(AGVFunc3CalcValueChanged3), val,val2);
                return;
            }      
            msg4.Content = val2.ToString();
            msg3.Content = val.ToString();
        }
        private void Button_Click(object sender, EventArgs e)
        {
            
        }

        private void Button_Click(object sender, RoutedEventArgs e) //버튼을 클릭했을때 
          {
            if (!validateStartCondition())
            { //vail 메서드 리턴 함수의 not 값일 경우 경고창 true반환시 호출 false 반환시 무시
                MessageBox.Show("최소 1개라인 체크", "Warnning");
                return;
            }
            msg1.Content = "0";
            msg2.Content = "0";
            msg3.Content = "0";
            
            Btn_Methond();    
        }
        private void RadioButton_Checked(object sender, RoutedEventArgs e) //TKS 충전대 버튼 
        {
            // 충전대 -> TKS 이동;
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

        private void agvfun_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void agvfun1_Loaded_1(object sender, RoutedEventArgs e)
        {
            
        }

        private void agvfun2_Loaded(object sender, RoutedEventArgs e)
        {

        }
        private void MainMethon()
        {
            agvfun1.SetTitle("TKS");
            agvfun2.SetTitle("CTS");
            agvfun3.SetTitle("BTS");
            agvfun1.AGVTimeChangedEvent1 += AGVFunc3CalcValueChanged1;
            agvfun2.AGVTimeChangedEvent2 += AGVFunc3CalcValueChanged2;
            agvfun3.AGVTimeChangedEvent3 += AGVFunc3CalcValueChanged3;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
           
            agvfun1.Clear();
            agvfun2.Clear();
            agvfun3.Clear();
            msg1.Content = "0";
            msg2.Content = "0";
            msg3.Content = "0";
            msg4.Content = "0";

          //  agvfun1.TimerStop();
        }
        private void Btn_Methond()
        {
            double total = 0.0;
            agvfun1.StartCalc(); //3개의 호출을 하나로 통일 해야된다. 
            agvfun2.StartCalc();
            agvfun3.StartCalc();
            agvfun1.Mesg(); //값 요청 경고창.
            agvfun2.Mesg();
            agvfun3.Mesg();

            msg4.Content = total.ToString(); //최종결과값.
            agvfun1.TotalClear();
            agvfun2.TotalClear();
            agvfun3.TotalClear();
        }

         

       // (bool) Chk1.IsChecked && ((bool) radi1.IsChecked || (bool) radi2.IsChecked || (bool) radi3.IsChecked)
        private bool validateStartCondition() //버튼 클릭시 경고창제어 메서드
        { //bool 메서드에선 논리곱,논리합 연산자사용 불가 

            if (((bool)agvfun1.Chk1.IsChecked == true && (bool)agvfun1.radi1.IsChecked == true) || ((bool)agvfun1.radi2.IsChecked == true || (bool)agvfun1.radi3.IsChecked == true))
                return true;
            else if (((bool)agvfun2.Chk1.IsChecked == true && (bool)agvfun2.radi1.IsChecked == true) || ((bool)agvfun2.radi2.IsChecked == true || (bool)agvfun2.radi3.IsChecked == true))
                return true;
            else if (((bool)agvfun3.Chk1.IsChecked == true && (bool)agvfun3.radi1.IsChecked == true) || ((bool)agvfun3.radi2.IsChecked == true || (bool)agvfun3.radi3.IsChecked == true))
                return true;
            else
                return false;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {

            agvfun1.TimerStop();
            agvfun2.TimerStop();
            agvfun3.TimerStop();
        } //stopbutton
    }
}
