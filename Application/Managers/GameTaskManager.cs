using Domain.Entities;

namespace Application.Managers;

public class GameTaskManager
{
    private const string TasksResource = "Application.Resources.tasks.txt";
    private const string SuperTasksResource = "Application.Resources.supertasks.txt";
    private GameTask _currentTask = new GameTask();


    public static List<GameTask> LoadAllTasks(string resourceName)
    {
        var tasks = new List<GameTask>();
        // Берём именно сборку с этим классом, чтобы точно найти EmbeddedResource
        var asm = typeof(GameTaskManager).Assembly;

        using var stream = asm.GetManifestResourceStream(resourceName)
                        ?? throw new FileNotFoundException($"Ресурс «{resourceName}» не найден в сборке {asm.FullName}");
        using var reader = new StreamReader(stream);

        string? question = null;
        while (!reader.EndOfStream)
        {
            var line = reader.ReadLine()?.Trim();
            if (string.IsNullOrEmpty(line))
                continue;

            if (question == null)
            {
                // первая строка пары — вопрос
                question = line;
            }
            else
            {
                // вторая строка пары — ответ
                tasks.Add(new GameTask
                {
                    Question = question,
                    Answer = line
                });
                question = null;
            }
        }

        return tasks;
    }

    public GameTask GetRandomTask()
    {
        var all = LoadAllTasks(TasksResource);
        if (all.Count == 0)
            throw new Exception("Не удалось загрузить ни одной задачи из tasks.txt");
        var rnd = new Random();
        var task = all[rnd.Next(all.Count)];
        _currentTask.Question = task.Question;
        _currentTask.Answer = task.Answer;
        return _currentTask;
    }

    public GameTask GetRandomSuperTask()
    {
        var all = LoadAllTasks(SuperTasksResource);
        if (all.Count == 0)
            throw new Exception("Не удалось загрузить ни одной задачи из supertasks.txt");
        var rnd = new Random();
        var supertask = all[rnd.Next(all.Count)];
        _currentTask.Question = supertask.Question;
        _currentTask.Answer = supertask.Answer;
        return _currentTask;
    }
}
