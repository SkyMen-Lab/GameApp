namespace GameService.TCP.EventArgs
{
    public class MovementReceivedEventArgs: System.EventArgs
    {
        private string _code;
        private int _clicks;

        public MovementReceivedEventArgs(string code, int clicks)
        {
            _clicks = clicks;
            _code = code;
        }

        public string Code => _code;

        public int Clicks => _clicks;
    }
}