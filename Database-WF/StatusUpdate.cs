namespace Database_WF
{
    internal class StatusUpdate
    {
        public static void LabelUpdate(bool result, Label label)
        {
            label.Text = $"{DateTime.Now.ToString()}: {result.ToString()}";
        }
    }
}
