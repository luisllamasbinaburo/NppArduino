using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using Kbg.NppPluginNET;


namespace NppArduino.Domain
{
    public static class ArduinoCLI_API
    {

        public static string[] GetPorts()
        {
            return SerialPort.GetPortNames();
        }

        public static string CompileSketch(string boardFqbn, string skechtFolder)
        {
            var cliCommand = $"compile --fqbn {boardFqbn} {skechtFolder}";
            return RunCliCommand(cliCommand);
        }

        public static string UploadSketch(string port, string boardFqbn, string skechtFolder)
        {
            string cliCommand = $"compile -u -p {port} --fqbn {boardFqbn} {skechtFolder}";
            return RunCliCommand(cliCommand);
        }

        public static Board[] GetInstalledBoards()
        {

            string cliCommand = "board listall";
            string json = RunCliCommand(cliCommand);
            var boards = ParseBoards(json);

            return boards;
        }

        public static string GetConnectedBoards()
        {
            string cliCommand = "board list";
            string json = RunCliCommand(cliCommand);

            return json;
        }

        public static string SetBoardConfig(string boardFqbn)
        {
            string cliCommand = $"board details {boardFqbn}";
            return RunCliCommand(cliCommand);
        }


        internal static string RunCliCommand(string arguments)
        {
            var process = new System.Diagnostics.Process {
                StartInfo = {
                    FileName = "arduino-cli",
                    Arguments = arguments + " --format json",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                }
            };

            process.Start();
            string strOutput = process.StandardOutput.ReadToEnd();
            process.WaitForExit();

            return strOutput;
        }

        private static Board[] ParseBoards(string json)
        {
            var rst = new List<Board>();

            var newBoard = new Board();
            string[] lines = json.Replace("\"", "").Split('\n');
            foreach (string line in lines)
            {
                var cleanLine = line.Trim();
                if (cleanLine.StartsWith("name: "))
                {
                    newBoard.Name = cleanLine.Replace("name: ", "").Replace(",", ""); ;
                }
                else if (cleanLine.StartsWith("FQBN: "))
                {
                    newBoard.Fqbn = cleanLine.Replace("FQBN: ", "");
                    rst.Add(newBoard);
                    newBoard = new Board();
                }
            }

            return rst.ToArray();
        }
    }
}