using HtmlAgilityPack;
using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

public class CodeGenerator
{
    // Method to generate Model class
    public static void GenerateModel(string modelName, (string Name, string Type)[] properties)
    {
        StringBuilder modelCode = new StringBuilder();

        // Create class definition
        modelCode.AppendLine($"public class {modelName}");
        modelCode.AppendLine("{");

        // Add properties with dynamic data types
        foreach (var prop in properties)
        {
            modelCode.AppendLine($"    public {prop.Type} {prop.Name} {{ get; set; }}");
        }

        modelCode.AppendLine("}");

        // Create the directory if it doesn't exist
        string path = Path.Combine("Models", $"{modelName}.cs");
        Directory.CreateDirectory("Models");

        // Write code to file
        File.WriteAllText(path, modelCode.ToString());
        Console.WriteLine($"Model {modelName} generated at {path}");
    }

    // Method to generate DTO class
    public static void GenerateDTO(string modelName, (string Name, string Type)[] properties)
    {
        string dtoName = modelName + "DTO";
        StringBuilder dtoCode = new StringBuilder();

        // Create DTO class definition
        dtoCode.AppendLine($"public class {dtoName}");
        dtoCode.AppendLine("{");

        // Add properties with dynamic data types
        foreach (var prop in properties)
        {
            dtoCode.AppendLine($"    public {prop.Type} {prop.Name} {{ get; set; }}");
        }

        dtoCode.AppendLine("}");

        // Create the directory if it doesn't exist
        string path = Path.Combine("DTOs", $"{modelName}", $"{dtoName}.cs");
        Directory.CreateDirectory(Path.Combine("DTOs", $"{modelName}"));

        // Write code to file
        File.WriteAllText(path, dtoCode.ToString());
        Console.WriteLine($"DTO {dtoName} generated at {path}");
    }

    // Method to generate Repository Interface
    public static void GenerateRepositoryInterface(string modelName)
    {
        string projectPath = Directory.GetCurrentDirectory() + "\\Repositories\\Interfaces\\";
        var gg = File.ReadAllText("D:\\Project\\API Code Generator\\bin\\Debug\\net8.0\\Repositories\\Interfaces\\ICustomer4Repository.cs");
        var ff = gg.Replace("{modelName}", modelName);
        string interfaceName = "I" + modelName + "Repository";
        StringBuilder interfaceCode = new StringBuilder();
        interfaceCode.AppendLine(gg.Replace("{modelName}", modelName));
        // Create the directory if it doesn't exist
        string path = Path.Combine("Repositories", "Interfaces", $"{interfaceName}.cs");
        Directory.CreateDirectory(Path.Combine("Repositories", "Interfaces"));
        EnsureDirectoryExists(projectPath);
        // Write code to file
        File.WriteAllText(path, interfaceCode.ToString());
        Console.WriteLine($"Repository Interface {interfaceName} generated at {path}");
    }

    // Method to generate Repository class
    public static void GenerateRepository(string modelName)
    {
        string repoName = modelName + "Repository";
        StringBuilder repoCode = new StringBuilder();

        // Create repository class definition
        repoCode.AppendLine($"public class {repoName} : I{modelName}Repository");
        repoCode.AppendLine("{");
        repoCode.AppendLine($"    private readonly DbContext _context;");
        repoCode.AppendLine($"    public {repoName}(DbContext context)");
        repoCode.AppendLine("    {");
        repoCode.AppendLine("        _context = context;");
        repoCode.AppendLine("    }");

        // Implement CRUD operations
        repoCode.AppendLine("    public void Add(object entity) => _context.Add(entity);");
        repoCode.AppendLine("    public void Save() => _context.SaveChanges();");
        repoCode.AppendLine($"    public {modelName} GetById(int id) => _context.Set<{modelName}>().Find(id);");

        repoCode.AppendLine("}");

        // Create the directory if it doesn't exist
        string path = Path.Combine("Repositories", $"{repoName}.cs");
        Directory.CreateDirectory("Repositories");

        // Write code to file
        File.WriteAllText(path, repoCode.ToString());
        Console.WriteLine($"Repository {repoName} generated at {path}");
    }

    // Method to generate Service class
    public static void GenerateService(string modelName)
    {
        string serviceName = modelName + "Service";
        StringBuilder serviceCode = new StringBuilder();

        // Create service class definition
        serviceCode.AppendLine($"public class {serviceName}");
        serviceCode.AppendLine("{");
        serviceCode.AppendLine($"    private readonly {modelName}Repository _repository;");
        serviceCode.AppendLine($"    public {serviceName}({modelName}Repository repository)");
        serviceCode.AppendLine("    {");
        serviceCode.AppendLine("        _repository = repository;");
        serviceCode.AppendLine("    }");

        serviceCode.AppendLine("    // Service methods (example)");
        serviceCode.AppendLine("    public void Add() { /* Add logic */ }");
        serviceCode.AppendLine("}");

        // Create the directory if it doesn't exist
        string path = Path.Combine("Services", $"{serviceName}.cs");
        Directory.CreateDirectory("Services");

        // Write code to file
        File.WriteAllText(path, serviceCode.ToString());
        Console.WriteLine($"Service {serviceName} generated at {path}");
    }

