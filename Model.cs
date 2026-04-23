using Newtonsoft.Json;
using SimBioseTasks;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

/// <summary>
/// Representa o Model da aplicação no padrão MVC.
/// É responsável por gerir os dados, aplicar regras de negócio,
/// persistir tarefas em ficheiro JSON e notificar alterações ao Controller
/// através de delegates e eventos.
/// </summary>
public class Model
{
    private const string FileName = "tasks.json";
    private List<BaseTask> _tasks;

    /// <summary>
    /// Obtém a coleção atual de tarefas em modo de leitura.
    /// </summary>
    public IReadOnlyList<BaseTask> Tasks => _tasks;

    /// <summary>
    /// Evento emitido pelo Model quando ocorre uma alteração nas tarefas.
    /// Permite ao Controller ser notificado sem dependência direta da View.
    /// </summary>
    public event Action<OperTask>? OnModelEvent;

    /// <summary>
    /// Inicializa uma nova instância da classe <see cref="Model"/>.
    /// Cria a coleção interna e carrega as tarefas persistidas em ficheiro.
    /// </summary>
    public Model()
    {
        _tasks = new List<BaseTask>();
        LoadTasksFromJson();
    }

    /// <summary>
    /// Trata os pedidos emitidos pelo Controller.
    /// Recebe uma operação sobre uma tarefa e encaminha-a para o método interno adequado.
    /// </summary>
    /// <param name="operTask">
    /// Objeto que contém a operação pedida e a tarefa associada.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Lançada quando a operação ou a tarefa são nulas.
    /// </exception>
    public void EventOnController(OperTask operTask)
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
    /// Carrega as tarefas a partir do ficheiro JSON para memória.
    /// </summary>
    /// <returns>
    /// Devolve true se o carregamento for realizado com sucesso;
    /// devolve false se o ficheiro não existir ou estiver vazio.
    /// </returns>
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
    /// Cria uma nova tarefa, atribui-lhe um identificador,
    /// persiste os dados e notifica o Controller.
    /// </summary>
    /// <param name="task">Tarefa a criar.</param>
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
    /// Atualiza uma tarefa existente, persiste a alteração
    /// e notifica o Controller.
    /// </summary>
    /// <param name="task">Tarefa com os novos dados.</param>
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
    /// Remove uma tarefa existente, persiste a alteração
    /// e notifica o Controller.
    /// </summary>
    /// <param name="task">Tarefa a remover.</param>
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
    /// Calcula o próximo identificador disponível para uma nova tarefa.
    /// </summary>
    /// <returns>
    /// O próximo identificador inteiro disponível.
    /// </returns>
    private int GetNextId()
    {
        if (_tasks.Count == 0)
            return 1;

        return _tasks.Max(t => t.Id ?? 0) + 1;
    }

    /// <summary>
    /// Valida os dados de uma tarefa antes da sua persistência.
    /// </summary>
    /// <param name="task">Tarefa a validar.</param>
    /// <returns>
    /// Devolve true se a tarefa for válida; caso contrário, devolve false.
    /// </returns>
    private bool ValidateTask(BaseTask task)
    {
        if (task == null) return false;
        if (string.IsNullOrWhiteSpace(task.Title)) return false;

        return true;
    }
}