namespace Deploy.LaunchPad.Util.CommandLine
{
    public partial interface ICommandResult
    {
        public object Data { get; set; }
        public string Message { get; set; }
        public bool Success { get; set; }
    }
}