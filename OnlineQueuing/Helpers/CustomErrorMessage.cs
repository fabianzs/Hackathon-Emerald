namespace OnlineQueuing.Helpers
{
    public class CustomErrorMessage
    {
        public string Error { get; set; }

        public CustomErrorMessage()
        {
        }

        public CustomErrorMessage(string error)
        {
            Error = error;
        }
    }
}
