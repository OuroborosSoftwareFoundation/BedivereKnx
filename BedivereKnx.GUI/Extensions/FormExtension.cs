namespace BedivereKnx.GUI
{

    internal static class FormExtension
    {

        /// <summary>
        /// 打开窗体，如果已经打开则使其置顶
        /// </summary>
        /// <param name="form"></param>
        internal static void ShowOrFront(this Form form)
        {
            foreach (Form f in Application.OpenForms)
            {
                if (f.GetType() == form.GetType())
                {
                    f.WindowState = FormWindowState.Normal;
                    f.BringToFront();
                    return;
                }
            }
            form.Show();
        }

    }

}
