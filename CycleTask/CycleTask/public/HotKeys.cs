using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace CycleTask
{
    /*
    // 使用说明
    // 在调用的窗体里放如下的代码
    // 注意：ShowInTaskbar = false; 语句可能会影响使用

    // 注册全局热键(100, Alt+L)
    HotKeys.RegisterHotKey(Handle, 100, (int)HotKeys.KeyModifiers.Alt, Keys.L); 
    // 注销全局热键(100)
    HotKeys.UnregisterHotKey(Handle, 100);

    // 响应函数
    protected override void WndProc(ref Message m)
    {
        if (m.Msg == 0x0312)
        {
            switch (m.WParam.ToInt32())
            {
                case 100:
                    // 此处填写按下Alt+L快捷键的响应代码 
                    break;
                case 101:
                    // ...
                    break;
            }
        }
        base.WndProc(ref m);
    }

    // 参考
    // https://www.cnblogs.com/lujin49/p/3509615.html
    // https://www.cnblogs.com/TianFang/archive/2007/05/14/745489.html
    // https://www.cnblogs.com/bomo/archive/2012/12/09/2809981.html
    */

    /// <summary>
    /// 全局快捷键类
    /// </summary>
    public static class HotKeys
    {
        [DllImport("user32.dll")]
        public static extern bool RegisterHotKey(IntPtr hWnd, int id, int modifiers, Keys vk);
        [DllImport("user32.dll")]
        public static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        // 定义组合控制键枚举
        public enum KeyModifiers
        {
            Alt = 1,
            Control = 2,
            Shift = 4,
            Win = 8
        }
    }
}