    // Method to generate Controller class
    public static void GenerateController(string ModelName)
    {
        string projectPath = Directory.GetCurrentDirectory() + "\\Sample Files\\";
        string controllerName = ModelName + "Controller";
        StringBuilder controllerCode = new StringBuilder();
        var gg = File.ReadAllText("D:\\Project\\API Code Generator\\bin\\Debug\\net8.0\\Sample Files\\Controller.txt");
        var objName=char.ToLower(ModelName[0]) + ModelName.Substring(1);
        gg = gg.Replace("{ModalObjectName}", objName);
        
        // Create controller class definition
        //controllerCode.AppendLine($"[ApiController]");
        //controllerCode.AppendLine($"[Route(\"api/[controller]\")]");
        //controllerCode.AppendLine($"public class {controllerName} : ControllerBase");
        //controllerCode.AppendLine("{");
        //controllerCode.AppendLine($"    private readonly {ModelName}Service _service;");
        //controllerCode.AppendLine($"    public {controllerName}({ModelName}Service service)");
        //controllerCode.AppendLine("    {");
        //controllerCode.AppendLine("        _service = service;");
        //controllerCode.AppendLine("    }");

        //controllerCode.AppendLine("    [HttpPost]");
        //controllerCode.AppendLine("    public IActionResult Create() { /* Create logic */ return Ok(); }");

        //controllerCode.AppendLine("}");

        // Create the directory if it doesn't exist
        string path = Path.Combine("Controllers", $"{controllerName}.cs");
        gg = gg.Replace("{ModalName}", ModelName);
        Directory.CreateDirectory("Controllers");

        // Write code to file
        File.WriteAllText(path, gg.ToString());
        Console.WriteLine($"Controller {controllerName} generated at {path}");
    }

    // General method to generate all classes: Model, DTO, Repository, Repository Interface, Service, and Controller
    public static void GenerateAll(string modelName, (string Name, string Type)[] properties)
    {
        GenerateModel(modelName, properties);
        GenerateDTO(modelName, properties);
        GenerateRepositoryInterface(modelName);
        GenerateRepository(modelName);
        GenerateService(modelName);
        GenerateController(modelName);
        var lines = File.ReadAllText("D:\\Project\\API Code Generator\\bin\\Debug\\net8.0\\Models\\Customer.cs");
        var ff = lines.Replace("$ModalName", modelName);
        foreach (var item in lines)
        {
            //item.r
        }
    }
    public static string ReplaceKeyword(string input, object dto)
    {
        string pattern = @"\$\$-\s*(\w[\w\s\-]*\w)\s*-\$\$";
        return Regex.Replace(input, pattern, match =>
        {
            // Allow words, spaces, and dashes inside the Keyword
            string propertyName = match.Groups[1].Value.Replace(" ", string.Empty).Replace("-", string.Empty);
            PropertyInfo propertyInfo = dto.GetType().GetProperty(propertyName);
            if (propertyInfo != null)
            {
                var value = propertyInfo.GetValue(dto);
                return value switch
                {
                    null => " ",  // Return space if value is null
                    DateTime dateTime => dateTime.ToString("dd-MM-yyyy"),  // Format DateTime
                    decimal decimalValue => decimalValue.ToString("C2"),  // Format decimal as currency
                    _ => value?.ToString() ?? " "  // Convert other values to string, default to space for null values
                };
            }
            return match.Value;
        });
    }

