using System.Text.RegularExpressions;
using System.Diagnostics;

public static class Helpers{
    public static string GetVideoId(string url)
    {
        var regex = new Regex(@"(?:https?:\/\/)?(?:www\.)?(?:youtube\.com\/(?:[^\/\n\s]+\/\S+\/|(?:v|e(?:mbed)?)\/|.*[?&]v=)|youtu\.be\/)([a-zA-Z0-9_-]{11})");
        var match = regex.Match(url);
        
        return match.Success ? match.Groups[1].Value : null;
    }
    public static string ExecuteCommand(string fileName, string arguments)
    {
        string argumentsEscaped = arguments.Replace("\"", "\\\"");
        string fullArgs = $"-c \"echo sudo \\\"{fileName}\\\" {argumentsEscaped} | at now\"";

        var processInfo = new ProcessStartInfo
        {
            FileName = "/bin/bash",
            Arguments = fullArgs,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            CreateNoWindow = true,
            UseShellExecute = false,
            WindowStyle = ProcessWindowStyle.Hidden
        };

        using var process = Process.Start(processInfo);

        // Capture the output and error
        string output = process.StandardOutput.ReadToEnd();
        string error = process.StandardError.ReadToEnd();

        process.WaitForExit();

        // Return output and error as a single string
        if (process.ExitCode != 0)
        {
            throw new Exception($"Command failed with error: {error}");
        }

        return output;
    }
   
}