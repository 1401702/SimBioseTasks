using Newtonsoft.Json;
using SimBioseTasks;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

/// <summary>
/// Model do MVC
/// </summary>
public class Model
{
    private const string FileName = "tasks.json";
    private List<BaseTask> _tasks;

    /// <summary>
    /// Obtém a lista de tarefas em modo de leitura.
    /// </summary>
    public IReadOnlyList<BaseTask> Tasks => _tasks;

    /// <summary>
    /// Evento disparado quando o model altera tarefas.
    /// </summary>
    public event Action<OperTask>? OnModelEvent;

    /// <summary>
    /// Inicializa uma nova instância do modelo e carrega as tarefas do ficheiro JSON.
    /// </summary>
    public Model()
    {
        _tasks = new List<BaseTask>();
        LoadTasksFromJson();
    }

    /// <summary>
    /// Carrega as tarefas a partir do ficheiro JSON para memória.
    /// </summary>
    private bool LoadTasksFromJson()
    {
        if (!File.Exists(FileName))
        {
            _tasks = new List<BaseTask>();
            return false;
        }

        string json = File.ReadAllText(FileName);

        if (string.IsNullOrWhiteSpace(json))
        {
            _tasks = new List<BaseTask>();
            return false;
        }

        _tasks = JsonConvert.DeserializeObject<List<BaseTask>>(json) ?? new List<BaseTask>();
        return true;
    }

    /// <summary>
    /// Guarda a lista atual de tarefas no ficheiro JSON.
    /// </summary>
    private void SaveTasks()
    {
        string json = JsonConvert.SerializeObject(_tasks, Formatting.Indented);
        File.WriteAllText(FileName, json);
    }

    /// <summary>
    /// Executa uma operação sobre uma tarefa com base no comando recebido.
    /// </summary>
    public void Execute(OperTask operTask)
    {
        if (operTask == null)
            throw new ArgumentNullException(nameof(operTask));

        if (operTask.Task == null)
            throw new ArgumentNullException(nameof(operTask.Task));

        switch (operTask.Operation)
        {
            case TaskOp.Create:
                Create(operTask.Task);
                break;

            case TaskOp.Update:
                Update(operTask.Task);
                break;

            case TaskOp.Delete:
                Delete(operTask.Task);
                break;

            case TaskOp.Save:
                break;

            case TaskOp.Confirm:
                break;
        }
    }

    /// <summary>
    /// Cria uma nova tarefa.
    /// </summary>
    private void Create(BaseTask task)
    {
        if (ValidateTask(task))
        {
            task.Id = GetNextId();
            _tasks.Add(task);
            SaveTasks();
            OnModelEvent?.Invoke(new OperTask { Operation = TaskOp.Create, Task = task });
        }
    }

    /// <summary>
    /// Atualiza uma tarefa existente.
    /// </summary>
    private void Update(BaseTask task)
    {
        if (ValidateTask(task))
        {
            var existing = _tasks.FirstOrDefault(t => t.Id == task.Id);
            if (existing == null) return;

            existing.Title = task.Title;
            existing.Description = task.Description;
            existing.IsCompleted = task.IsCompleted;

            SaveTasks();
            OnModelEvent?.Invoke(new OperTask { Operation = TaskOp.Update, Task = existing });
        }
    }

    /// <summary>
    /// Remove uma tarefa existente.
    /// </summary>
    private void Delete(BaseTask task)
    {
        if (task.Id == null) return;

        var existing = _tasks.FirstOrDefault(t => t.Id == task.Id);
        if (existing == null) return;

        _tasks.Remove(existing);
        SaveTasks();
        OnModelEvent?.Invoke(new OperTask { Operation = TaskOp.Delete, Task = existing });
    }

    /// <summary>
    /// Calcula o próximo identificador disponível.
    /// </summary>
    private int GetNextId()
    {
        if (_tasks.Count == 0)
            return 1;

        return _tasks.Max(t => t.Id ?? 0) + 1;
    }

    /// <summary>
    /// Valida os dados de uma tarefa.
    /// </summary>
    private bool ValidateTask(BaseTask task)
    {
        if (task == null) return false;
        if (string.IsNullOrWhiteSpace(task.Title)) return false;

        return true;
    }
}