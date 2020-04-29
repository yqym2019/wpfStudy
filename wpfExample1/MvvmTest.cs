using PropertyChanged;
using System.ComponentModel;
using System.Threading.Tasks;

namespace wpfExample1
{
    //加了这个头 可以不用再调用Event 就可以实现数据刷新
    [AddINotifyPropertyChangedInterface]
    public class MvvmTest : INotifyPropertyChanged
    {
       // private string mTest;

        /// <summary>
        /// 声明委托事件
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };

        /// <summary>
        /// 重载输出方法 实际上就是 button.content 的内容
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "Hello World";
        }

        public string Test { get; set; }
        //{
        //    get
        //    {
        //        return mTest;
        //    }
        //    set
        //    {
        //        if (mTest == value)
        //            return;
        //        mTest = value;
        //        PropertyChanged(this, new PropertyChangedEventArgs(nameof(Test)));
        //    }
        //}

        public MvvmTest()
        {
            Task.Run(async()=>
            {
                int i = 1;
                while(true)
                {
                    await Task.Delay(100);
                    Test = (i++).ToString();
                }
            }

            );
        }
    }
}
