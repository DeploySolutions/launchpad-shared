namespace Deploy.LaunchPad.Python
{
    public partial interface IPythonScript
    {
        string FileName { get; set; }
        string FolderPath { get; set; }
    }
}