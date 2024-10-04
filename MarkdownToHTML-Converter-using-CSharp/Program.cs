using System;
using System.IO;
using Markdig;
using System.Configuration;

namespace MarkdownToHTML_Converter_using_CSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            // Get source and destination paths from app.config
            string sourcePath = ConfigurationManager.AppSettings["SourcePath"];
            string destinationPath = ConfigurationManager.AppSettings["DestinationPath"];

            // Ensure the destination folder exists
            if (!Directory.Exists(destinationPath))
            {
                Directory.CreateDirectory(destinationPath);
            }

            // Get all Markdown files in the source folder
            string[] markdownFiles = Directory.GetFiles(sourcePath, "*.md");

            foreach (var markdownFile in markdownFiles)
            {
                try
                {
                    // Define the destination HTML file name
                    string htmlFileName = Path.GetFileNameWithoutExtension(markdownFile) + ".html";
                    string htmlFilePath = Path.Combine(destinationPath, htmlFileName);

                    // Read the Markdown file content
                    string markdownContent = File.ReadAllText(markdownFile);

                    // Convert Markdown to HTML using Markdig
                    string htmlContent = Markdown.ToHtml(markdownContent);

                    // Write the HTML content to the destination file
                    File.WriteAllText(htmlFilePath, htmlContent);

                    Console.WriteLine($"Successfully converted: {markdownFile} to {htmlFilePath}");

                    // Delete the original Markdown file after conversion
                    File.Delete(markdownFile);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error converting file {markdownFile}: {ex.Message}");
                }
            }

            Console.WriteLine("Conversion process completed.");
        }
    }
}