﻿using System.Reflection;
using PoleChudes.Domain.Entities;

namespace PoleChudes;

public class GameTaskManager
{
    const string ResourceName = "PoleChudes.Resources.tasks.txt";

    public static List<GameTask> LoadAllTasks()
    {
        var tasks = new List<GameTask>();
        var asm = Assembly.GetExecutingAssembly();

        using var stream = asm.GetManifestResourceStream(ResourceName)
                         ?? throw new FileNotFoundException($"Ресурс «{ResourceName}» не найден");
        using var reader = new StreamReader(stream);

        string? question = null;
        while (!reader.EndOfStream)
        {
            var line = reader.ReadLine()?.Trim();
            if (string.IsNullOrEmpty(line))
                continue;

            if (question == null)
            {
                // эта строка — вопрос
                question = line;
            }
            else
            {
                // а это — ответ
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
    public static GameTask GetRandomTask()
    {
        var all = LoadAllTasks();
        if (all.Count == 0) throw new Exception("Task for game not found!");
        var rnd = new Random();
        return all[rnd.Next(all.Count)];
    }
}

