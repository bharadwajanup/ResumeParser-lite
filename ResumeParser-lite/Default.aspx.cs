using Sharpenter.ResumeParser.OutputFormatter.Json;
using Sharpenter.ResumeParser.ResumeProcessor;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Title = "Resume Parser";
    }


    protected void Resume_Click(object sender, EventArgs e)
    {
        String resumeText = resume.Text;
        String resumeFolder = Path.Combine(Directory.GetCurrentDirectory(), "Resumes");
        var outputFolder = Path.Combine(Directory.GetCurrentDirectory(), "Output");
        if (Directory.Exists(outputFolder))
        {
            Directory.Delete(outputFolder, true);
        }

        Directory.CreateDirectory(outputFolder);

        if (!Directory.Exists(resumeFolder))
            Directory.CreateDirectory(resumeFolder);

        String fileName = Path.Combine(resumeFolder, "resumeText.txt");

        File.WriteAllText(fileName, resumeText);

        var processor = new ResumeProcessor(new JsonOutputFormatter());

        String output = processor.Process(fileName);




        message.Text = output;



    }
}