    // Ensure that directories exist
    private static void EnsureDirectoryExists(string directoryPath)
    {
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }
    }

    public static void AddProgramfile(string modelName)
    {
        string programFilePath = @"D:\Project\TestMVC\Program.cs";
        //string programFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Program.cs");

        if (File.Exists(programFilePath))
        {
            // Read the entire content of Program.cs
            string programContent = File.ReadAllText(programFilePath);

            // Define the line to insert dynamically
            string serviceLineToAdd = $"builder.Services.AddScoped<I{modelName}, {modelName}>();";

            // Find where to insert the new service line
            string serviceRegistrationBlock = "builder.Services.AddControllers();"; // You can customize this to any other line within the block

            int insertPosition = programContent.IndexOf(serviceRegistrationBlock);

            if (insertPosition >= 0)
            {
                // Insert the new service line right before the identified service registration block
                string updatedContent = programContent.Insert(insertPosition - 2, serviceLineToAdd + "\n");

                // Write the updated content back to Program.cs
                File.WriteAllText(programFilePath, updatedContent);
            }
        }
    }

    public static void AddDbSetToContext(string filePath, string newDbSet)
    {
        try
        {
            // Read the existing code from the file
            var lines = File.ReadAllLines(filePath);

            // Define the region and line to insert
            string regionStart = "#region Survey Setup";
            string regionEnd = "#endregion";

            bool insideRegion = false;
            bool inserted = false;

            // Iterate over the lines to find the region and insert the new line
            for (int i = 0; i < lines.Length; i++)
            {
                // Check if we are inside the "Survey Setup" region
                if (lines[i].Contains(regionStart))
                {
                    insideRegion = true;
                }

                // If we are inside the region and find the end, insert the new DbSet line
                if (insideRegion && lines[i].Contains(regionEnd) && !inserted)
                {
                    var newLines = new string[lines.Length + 1];
                    Array.Copy(lines, newLines, i);  // Copy all lines up to the region's end
                    newLines[i] = $"    {newDbSet}";  // Insert the new line
                    Array.Copy(lines, i, newLines, i + 1, lines.Length - i);  // Copy remaining lines
                    lines = newLines;
                    inserted = true;
                    break;
                }
            }

            // If the region is found and modified, save the changes
            if (inserted)
            {
                File.WriteAllLines(filePath, lines);
                Console.WriteLine("Line successfully added.");
            }
            else
            {
                Console.WriteLine("Could not find the region to insert the line.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }

    public static void BuildProjectWithDotnetCli(string projectDirectory)
    {
        try
        {
            // Ensure the directory exists
            if (!Directory.Exists(projectDirectory))
            {
                Console.WriteLine("Project directory does not exist.");
                return;
            }

            // Prepare the process to run dotnet build
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = "dotnet",
                Arguments = "build", // You can specify more arguments like --configuration Release
                WorkingDirectory = projectDirectory,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false
            };

            // Start the process
            using (Process process = Process.Start(startInfo))
            {
                if (process != null)
                {
                    // Read and display the output from the build process
                    string output = process.StandardOutput.ReadToEnd();
                    string error = process.StandardError.ReadToEnd();

                    if (!string.IsNullOrEmpty(output))
                    {
                        Console.WriteLine("Build Output:");
                        Console.WriteLine(output);
                    }

                    if (!string.IsNullOrEmpty(error))
                    {
                        Console.WriteLine("Build Errors:");
                        Console.WriteLine(error);
                    }

                    // Wait for the process to exit
                    process.WaitForExit();

                    if (process.ExitCode == 0)
                    {
                        Console.WriteLine("Build successful!");
                        // Optionally, display the path to the generated DLL
                        string outputPath = Path.Combine(projectDirectory, "bin", "Debug", "net8.0"); // Modify as per your target framework
                        Console.WriteLine($"Generated DLL located at: {outputPath}");
                    }
                    else
                    {
                        Console.WriteLine("Build failed.");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    public static string PrepareFormHtml(string filepath,string htmlContent)
    {
        var formContent = "";
        var samplefilecontent=File.ReadAllText(filepath);
        // Load the HTML content
        HtmlDocument doc = new HtmlDocument();
        doc.LoadHtml(samplefilecontent);

        // Find the div by id and insert the long string content
        var div = doc.GetElementbyId("myDiv");
        if (div != null)
        {
            div.InnerHtml = htmlContent;
        }

        // Output the modified HTML
        Console.WriteLine(doc.DocumentNode.OuterHtml);
        return formContent;
    }

}

class Program
{
    static void Main(string[] args)
    {
        // Example model name and properties
        string modelName = "Customer4";
        string filePath = @"E:\Project\SN.API\SN.Domain\DBContext\AppDbContext.cs";

        // Define the new line to add to the region
        string newDbSet = "public DbSet<SurveyQuestion> SurveyQuestions { get; set; }";
        CodeGenerator.AddDbSetToContext(filePath,newDbSet);
        //CodeGenerator.AddProgramfile(modelName);
        var properties = new (string Name, string Type)[]
        {
            ("Id", "int"),
            ("Name", "string"),
            ("Price", "decimal"),
            ("Description", "string"),("Created On", "DateTime"),("Created By", "long"),("Modified on", "DateTime")
        };

        // Call the GenerateAll method to create Model, DTO, Repository, Service, and Controller
        // CodeGenerator.GenerateAll(modelName, properties);
        CodeGenerator.GenerateController(modelName);
        // Create an instance of the DTO with sample data
        var myDto = new MyDto
        {
            StartDate = DateTime.Now,
            CreatedBy = DateTime.Now,
            UserName = "JohnDoe",
            UserName1 = "JohnDoe1",
            UserName2 = "JohnDoe2",
            Amount = 123.45m,
            Modifiedon = null
        };

        // Sample string with placeholders
        string inputString = "User: $$-User-Name-$$, Date: $$-Start Date-$$,$$-Created By-$$,mfngb,fdmbfn  $$-Modified on-$$ sfgsdgfsgsg,$$-User-Name2-$$, Amount: $$-Amount-$$";

        // Replace placeholders dynamically
        string outputString = CodeGenerator.ReplaceKeyword(inputString, myDto);

        // Output the result
        Console.WriteLine(outputString);
    }
}

public class MyDto
{
    public DateTime StartDate { get; set; }
    public DateTime CreatedBy { get; set; }
    public string? Modifiedon { get; set; }
    public string? UserName { get; set; }
    public string? UserName1 { get; set; }
    public string? UserName2 { get; set; }
    public decimal Amount { get; set; }
}