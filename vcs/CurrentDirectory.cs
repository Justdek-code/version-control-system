using System.IO;


namespace vcs
{

    public class CurrentDirectory
    {
        private string _path;

        public CurrentDirectory()
        {
            _path = Directory.GetCurrentDirectory();
        }

        public string GetContent()
        {
            return _path;
        }
    }
}